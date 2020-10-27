using System;
using Neulib.Extensions;
using static System.Math;

namespace Neulib.Numerics
{

    public static class LinearAlgebra
    {

        public static float AngleBetweenVectors(Single3 p1, Single3 p2)
        {
            double l1 = p1.Length();
            double l2 = p2.Length();
            if ((l1 == 0) || (l2 == 0))
                throw new Exception("Angle between vectors is not defined if length of the vector(s) is zero");
            double cs = (p1 * p2) / (l1 * l2);
            double ad;
            if (Abs(cs) < 1d)
                ad = Acos(cs).ToDegrees();
            else
                ad = (cs > 0) ? 0d : 180d;
            return (float)ad;
        }

        public static double AngleBetweenVectors(Double3 p1, Double3 p2)
        {
            double l1 = p1.Length();
            double l2 = p2.Length();
            if ((l1 == 0) || (l2 == 0))
                throw new Exception("Angle between vectors is not defined if length of the vector(s) is zero");
            double cs = (p1 * p2) / (l1 * l2);
            double ad;
            if (Abs(cs) < 1d)
                ad = Acos(cs).ToDegrees();
            else
                ad = (cs > 0) ? 0d : 180d;
            return ad;
        }

        public static bool IsUnitVector(Single3 p)
        {
            const double Tiny = 0.000001f;
            return (Abs(p.Length() - 1f) < Tiny);
        }

        public static bool IsUnitVector(Double3 p)
        {
            const double Tiny = 0.000001d;
            return (Abs(p.Length() - 1d) < Tiny);
        }

        public static bool IsOrthogonal(Single3 p1, Single3 p2)
        {
            const double Tiny = 0.000001f;
            return (Abs(p1 * p2) < Tiny);
        }

        public static bool IsOrthogonal(Double3 p1, Double3 p2)
        {
            const double Tiny = 0.000001d;
            return (Abs(p1 * p2) < Tiny);
        }

        public static bool IsOrthogonal(Single3 c1, Single3 c2, Single3 c3)
        {
            if (!IsUnitVector(c1)) return false;
            if (!IsUnitVector(c2)) return false;
            if (!IsUnitVector(c3)) return false;
            if (!IsOrthogonal(c1, c2)) return false;
            if (!IsOrthogonal(c2, c3)) return false;
            if (!IsOrthogonal(c3, c1)) return false;
            return true;
        }

        public static bool IsOrthogonal(Double3 c1, Double3 c2, Double3 c3)
        {
            if (!IsUnitVector(c1)) return false;
            if (!IsUnitVector(c2)) return false;
            if (!IsUnitVector(c3)) return false;
            if (!IsOrthogonal(c1, c2)) return false;
            if (!IsOrthogonal(c2, c3)) return false;
            if (!IsOrthogonal(c3, c1)) return false;
            return true;
        }

        public static bool IsOrthogonal(Single3x3 m)
        {
            if (!IsOrthogonal(m.Col1, m.Col2, m.Col3)) return false;
            if (!IsOrthogonal(m.Row1, m.Row2, m.Row3)) return false;
            return true;
        }

        public static bool IsOrthogonal(Double3x3 m)
        {
            if (!IsOrthogonal(m.Col1, m.Col2, m.Col3)) return false;
            if (!IsOrthogonal(m.Row1, m.Row2, m.Row3)) return false;
            return true;
        }

    }

}
