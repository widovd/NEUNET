using System;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{
    public class DoubleN
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public int Count
        {
            get { return _values.Length; }
        }

        private readonly double[] _values;

        public double this[int i]
        {
            get { return _values[i]; }
            set { _values[i] = value; }
        }

        public double EuclideanDistanceSqr
        {
            get
            {
                int count = Count;
                double sum = 0f;
                for (int i = 0; i < count; i++)
                {
                    double value = _values[i];
                    sum += value * value;
                }
                return sum;
            }
        }

        public double EuclideanDistance
        {
            get => Sqrt(EuclideanDistanceSqr);
        }

        public static bool IsNaN(DoubleN value)
        {
            int count = value.Count;
            for (int i = 0; i < count; i++)
            {
                if (double.IsNaN(value._values[i])) return true;
            }
            return false;
        }

        public static bool IsZero(DoubleN value)
        {
            int count = value.Count;
            for (int i = 0; i < count; i++)
            {
                if (value._values[i] != 0f) return false;
            }
            return true;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public DoubleN(int n)
        {
            _values = new double[n];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            if (!base.Equals(o)) return false;
            DoubleN value = (DoubleN)o;
            int count = Count;
            if (count != value.Count) return false;
            for (int i = 0; i < count; i++)
            {
                if (_values[i] != value._values[i]) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                int count = Count;
                for (int i = 0; i < count; i++)
                {
                    h = h * 23 + _values[i].GetHashCode();
                }
            }
            return h;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        //public static bool operator ==(DoubleN left, DoubleN right)
        //{
        //    return left.Equals(right);
        //}

        //public static bool operator !=(DoubleN left, DoubleN right)
        //{
        //    return !left.Equals(right);
        //}

        public static DoubleN operator +(DoubleN left, DoubleN right)
        {
            int count = left.Count;
            if (count != right.Count) throw new UnequalValueException(count, right.Count, 693639);
            DoubleN result = new DoubleN(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = left._values[i] + right._values[i];
            }
            return result;
        }

        public static DoubleN operator -(DoubleN left, DoubleN right)
        {
            int count = left.Count;
            if (count != right.Count) throw new UnequalValueException(count, right.Count, 474703);
            DoubleN result = new DoubleN(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = left._values[i] - right._values[i];
            }
            return result;
        }

        public static DoubleN operator -(DoubleN right)
        {
            int count = right.Count;
            DoubleN result = new DoubleN(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = -right._values[i];
            }
            return result;
        }

        public static DoubleN operator *(double f, DoubleN right)
        {
            int count = right.Count;
            DoubleN result = new DoubleN(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = f * right._values[i];
            }
            return result;
        }

        public static DoubleN operator *(DoubleN left, double f)
        {
            int count = left.Count;
            DoubleN result = new DoubleN(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = f * left._values[i];
            }
            return result;
        }

        public static DoubleN operator /(DoubleN left, double f)
        {
            int count = left.Count;
            DoubleN result = new DoubleN(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = left._values[i] / f;
            }
            return result;
        }

        public static double operator *(DoubleN left, DoubleN right)
        {
            int count = left.Count;
            if (count != right.Count) throw new UnequalValueException(count, right.Count, 375076);
            double sum = 0f;
            for (int i = 0; i < count; i++)
            {
                sum += left._values[i] * right._values[i];
            }
            return sum;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region DoubleN

        public void ForEach(Action<double> action)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                action(_values[i]);
            }
        }

        public void ForEach(Action<int, double> action)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                action(i, _values[i]);
            }
        }

        public void ForEach(Func<double, double> func)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] = func(_values[i]);
            }
        }

        public void ForEach(Func<int, double, double> func)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] = func(i, _values[i]);
            }
        }

        public void Assign(DoubleN value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 403516);
            for (int i = 0; i < count; i++)
            {
                _values[i] = value._values[i];
            }
        }

        public void Assign(double value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] = value;
            }
        }

        public void Add(DoubleN value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 826652);
            for (int i = 0; i < count; i++)
            {
                _values[i] += value._values[i];
            }
        }

        public void Add(double value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] += value;
            }
        }

        public void Subtract(DoubleN value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 657736);
            for (int i = 0; i < count; i++)
            {
                _values[i] -= value._values[i];
            }
        }

        public void Subtract(double value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] -= value;
            }
        }

        public void Multiply(DoubleN value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 893830);
            for (int i = 0; i < count; i++)
            {
                _values[i] *= value._values[i];
            }
        }

        public void Multiply(double value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] *= value;
            }
        }

        public void Divide(DoubleN value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 663127);
            for (int i = 0; i < count; i++)
            {
                _values[i] /= value._values[i];
            }
        }

        public void Divide(double value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] /= value;
            }
        }

        public double Product(DoubleN value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 282607);
            double sum = 0f;
            for (int i = 0; i < count; i++)
            {
                sum += _values[i] * value._values[i];
            }
            return sum;
        }


        #endregion
    }
}
