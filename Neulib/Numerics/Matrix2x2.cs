using System.ComponentModel;
using Neulib.Extensions;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Matrix2x2
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        [Browsable(false)]
        public double XX { get; set; }

        [Browsable(false)]
        public double XY { get; set; }

        [Browsable(false)]
        public double YX { get; set; }

        [Browsable(false)]
        public double YY { get; set; }

        public Double2 Row1
        {
            get
            {
                return new Double2(XX, XY);
            }
            set
            {
                XX = value.X;
                XY = value.Y;
            }
        }

        public Double2 Row2
        {
            get
            {
                return new Double2(YX, YY);
            }
            set
            {
                YX = value.X;
                YY = value.Y;
            }
        }

        public Double2 Col1
        {
            get
            {
                return new Double2(XX, YX);
            }
            set
            {
                XX = value.X;
                YX = value.Y;
            }
        }

        public Double2 Col2
        {
            get
            {
                return new Double2(XY, YY);
            }
            set
            {
                XY = value.X;
                YY = value.Y;
            }
        }

        public double Determinant
        {
            get { return XX * YY - XY * YX; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Matrix2x2(int option)
        {
            switch (option)
            {
                case 1:
                    XX = 1f; XY = 0f;
                    YX = 0f; YY = 1f;
                    break;
                case 2:
                    XX = 0f; XY = 1f;
                    YX = 1f; YY = 0f;
                    break;
                default:
                    XX = 1f; XY = 0f;
                    YX = 0f; YY = 1f;
                    break;
            }
        }

        public Matrix2x2(
            double xx, double xy,
            double yx, double yy
            )
        {
            XX = xx; XY = xy;
            YX = yx; YY = yy;
        }

        public Matrix2x2(Double2 rotationAxis, double rotationAngle)
        {
            double a = rotationAngle.ToRadians();
            double cs = Cos(a);
            double sn = Sin(a);

            double ux = rotationAxis.X;
            double uy = rotationAxis.Y;

            XX = cs + ux * ux * (1 - cs);
            YX = uy * ux * (1 - cs);

            XY = ux * uy * (1 - cs);
            YY = cs + uy * uy * (1 - cs);
        }

        public Matrix2x2(string s)
        {
            string[] Parts = s.Split(new char[] { ',', ';' });
            XX = 1f; XY = 0f;
            YX = 0f; YY = 1f;
            if (Parts.Length >= 9)
            {
                if (double.TryParse(Parts[0], out double v)) XX = v;
                if (double.TryParse(Parts[1], out v)) XY = v;
                if (double.TryParse(Parts[3], out v)) YX = v;
                if (double.TryParse(Parts[4], out v)) YY = v;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"{XX}, {XY}; {YX}, {YY};";
        }

        public override bool Equals(object o)
        {
            Matrix2x2 value = (Matrix2x2)o;
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

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(Matrix2x2 left, Matrix2x2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix2x2 left, Matrix2x2 right)
        {
            return !(left == right);
        }

        public static Matrix2x2 operator +(Matrix2x2 M1, Matrix2x2 M2)
        {
            return new Matrix2x2(
                M1.XX + M2.XX, M1.XY + M2.XY,
                M1.YX + M2.YX, M1.YY + M2.YY
                );
        }

        public static Matrix2x2 operator -(Matrix2x2 M1, Matrix2x2 M2)
        {
            return new Matrix2x2(
                M1.XX - M2.XX, M1.XY - M2.XY,
                M1.YX - M2.YX, M1.YY - M2.YY
                );
        }

        public static Matrix2x2 operator -(Matrix2x2 M)
        {
            return new Matrix2x2(
                -M.XX, -M.XY,
                -M.YX, -M.YY
                );
        }

        public static Matrix2x2 operator *(double f, Matrix2x2 M)
        {
            return new Matrix2x2(
                f * M.XX, f * M.XY,
                f * M.YX, f * M.YY
                );
        }

        public static Matrix2x2 operator *(Matrix2x2 M, double f)
        {
            return new Matrix2x2(
                f * M.XX, f * M.XY,
                f * M.YX, f * M.YY
                );
        }

        public static Double2 operator *(Matrix2x2 M, Double2 p)
        {
            return new Double2(
                M.XX * p.X + M.XY * p.Y,
                M.YX * p.X + M.YY * p.Y
                );
        }

        public static Double2 operator *(Double2 p, Matrix2x2 M)
        {
            return new Double2(
                p.X * M.XX + p.Y * M.YX,
                p.X * M.XY + p.Y * M.YY
                );
        }

        public static Matrix2x2 operator *(Matrix2x2 M1, Matrix2x2 M2)
        {
            return new Matrix2x2(
                M1.XX * M2.XX + M1.XY * M2.YX, M1.XX * M2.XY + M1.XY * M2.YY,
                M1.YX * M2.XX + M1.YY * M2.YX, M1.YX * M2.XY + M1.YY * M2.YY
                );
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Misc

        public Matrix2x2 Transpose()
        {
            return new Matrix2x2(
                XX, YX,
                XY, YY
                );
        }

        public Matrix2x2 Inverse()
        {
            double d = Determinant;
            return new Matrix2x2(
                YY / d, -XY / d,
                -YX / d, XX / d
                );
        }

        public Matrix2x2 Orthogonalize(Matrix2x2 defaultResult)
        {
            // Gram-Schmidtmethod
            // ToDo: Improve with Singular Value Decomposition A = UWVT, and set W = I
            Matrix2x2 result = defaultResult;
            Double2 x1 = Row1, x2 = Row2;
            Double2 y1 = x1;
            if (Double2.IsZero(y1)) return result;
            y1 = y1.Normalize();
            Double2 y2 = x2 - (x2 * y1) * y1;
            if (Double2.IsZero(y2)) return result;
            y2 = y2.Normalize();
            return new Matrix2x2(
                y1.X, y1.Y,
                y2.X, y2.Y
                );
        }

        #endregion
    }
}
