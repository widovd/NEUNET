using System;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Single2x2
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float XX;

        public float XY;

        public float YX;

        public float YY;

        public Single2 Row1
        {
            get { return new Single2(XX, XY); }
            set
            {
                XX = value.X;
                XY = value.Y;
            }
        }

        public Single2 Row2
        {
            get { return new Single2(YX, YY); }
            set
            {
                YX = value.X;
                YY = value.Y;
            }
        }

        public Single2 Col1
        {
            get { return new Single2(XX, YX); }
            set
            {
                XX = value.X;
                YX = value.Y;
            }
        }

        public Single2 Col2
        {
            get { return new Single2(XY, YY); }
            set
            {
                XY = value.X;
                YY = value.Y;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Single2x2(float xx, float xy, float yx, float yy)
        {
            XX = xx; XY = xy;
            YX = yx; YY = yy;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Single2x2 value = (Single2x2)o;
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

        public static bool operator ==(Single2x2 left, Single2x2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Single2x2 left, Single2x2 right)
        {
            return !left.Equals(right);
        }

        public static Single2x2 operator +(Single2x2 M1, Single2x2 M2)
        {
            return new Single2x2(
                M1.XX + M2.XX, M1.XY + M2.XY,
                M1.YX + M2.YX, M1.YY + M2.YY
                );
        }

        public static Single2x2 operator -(Single2x2 M1, Single2x2 M2)
        {
            return new Single2x2(
                M1.XX - M2.XX, M1.XY - M2.XY,
                M1.YX - M2.YX, M1.YY - M2.YY
                );
        }

        public static Single2x2 operator -(Single2x2 M)
        {
            return new Single2x2(
                -M.XX, -M.XY,
                -M.YX, -M.YY
                );
        }

        public static Single2x2 operator *(float f, Single2x2 M)
        {
            return new Single2x2(
                f * M.XX, f * M.XY,
                f * M.YX, f * M.YY
                );
        }

        public static Single2x2 operator *(Single2x2 M, float f)
        {
            return new Single2x2(
                f * M.XX, f * M.XY,
                f * M.YX, f * M.YY
                );
        }

        public static Single2 operator *(Single2x2 M, Single2 p)
        {
            return new Single2(
                M.XX * p.X + M.XY * p.Y,
                M.YX * p.X + M.YY * p.Y
                );
        }

        public static Single2 operator *(Single2 p, Single2x2 M)
        {
            return new Single2(
                p.X * M.XX + p.Y * M.YX,
                p.X * M.XY + p.Y * M.YY
                );
        }

        public static Single2x2 operator *(Single2x2 M1, Single2x2 M2)
        {
            return new Single2x2(
                M1.XX * M2.XX + M1.XY * M2.YX, M1.XX * M2.XY + M1.XY * M2.YY,
                M1.YX * M2.XX + M1.YY * M2.YX, M1.YX * M2.XY + M1.YY * M2.YY
                );
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Misc

        public Single2x2 Transpose()
        {
            return new Single2x2(XX, YX, XY, YY);
        }

        public Single2x2 Inverse()
        {
            if (IsNaN(this)) return NaN;
            MatrixNxN M1 = new MatrixNxN(2);
            M1[0, 0] = XX; M1[0, 1] = XY;
            M1[1, 0] = YX; M1[1, 1] = YY;
            try
            {
                MatrixNxN M2 = M1.Inverse();
                return new Single2x2(
                    (float)M2[0, 0], (float)M2[0, 1],
                    (float)M2[1, 0], (float)M2[1, 1]
                    );
            }
            catch (SingularMatrixException)
            {
                return NaN;
            }
        }

        public Single2x2 Orthogonalize(Single2x2 defaultResult)
        {
            // Gram-Schmidtmethod
            // ToDo: Improve with Singular Value Decomposition A = UWVT, and set W = I
            Single2x2 result = defaultResult;
            Single2 x1 = Row1, x2 = Row2;
            Single2 y1 = x1;
            if (y1.LengthSqr() == 0f) return result;
            y1 = y1.Normalize();
            Single2 y2 = x2 - x2 * y1 * y1;
            if (y2.LengthSqr() == 0f) return result;
            y2 = y2.Normalize();
            return new Single2x2(y1.X, y1.Y, y2.X, y2.Y);
        }

        public Single2x2 Orthogonalize()
        {
            return Orthogonalize(this);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Single2x2 value)
        {
            return float.IsNaN(value.XX) || float.IsNaN(value.XY) || float.IsNaN(value.YX) || float.IsNaN(value.YY);
        }

        public static Single2x2 NaN
        {
            get => new Single2x2(float.NaN, float.NaN, float.NaN, float.NaN);
        }

        public static Single2x2 Zero
        {
            get => new Single2x2(0f, 0f, 0f, 0f);
        }

        public static Single2x2 One
        {
            get => new Single2x2(1f, 0f, 0f, 1f);
        }

        public static Single2x2 Rot1(float a)
        {
            float cs = (float)Cos(a);
            float sn = (float)Sin(a);
            return new Single2x2(cs, sn, -sn, cs);
        }

        #endregion
    }
}
