using System.IO;
using System.Xml;
using Neulib.Exceptions;
using Neulib.Serializers;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Int2 : IBinarySerializable, IXmlDocSerializable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public int I;

        public int J;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Int2(int i, int j)
        {
            I = i;
            J = j;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IBinarySerializable

        public void ReadFromStream(Stream stream, BinarySerializer serializer)
        {
            I = stream.ReadInt();
            J = stream.ReadInt();
        }

        public void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            stream.WriteInt(I);
            stream.WriteInt(J);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IXmlDocSerializable

        private const string _IId = "I";
        private const string _JId = "J";

        public void ReadFromXml(XmlElement element, XmlDocSerializer serializer)
        {
            I = element.ReadInt(_IId, I);
            J = element.ReadInt(_JId, J);
        }

        public void WriteToXml(XmlElement element, XmlDocSerializer serializer)
        {
            element.WriteInt(_IId, I);
            element.WriteInt(_JId, J);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Int2 value = (Int2)o;
            if (I != value.I) return false;
            if (J != value.J) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + I.GetHashCode();
                h = h * 23 + J.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{I}, {J}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(Int2 left, Int2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Int2 left, Int2 right)
        {
            return !(left == right);
        }

        public static Int2 operator +(Int2 p1, Int2 p2)
        {
            return new Int2(p1.I + p2.I, p1.J + p2.J);
        }

        public static Int2 operator -(Int2 p1, Int2 p2)
        {
            return new Int2(p1.I - p2.I, p1.J - p2.J);
        }

        public static Int2 operator -(Int2 p)
        {
            return new Int2(-p.I, -p.J);
        }

        public static Int2 operator *(int f, Int2 p)
        {
            return new Int2(f * p.I, f * p.J);
        }

        public static Int2 operator *(Int2 p, int f)
        {
            return new Int2(f * p.I, f * p.J);
        }

        public static Int2 operator /(Int2 p, int f)
        {
            return new Int2(p.I / f, p.J / f);
        }

        public static Int2 operator %(Int2 p, int f)
        {
            return new Int2(p.I % f, p.J % f);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Methods

        public Single2 ToSingle2()
        {
            return new Single2((float)I, (float)J);
        }

        public Double2 ToDouble2()
        {
            return new Double2((double)I, (double)J);
        }

        #endregion
    }
}
