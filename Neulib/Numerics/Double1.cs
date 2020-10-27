using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;
using Neulib.Exceptions;
using Neulib.Serializers;
using System.ComponentModel;
using static System.Math;

namespace Neulib.Numerics
{
    public struct Double1 : IBinarySerializable, IXmlDocSerializable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public double X;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Double1(double x)
        {
            X = x;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IXmlDocSerializable

        private const string _XId = "X";

        public void ReadFromXml(XmlElement element, XmlDocSerializer serializer)
        {
            X = element.ReadDouble(_XId, X);
        }

        public void WriteToXml(XmlElement element, XmlDocSerializer serializer)
        {
            element.WriteDouble(_XId, X);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IBinarySerializable

        public void ReadFromStream(Stream stream, BinarySerializer serializer)
        {
            X = stream.ReadDouble();
        }

        public void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            stream.WriteDouble(X);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return X.ToString();
        }

        public override bool Equals(object o)
        {
            Double1 value = (Double1)o;
            if (IsNaN(this) && IsNaN(value)) return true;
            if (X != value.X) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + X.GetHashCode();
            }
            return h;
        }

        public static bool operator ==(Double1 left, Double1 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Double1 left, Double1 right)
        {
            return !(left == right);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Double1 value)
        {
            return double.IsNaN(value.X);
        }

        public static Double1 NaN
        {
            get => new Double1(double.NaN);
        }

        #endregion
    }
}
