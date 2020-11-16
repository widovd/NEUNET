using System;
using System.Threading.Tasks;
using Neulib.Extensions;
using Neulib.Exceptions;

namespace Neulib.Numerics
{
    public static class PartialDerivatives
    {

        public static void Calc_dZdu_p(double[,] Z, double[,] dZdu, double du)
        {
            int n = Z.GetLength(0), m = Z.GetLength(1);
            if ((n != dZdu.GetLength(0)) || (m != dZdu.GetLength(1))) throw new ArgumentException("dZdu must have the same dimensions as Z.");
            Parallel.For(0, n, (i) =>
            {
                for (int j = 0; j < m; j++)
                {
                    double Zm = Z[(i - 1).Trim(n), j];
                    double Zp = Z[(i + 1).Trim(n), j];
                    dZdu[i, j] = 0.5d * (Zp - Zm) / du;
                }
            });
        }

        public static void Calc_dZdu_n(double[,] Z, double[,] dZdu, double du)
        {
            int n = Z.GetLength(0), m = Z.GetLength(1);
            if (n != dZdu.GetLength(0))
                throw new UnequalValueException(n, dZdu.GetLength(0), 186866);
            if (m != dZdu.GetLength(1))
                throw new UnequalValueException(m, dZdu.GetLength(1), 952785);
            {
                int i = 0;
                for (int j = 0; j < m; j++)
                {
                    double Z0 = Z[i + 0, j];
                    double Z1 = Z[i + 1, j];
                    double Z2 = Z[i + 2, j];
                    dZdu[i, j] = 0.5d * (-3d * Z0 + 4d * Z1 - Z2) / du;
                }
            }
            Parallel.For(1, n - 1, (i) =>
            {
                for (int j = 0; j < m; j++)
                {
                    double Zm = Z[i - 1, j];
                    double Zp = Z[i + 1, j];
                    dZdu[i, j] = 0.5d * (Zp - Zm) / du;
                }
            });
            {
                int i = n - 1;
                for (int j = 0; j < m; j++)
                {
                    double Z0 = Z[i - 0, j];
                    double Z1 = Z[i - 1, j];
                    double Z2 = Z[i - 2, j];
                    dZdu[i, j] = -0.5d * (-3d * Z0 + 4d * Z1 - Z2) / du;
                }
            }
        }

        /// <summary>
        /// Calculates the partial derivatives of Z with respect to u.
        /// </summary>
        /// <param name="Z">Input: A 2-dimensional array of double values.</param>
        /// <param name="dZdu">Input: Initialized array with same dimensions as Z. Output: Partial derivatives of Z.</param>
        /// <param name="du">The step size of u.</param>
        /// <param name="p">Bool indicating the periodicity of the array in u. If true then Z[i+n,j] = Z[i,j].</param>
        public static double[,] Calc_dZdu(double[,] Z, double du, bool p)
        {
            int uCount = Z.GetLength(0), vCount = Z.GetLength(1);
            double[,] dZdu = new double[uCount, vCount];
            if (p)
                Calc_dZdu_p(Z, dZdu, du);
            else
                Calc_dZdu_n(Z, dZdu, du);
            return dZdu;
        }

        public static void Calc_dZdv_p(double[,] Z, double[,] dZdv, double dv)
        {
            int n = Z.GetLength(0), m = Z.GetLength(1);
            if (n != dZdv.GetLength(0))
                throw new UnequalValueException(n, dZdv.GetLength(0), 353915);
            if (m != dZdv.GetLength(1))
                throw new UnequalValueException(m, dZdv.GetLength(1), 902550);
            Parallel.For(0, m, (j) =>
            {
                for (int i = 0; i < n; i++)
                {
                    double Zm = Z[i, (j - 1).Trim(m)];
                    double Zp = Z[i, (j + 1).Trim(m)];
                    dZdv[i, j] = 0.5d * (Zp - Zm) / dv;
                }
            });
        }

