using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using Neulib.Exceptions;
using static Neulib.Extensions.FloatExtensions;

namespace Neulib.Numerics
{
    public enum MinimizationAlgorithmEnum
    {
        PolakRibiere,
        FletcherReeves,
        SteepestDescent,
    }

    public class Minimization
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public MinimizationAlgorithmEnum MinimizationAlgorithm { get; set; } = MinimizationAlgorithmEnum.PolakRibiere;
        public int MaxIter { get; set; } = 20;

        public float DBrentTol { get; set; } = (float)1e-4;

        public float Eps { get; set; } = (float)1.0e-10;

        public float Tol { get; set; } = (float)1e-4;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Minimization()
        {

        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Minimization

        public void ConjugateGradient(float[] point, Func<float[], float[], float> func)
        // Frprmn
        {
            int n = point.Length;
            float[] xi = new float[n]; // the local gradient
            float[] df = new float[n];
            float[] g = new float[n];
            float[] h = new float[n];
            float[] pNext = new float[n];
            float fp = func(point, xi);
            for (int j = 0; j < n; j++)
            {
                float t = -xi[j]; g[j] = t; h[j] = t; xi[j] = t;
            }
            for (int iter = 0; iter < MaxIter; iter++)
            {
                (float xmin, float fmin, float dfmin) = DBrent(0f, 1f, DBrentTol, x1 =>
                {
                    for (int j = 0; j < n; j++)
                    {
                        pNext[j] = point[j] + x1 * xi[j];
                    }
                    float fx = func(pNext, df);
                    float dfx = 0;
                    for (int j = 0; j < n; j++)
                    {
                        dfx += df[j] * xi[j];
                    }
                    return (fx, dfx);
                });
                if ((2f * Abs(fmin - fp)) <= (Tol * (Abs(fmin) + Abs(fp) + Eps))) return;
                fp = fmin;
                for (int j = 0; j < n; j++)
                {
                    point[j] = pNext[j];
                    xi[j] = df[j];
                }
                float gg = 0f, dgg = 0f;
                for (int j = 0; j < n; j++)
                {
                    gg += Sqr(g[j]);
                    switch (MinimizationAlgorithm)
                    {
                        case MinimizationAlgorithmEnum.PolakRibiere:
                            dgg += (xi[j] + g[j]) * xi[j];
                            break;
                        case MinimizationAlgorithmEnum.FletcherReeves:
                            dgg += Sqr(xi[j]);
                            break;
                        default:
                            break;
                    }
                }
                if (gg == 0f) return;
                float gam = dgg / gg;
                for (int j = 0; j < n; j++)
                {
                    g[j] = -xi[j];
                    h[j] = g[j] + gam * h[j];
                    xi[j] = h[j];
                }
            }
        }

        private static void SubOne(float[] point, float[] xi, float alpha)
        {
            int n = point.Length;
            for (int j = 0; j < n; j++)
            {
                point[j] -= alpha * xi[j];
            }
        }

        public void SteepestDescent(float[] point, Func<float[], float[], float> func, float alpha)
        {
            int n = point.Length;
            float[] xi = new float[n];
            float f2 = float.NaN;
            for (int iter = 0; iter < MaxIter; iter++)
            {
                float f1 = f2;
                f2 = func(point, xi);
                if ((2f * Abs(f2 - f1)) <= (Tol * (Abs(f1) + Abs(f2) + Eps))) return;
                SubOne(point, xi, alpha);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Minimization1D

        private static float Sign2(float a, float b)
        {
            return b > 0f ? Abs(a) : -Abs(a);
        }

        private static void Swap(ref float a, ref float b)
        {
            float t = a; a = b; b = t;
        }

        private static float DefaultMagnification(float x1, float x2)
        {
            const float gold = 1.618034f;
            return x2 + gold * (x2 - x1);
        }

        public static void Mnbrak(
            float x1, float x2,
            out float xa, out float xb, out float xc,
            out float ya, out float yb, out float yc,
            Func<float, float> func)
        {
            const float glimit = 100f;
            const float tiny = (float)1.0e-20;
            xa = x1;
            ya = func(xa);
            xb = x2;
            yb = func(xb);
            if (yb > ya) // Switch roles so that we can go downhill in the direction from xa to xb
            {
                Swap(ref xa, ref xb);
                Swap(ref ya, ref yb);
            }
            xc = DefaultMagnification(xa, xb);
            yc = func(xc);

            while (yb >= yc)
            {
                float xu;
                float yu;
                float ulim = xb + glimit * (xc - xb);
                // Compute xu by parabolic extrapolation from xa, xb, and xc.
                float r = (xb - xa) * (yb - yc);
                float q = (xb - xc) * (yb - ya);
                float d = q - r;
                if (Abs(d) > tiny)
                {
                    xu = xb - ((xb - xc) * q - (xb - xa) * r) / (2f * d);
                    yu = func(xu);
                    if ((xb - xu) * (xu - xc) > 0f) // Parabolic ux is between bx and cx: Try it.
                    {
                        if (yu < yc) // Got a minimum between xb and xc
                        {
                            xa = xb; ya = yb;
                            xb = xu; yb = yu;
                            return;
                        }
                        if (yu > yb) // Got a minimum between xa and xu
                        {
                            xc = xu;
                            yc = yu;
                            return;
                        };
                        xu = DefaultMagnification(xb, xc);
                        yu = func(xu);
                    }
                    else if ((xc - xu) * (xu - ulim) > 0f) // Parabolic fit is between xc and its allowed limit
                    {
                        if (yu < yc)
                        {
                            xb = xc; xc = xu;
                            yb = yc; yc = yu;
                            xu = DefaultMagnification(xb, xc);
                            yu = func(xu);
                        }
                    }
                    else if ((xu - ulim) * (ulim - xc) >= 0f) // Limit parabolic xu to maximum allowed value
                    {
                        xu = ulim;
                        yu = func(xu);
                    }
                    else // Reject parabolic u, use default magnification
                    {
                        xu = DefaultMagnification(xb, xc);
                        yu = func(xu);
                    };
                }
                else
                {
                    xu = DefaultMagnification(xb, xc);
                    yu = func(xu);
                }

                // Eliminate oldest point and continue
                xa = xb; xb = xc; xc = xu;
                ya = yb; yb = yc; yc = yu;
            }
        }

        public static (float, float) Brent(float x1, float x2, float tol, Func<float, float> func)
        {
            const int maxIter = 100;
            const float zeps = (float)1.0e-10;
            const float cgold = 0.3819660f;

            Mnbrak(x1, x2, out float ax, out float bx, out float cx, out float fa, out float fb, out float fc,
                func);

            float u;

            float a = ax < cx ? ax : cx;
            float b = ax > cx ? ax : cx;
            float d = float.NaN;
            float v = bx;
            float w = v;
            float x = v;
            float e = 0f;
            float fx = func(x);
            float fv = fx;
            float fw = fx;
            for (int iter = 0; iter < maxIter; iter++)
            {
                float xm = 0.5f * (a + b);
                float tol1 = tol * Abs(x) + zeps;
                float tol2 = 2f * tol1;
                if (Abs(x - xm) <= (tol2 - 0.5 * (b - a))) goto L3;

                if (Abs(e) > tol1)
                {
                    float r = (x - w) * (fx - fv);
                    float q = (x - v) * (fx - fw);
                    float p = (x - v) * q - (x - w) * r;
                    q = 2f * (q - r);
                    if (q > 0.0) p = -p;
                    q = Abs(q);
                    float etemp = e;
                    e = d;
                    if ((Abs(p) >= Abs(0.5f * q * etemp)) || (p <= q * (a - x)) || (p >= q * (b - x))) goto L1;
                    d = p / q;
                    u = x + d;
                    if (((u - a) < tol2) || ((b - u) < tol2)) d = Sign2(tol1, xm - x);
                    goto L2;
                };
            L1:
                if (x >= xm) e = a - x; else e = b - x;
                d = cgold * e;
            L2:
                if (Abs(d) >= tol1) u = x + d; else u = x + Sign2(tol1, d);
                float fu = func(u);
                if (fu <= fx)
                {
                    if (u >= x) a = x; else b = x;
                    v = w;
                    fv = fw;
                    w = x;
                    fw = fx;
                    x = u;
                    fx = fu;
                }
                else
                {
                    if (u < x) a = u; else b = u;
                    if (fu <= fw || w == x)
                    {
                        v = w;
                        fv = fw;
                        w = u;
                        fw = fu;
                    }
                    else if (fu <= fv || v == x || v == 2)
                    {
                        v = u;
                        fv = fu;
                    }
                }
            };
            throw new IterationException(266332);
        L3:
            return (x, fx);
        }

        public static (float, float, float) DBrent(float x1, float x2, float tol, Func<float, (float, float)> func)
        {
            const int maxIter = 100;
            const float zeps = (float)1.0e-10;

            Mnbrak(x1, x2, out float ax, out float bx, out float cx, out float fa, out float fb, out float fc,
                (x0) =>
                {
                    (float fx0, float dx0) = func(x0); return fx0;
                });

            float a = ax < cx ? ax : cx;
            float b = ax > cx ? ax : cx;
            float d = float.NaN;
            float v = bx;
            float w = v;
            float x = v;
            float e = 0f;
            (float fx, float dfx) = func(x);
            float fv = fx;
            float fw = fx;
            float dfv = dfx;
            float dfw = dfx;
            for (int iter = 0; iter < maxIter; iter++)
            {

                float xm = 0.5f * (a + b);
                float tol1 = tol * Abs(x) + zeps;
                float tol2 = 2f * tol1;
                if (Abs(x - xm) <= (tol2 - 0.5 * (b - a))) goto L3;
                float u;
                if (Abs(e) > tol1)
                {
                    float d1 = 2f * (b - a);
                    float d2 = d1;
                    if (dfw != dfx) d1 = (w - x) * dfx / (dfx - dfw);
                    if (dfv != dfx) d2 = (v - x) * dfx / (dfx - dfv);
                    float u1 = x + d1;
                    float u2 = x + d2;
                    bool ok1 = ((a - u1) * (u1 - b) > 0f) && (dfx * d1 <= 0f);
                    bool ok2 = ((a - u2) * (u2 - b) > 0f) && (dfx * d2 <= 0f);
                    float olde = e;
                    e = d;
                    if (!(ok1 || ok2)) goto L1;
                    else if (ok1 && ok2)
                    {
                        if (Abs(d1) < Abs(d2))
                        {
                            d = d1;
                        }
                        else
                        {
                            d = d2;
                        }
                    }
                    else if (ok1)
                    {
                        d = d1;
                    }
                    else
                    {
                        d = d2;
                    };
                    if (Abs(d) > Abs(0.5f * olde)) goto L1;
                    u = x + d;
                    if (((u - a) < tol2) || ((b - u) < tol2))
                    {
                        d = Sign2(tol1, xm - x);
                    }
                    goto L2;
                }
            L1:
                if (dfx >= 0f) e = a - x; else e = b - x;
                d = 0.5f * e;
                float fu, du;
            L2:
                if (Abs(d) >= tol1)
                {
                    u = x + d;
                    (fu, du) = func(u);
                }
                else
                {
                    u = x + Sign2(tol1, d);
                    (fu, du) = func(u);
                    if (fu > fx) goto L3;
                };
                if (fu <= fx)
                {
                    if (u >= x) a = x; else b = x;
                    v = w;
                    fv = fw;
                    dfv = dfw;
                    w = x;
                    fw = fx;
                    dfw = dfx;
                    x = u;
                    fx = fu;
                    dfx = du;
                }
                else
                {
                    if (u < x) a = u; else b = u;
                    if ((fu <= fw) || (w == x))
                    {
                        v = w;
                        fv = fw;
                        dfv = dfw;
                        w = u;
                        fw = fu;
                        dfw = du;
                    }
                    else if ((fu < fv) || (v == x) || (v == w))
                    {
                        v = u;
                        fv = fu;
                        dfv = du;
                    }
                }
            };
            throw new IterationException(855388);
        L3:
            return (x, fx, dfx);
        }

        #endregion
    }
}
