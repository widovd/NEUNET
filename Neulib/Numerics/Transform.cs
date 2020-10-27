using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Xml;
using Neulib.Serializers;

namespace Neulib.Numerics
{

    public class Transform : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private Double3 _origin;
        [
            DisplayName("Origin"),
            Description("The global coordinates of the origin of the local axes."),
        ]
        public Double3 Origin
        {
            get { return _origin; }
            set
            {
                if (Equals(value, Origin)) return;
                _origin = value;
            }
        }

        private Double3x3 _rotMat;
        /// <summary>
        /// local = R * global; global = R^-1 * local
        /// </summary>
        [
            DisplayName("Rotation matrix"),
            Description("The rotation matrix for the local axes."),
        ]
        public Double3x3 RotMat
        {
            get { return _rotMat; }
            set
            {
                if (Equals(value, RotMat)) return;
                _rotMat = value;
                InvMat = RotMat.Inverse();
            }
        }

        [Browsable(false)]
        public Double3x3 InvMat { get; private set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Transform()
        {
            Origin = new Double3(0d, 0d, 0d);
            RotMat = new Double3x3(1); // unity matrix
        }

        public Transform(Double3 origin, Double3x3 rotMat)
        {
            Origin = origin;
            RotMat = rotMat;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            if (!base.Equals(o)) return false;
            if (!(o is Transform value)) return false;
            if (!Equals(Origin, value.Origin)) return false;
            if (!Equals(RotMat, value.RotMat)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = base.GetHashCode();
            unchecked
            {
                h = h * 23 + Origin.GetHashCode();
                h = h * 23 + RotMat.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{Origin}, {RotMat}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        private const string _OriginId = "Origin";
        private const string _RotMatId = "RotMat";

        public override void ReadFromXml(XmlElement element, XmlDocSerializer serializer)
        {
            base.ReadFromXml(element, serializer);
            Origin = (Double3)element.ReadValue(_OriginId, serializer);
            RotMat = (Double3x3)element.ReadValue(_RotMatId, serializer);
        }

        public override void WriteToXml(XmlElement element, XmlDocSerializer serializer)
        {
            base.WriteToXml(element, serializer);
            element.WriteValue(_OriginId, Origin, serializer);
            element.WriteValue(_RotMatId, RotMat, serializer);
        }

        protected Transform(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            Origin = (Double3)stream.ReadValue(serializer);
            RotMat = (Double3x3)stream.ReadValue(serializer);
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteValue(Origin, serializer);
            stream.WriteValue(RotMat, serializer);
        }

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            if (!(o is Transform value)) return;
            Origin = value.Origin;
            RotMat = value.RotMat;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static Transform operator *(Transform T1, Transform T2)
        {
            return new Transform()
            {
                Origin = T1.Origin + T2.Origin,
                RotMat = T1.RotMat * T2.RotMat,
            };
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Transform

        public Double3 GlobalPosToLocal(Double3 p)
        {
            return RotMat * (p - Origin);
        }

        public Double3 LocalPosToGlobal(Double3 p)
        {
            return InvMat * p + Origin;
        }

        public Double3 GlobalDirToLocal(Double3 k)
        {
            return RotMat * k;
        }

        public Double3 LocalDirToGlobal(Double3 k)
        {
            return InvMat * k;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Transform value)
        {
            return Double3.IsNaN(value.Origin) || Double3x3.IsNaN(value.RotMat) || Double3x3.IsNaN(value.InvMat);
        }

        public static Transform NaN()
        {
            return new Transform(Double3.NaN, Double3x3.NaN);
        }

        #endregion
    }
}
