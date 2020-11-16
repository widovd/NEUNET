using System.IO;
using System.Xml;
using Neulib.Exceptions;
using Neulib.Serializers;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Double3 : IBinarySerializable, IXmlDocSerializable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public double X;

        public double Y;

        public double Z;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Double3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Double3(int option)
        {
            switch (option)
            {
                case 1:
                    X = 1d; Y = 0d; Z = 0d;
                    break;
                case 2:
                    X = 0d; Y = 1d; Z = 0d;
                    break;
                case 3:
                    X = 0d; Y = 0d; Z = 1d;
                    break;
                default:
                    X = 0d; Y = 0d; Z = 0d;
                    break;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IBinarySerializable

        public void ReadFromStream(Stream stream, BinarySerializer serializer)
        {
            X = stream.ReadDouble();
            Y = stream.ReadDouble();
            Z = stream.ReadDouble();
        }

        public void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            stream.WriteDouble(X);
            stream.WriteDouble(Y);
            stream.WriteDouble(Z);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IXmlDocSerializable

        private const string _XId = "X";
        private const string _YId = "Y";
        private const string _ZId = "Z";

        public void ReadFromXml(XmlElement element, XmlDocSerializer serializer)
        {
            X = element.ReadDouble(_XId, X);
            Y = element.ReadDouble(_YId, Y);
            Z = element.ReadDouble(_ZId, Z);
        }

        public void WriteToXml(XmlElement element, XmlDocSerializer serializer)
        {
            element.WriteDouble(_XId, X);
            element.WriteDouble(_YId, Y);
            element.WriteDouble(_ZId, Z);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Double3 value = (Double3)o;
            if (IsNaN(this) && IsNaN(value)) return true;
            if (X != value.X) return false;
            if (Y != value.Y) return false;
            if (Z != value.Z) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + X.GetHashCode();
                h = h * 23 + Y.GetHashCode();
                h = h * 23 + Z.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(Double3 left, Double3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Double3 left, Double3 right)
        {
            return !(left == right);
        }

        public static Double3 operator +(Double3 p1, Double3 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Double3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Double3 operator -(Double3 p1, Double3 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Double3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Double3 operator -(Double3 p)
        {
            if (IsNaN(p)) return NaN;
            return new Double3(-p.X, -p.Y, -p.Z);
        }

        public static Double3 operator *(double f, Double3 p)
        {
            if (IsNaN(p) || double.IsNaN(f)) return NaN;
            return new Double3(f * p.X, f * p.Y, f * p.Z);
        }

        public static Double3 operator *(Double3 p, double f)
        {
            if (IsNaN(p) || double.IsNaN(f)) return NaN;
            return new Double3(f * p.X, f * p.Y, f * p.Z);
        }

        public static Double3 operator /(Double3 p, double f)
        {
            if (IsNaN(p) || double.IsNaN(f)) return NaN;
            return new Double3(p.X / f, p.Y / f, p.Z / f);
        }

        // Scalar product
        public static double operator *(Double3 p1, Double3 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return double.NaN;
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }

        // Cross product
        public static Double3 operator %(Double3 p1, Double3 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Double3(p1.Y * p2.Z - p1.Z * p2.Y, p1.Z * p2.X - p1.X * p2.Z, p1.X * p2.Y - p1.Y * p2.X);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Methods

        public double LengthSqr()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Length()
        {
            return Sqrt(LengthSqr());
        }

        public Double3 Normalize()
        {
            if (IsNaN(this)) return this;
            double length = Length();
            if (length == 0d) // Can not normalize a vector with length 0.
                throw new InvalidValueException(nameof(length), length, 468007);
            return this / length;
        }

        public Double3 Normalize(Double3 direction)
        {
            Double3 value = Normalize();
            if (direction * value < 0) value = -value;
            return value;
        }

        public Single3 ToSingle3()
        {
            return new Single3((float)X, (float)Y, (float)Z);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Double3 value)
        {
            return double.IsNaN(value.X) || double.IsNaN(value.Y) || double.IsNaN(value.Z);
        }

        public static Double3 NaN
        {
            get { return new Double3(double.NaN, double.NaN, double.NaN); }
        }

        #endregion
    }

}
