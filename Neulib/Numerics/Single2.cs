using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Xml;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{

    public struct Single2
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float X;

        public float Y;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Single2(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Single2 value = (Single2)o;
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

        public static bool operator ==(Single2 left, Single2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Single2 left, Single2 right)
        {
            return !(left == right);
        }

        public static Single2 operator +(Single2 p1, Single2 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Single2(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Single2 operator -(Single2 p1, Single2 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return NaN;
            return new Single2(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Single2 operator -(Single2 p)
        {
            if (IsNaN(p)) return NaN;
            return new Single2(-p.X, -p.Y);
        }

        public static Single2 operator *(float f, Single2 p)
        {
            if (IsNaN(p) || float.IsNaN(f)) return NaN;
            return new Single2(f * p.X, f * p.Y);
        }

        public static Single2 operator *(Single2 p, float f)
        {
            if (IsNaN(p) || float.IsNaN(f)) return NaN;
            return new Single2(f * p.X, f * p.Y);
        }

        public static Single2 operator /(Single2 p, float f)
        {
            if (IsNaN(p) || float.IsNaN(f)) return NaN;
            return new Single2(p.X / f, p.Y / f);
        }

        // Scalar product
        public static float operator *(Single2 p1, Single2 p2)
        {
            if (IsNaN(p1) || IsNaN(p2)) return float.NaN;
            return p1.X * p2.X + p1.Y * p2.Y;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Methods

        public float LengthSqr()
        {
            return X * X + Y * Y;
        }

        public float Length()
        {
            return (float)Sqrt(LengthSqr());
        }

        public Single2 Normalize()
        {
            if (IsNaN(this)) return this;
            float length = Length();
            if (length == 0d) // Can not normalize a vector with length 0.
                throw new InvalidValueException(nameof(length), length, 275453);
            return this / length;
        }

        public Single2 Normalize(Single2 direction)
        {
            Single2 value = Normalize();
            if (direction * value < 0) value = -value;
            return value;
        }

        public Double2 ToDouble2()
        {
            return new Double2(X, Y);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool IsNaN(Single2 value)
        {
            return float.IsNaN(value.X) || float.IsNaN(value.Y);
        }

        public static Single2 NaN
        {
            get { return new Single2(float.NaN, float.NaN); }
        }

        public static Single2 Zero
        {
            get { return new Single2(0f, 0f); }
        }

        #endregion
    }

}
