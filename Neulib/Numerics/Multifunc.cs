using Neulib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neulib.Numerics
{
    public class Multifunc
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public int N { get; private set; }

        private readonly double[,,] _c;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Multifunc(int n)
        {
            N = n;
            _c = new double[n + 1, n + 1, n + 1];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Multifunc

        public void Randomize(Random random)
        {
            int n = N;
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= n; j++)
                    for (int k = 0; k <= n; k++)
                        _c[i, j, k] = -1f + 2f * (double)random.NextDouble();
        }

        public float Calculate(Single1D x, Single1D df)
        {
            int n = N;
            if (n != x.Count) throw new UnequalValueException(n, x.Count, 539428);
            if (n != df.Count) throw new UnequalValueException(n, df.Count, 624376);

            double f = 0f;
            for (int i = 0; i <= n; i++)
            {
                double xi = i < n ? x[i] : 1f;
                for (int j = 0; j <= n; j++)
                {
                    double xj = j < n ? x[j] : 1f;
                    for (int k = 0; k <= n; k++)
                    {
                        double xk = k < n ? x[k] : 1f;
                        f += _c[i, j, k] * xi * xj * xk;
                    }
                }
            }

            double[] df2 = new double[n];
            for (int l = 0; l < n; l++) df2[l] = 0;
            for (int i = 0; i <= n; i++)
            {
                double xi = i < n ? x[i] : 1f;
                for (int j = 0; j <= n; j++)
                {
                    double xj = j < n ? x[j] : 1f;
                    for (int k = 0; k <= n; k++)
                    {
                        double xk = k < n ? x[k] : 1f;
                        for (int l = 0; l < n; l++)
                        {

                            double t = 0f;
                            if (i == l && j == l && k == l)
                                t = 3 * xi * xi;
                            else if (i == l && j == l)
                                t = 2 * xi * xk;
                            else if (i == l && k == l)
                                t = 2 * xi * xj;
                            else if (j == l && k == l)
                                t = 2 * xj * xk;
                            else if (i == l)
                                t = xj * xk;
                            else if (j == l)
                                t = xi * xk;
                            else if (k == l)
                                t = xi * xj;
                            df2[l] += _c[i, j, k] * t;
                        }
                    }
                }
            }
            for (int l = 0; l < n; l++) df[l] = (float)(f * df2[l]);
            f = 0.5f * f * f;
            return (float)f;
        }

        #endregion
    }
}
