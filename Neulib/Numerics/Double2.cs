using System.IO;
using System.Xml;
using Neulib.Exceptions;
using Neulib.Serializers;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Double2 : IBinarySerializable, IXmlDocSerializable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public double X;

        public double Y;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Double2(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IBinarySerializable

        public void ReadFromStream(Stream stream, BinarySerializer serializer)
        {
            X = stream.ReadDouble();
            Y = stream.ReadDouble();
        }

        public void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            stream.WriteDouble(X);
            stream.WriteDouble(Y);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IXmlDocSerializable

        private const string _XId = "X";
        private const string _YId = "Y";

        public void ReadFromXml(XmlElement element, XmlDocSerializer serializer)
        {
            X = element.ReadDouble(_XId, X);
            Y = element.ReadDouble(_YId, Y);
        }

        public void WriteToXml(XmlElement element, XmlDocSerializer serializer)
        {
            element.WriteDouble(_XId, X);
            element.WriteDouble(_YId, Y);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Double2 value = (Double2)o;
            if (IsNaN(this) && IsNaN(value)) return true;
            if (X != value.X) return false;
            if (Y != value.Y) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + X.GetHashCode();
                h = h * 23 + Y.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(Double2 left, Double2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Double2 left, Double2 right)
        {
            return !(left == right);
        }

        public static Double2 operator +(Double2 p1, Double2 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Double2(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Double2 operator -(Double2 p1, Double2 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Double2(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Double2 operator -(Double2 p)
        {
            if (IsNaN(p)) return NaN;
            return new Double2(-p.X, -p.Y);
        }

        public static Double2 operator *(double f, Double2 p)
        {
            if (IsNaN(p) || double.IsNaN(f)) return NaN;
            return new Double2(f * p.X, f * p.Y);
        }

        public static Double2 operator *(Double2 p, double f)
        {
            if (IsNaN(p) || double.IsNaN(f)) return NaN;
            return new Double2(f * p.X, f * p.Y);
        }

        public static Double2 operator /(Double2 p, double f)
        {
            if (IsNaN(p) || double.IsNaN(f)) return NaN;
            return new Double2(p.X / f, p.Y / f);
        }

        // Scalar product
        public static double operator *(Double2 p1, Double2 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return double.NaN;
            return p1.X * p2.X + p1.Y * p2.Y;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Methods

        public double LengthSqr()
        {
            return X * X + Y * Y;
        }

        public double Length()
        {
            return Sqrt(LengthSqr());
        }

        public Double2 Normalize()
        {
            if (IsNaN(this)) return this;
            double length = Length();
            if (length == 0d) // Can not normalize a vector with length 0.
                throw new InvalidValueException(nameof(length), length, 783898);
            return this / length;
        }

        public Double2 Normalize(Double2 direction)
        {
            Double2 value = Normalize();
            if (direction * value < 0) value = -value;
            return value;
        }

        public Single2 ToSingle2()
        {
            return new Single2((float)X, (float)Y);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsZero(Double2 value)
        {
            return value.X == 0d && value.Y == 0d;
        }
        
        public static bool IsNaN(Double2 value)
        {
            return double.IsNaN(value.X) || double.IsNaN(value.Y);
        }

        public static Double2 NaN
        {
            get { return new Double2(double.NaN, double.NaN); }
        }

        #endregion
    }

}
