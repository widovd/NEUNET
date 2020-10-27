using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using Neulib.Extensions;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Single3x3
    {

        // ----------------------------------------------------------------------------------------
        #region Properties

        [Browsable(false)]
        public float XX { get; set; }

        [Browsable(false)]
        public float XY { get; set; }

        [Browsable(false)]
        public float XZ { get; set; }

        [Browsable(false)]
        public float YX { get; set; }

        [Browsable(false)]
        public float YY { get; set; }

        [Browsable(false)]
        public float YZ { get; set; }

        [Browsable(false)]
        public float ZX { get; set; }

        [Browsable(false)]
        public float ZY { get; set; }

        [Browsable(false)]
        public float ZZ { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Single3 Row1
        {
            get
            {
                return new Single3(XX, XY, XZ);
            }
            set
            {
                XX = value.X;
                XY = value.Y;
                XZ = value.Z;
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Single3 Row2
        {
            get
            {
                return new Single3(YX, YY, YZ);
            }
            set
            {
                YX = value.X;
                YY = value.Y;
                YZ = value.Z;
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Single3 Row3
        {
            get
            {
                return new Single3(ZX, ZY, ZZ);
            }
            set
            {
                ZX = value.X;
                ZY = value.Y;
                ZZ = value.Z;
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Single3 Col1
        {
            get
            {
                return new Single3(XX, YX, ZX);
            }
            set
            {
                XX = value.X;
                YX = value.Y;
                ZX = value.Z;
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Single3 Col2
        {
            get
            {
                return new Single3(XY, YY, ZY);
            }
            set
            {
                XY = value.X;
                YY = value.Y;
                ZY = value.Z;
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Single3 Col3
        {
            get
            {
                return new Single3(XZ, YZ, ZZ);
            }
            set
            {
                XZ = value.X;
                YZ = value.Y;
                ZZ = value.Z;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Single3x3(int option)
        {
            switch (option)
            {
                case -3:
                    XX = 0f; XY = 1f; XZ = 0f;
                    YX = 1f; YY = 0f; YZ = 0f;
                    ZX = 0f; ZY = 0f; ZZ = 1f;
                    break;
                case -2:
                    XX = 1f; XY = 0f; XZ = 0f;
                    YX = 0f; YY = 0f; YZ = 1f;
                    ZX = 0f; ZY = 1f; ZZ = 0f;
                    break;
                case -1:
                    XX = 0f; XY = 0f; XZ = 1f;
                    YX = 0f; YY = 1f; YZ = 0f;
                    ZX = 1f; ZY = 0f; ZZ = 0f;
                    break;
                case 0:
                    XX = 0f; XY = 0f; XZ = 0f;
                    YX = 0f; YY = 0f; YZ = 0f;
                    ZX = 0f; ZY = 0f; ZZ = 0f;
                    break;
                case 1:
                    XX = 1f; XY = 0f; XZ = 0f;
                    YX = 0f; YY = 1f; YZ = 0f;
                    ZX = 0f; ZY = 0f; ZZ = 1f;
                    break;
                case 2:
                    XX = 0f; XY = 1f; XZ = 0f;
                    YX = 0f; YY = 0f; YZ = 1f;
                    ZX = 1f; ZY = 0f; ZZ = 0f;
                    break;
                case 3:
                    XX = 0f; XY = 0f; XZ = 1f;
                    YX = 1f; YY = 0f; YZ = 0f;
                    ZX = 0f; ZY = 1f; ZZ = 0f;
                    break;
                default:
                    XX = 1f; XY = 0f; XZ = 0f;
                    YX = 0f; YY = 1f; YZ = 0f;
                    ZX = 0f; ZY = 0f; ZZ = 1f;
                    break;
            }
        }

        public Single3x3(
            float xx, float xy, float xz,
            float yx, float yy, float yz,
            float zx, float zy, float zz)
        {
            XX = xx; XY = xy; XZ = xz;
            YX = yx; YY = yy; YZ = yz;
            ZX = zx; ZY = zy; ZZ = zz;
        }

        public Single3x3(Single3 rotationAxis, float rotationAngle)
        {
            float a = rotationAngle.ToRadians();
            float cs = (float)Cos(a);
            float sn = (float)Sin(a);

            float ux = rotationAxis.X;
            float uy = rotationAxis.Y;
            float uz = rotationAxis.Z;

            XX = cs + ux * ux * (1 - cs);
            YX = uy * ux * (1 - cs) + uz * sn;
            ZX = uz * ux * (1 - cs) - uy * sn;

            XY = ux * uy * (1 - cs) - uz * sn;
            YY = cs + uy * uy * (1 - cs);
            ZY = uz * uy * (1 - cs) + ux * sn;

            XZ = ux * uz * (1 - cs) + uy * sn;
            YZ = uy * uz * (1 - cs) - ux * sn;
            ZZ = cs + uz * uz * (1 - cs);

        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ISerializable, IXmlDocSerializable

        private const string _XXId = "XX";
        private const string _XYId = "XY";
        private const string _XZId = "XZ";
        private const string _YXId = "YX";
        private const string _YYId = "YY";
        private const string _YZId = "YZ";
        private const string _ZXId = "ZX";
        private const string _ZYId = "ZY";
        private const string _ZZId = "ZZ";

        private Single3x3(SerializationInfo info, StreamingContext context)
        {
            XX = 0f; XY = 0f; XZ = 0f;
            YX = 0f; YY = 0f; YZ = 0f;
            ZX = 0f; ZY = 0f; ZZ = 0f;
            foreach (SerializationEntry entry in info)
            {
                switch (entry.Name)
                {
                    case _XXId:
                        XX = (float)entry.Value;
                        break;
                    case _XYId:
                        XY = (float)entry.Value;
                        break;
                    case _XZId:
                        XZ = (float)entry.Value;
                        break;
                    case _YXId:
                        YX = (float)entry.Value;
                        break;
                    case _YYId:
                        YY = (float)entry.Value;
                        break;
                    case _YZId:
                        YZ = (float)entry.Value;
                        break;
                    case _ZXId:
                        ZX = (float)entry.Value;
                        break;
                    case _ZYId:
                        ZY = (float)entry.Value;
                        break;
                    case _ZZId:
                        ZZ = (float)entry.Value;
                        break;
                }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(_XXId, XX);
            info.AddValue(_XYId, XY);
            info.AddValue(_XZId, XZ);
            info.AddValue(_YXId, YX);
            info.AddValue(_YYId, YY);
            info.AddValue(_YZId, YZ);
            info.AddValue(_ZXId, ZX);
            info.AddValue(_ZYId, ZY);
            info.AddValue(_ZZId, ZZ);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Single3x3 value = (Single3x3)o;
            if (IsNaN(this) && IsNaN(value)) return true;
            if (XX != value.XX) return false;
            if (XY != value.XY) return false;
            if (XZ != value.XZ) return false;
            if (YX != value.YX) return false;
            if (YY != value.YY) return false;
            if (YZ != value.YZ) return false;
            if (ZX != value.ZX) return false;
            if (ZY != value.ZY) return false;
            if (ZZ != value.ZZ) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + XX.GetHashCode();
                h = h * 23 + XY.GetHashCode();
                h = h * 23 + XZ.GetHashCode();
                h = h * 23 + YX.GetHashCode();
                h = h * 23 + YY.GetHashCode();
                h = h * 23 + YZ.GetHashCode();
                h = h * 23 + ZX.GetHashCode();
                h = h * 23 + ZY.GetHashCode();
                h = h * 23 + ZZ.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{XX}, {XY}, {XZ}, {YX}, {YY}, {YZ}, {ZX}, {ZY}, {ZZ}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(Single3x3 left, Single3x3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Single3x3 left, Single3x3 right)
        {
            return !(left == right);
        }

        public static Single3x3 operator +(Single3x3 M1, Single3x3 M2)
        {
            return new Single3x3(
                M1.XX + M2.XX, M1.XY + M2.XY, M1.XZ + M2.XZ,
                M1.YX + M2.YX, M1.YY + M2.YY, M1.YZ + M2.YZ,
                M1.ZX + M2.ZX, M1.ZY + M2.ZY, M1.ZZ + M2.ZZ
                );
        }

        public static Single3x3 operator -(Single3x3 M1, Single3x3 M2)
        {
            return new Single3x3(
                M1.XX - M2.XX, M1.XY - M2.XY, M1.XZ - M2.XZ,
                M1.YX - M2.YX, M1.YY - M2.YY, M1.YZ - M2.YZ,
                M1.ZX - M2.ZX, M1.ZY - M2.ZY, M1.ZZ - M2.ZZ
                );
        }

        public static Single3x3 operator -(Single3x3 M)
        {
            return new Single3x3(
                -M.XX, -M.XY, -M.XZ,
                -M.YX, -M.YY, -M.YZ,
                -M.ZX, -M.ZY, -M.ZZ
                );
        }

        public static Single3x3 operator *(float f, Single3x3 M)
        {
            return new Single3x3(
                f * M.XX, f * M.XY, f * M.XZ,
                f * M.YX, f * M.YY, f * M.YZ,
                f * M.ZX, f * M.ZY, f * M.ZZ
                );
        }

        public static Single3x3 operator *(Single3x3 M, float f)
        {
            return new Single3x3(
                f * M.XX, f * M.XY, f * M.XZ,
                f * M.YX, f * M.YY, f * M.YZ,
                f * M.ZX, f * M.ZY, f * M.ZZ
                );
        }

        public static Single3 operator *(Single3x3 M, Single3 p)
        {
            return new Single3(
                M.XX * p.X + M.XY * p.Y + M.XZ * p.Z,
                M.YX * p.X + M.YY * p.Y + M.YZ * p.Z,
                M.ZX * p.X + M.ZY * p.Y + M.ZZ * p.Z
                );
        }

        public static Single3 operator *(Single3 p, Single3x3 M)
        {
            return new Single3(
                p.X * M.XX + p.Y * M.YX + p.Z * M.ZX,
                p.X * M.XY + p.Y * M.YY + p.Z * M.ZY,
                p.X * M.XZ + p.Y * M.YZ + p.Z * M.ZZ
                );
        }

        public static Single3x3 operator *(Single3x3 M1, Single3x3 M2)
        {
            return new Single3x3(
                M1.XX * M2.XX + M1.XY * M2.YX + M1.XZ * M2.ZX, M1.XX * M2.XY + M1.XY * M2.YY + M1.XZ * M2.ZY, M1.XX * M2.XZ + M1.XY * M2.YZ + M1.XZ * M2.ZZ,
                M1.YX * M2.XX + M1.YY * M2.YX + M1.YZ * M2.ZX, M1.YX * M2.XY + M1.YY * M2.YY + M1.YZ * M2.ZY, M1.YX * M2.XZ + M1.YY * M2.YZ + M1.YZ * M2.ZZ,
                M1.ZX * M2.XX + M1.ZY * M2.YX + M1.ZZ * M2.ZX, M1.ZX * M2.XY + M1.ZY * M2.YY + M1.ZZ * M2.ZY, M1.ZX * M2.XZ + M1.ZY * M2.YZ + M1.ZZ * M2.ZZ
                );
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Misc

        public Single3x3 Transpose()
        {
            return new Single3x3(
                XX, YX, ZX,
                XY, YY, ZY,
                XZ, YZ, ZZ
                );
        }

        public Single3x3 Inverse()
        {
            if (IsNaN(this)) return NaN;
            MatrixNxN M1 = new MatrixNxN(3);
            M1.Values[0, 0] = XX; M1.Values[0, 1] = XY; M1.Values[0, 2] = XZ;
            M1.Values[1, 0] = YX; M1.Values[1, 1] = YY; M1.Values[1, 2] = YZ;
            M1.Values[2, 0] = ZX; M1.Values[2, 1] = ZY; M1.Values[2, 2] = ZZ;
            try
            {
                MatrixNxN M2 = M1.Inverse();
                return new Single3x3(
                    (float)M2.Values[0, 0], (float)M2.Values[0, 1], (float)M2.Values[0, 2],
                    (float)M2.Values[1, 0], (float)M2.Values[1, 1], (float)M2.Values[1, 2],
                    (float)M2.Values[2, 0], (float)M2.Values[2, 1], (float)M2.Values[2, 2]
                    );
            }
            catch (SingularMatrixException)
            {
                return NaN;
            }
        }

        public Single3x3 Orthogonalize(Single3x3 defaultResult)
        {
            // Gram-Schmidtmethod
            // ToDo: Improve with Singular Value Decomposition A = UWVT, and set W = I
            Single3x3 result = defaultResult;
            Single3 x1 = Row1, x2 = Row2, x3 = Row3;
            Single3 y1 = x1;
            if (y1.LengthSqr() == 0f) return result;
            y1 = y1.Normalize();
            Single3 y2 = x2 - (x2 * y1) * y1;
            if (y2.LengthSqr() == 0f) return result;
            y2 = y2.Normalize();
            Single3 y3 = x3 - (x3 * y1) * y1 - (x3 * y2) * y2;
            if (y3.LengthSqr() == 0f) return result;
            y3 = y3.Normalize();
            return new Single3x3(
                y1.X, y1.Y, y1.Z,
                y2.X, y2.Y, y2.Z,
                y3.X, y3.Y, y3.Z
                );
        }

        public Single3x3 Orthogonalize()
        {
            return Orthogonalize(this);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Single3x3 value)
        {
            return
                float.IsNaN(value.XX) || float.IsNaN(value.XY) || float.IsNaN(value.XZ) ||
                float.IsNaN(value.YX) || float.IsNaN(value.YY) || float.IsNaN(value.YZ) ||
                float.IsNaN(value.ZX) || float.IsNaN(value.ZY) || float.IsNaN(value.ZZ);
        }

        public static Single3x3 NaN
        {
            get
            {
                return new Single3x3(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
            }
        }

        public static Single3x3 RotationXMatrix(float tiltAngle)
        {
            float a = tiltAngle.ToRadians();
            float cs = (float)Cos(a);
            float sn = (float)Sin(a);
            return new Single3x3(1f, 0f, 0f, 0f, cs, sn, 0f, -sn, cs);
        }

        public static Single3x3 RotationYMatrix(float rotationAngle)
        {
            float a = rotationAngle.ToRadians();
            float cs = (float)Cos(a);
            float sn = (float)Sin(a);
            return new Single3x3(cs, 0f, sn, 0f, 1f, 0, -sn, 0f, cs);
        }

        public static Single3x3 RotationZMatrix(float orientationAngle)
        {
            float a = orientationAngle.ToRadians();
            float cs = (float)Cos(a);
            float sn = (float)Sin(a);
            return new Single3x3(
                cs, sn, 0f,
                -sn, cs, 0f,
                0f, 0f, 1f);
        }

        public static Single3x3 RotationMatrix(float orientationAngle, float rotationAngle, float tiltAngle)
        {
            Single3x3 matrix = RotationXMatrix(tiltAngle);
            matrix = RotationYMatrix(rotationAngle) * matrix;
            matrix = RotationZMatrix(orientationAngle) * matrix;
            return matrix;
        }

        #endregion
    }

}