        public static void Calc_dZdv_n(double[,] Z, double[,] dZdv, double dv)
        {
            int n = Z.GetLength(0), m = Z.GetLength(1);
            if ((n != dZdv.GetLength(0)) || (m != dZdv.GetLength(1))) throw new ArgumentException("dZdv must have the same dimensions as Z.");
            {
                int j = 0;
                for (int i = 0; i < n; i++)
                {
                    double Z0 = Z[i, j + 0];
                    double Z1 = Z[i, j + 1];
                    double Z2 = Z[i, j + 2];
                    dZdv[i, j] = 0.5d * (-3d * Z0 + 4d * Z1 - Z2) / dv;
                }
            }
            Parallel.For(1, m - 1, (j) =>
            {
                for (int i = 0; i < n; i++)
                {
                    double Zm = Z[i, j - 1];
                    double Zp = Z[i, j + 1];
                    dZdv[i, j] = 0.5d * (Zp - Zm) / dv;
                }
            });
            {
                int j = m - 1;
                for (int i = 0; i < n; i++)
                {
                    double Z0 = Z[i, j - 0];
                    double Z1 = Z[i, j - 1];
                    double Z2 = Z[i, j - 2];
                    dZdv[i, j] = -0.5d * (-3d * Z0 + 4d * Z1 - Z2) / dv;
                }
            }
        }

        /// <summary>
        /// Calculates the partial derivatives of Z with respect to v.
        /// </summary>
        /// <param name="Z">Input: A 2-dimensional array of double values.</param>
        /// <param name="dZdv">Input: Initialized array with same dimensions as Z. Output: Partial derivatives of Z.</param>
        /// <param name="dv">The step size of v.</param>
        /// <param name="p">Bool indicating the periodicity of the array in v. If true then Z[i,j+m] = Z[i,j].</param>
        public static double[,] Calc_dZdv(double[,] Z, double dv, bool p)
        {
            int uCount = Z.GetLength(0), vCount = Z.GetLength(1);
            double[,] dZdv = new double[uCount, vCount];
            if (p)
                Calc_dZdv_p(Z, dZdv, dv);
            else
                Calc_dZdv_n(Z, dZdv, dv);
            return dZdv;
        }

        public static void Calc_dZdu_p(Double3[,] Z, Double3[,] dZdu, double du)
        {
            int n = Z.GetLength(0), m = Z.GetLength(1);
            if ((n != dZdu.GetLength(0)) || (m != dZdu.GetLength(1))) throw new ArgumentException("dZdu must have the same dimensions as Z.");
            Parallel.For(0, n, (i) =>
            {
                for (int j = 0; j < m; j++)
                {
                    dZdu[i, j] = Double3.NaN; // default
                    Double3 Zm = Z[(i - 1).Trim(n), j];
                    Double3 Zp = Z[(i + 1).Trim(n), j];
                    if (Double3.IsNaN(Zm) || Double3.IsNaN(Zp)) continue;
                    dZdu[i, j] = 0.5d * (Zp - Zm) / du;
                }
            });
        }

        public static void Calc_dZdu_n(Double3[,] Z, Double3[,] dZdu, double du)
        {
            int n = Z.GetLength(0);
            int m = Z.GetLength(1);
            if ((n != dZdu.GetLength(0)) || (m != dZdu.GetLength(1))) throw new ArgumentException("dZdu must have the same dimensions as Z.");
            {
                int i = 0;
                for (int j = 0; j < m; j++)
                {
                    dZdu[i, j] = Double3.NaN; // default
                    Double3 Z0 = Z[i + 0, j];
                    Double3 Z1 = Z[i + 1, j];
                    Double3 Z2 = Z[i + 2, j];
                    if (Double3.IsNaN(Z0) || Double3.IsNaN(Z1) || Double3.IsNaN(Z2)) continue;
                    dZdu[i, j] = 0.5d * (-3d * Z0 + 4d * Z1 - Z2) / du;
                }
            }
            Parallel.For(1, n - 1, (i) =>
            {
                for (int j = 0; j < m; j++)
                {
                    dZdu[i, j] = Double3.NaN; // default
                    Double3 Zm = Z[i - 1, j];
                    Double3 Zp = Z[i + 1, j];
                    if (Double3.IsNaN(Zm) || Double3.IsNaN(Zp)) continue;
                    dZdu[i, j] = 0.5d * (Zp - Zm) / du;
                }
            });
            {
                int i = n - 1;
                for (int j = 0; j < m; j++)
                {
                    dZdu[i, j] = Double3.NaN; // default
                    Double3 Z0 = Z[i - 0, j];
                    Double3 Z1 = Z[i - 1, j];
                    Double3 Z2 = Z[i - 2, j];
                    if (Double3.IsNaN(Z0) || Double3.IsNaN(Z1) || Double3.IsNaN(Z2)) continue;
                    dZdu[i, j] = -0.5d * (-3d * Z0 + 4d * Z1 - Z2) / du;
                }
            }

        }

