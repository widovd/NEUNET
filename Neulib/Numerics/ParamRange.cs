using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using static System.Convert;
using static System.Math;
using Neulib.Exceptions;

namespace Neulib.Numerics
{
    [TypeConverter(typeof(ParamRangeConverter))]
    public struct ParamRange
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public double Lo;

        public double Hi;

        public double Delta { get { return Hi - Lo; } }

        public double Center { get { return 0.5d * (Hi + Lo); } }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ParamRange(double lo, double hi)
        {
            Lo = lo;
            Hi = hi;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            ParamRange value = (ParamRange)o;
            if (IsNaN(this) && IsNaN(value)) return true;
            if (Lo != value.Lo) return false;
            if (Hi != value.Hi) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + Lo.GetHashCode();
                h = h * 23 + Hi.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{Lo}, {Hi}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(ParamRange left, ParamRange right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ParamRange left, ParamRange right)
        {
            return !left.Equals(right);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region poles not allowed

        public static double Val_static(int i, int n, bool periodic = false)
        {
            if (periodic) n++;
            return (double)i / ((double)n - 1d);
        }

        public static double Inv_static(double x, int n, bool periodic = false)
        {
            if (periodic) n++;
            return x * ((double)n - 1d);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ParamRange

        public bool InRange(double x, double tiny = 0d)
        {
            if (!double.IsNaN(Lo) && (x < Lo - tiny)) return false;
            if (!double.IsNaN(Hi) && (x > Hi + tiny)) return false;
            return true;
        }

        public double Val(double x)
        {
            return Lo + (Hi - Lo) * x;
        }

        public double Inv(double x)
        {
            return (x - Lo) / (Hi - Lo);
        }

        public double Val(int i, int n, bool periodic = false)
        {
            return Val(Val_static(i, n, periodic));
        }

        public int Inv(double x, int n, bool periodic = false)
        {
            return ToInt32(Inv_static(Inv(x), n, periodic));
        }

        public double GetStep(int count, bool periodic = false)
        // if (periodic) step = (Hi - Lo) / count; else step = (Hi - Lo) / (count - 1);
        {
            int n = periodic ? count : count - 1;
            double step = n > 0 ? Delta / (double)n : double.NaN;
            return step;
        }

        public int GetCount(double step, bool periodic = false)
        // if (periodic) count = (Hi - Lo) / step; else count = (Hi - Lo) / step + 1;
        {
            if (step <= 0d) return 0;
            int n = ToInt32(Round(Delta / step));
            int count = periodic ? n : n + 1;
            return count;
        }

        private static bool InFilterRange0(double u, double uLo, double uHi, double uD, double uFLo, double uFHi)
        {
            while (uFLo < uLo) uFLo += uD;
            while (uFLo > uHi) uFLo -= uD;
            while (uFHi < uLo) uFHi += uD;
            while (uFHi > uHi) uFHi -= uD;
            if (uFLo <= uFHi)
            {
                if (u < uFLo || u > uFHi) return false;
            }
            else // uFHi < uFLo
            {
                if (u >= uFHi && u <= uFLo) return false;
            }
            return true;
        }

        public bool InFilterRange(double x, ParamRange range)
        {
            if (!InFilterRange0(x, range.Lo, range.Hi, range.Delta, Lo, Hi)) return false;
            return true;
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Zero

        public static bool IsNaN(ParamRange value)
        {
            return double.IsNaN(value.Lo) || double.IsNaN(value.Hi);
        }

        public static ParamRange NaN
        {
            get => new ParamRange(double.NaN, double.NaN);
        }


        public static bool IsDefined(ParamRange value)
        {
            if (double.IsNaN(value.Lo)) return false;
            if (double.IsNaN(value.Hi)) return false;
            return value.Lo < value.Hi;
        }

        public static ParamRange Undefined
        {
            get => new ParamRange(0d, 0d);
        }

        #endregion
    }

    public class ParamRangeConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(
            ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                string[] sArr = s.Split(new char[] { ',' });
                int n = sArr.Length;
                if (n != 2) // Wrong number of ParamRange parameters
                    throw new InvalidValueException(nameof(n), n, 350499);
                double lo = double.Parse(sArr[0]);
                double hi = double.Parse(sArr[1]);
                return new ParamRange(lo, hi);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if (value is ParamRange && destType == typeof(string))
            {
                ParamRange range = (ParamRange)value;
                string s = $"{range.Lo}, {range.Hi}";
                return s;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

}
