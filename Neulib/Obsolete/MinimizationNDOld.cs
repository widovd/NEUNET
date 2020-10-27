using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Extensions;
using static System.Math;

namespace Neulib.Numerics
{

    public static class MinimizationNDOld
    {
        public static void FRPRMN(float[] p, Func<float[], float> fnc, Action<float[], float[]> dfnc, float ftol)
        {
            const int maxIter = 200;
            const float eps = (float)1.0e-10;
            int n = p.Length;
            float[] xi = new float[n];
            float fp = fnc(p);
            dfnc(p, xi);
            float[] g = new float[n];
            float[] h = new float[n];
            for (int j1 = 0; j1 < n; j1++)
            {
                g[j1] = -xi[j1];
                h[j1] = g[j1];
                xi[j1] = h[j1];
            }
            Line3 line3 = new Line3(fnc, dfnc);

            for (int iter = 0; iter < maxIter; iter++)
            {

                (float xmin, float fret) = line3.DLinmin(p, xi);
                if ((2f * Abs(fret - fp)) <= (ftol * (Abs(fret) + Abs(fp) + eps))) return;

                for (int j = 0; j < n; j++)
                {
                    p[j] += xmin * xi[j];
                }


                fp = fnc(p);
                dfnc(p, xi);
                float gg = 0f;
                float dgg = 0f;
                for (int j = 0; j < n; j++)
                {

                    gg += FloatExtensions.Sqr(g[j]);
                    // dgg += FloatExtensions.Sqr(xi[j]);
                    dgg += (xi[j] + g[j]) * xi[j];
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

    }
}
