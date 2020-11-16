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
                    Values[i, j] = mtx.Values[i, j];
                }
            }

            double[] vv = new double[n];
            for (int i = 0; i < n; i++)
            {
                double vMax = 0d;
                for (int j = 0; j < n; j++)
                {
                    double v = Abs(Values[i, j]);
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
                    double Sum = Values[i, j];
                    for (int k = 0; k < i; k++)
                    {
                        Sum = Sum - Values[i, k] * Values[k, j];
                    }
                    Values[i, j] = Sum;
                }

                double big = 0d;
                iMax = j;
                for (int i = j; i < n; i++)
                {
                    double Sum = Values[i, j];
                    for (int k = 0; k <= j - 1; k++)
                    {
                        Sum = Sum - Values[i, k] * Values[k, j];
                    }
                    Values[i, j] = Sum;
                    double dum = vv[i] * Abs(Sum);
                    if (dum >= big) { big = dum; iMax = i; }
                }
                Ixx[j] = iMax;

                if (j != iMax)
                {
                    for (int k = 0; k < n; k++)
                    {
                        double dum1 = Values[iMax, k]; Values[iMax, k] = Values[j, k]; Values[j, k] = dum1;
                    }
                    D = -D;
                    double dum2 = vv[iMax]; vv[iMax] = vv[j]; vv[j] = dum2;
                }

                double dum3 = Values[j, j];
                if (Abs(dum3) < Tiny)
                    throw new SingularMatrixException(563278);
                if (j != n)
                {
                    dum3 = 1.0 / dum3;
                    for (int i = j + 1; i < n; i++)
                    {
                        Values[i, j] = Values[i, j] * dum3;
                    }
                }
            }
        }

        public Single1D LubKsb(Single1D vec)
        {
            int n = Count1;
            if (n != vec.Count) throw new UnequalValueException(n, vec.Count, 873837);
            Single1D Out = new Single1D(n);
            for (int i = 0; i < n; i++)
            {
                Out[i] = vec[i];
            }
            for (int i = 0; i < n; i++)
            {
                int j = Ixx[i];
                double Sum = Out[j];
                Out[j] = Out[i];
                for (j = 0; j < i; j++)
                {
                    double tmp = Out[j];
                    if (tmp != 0) Sum = Sum - Values[i, j] * tmp;
                }
                Out[i] = (float)Sum;
            }
            for (int i = n - 1; i >= 0; i--)
            {
                double Sum = Out[i];
                for (int j = i + 1; j < n; j++) Sum = Sum - Values[i, j] * Out[j];
                Out[i] = (float)(Sum / Values[i, i]);
            }
            return Out;
        }


    }

}
