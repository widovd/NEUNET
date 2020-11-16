using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{
    public class LudCmp : MatrixNxN
    {
        public int[] Ixx { get; }
        public double D { get; set; }

        public LudCmp(MatrixNxN mtx) : base(mtx.Count1)
        {
            const double Tiny = 1E-10;
            int n = Count1;
            Ixx = new int[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    this[i, j] = mtx[i, j];
                }
            }

            double[] vv = new double[n];
            for (int i = 0; i < n; i++)
            {
                double vMax = 0d;
                for (int j = 0; j < n; j++)
                {
                    double v = Abs(this[i, j]);
                    if (v > vMax) vMax = v;
                }
                if (vMax == 0d)  // Singular matrix
                    throw new SingularMatrixException(690034);
                vv[i] = 1d / vMax;
            }


            D = 1d;
            int iMax = 0;
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    double Sum = this[i, j];
                    for (int k = 0; k < i; k++)
                    {
                        Sum = Sum - this[i, k] * this[k, j];
                    }
                    this[i, j] = Sum;
                }

                double big = 0d;
                iMax = j;
                for (int i = j; i < n; i++)
                {
                    double Sum = this[i, j];
                    for (int k = 0; k <= j - 1; k++)
                    {
                        Sum = Sum - this[i, k] * this[k, j];
                    }
                    this[i, j] = Sum;
                    double dum = vv[i] * Abs(Sum);
                    if (dum >= big) { big = dum; iMax = i; }
                }
                Ixx[j] = iMax;

                if (j != iMax)
                {
                    for (int k = 0; k < n; k++)
                    {
                        double dum1 = this[iMax, k]; this[iMax, k] = this[j, k]; this[j, k] = dum1;
                    }
                    D = -D;
                    double dum2 = vv[iMax]; vv[iMax] = vv[j]; vv[j] = dum2;
                }

                double dum3 = this[j, j];
                if (Abs(dum3) < Tiny)
                    throw new SingularMatrixException(563278);
                if (j != n)
                {
                    dum3 = 1.0 / dum3;
                    for (int i = j + 1; i < n; i++)
                    {
                        this[i, j] = this[i, j] * dum3;
                    }
                }
            }
        }

        public double[] LubKsb(double[] vec)
        {
            int n = Count1;
            if (n != vec.Length) throw new UnequalValueException(n, vec.Length, 873837);
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
                result[i] = vec[i];
            for (int i = 0; i < n; i++)
            {
                int j = Ixx[i];
                double sum = result[j];
                result[j] = result[i];
                for (j = 0; j < i; j++)
                {
                    double tmp = result[j];
                    if (tmp != 0) sum -= this[i, j] * tmp;
                }
                result[i] = (float)sum;
            }
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = result[i];
                for (int j = i + 1; j < n; j++) sum -= this[i, j] * result[j];
                result[i] = (float)(sum / this[i, i]);
            }
            return result;
        }


    }

}
