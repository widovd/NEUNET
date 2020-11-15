using System;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Double2x2
    {

        // ----------------------------------------------------------------------------------------
        #region Properties

        public double XX;

        public double XY;

        public double YX;

        public double YY;

        public Double2 Row1
        {
            get { return new Double2(XX, XY); }
            set
            {
                XX = value.X;
                XY = value.Y;
            }
        }

        public Double2 Row2
        {
            get { return new Double2(YX, YY); }
            set
            {
                YX = value.X;
                YY = value.Y;
            }
        }

        public Double2 Col1
        {
            get { return new Double2(XX, YX); }
            set
            {
                XX = value.X;
                YX = value.Y;
            }
        }

        public Double2 Col2
        {
            get { return new Double2(XY, YY); }
            set
            {
                XY = value.X;
                YY = value.Y;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Double2x2(double xx, double xy, double yx, double yy)
        {
            XX = xx; XY = xy;
            YX = yx; YY = yy;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Double2x2 value = (Double2x2)o;
            if (IsNaN(this) && IsNaN(value)) return true;
            if (XX != value.XX) return false;
            if (XY != value.XY) return false;
            if (YX != value.YX) return false;
            if (YY != value.YY) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + XX.GetHashCode();
                h = h * 23 + XY.GetHashCode();
                h = h * 23 + YX.GetHashCode();
                h = h * 23 + YY.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{XX}, {XY}, {YX}, {YY}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(Double2x2 left, Double2x2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Double2x2 left, Double2x2 right)
        {
            return !left.Equals(right);
        }

        public static Double2x2 operator +(Double2x2 M1, Double2x2 M2)
        {
            return new Double2x2(
                M1.XX + M2.XX, M1.XY + M2.XY,
                M1.YX + M2.YX, M1.YY + M2.YY
                );
        }

        public static Double2x2 operator -(Double2x2 M1, Double2x2 M2)
        {
            return new Double2x2(
                M1.XX - M2.XX, M1.XY - M2.XY,
                M1.YX - M2.YX, M1.YY - M2.YY
                );
        }

        public static Double2x2 operator -(Double2x2 M)
        {
            return new Double2x2(
                -M.XX, -M.XY,
                -M.YX, -M.YY
                );
        }

        public static Double2x2 operator *(double f, Double2x2 M)
        {
            return new Double2x2(
                f * M.XX, f * M.XY,
                f * M.YX, f * M.YY
                );
        }

        public static Double2x2 operator *(Double2x2 M, double f)
        {
            return new Double2x2(
                f * M.XX, f * M.XY,
                f * M.YX, f * M.YY
                );
        }

        public static Double2 operator *(Double2x2 M, Double2 p)
        {
            return new Double2(
                M.XX * p.X + M.XY * p.Y,
                M.YX * p.X + M.YY * p.Y
                );
        }

        public static Double2 operator *(Double2 p, Double2x2 M)
        {
            return new Double2(
                p.X * M.XX + p.Y * M.YX,
                p.X * M.XY + p.Y * M.YY
                );
        }

        public static Double2x2 operator *(Double2x2 M1, Double2x2 M2)
        {
            return new Double2x2(
                M1.XX * M2.XX + M1.XY * M2.YX, M1.XX * M2.XY + M1.XY * M2.YY,
                M1.YX * M2.XX + M1.YY * M2.YX, M1.YX * M2.XY + M1.YY * M2.YY
                );
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Misc

        public Double2x2 Transpose()
        {
            return new Double2x2(XX, YX, XY, YY);
        }

        public Double2x2 Inverse()
        {
            if (IsNaN(this)) return NaN;
            MatrixNxN M1 = new MatrixNxN(2);
            M1.Values[0, 0] = XX; M1.Values[0, 1] = XY;
            M1.Values[1, 0] = YX; M1.Values[1, 1] = YY;
            try
            {
                MatrixNxN M2 = M1.Inverse();
                return new Double2x2(
                    M2.Values[0, 0], M2.Values[0, 1],
                    M2.Values[1, 0], M2.Values[1, 1]
                    );
            }
            catch (SingularMatrixException)
            {
                return NaN;
            }
        }

        public Double2x2 Orthogonalize(Double2x2 defaultResult)
        {
            // Gram-Schmidtmethod
            // ToDo: Improve with Singular Value Decomposition A = UWVT, and set W = I
            Double2x2 result = defaultResult;
            Double2 x1 = Row1, x2 = Row2;
            Double2 y1 = x1;
            if (y1.LengthSqr() == 0d) return result;
            y1 = y1.Normalize();
            Double2 y2 = x2 - x2 * y1 * y1;
            if (y2.LengthSqr() == 0d) return result;
            y2 = y2.Normalize();
            return new Double2x2(y1.X, y1.Y, y2.X, y2.Y);
        }

        public Double2x2 Orthogonalize()
        {
            return Orthogonalize(this);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Double2x2 value)
        {
            return double.IsNaN(value.XX) || double.IsNaN(value.XY) || double.IsNaN(value.YX) || double.IsNaN(value.YY);
        }

        public static Double2x2 NaN
        {
            get => new Double2x2(double.NaN, double.NaN, double.NaN, double.NaN);
        }

        public static Double2x2 Zero
        {
            get => new Double2x2(0d, 0d, 0d, 0d);
        }

        public static Double2x2 One
        {
            get => new Double2x2(1d, 0d, 0d, 1d);
        }

        public static Double2x2 Rot1(double a)
        {
            double cs = Cos(a);
            double sn = Sin(a);
            return new Double2x2(cs, sn, -sn, cs);
        }

        #endregion
    }
}
