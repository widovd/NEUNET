using System;
using System.Threading.Tasks;
using System.Numerics;
using Neulib.Exceptions;
using Neulib.Extensions;
using static System.Math;
using static System.Convert;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.BitConverter;
using Neulib.Serializers;

namespace Neulib.MultiArrays
{
    public delegate double FilterDelegate(double[] Frequencies);

    public class ComplexArray : BaseArray
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// A one dimensional array of nData complex numbers used as public storage to be addressed by GetIndex.
        /// Example (Rank = 2):
        /// A00, A01, A02,... A0m-1, A10, A11, A12, ... A1m-1, A20, ... An-10, An-11, An-1m-1
        /// </summary>
        private readonly Complex[] _values;

        /// <summary>
        /// Element of the complex data array.
        /// </summary>
        /// <param name="indices">
        /// A list of integer values which address the complex data array.
        /// The number of values must be equal to the number of dimensions.
        /// The last element corresponds to the lowest dimension and the inner loop.
        /// </param>
        /// <returns>The addressed element of the complex data array.</returns>
        public Complex this[params int[] indices]
        {
            get { return _values[GetLongIndex(indices)]; }
            set { _values[GetLongIndex(indices)] = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ComplexArray(params int[] lengths) : base(lengths)
        {
            _values = new Complex[LongLength];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseIdx

        public override void ReadValuesFromStream(Stream stream)
        {
            ForEach(i => _values[i] = stream.ReadComplex());
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region FFT

        /// <summary>
        /// Returns the frequency of a FFT-transformed data element: -0.5 .. 0.5
        /// </summary>
        /// <param name="iDim">The dimension: 0 .. Rank - 1</param>
        /// <param name="i">The index of dimension iDim.</param>
        /// <returns>The frequency</returns>
        public double GetFrequency(int iDim, int i)
        {
            int n = GetLength(iDim); // length of dimension iDim
            double f = ToDouble(i) / ToDouble(n); // the frequency
            if (i > n / 2) f -= 1d; // the top half represents negative frequencies
            return f;
        }

        /// <summary>
        /// Applies a filter to the data. (For example a low pass filter).
        /// The data is first FFT-transformed, then the filter is applied in the frequency domain, and finally the data is transformed back.
        /// For each dimension, the frequency ranges from -0.5 to +0.5. There are Rank frequencies.
        /// The array of frequencies is passed as argument to the filter delegate.
        /// </summary>
        /// <param name="filter">A filter delegate which must accept an array of frequencies and must return an attenuation factor.</param>
        public void ApplyFilter(bool parallel, FilterDelegate filter)
        {
            FFT(false, parallel);
            long n = LongLength;
            int nDim = Rank;
            double[] Frequencies = new double[nDim];
            int[] Indices = new int[nDim];
            for (int iDim = 0; iDim < nDim; iDim++)
            {
                Indices[iDim] = 0;
                Frequencies[iDim] = 0d;
            }
            for (long i = 0; i < n; i++)
            {
                //Console.Write($"{i}: (");
                //for (int iDim = 0; iDim < nDim; iDim++)
                //{
                //    Console.Write($"{Frequencies[iDim]}, ");
                //}
                //Console.WriteLine(")");
                double r = filter(Frequencies);
                _values[i] *= r;
                for (int iDim = 0; iDim < nDim; iDim++)
                {
                    int j = Indices[nDim - 1 - iDim];
                    int l = GetLength(nDim - 1 - iDim);
                    if (j < l - 1)
                    {
                        j++;
                        Indices[nDim - 1 - iDim] = j;
                        double f = ToDouble(j) / ToDouble(l);
                        if (f > 0.5d) f -= 1d;
                        Frequencies[nDim - 1 - iDim] = f;
                        break;
                    }
                    Indices[nDim - 1 - iDim] = 0;
                    Frequencies[nDim - 1 - iDim] = 0d;
                }
            }
            FFT(true, parallel);
        }


        public void Convolution(Complex[] refData, bool parallel)
        {
            FFT(false, parallel);
            MultiplyData(refData);
            FFT(true, parallel);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Data

        /// <summary>
        /// Calculates the list of index values from the long index. The opposite operation of GetLongIndex.
        /// </summary>
        /// <param name="longIndex">The long index.</param>
        /// <returns>The list of index values.</returns>
        public int[] GetIndices(long longIndex)
        {
            int nDim = Rank;
            int[] Result = new int[nDim];
            long n = LongLength;
            for (int iDim = nDim - 1; iDim >= 0; iDim--)
            {
                n /= GetLength(nDim - 1 - iDim);
                long d = longIndex / n;
                longIndex -= n * d;
                Result[nDim - 1 - iDim] = ToInt32(d);
            }
            return Result;
        }

        /// <summary>
        /// Creates a new complex data array and copies Data into it.
        /// </summary>
        /// <returns>The created copy of Data.</returns>
        public Complex[] GetData()
        {
            long nData = _values.LongLength;
            Complex[] NewData = new Complex[nData];
            for (long iData = 0; iData < nData; iData++)
            {
                NewData[iData] = _values[iData];
            }
            return NewData;
        }

        /// <summary>
        /// The complex data array is copied to Data.
        /// </summary>
        /// <param name="newData">The complex data array to be copied.</param>
        public void SetData(Complex[] newData)
        {
            long nData = _values.LongLength;
            if (newData == null) throw new VarNullException(nameof(newData), 747337);
            if (nData != newData.LongLength) throw new UnequalValueException(nData, newData.LongLength, 327547);
            for (long iData = 0; iData < nData; iData++)
            {
                _values[iData] = newData[iData];
            }
        }

        /// <summary>
        /// A quick and dirty way to compare complex data arrays of the same length.
        /// </summary>
        /// <param name="refData">The complex data array to be compared with Data.</param>
        /// <returns>True if the magnitude of difference of each element-pair is smaller than Tiny = 1e-10.</returns>
        public bool CompareData(Complex[] refData)
        {
            const double Tiny = 1e-10;
            long nData = _values.LongLength;
            if (nData != refData.LongLength) return false;
            for (long iData = 0; iData < nData; iData++)
            {
                if ((_values[iData] - refData[iData]).Magnitude > Tiny) return false;
            }
            return true;
        }

        /// <summary>
        /// Element-wise multiplies Data with the supplied complex data array.
        /// </summary>
        /// <param name="refData">The complex data array to be element-wise multiplied with Data.</param>
        public void MultiplyData(Complex[] refData)
        {
            long nData = _values.LongLength;
            if (nData != refData.LongLength) // Length of complex data arrays must be equal
                throw new UnequalValueException(nData, refData.LongLength, 311666);
            for (long iData = 0; iData < nData; iData++)
            {
                _values[iData] *= refData[iData];
            }
        }
        #endregion
        // ----------------------------------------------------------------------------------------
        #region FFT

        /// <summary>
        /// DanielsonLanczos implementation of multi-dimensional FFT for dimension ni.
        /// </summary>
        /// <param name="data">Input/output: Array of complex values. The algorithm works in-place.</param>
        /// <param name="inverse">If true, the inverse transform will be calculated.</param>
        /// <param name="np">1*n0*n1*...*(ni-1)</param>
        /// <param name="nn">np*ni</param>
        /// <param name="nData">Data.LongLength</param>
        private unsafe static void DanielsonLanczos_nD(Complex[] data, bool inverse, long np, long nn, long nData)
        {
            //BitReversal
            long i2rev = 0;
            for (long i2 = 0; i2 < nn + 1; i2 += np)
            {
                if (i2 < i2rev)
                    for (long i1 = i2; i1 <= i2 + np - 1; i1 += 1)
                        for (long i3 = i1; i3 < nData; i3 += nn)
                            fixed (Complex* p1 = &data[i3], p2 = &data[i2rev + i3 - i2])
                            {
                                Complex t = *p1; *p1 = *p2; *p2 = t;
                            }
                long ibit = nn >> 1;
                while ((ibit >= np) && (i2rev >= ibit))
                {
                    i2rev -= ibit;
                    ibit >>= 1;
                }
                i2rev += ibit;
            }
            //DanielsonLanczos
            long dij = np;
            while (dij < nn)
            {
                long istep = dij << 1;
                double theta = PI / (dij / np);
                if (inverse) theta = -theta;
                Complex wp = new Complex(-2d * (Sin(0.5d * theta)).Sqr(), Sin(theta));
                Complex w = new Complex(1d, 0d);
                for (long k = 0; k < dij; k += np)
                {
                    for (long m = k; m <= k + np - 1; m += 1)
                        for (long i = m; i <= nData - 1; i += istep)
                            fixed (Complex* p1 = &data[i], p2 = &data[i + dij])
                            {
                                Complex t1 = w * *p2; *p2 = *p1 - t1; *p1 += t1;
                            }
                    Complex t2 = w;
                    w *= wp;
                    w += t2;
                }
                dij = istep;
            }
        }

        /// <summary>
        /// DanielsonLanczos implementation of FFT for 1 dimension.
        /// </summary>
        /// <param name="data">The complex data array to be transformed in-place.</param>
        /// <param name="inverse">True: Data is replaced by the inverse Fourier transform.</param>
        /// <param name="a">Multiplication constant for the Data-index.</param>
        /// <param name="b">Addition constant for the Data-index (the base value).</param>
        /// <param name="n">The number of index values in the dimension being calculated.</param>
        private unsafe static void DanielsonLanczos_1D(Complex[] data, bool inverse, long a, long b, int n)
        {
            // BitReversal
            int i2 = 0;
            for (int i1 = 0; i1 <= n; i1++)
            {
                if (i1 < i2)
                    fixed (Complex* p1 = &data[a * i1 + b], p2 = &data[a * i2 + b])
                    {
                        Complex t = *p1; *p1 = *p2; *p2 = t;
                    }
                int ibit = n >> 1;
                while ((ibit >= 1) && (i2 >= ibit))
                {
                    i2 -= ibit;
                    ibit >>= 1;
                }
                i2 += ibit;
            }
            // DanielsonLanczos
            int dij = 1;
            while (dij < n)
            {
                int istep = dij << 1;
                double theta = PI / dij;
                if (inverse) theta = -theta;
                Complex wp = new Complex(-2d * (Sin(0.5d * theta)).Sqr(), Sin(theta));
                Complex w = new Complex(1d, 0d);
                for (int m = 0; m < dij; m++)
                {
                    for (int i1 = m; i1 <= n - 1; i1 += istep)
                        fixed (Complex* p1 = &data[a * i1 + b], p2 = &data[a * (i1 + dij) + b])
                        {
                            Complex t1 = w * *p2; *p2 = *p1 - t1; *p1 += t1;
                        }
                    Complex t2 = w;
                    w *= wp;
                    w += t2;
                }
                dij = istep;
            }
            if (inverse)
            {
                for (int i1 = 0; i1 < n; i1++) data[a * i1 + b] /= n;
            }
        }


        /// <summary>
        /// DanielsonLanczos implementation of multi-dimensional FFT with sequential processing.
        /// Replaces array by the (inverse) discrete Fourier transform.
        /// </summary>
        /// <param name="inverse">True: array is replaced by the inverse Fourier transform.</param>
        public void FFTSequential(bool inverse)
        {
            long a = 1;
            long nData = LongLength;
            int nDim = Rank;
            for (int iDim = 0; iDim < Rank; iDim++)
            {
                int n = GetLength(nDim - 1 - iDim);
                long di2 = a * n;
                DanielsonLanczos_nD(_values, inverse, a, di2, nData);
                a = di2;
            }
            if (inverse)
            {
                for (long i = 0; i < nData; i++)
                {
                    _values[i] /= nData;
                }
            }
        }

        /// <summary>
        /// DanielsonLanczos implementation of multi-dimensional FFT with parallel processing.
        /// Replaces array by the (inverse) discrete Fourier transform.
        /// </summary>
        /// <param name="inverse">True: array is replaced by the inverse Fourier transform.</param>
        public void FFTParallel(bool inverse)
        {
            long a = 1; // The index multiplication factor for the dimension being 1D transformed.
            long nData = _values.Length;
            int nDim = Rank;
            for (int iDim = 0; iDim < nDim; iDim++)
            {
                int n = GetLength(nDim - 1 - iDim);
                long di2 = a * n;
                long m = a * nData / di2;
                long[] baseIndex = new long[m];
                // loop through all possible index values not part of the dimension to be 1D FFT calculated
                for (long i1 = 0, j = 0; i1 < a; i1++)
                    for (long i2 = 0; i2 < nData; i2 += di2)
                    {
                        baseIndex[j++] = i1 + i2; // build a list of index base values which will be split into about equal pieces
                    }
                Parallel.For(0, m, (j) =>
                {
                    DanielsonLanczos_1D(_values, inverse, a, baseIndex[j], n);
                });
                a = di2;
            }
        }

        public void FFT(bool inverse, bool parallel)
        {
            if (parallel)
                FFTParallel(inverse);
            else
                FFTSequential(inverse);
        }

        #endregion

    }
}

