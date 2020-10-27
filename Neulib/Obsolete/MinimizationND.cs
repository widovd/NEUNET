using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Extensions;
using static System.Math;
using static Neulib.Extensions.FloatExtensions;

namespace Neulib.Numerics
{
    public class MinimizationND
    {
        // ----------------------------------------------------------------------------------------
        #region Properties


        public int Count { get; private set; }

        public Func<float[], float> Fnc { get; private set; }
        public Action<float[], float[]> Dfnc { get; private set; }

        private readonly float[] _p;
        private readonly float[] _ptemp;
        private readonly float[] _xi;
        private readonly float[] _df;
        private float _fp;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public MinimizationND(float[] p, Func<float[], float> fnc, Action<float[], float[]> dfnc)
        {
            int count = p.Length;
            Count = count;
            Fnc = fnc;
            Dfnc = dfnc;
            _p = new float[count];
            _ptemp = new float[count];
            _xi = new float[count];
            _df = new float[count];
            for (int i = 0; i < count; i++)
            {
                float pi = p[i];
                _p[i] = pi;
                _ptemp[i] = pi;
            }
            _fp = fnc(_p);
            dfnc(_p, _xi);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Minimization

        private float f1dim(float x)
        {
            int n = Count;
            for (int j = 0; j < n; j++)
            {
                _ptemp[j] = _p[j] + x * _xi[j];
            }
            return Fnc(_ptemp);
        }

        private (float, float) f2dim(float x)
        {
            int n = Count;
            for (int j = 0; j < n; j++)
            {
                _ptemp[j] = _p[j] + x * _xi[j];
            }
            float f1 = Fnc(_ptemp);
            Dfnc(_ptemp, _df);
            float df1 = 0;
            for (int j = 0; j < n; j++)
            {
                df1 += _df[j] * _xi[j];
            }
            return (f1, df1);
        }

        public float Linmin()
        {
            const float tol = (float)1e-4;
            (float x, float fx) = Minimization1D.Brent(0f, 1f, tol, f1dim);
            return fx;
        }

        public float DLinmin()
        {
            const float tol = (float)1e-4;
            (float x, float fx) = Minimization1D.DBrent(0f, 1f, tol, f2dim);
            return fx;
        }

        public void FRPRMN(float ftol)
        {
            const int maxIter = 200;
            const float eps = (float)1.0e-10;
            int n = Count;
            float[] g = new float[n];
            float[] h = new float[n];

            for (int j = 0; j < n; j++)
            {
                g[j] = -_xi[j];
                h[j] = g[j];
                _xi[j] = h[j];
            }

            for (int iter = 0; iter < maxIter; iter++)
            {
                float fprev = _fp;
                _fp = DLinmin();
                if ((2f * Abs(_fp - fprev)) <= (ftol * (Abs(_fp) + Abs(fprev) + eps))) return;
                for (int j = 0; j < n; j++)
                {
                    _p[j] = _ptemp[j];
                }
                Dfnc(_p, _xi);
                float gg = 0f, dgg = 0f;
                for (int j = 0; j < n; j++)
                {
                    gg += Sqr(g[j]);
                    dgg += (_xi[j] + g[j]) * _xi[j]; // dgg += Sqr(_xi[j]);
                }
                if (gg == 0f) return;
                float gam = dgg / gg;
                for (int j = 0; j < n; j++)
                {
                    g[j] = -_xi[j];
                    h[j] = g[j] + gam * h[j];
                    _xi[j] = h[j];
                }
            }
        }

        #endregion
    }
}
