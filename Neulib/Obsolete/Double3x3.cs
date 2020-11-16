using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using Neulib.Extensions;
using Neulib.Exceptions;
using Neulib.Serializers;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Double3x3 : IBinarySerializable, IXmlDocSerializable
    {

        // ----------------------------------------------------------------------------------------
        #region Properties

        [Browsable(false)]
        public double XX { get; set; }

        [Browsable(false)]
        public double XY { get; set; }

        [Browsable(false)]
        public double XZ { get; set; }

        [Browsable(false)]
        public double YX { get; set; }

        [Browsable(false)]
        public double YY { get; set; }

        [Browsable(false)]
        public double YZ { get; set; }

        [Browsable(false)]
        public double ZX { get; set; }

        [Browsable(false)]
        public double ZY { get; set; }

        [Browsable(false)]
        public double ZZ { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Double3 Row1
        {
            get
            {
                return new Double3(XX, XY, XZ);
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
        public Double3 Row2
        {
            get
            {
                return new Double3(YX, YY, YZ);
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
        public Double3 Row3
        {
            get
            {
                return new Double3(ZX, ZY, ZZ);
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
        public Double3 Col1
        {
            get
            {
                return new Double3(XX, YX, ZX);
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
        public Double3 Col2
        {
            get
            {
                return new Double3(XY, YY, ZY);
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
        public Double3 Col3
        {
            get
            {
                return new Double3(XZ, YZ, ZZ);
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

        public Double3x3(int option)
        {
            switch (option)
            {
                case -3:
                    XX = 0d; XY = 1d; XZ = 0d;
                    YX = 1d; YY = 0d; YZ = 0d;
                    ZX = 0d; ZY = 0d; ZZ = 1d;
                    break;
                case -2:
                    XX = 1d; XY = 0d; XZ = 0d;
                    YX = 0d; YY = 0d; YZ = 1d;
                    ZX = 0d; ZY = 1d; ZZ = 0d;
                    break;
                case -1:
                    XX = 0d; XY = 0d; XZ = 1d;
                    YX = 0d; YY = 1d; YZ = 0d;
                    ZX = 1d; ZY = 0d; ZZ = 0d;
                    break;
                case 0:
                    XX = 0d; XY = 0d; XZ = 0d;
                    YX = 0d; YY = 0d; YZ = 0d;
                    ZX = 0d; ZY = 0d; ZZ = 0d;
                    break;
                case 1:
                    XX = 1d; XY = 0d; XZ = 0d;
                    YX = 0d; YY = 1d; YZ = 0d;
                    ZX = 0d; ZY = 0d; ZZ = 1d;
                    break;
                case 2:
                    XX = 0d; XY = 1d; XZ = 0d;
                    YX = 0d; YY = 0d; YZ = 1d;
                    ZX = 1d; ZY = 0d; ZZ = 0d;
                    break;
                case 3:
                    XX = 0d; XY = 0d; XZ = 1d;
                    YX = 1d; YY = 0d; YZ = 0d;
                    ZX = 0d; ZY = 1d; ZZ = 0d;
                    break;
                default:
                    XX = 1d; XY = 0d; XZ = 0d;
                    YX = 0d; YY = 1d; YZ = 0d;
                    ZX = 0d; ZY = 0d; ZZ = 1d;
                    break;
            }
        }

        public Double3x3(
            double xx, double xy, double xz,
            double yx, double yy, double yz,
            double zx, double zy, double zz)
        {
            XX = xx; XY = xy; XZ = xz;
            YX = yx; YY = yy; YZ = yz;
            ZX = zx; ZY = zy; ZZ = zz;
        }

        public Double3x3(Double3 rotationAxis, double rotationAngle)
        {
            double a = rotationAngle.ToRadians();
            double cs = Cos(a);
            double sn = Sin(a);

            double ux = rotationAxis.X;
            double uy = rotationAxis.Y;
            double uz = rotationAxis.Z;

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

        private Double3x3(SerializationInfo info, StreamingContext context)
        {
            XX = 0d; XY = 0d; XZ = 0d;
            YX = 0d; YY = 0d; YZ = 0d;
            ZX = 0d; ZY = 0d; ZZ = 0d;
            foreach (SerializationEntry entry in info)
            {
                switch (entry.Name)
                {
                    case _XXId:
                        XX = (double)entry.Value;
                        break;
                    case _XYId:
                        XY = (double)entry.Value;
                        break;
                    case _XZId:
                        XZ = (double)entry.Value;
                        break;
                    case _YXId:
                        YX = (double)entry.Value;
                        break;
                    case _YYId:
                        YY = (double)entry.Value;
                        break;
                    case _YZId:
                        YZ = (double)entry.Value;
                        break;
                    case _ZXId:
                        ZX = (double)entry.Value;
                        break;
                    case _ZYId:
                        ZY = (double)entry.Value;
                        break;
                    case _ZZId:
                        ZZ = (double)entry.Value;
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
        #region IBinarySerializable

        public Double3x3(Stream stream, BinarySerializer serializer)
        {
            XX = stream.ReadDouble();
            XY = stream.ReadDouble();
            XZ = stream.ReadDouble();
            YX = stream.ReadDouble();
            YY = stream.ReadDouble();
            YZ = stream.ReadDouble();
            ZX = stream.ReadDouble();
            ZY = stream.ReadDouble();
            ZZ = stream.ReadDouble();
        }

        public void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            stream.WriteDouble(XX);
            stream.WriteDouble(XY);
            stream.WriteDouble(XZ);
            stream.WriteDouble(YX);
            stream.WriteDouble(YY);
            stream.WriteDouble(YZ);
            stream.WriteDouble(ZX);
            stream.WriteDouble(ZY);
            stream.WriteDouble(ZZ);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IXmlDocSerializable

        public void ReadFromXml(XmlElement element, XmlDocSerializer serializer)
        {
            XX = element.ReadDouble(_XXId, XX);
            XY = element.ReadDouble(_XYId, XY);
            XZ = element.ReadDouble(_XZId, XZ);

            YX = element.ReadDouble(_YXId, YX);
            YY = element.ReadDouble(_YYId, YY);
            YZ = element.ReadDouble(_YZId, YZ);

            ZX = element.ReadDouble(_ZXId, ZX);
            ZY = element.ReadDouble(_ZYId, ZY);
            ZZ = element.ReadDouble(_ZZId, ZZ);
        }

        public void WriteToXml(XmlElement element, XmlDocSerializer serializer)
        {
            element.WriteDouble(_XXId, XX);
            element.WriteDouble(_XYId, XY);
            element.WriteDouble(_XZId, XZ);

            element.WriteDouble(_YXId, YX);
            element.WriteDouble(_YYId, YY);
            element.WriteDouble(_YZId, YZ);

            element.WriteDouble(_ZXId, ZX);
            element.WriteDouble(_ZYId, ZY);
            element.WriteDouble(_ZZId, ZZ);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Double3x3 value = (Double3x3)o;
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

        public static bool operator ==(Double3x3 left, Double3x3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Double3x3 left, Double3x3 right)
        {
            return !(left == right);
        }

        public static Double3x3 operator +(Double3x3 M1, Double3x3 M2)
        {
            return new Double3x3(
                M1.XX + M2.XX, M1.XY + M2.XY, M1.XZ + M2.XZ,
                M1.YX + M2.YX, M1.YY + M2.YY, M1.YZ + M2.YZ,
                M1.ZX + M2.ZX, M1.ZY + M2.ZY, M1.ZZ + M2.ZZ
                );
        }

        public static Double3x3 operator -(Double3x3 M1, Double3x3 M2)
        {
            return new Double3x3(
                M1.XX - M2.XX, M1.XY - M2.XY, M1.XZ - M2.XZ,
                M1.YX - M2.YX, M1.YY - M2.YY, M1.YZ - M2.YZ,
                M1.ZX - M2.ZX, M1.ZY - M2.ZY, M1.ZZ - M2.ZZ
                );
        }

        public static Double3x3 operator -(Double3x3 M)
        {
            return new Double3x3(
                -M.XX, -M.XY, -M.XZ,
                -M.YX, -M.YY, -M.YZ,
                -M.ZX, -M.ZY, -M.ZZ
                );
        }

        public static Double3x3 operator *(double f, Double3x3 M)
        {
            return new Double3x3(
                f * M.XX, f * M.XY, f * M.XZ,
                f * M.YX, f * M.YY, f * M.YZ,
                f * M.ZX, f * M.ZY, f * M.ZZ
                );
        }

        public static Double3x3 operator *(Double3x3 M, double f)
        {
            return new Double3x3(
                f * M.XX, f * M.XY, f * M.XZ,
                f * M.YX, f * M.YY, f * M.YZ,
                f * M.ZX, f * M.ZY, f * M.ZZ
                );
        }

        public static Double3 operator *(Double3x3 M, Double3 p)
        {
            return new Double3(
                M.XX * p.X + M.XY * p.Y + M.XZ * p.Z,
                M.YX * p.X + M.YY * p.Y + M.YZ * p.Z,
                M.ZX * p.X + M.ZY * p.Y + M.ZZ * p.Z
                );
        }

        public static Double3 operator *(Double3 p, Double3x3 M)
        {
            return new Double3(
                p.X * M.XX + p.Y * M.YX + p.Z * M.ZX,
                p.X * M.XY + p.Y * M.YY + p.Z * M.ZY,
                p.X * M.XZ + p.Y * M.YZ + p.Z * M.ZZ
                );
        }

        public static Double3x3 operator *(Double3x3 M1, Double3x3 M2)
        {
            return new Double3x3(
                M1.XX * M2.XX + M1.XY * M2.YX + M1.XZ * M2.ZX, M1.XX * M2.XY + M1.XY * M2.YY + M1.XZ * M2.ZY, M1.XX * M2.XZ + M1.XY * M2.YZ + M1.XZ * M2.ZZ,
                M1.YX * M2.XX + M1.YY * M2.YX + M1.YZ * M2.ZX, M1.YX * M2.XY + M1.YY * M2.YY + M1.YZ * M2.ZY, M1.YX * M2.XZ + M1.YY * M2.YZ + M1.YZ * M2.ZZ,
                M1.ZX * M2.XX + M1.ZY * M2.YX + M1.ZZ * M2.ZX, M1.ZX * M2.XY + M1.ZY * M2.YY + M1.ZZ * M2.ZY, M1.ZX * M2.XZ + M1.ZY * M2.YZ + M1.ZZ * M2.ZZ
                );
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Misc

        public Double3x3 Transpose()
        {
            return new Double3x3(
                XX, YX, ZX,
                XY, YY, ZY,
                XZ, YZ, ZZ
                );
        }

        public Double3x3 Inverse()
        {
            if (IsNaN(this)) return NaN;
            MatrixNxN M1 = new MatrixNxN(3);
            M1.Values[0, 0] = XX; M1.Values[0, 1] = XY; M1.Values[0, 2] = XZ;
            M1.Values[1, 0] = YX; M1.Values[1, 1] = YY; M1.Values[1, 2] = YZ;
            M1.Values[2, 0] = ZX; M1.Values[2, 1] = ZY; M1.Values[2, 2] = ZZ;
            try
            {
                MatrixNxN M2 = M1.Inverse();
                return new Double3x3(
                    M2.Values[0, 0], M2.Values[0, 1], M2.Values[0, 2],
                    M2.Values[1, 0], M2.Values[1, 1], M2.Values[1, 2],
                    M2.Values[2, 0], M2.Values[2, 1], M2.Values[2, 2]
                    );
            }
            catch (SingularMatrixException)
            {
                return NaN;
            }
        }

        public Double3x3 Orthogonalize(Double3x3 defaultResult)
        {
            // Gram-Schmidtmethod
            // ToDo: Improve with Singular Value Decomposition A = UWVT, and set W = I
            Double3x3 result = defaultResult;
            Double3 x1 = Row1, x2 = Row2, x3 = Row3;
            Double3 y1 = x1;
            if (y1.LengthSqr() == 0d) return result;
            y1 = y1.Normalize();
            Double3 y2 = x2 - (x2 * y1) * y1;
            if (y2.LengthSqr() == 0d) return result;
            y2 = y2.Normalize();
            Double3 y3 = x3 - (x3 * y1) * y1 - (x3 * y2) * y2;
            if (y3.LengthSqr() == 0d) return result;
            y3 = y3.Normalize();
            return new Double3x3(
                y1.X, y1.Y, y1.Z,
                y2.X, y2.Y, y2.Z,
                y3.X, y3.Y, y3.Z
                );
        }

        public Double3x3 Orthogonalize()
        {
            return Orthogonalize(this);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Double3x3 value)
        {
            return
                double.IsNaN(value.XX) || double.IsNaN(value.XY) || double.IsNaN(value.XZ) ||
                double.IsNaN(value.YX) || double.IsNaN(value.YY) || double.IsNaN(value.YZ) ||
                double.IsNaN(value.ZX) || double.IsNaN(value.ZY) || double.IsNaN(value.ZZ);
        }

        public static Double3x3 NaN
        {
            get
            {
                return new Double3x3(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);
            }
        }

        public static Double3x3 RotationXMatrix(double tiltAngle)
        {
            double a = tiltAngle.ToRadians();
            double cs = Cos(a);
            double sn = Sin(a);
            return new Double3x3(1d, 0d, 0d, 0d, cs, sn, 0d, -sn, cs);
        }

        public static Double3x3 RotationYMatrix(double rotationAngle)
        {
            double a = rotationAngle.ToRadians();
            double cs = Cos(a);
            double sn = Sin(a);
            return new Double3x3(cs, 0d, sn, 0d, 1d, 0, -sn, 0d, cs);
        }

        public static Double3x3 RotationZMatrix(double orientationAngle)
        {
            double a = orientationAngle.ToRadians();
            double cs = Cos(a);
            double sn = Sin(a);
            return new Double3x3(
                cs, sn, 0d,
                -sn, cs, 0d,
                0d, 0d, 1d);
        }

        public static Double3x3 RotationMatrix(double orientationAngle, double rotationAngle, double tiltAngle)
        {
            Double3x3 matrix = RotationXMatrix(tiltAngle);
            matrix = RotationYMatrix(rotationAngle) * matrix;
            matrix = RotationZMatrix(orientationAngle) * matrix;
            return matrix;
        }

        #endregion
    }

}