        /// <summary>
        /// Calculates the partial derivatives of Z with respect to u.
        /// </summary>
        /// <param name="Z">Input: A 2-dimensional array of sDouble3 values.</param>
        /// <param name="dZdu">Input: Initialized array with same dimensions as Z. Output: Partial derivatives of Z.</param>
        /// <param name="stepSize">The step size of u.</param>
        /// <param name="periodic">Bool indicating the periodicity of the array in u. If true then Z[i+n,j] = Z[i,j].</param>
        public static Double3[,] Calc_dZdu(Double3[,] Z, double stepSize, bool periodic)
        {
            int uCount = Z.GetLength(0), vCount = Z.GetLength(1);
            Double3[,] dZdu = new Double3[uCount, vCount];
            if (periodic)
                Calc_dZdu_p(Z, dZdu, stepSize);
            else
                Calc_dZdu_n(Z, dZdu, stepSize);
            return dZdu;
        }

        public static void Calc_dZdv_p(Double3[,] Z, Double3[,] dZdv, double dv)
        {
            int n = Z.GetLength(0);
            int m = Z.GetLength(1);
            if ((n != dZdv.GetLength(0)) || (m != dZdv.GetLength(1))) throw new ArgumentException("dZdv must have the same dimensions as Z.");
            Parallel.For(0, m, (j) =>
            {
                for (int i = 0; i < n; i++)
                {
                    dZdv[i, j] = Double3.NaN; // default
                    Double3 Zm = Z[i, (j - 1).Trim(m)];
                    Double3 Zp = Z[i, (j + 1).Trim(m)];
                    if (Double3.IsNaN(Zm) || Double3.IsNaN(Zp)) continue;
                    dZdv[i, j] = 0.5d * (Zp - Zm) / dv;
                }
            });
        }

        public static void Calc_dZdv_n(Double3[,] Z, Double3[,] dZdv, double dv)
        {
            int n = Z.GetLength(0);
            int m = Z.GetLength(1);
            if ((n != dZdv.GetLength(0)) || (m != dZdv.GetLength(1))) throw new ArgumentException("dZdv must have the same dimensions as Z.");
            {
                int j = 0;
                for (int i = 0; i < n; i++)
                {
                    dZdv[i, j] = Double3.NaN; // default
                    Double3 Z0 = Z[i, j + 0];
                    Double3 Z1 = Z[i, j + 1];
                    Double3 Z2 = Z[i, j + 2];
                    if (Double3.IsNaN(Z0) || Double3.IsNaN(Z1) || Double3.IsNaN(Z2)) continue;
                    dZdv[i, j] = 0.5d * (-3d * Z0 + 4d * Z1 - Z2) / dv;
                }
            }
            Parallel.For(1, m - 1, (j) =>
            {
                for (int i = 0; i < n; i++)
                {
                    dZdv[i, j] = Double3.NaN; // default
                    Double3 Zm = Z[i, j - 1];
                    Double3 Zp = Z[i, j + 1];
                    if (Double3.IsNaN(Zm) || Double3.IsNaN(Zp)) continue;
                    dZdv[i, j] = 0.5d * (Zp - Zm) / dv;
                }
            });
            {
                int j = m - 1;
                for (int i = 0; i < n; i++)
                {
                    dZdv[i, j] = Double3.NaN; // default
                    Double3 Z0 = Z[i, j - 0];
                    Double3 Z1 = Z[i, j - 1];
                    Double3 Z2 = Z[i, j - 2];
                    if (Double3.IsNaN(Z0) || Double3.IsNaN(Z1) || Double3.IsNaN(Z2)) continue;
                    dZdv[i, j] = -0.5d * (-3d * Z0 + 4d * Z1 - Z2) / dv;
                }
            }
        }

        /// <summary>
        /// Calculates the partial derivatives of Z with respect to v.
        /// </summary>
        /// <param name="Z">Input: A 2-dimensional array of sDouble3 values.</param>
        /// <param name="dZdv">Input: Initialized array with same dimensions as Z. Output: Partial derivatives of Z.</param>
        /// <param name="dv">The step size of v.</param>
        /// <param name="p">Bool indicating the periodicity of the array in v. If true then Z[i,j+m] = Z[i,j].</param>
        public static Double3[,] Calc_dZdv(Double3[,] Z, double dv, bool p)
        {
            int uCount = Z.GetLength(0), vCount = Z.GetLength(1);
            Double3[,] dZdv = new Double3[uCount, vCount];
            if (p)
                Calc_dZdv_p(Z, dZdv, dv);
            else
                Calc_dZdv_n(Z, dZdv, dv);
            return dZdv;
        }

    }
}
