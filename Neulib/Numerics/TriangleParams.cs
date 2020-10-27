using System;

namespace Neulib.Numerics
{
    public class TriangleParams
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public double AX { get; set; }
        public double AY { get; set; }
        public double Z0 { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public TriangleParams()
        {
        }

        public TriangleParams(double aX, double aY, double z0)
        {
            AX = aX;
            AY = aY;
            Z0 = z0;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region This

        public bool Define(Double2 p1, Double2 p2, Double2 p3, double f1, double f2, double f3)
        {
            if (Double2.IsNaN(p1) || Double2.IsNaN(p2) || Double2.IsNaN(p3)) return false;
            if (double.IsNaN(f1) || double.IsNaN(f2) || double.IsNaN(f3)) return false;
            double det = (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
            if (det == 0d) return false; // the area of the triangle is zero
            AX = ((f2 - f1) * (p3.Y - p1.Y) - (f3 - f1) * (p2.Y - p1.Y)) / det;
            AY = ((p2.X - p1.X) * (f3 - f1) - (p3.X - p1.X) * (f2 - f1)) / det;
            Z0 = ((f1 + f2 + f3) - AX * (p1.X + p2.X + p3.X) - AY * (p1.Y + p2.Y + p3.Y)) / 3;
            return true;
        }

        public double Evaluate(double u, double v)
        {
            return AX * u + AY * v + Z0;
        }

        public double Evaluate(Double2 p)
        {
            return AX * p.X + AY * p.Y + Z0;
        }

        public static double TriangleArea(Double2 p1, Double2 p2, Double2 p3)
        {
            return 0.5d * Math.Abs((p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X));
        }

        #endregion
    }
}
