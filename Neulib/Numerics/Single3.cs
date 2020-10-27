using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Xml;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Single3
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float X;

        public float Y;

        public float Z;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Single3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Single3 value = (Single3)o;
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

        public static bool operator ==(Single3 left, Single3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Single3 left, Single3 right)
        {
            return !(left == right);
        }

        public static Single3 operator +(Single3 p1, Single3 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Single3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Single3 operator -(Single3 p1, Single3 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Single3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Single3 operator -(Single3 p)
        {
            if (IsNaN(p)) return NaN;
            return new Single3(-p.X, -p.Y, -p.Z);
        }

        public static Single3 operator *(float f, Single3 p)
        {
            if (IsNaN(p) || float.IsNaN(f)) return NaN;
            return new Single3(f * p.X, f * p.Y, f * p.Z);
        }

        public static Single3 operator *(Single3 p, float f)
        {
            if (IsNaN(p) || float.IsNaN(f)) return NaN;
            return new Single3(f * p.X, f * p.Y, f * p.Z);
        }

        public static Single3 operator /(Single3 p, float f)
        {
            if (IsNaN(p) || float.IsNaN(f)) return NaN;
            return new Single3(p.X / f, p.Y / f, p.Z / f);
        }

        // Scalar product
        public static float operator *(Single3 p1, Single3 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return float.NaN;
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }

        // Cross product
        public static Single3 operator %(Single3 p1, Single3 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Single3(p1.Y * p2.Z - p1.Z * p2.Y, p1.Z * p2.X - p1.X * p2.Z, p1.X * p2.Y - p1.Y * p2.X);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Methods

        public float LengthSqr()
        {
            return X * X + Y * Y + Z * Z;
        }

        public float Length()
        {
            return (float)Sqrt(LengthSqr());
        }

        public Single3 Normalize()
        {
            if (IsNaN(this)) return this;
            float length = Length();
            if (length == 0d) // Can not normalize a vector with length 0.
                throw new InvalidValueException(nameof(length), length, 468007);
            return this / length;
        }

        public Single3 Normalize(Single3 direction)
        {
            Single3 value = Normalize();
            if (direction * value < 0) value = -value;
            return value;
        }

        public Double3 ToDouble3()
        {
            return new Double3(X, Y, Z);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Single3 value)
        {
            return float.IsNaN(value.X) || float.IsNaN(value.Y) || float.IsNaN(value.Z);
        }

        public static Single3 NaN
        {
            get { return new Single3(float.NaN, float.NaN, float.NaN); }
        }

        public static Single3 Zero
        {
            get { return new Single3(0f, 0f, 0f); }
        }

        #endregion
    }

}
