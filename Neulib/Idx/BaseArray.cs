using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using static System.Convert;
using static System.BitConverter;
using Neulib.Serializers;

namespace Neulib.MultiArrays
{
    public abstract class BaseArray
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The total number of elements in the one dimensional Data array.
        /// Equal to the product of the integer values of the Lengths array.
        /// </summary>
        public long LongLength { get; private set; }

        /// <summary>
        /// The lengths of the multidimensional array.
        /// The last integer varies fastest internally, i.e. it corresponds to the inner loop.
        /// </summary>
        private readonly int[] _lengths;

        /// <summary>
        /// The number of dimensions.
        /// </summary>
        public int Rank { get => (_lengths != null) ? _lengths.Length : 0; }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new multi-dimensional array.
        /// </summary>
        /// <param name="lengths">
        /// The lengths of the multidimensional array.
        /// The last integer in the list varies fastest internally, i.e. it corresponds to the inner loop.
        /// </param>
        public BaseArray(params int[] lengths)
        {
            int nDim = lengths.Length;
            if (nDim == 0) throw new InvalidValueException(nameof(nDim), nDim, 975669);
            _lengths = new int[nDim];
            long longLength = 1;
            for (int iDim = 0; iDim < nDim; iDim++)
            {
                int length = lengths[iDim];
                _lengths[iDim] = length;
                longLength *= length;
            }
            LongLength = longLength;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseIdx

        public int GetLength(int iDim)
        {
            return _lengths[iDim];
        }

        /// <summary>
        /// Calculates the long index from a list of index values. Typically used for addressing the onedimensional data array.
        /// </summary>
        /// <param name="indices">A list of index values. The number of values must be equal to Rank.</param>
        /// <returns>The long index.</returns>
        protected long GetLongIndex(params int[] indices)
        {
            int rank = Rank;
            int length = indices.Length;
            if (length != rank) // Wrong number of index parameters
                throw new UnequalValueException(length, rank, 801250);
            long index = 0, n = 1;
            for (int i = rank - 1; i >= 0; i--)
            {
                index += indices[i] * n;
                n *= GetLength(i);
            }
            return index;
        }

        public void ForEach(Action<long> action)
        {
            long length = LongLength;
            for (long i = 0; i < length; i++) action(i);
        }

        public abstract void ReadValuesFromStream(Stream stream);

        #endregion
        // ----------------------------------------------------------------------------------------
        #region static

        public static BaseArray ReadFromStream(Stream stream)
        {
            if (stream.ReadByte() != 0) throw new FileFormatException(568800);
            if (stream.ReadByte() != 0) throw new FileFormatException(667198);
            int format = stream.ReadByte();
            int dimensions = stream.ReadByte();
            int[] lengths = new int[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                lengths[i] = stream.ReadInt(true);
            }
            BaseArray baseIdx;
            if (format == 0x08) baseIdx = new ByteArray(lengths);
            else if (format == 0x09) baseIdx = new SignedArray(lengths);
            else if (format == 0x0B) baseIdx = new ShortArray(lengths);
            else if (format == 0x0C) baseIdx = new IntArray(lengths);
            else if (format == 0x0D) baseIdx = new SingleArray(lengths);
            else if (format == 0x0E) baseIdx = new DoubleArray(lengths);
            else throw new FileFormatException(743156);
            baseIdx.ReadValuesFromStream(stream);
            return baseIdx;
        }

        #endregion
    }
}
