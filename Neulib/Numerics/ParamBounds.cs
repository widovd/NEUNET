using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Xml;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{

    public struct ParamBounds
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public double XLo { get => URange.Lo; }
        public double XHi { get => URange.Hi; }
        public double YLo { get => VRange.Lo; }
        public double YHi { get => VRange.Hi; }

        public double DeltaX { get => URange.Delta; }
        public double DeltaY { get => VRange.Delta; }

        public ParamRange URange { get; private set; }

        public ParamRange VRange { get; private set; }

        [Browsable(false)]
        public double SideRatio { get { return Max(URange.Delta, VRange.Delta) / Min(URange.Delta, VRange.Delta); } }

        [Browsable(false)]
        public Double2 TopLeft
        {
            get { return new Double2(URange.Lo, VRange.Lo); }
        }

        [Browsable(false)]
        public Double2 TopRight
        {
            get { return new Double2(URange.Lo, VRange.Hi); }
        }

        [Browsable(false)]
        public Double2 BottomLeft
        {
            get { return new Double2(URange.Hi, VRange.Lo); }
        }

        [Browsable(false)]
        public Double2 BottomRight
        {
            get { return new Double2(URange.Hi, VRange.Hi); }
        }

        [Browsable(false)]
        public Double2 Center
        {
            get { return new Double2(URange.Center, VRange.Center); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ParamBounds(ParamRange uRange, ParamRange vRange)
        {
            URange = uRange;
            VRange = vRange;
        }

        public ParamBounds(double uLo, double uHi, double vLo, double vHi)
        {
            URange = new ParamRange(uLo, uHi);
            VRange = new ParamRange(vLo, vHi);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            ParamBounds value = (ParamBounds)o;
            if (IsNaN(this) && IsNaN(value)) return true;
            if (!Equals(URange, value.URange)) return false;
            if (!Equals(VRange, value.VRange)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + URange.GetHashCode();
                h = h * 23 + VRange.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{URange}, {VRange}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(ParamBounds left, ParamBounds right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ParamBounds left, ParamBounds right)
        {
            return !(left == right);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Methods

        public bool InRange(Double2 paramCoord, double tiny = 0d)
        {
            return URange.InRange(paramCoord.X, tiny) && VRange.InRange(paramCoord.Y, tiny);
        }

        public Double2 Clip(Double2 paramCoord)
        {
            double u = paramCoord.X, v = paramCoord.Y;
            if (u < URange.Lo) u = URange.Lo; else if (u > URange.Hi) u = URange.Hi;
            if (v < VRange.Lo) v = VRange.Lo; else if (v > VRange.Hi) v = VRange.Hi;
            return new Double2(u, v);
        }

        public bool InFilterRange(Double2 coord, ParamBounds bounds)
        {
            if (!URange.InFilterRange(coord.X, bounds.URange)) return false;
            if (!VRange.InFilterRange(coord.Y, bounds.VRange)) return false;
            return true;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Zero

        public static bool IsNaN(ParamBounds value)
        {
            return ParamRange.IsNaN(value.URange) || ParamRange.IsNaN(value.VRange);
        }

        public static ParamBounds NaN
        {
            get => new ParamBounds(ParamRange.NaN, ParamRange.NaN);
        }


        public static bool IsDefined(ParamBounds value)
        {
            return ParamRange.IsDefined(value.URange) && ParamRange.IsDefined(value.VRange);
        }

        public static ParamBounds Undefined
        {
            get => new ParamBounds(ParamRange.Undefined, ParamRange.Undefined); 
        }

        #endregion
    }
}
