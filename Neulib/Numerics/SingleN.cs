using System;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.Numerics
{
    public class Single1D
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public int Count
        {
            get => _values.Length;
        }

        private readonly float[] _values;

        public float this[int i]
        {
            get { return _values[i]; }
            set { _values[i] = value; }
        }

        public float EuclideanDistanceSqr
        {
            get
            {
                int count = Count;
                float sum = 0f;
                for (int i = 0; i < count; i++)
                {
                    float value = _values[i];
                    sum += value * value;
                }
                return sum;
            }
        }

        public float EuclideanDistance
        {
            get=> (float)Sqrt(EuclideanDistanceSqr);
        }

        public static bool IsNaN(Single1D value)
        {
            int count = value.Count;
            for (int i = 0; i < count; i++)
            {
                if (float.IsNaN(value._values[i])) return true;
            }
            return false;
        }

        public static bool IsZero(Single1D value)
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

        public Single1D(int n)
        {
            _values = new float[n];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            if (!base.Equals(o)) return false;
            Single1D value = (Single1D)o;
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

        //public static bool operator ==(Single1D left, Single1D right)
        //{
        //    return left.Equals(right);
        //}

        //public static bool operator !=(Single1D left, Single1D right)
        //{
        //    return !left.Equals(right);
        //}

        public static Single1D operator +(Single1D left, Single1D right)
        {
            int count = left.Count;
            if (count != right.Count) throw new UnequalValueException(count, right.Count, 693639);
            Single1D result = new Single1D(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = left._values[i] + right._values[i];
            }
            return result;
        }

        public static Single1D operator -(Single1D left, Single1D right)
        {
            int count = left.Count;
            if (count != right.Count) throw new UnequalValueException(count, right.Count, 474703);
            Single1D result = new Single1D(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = left._values[i] - right._values[i];
            }
            return result;
        }

        public static Single1D operator -(Single1D right)
        {
            int count = right.Count;
            Single1D result = new Single1D(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = - right._values[i];
            }
            return result;
        }

        public static Single1D operator *(float f, Single1D right)
        {
            int count = right.Count;
            Single1D result = new Single1D(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = f * right._values[i];
            }
            return result;
        }

        public static Single1D operator *(Single1D left, float f)
        {
            int count = left.Count;
            Single1D result = new Single1D(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = f * left._values[i];
            }
            return result;
        }

        public static Single1D operator /(Single1D left, float f)
        {
            int count = left.Count;
            Single1D result = new Single1D(count);
            for (int i = 0; i < count; i++)
            {
                result._values[i] = left._values[i] / f;
            }
            return result;
        }

        public static float operator *(Single1D left, Single1D right)
        {
            int count = left.Count;
            if (count != right.Count) throw new UnequalValueException(count, right.Count, 375076);
            float sum = 0f;
            for (int i = 0; i < count; i++)
            {
                sum += left._values[i] * right._values[i];
            }
            return sum;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SingleN

        public void ForEach(Action<float> action)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                action(_values[i]);
            }
        }

        public void ForEach(Action<int, float> action)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                action(i, _values[i]);
            }
        }

        public void ForEach(Func<float, float> func)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] = func(_values[i]);
            }
        }

        public void ForEach(Func<int, float, float> func)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] = func(i, _values[i]);
            }
        }

        public void Assign(Single1D value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 403516);
            for (int i = 0; i < count; i++)
            {
                _values[i] = value._values[i];
            }
        }

        public void Assign(float value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] = value;
            }
        }

        public void Add(Single1D value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 826652);
            for (int i = 0; i < count; i++)
            {
                _values[i] += value._values[i];
            }
        }

        public void Add(float value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] += value;
            }
        }

        public void Subtract(Single1D value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 657736);
            for (int i = 0; i < count; i++)
            {
                _values[i] -= value._values[i];
            }
        }

        public void Subtract(float value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] -= value;
            }
        }

        public void Multiply(Single1D value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 893830);
            for (int i = 0; i < count; i++)
            {
                _values[i] *= value._values[i];
            }
        }

        public void Multiply(float value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] *= value;
            }
        }

        public void Divide(Single1D value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 663127);
            for (int i = 0; i < count; i++)
            {
                _values[i] /= value._values[i];
            }
        }

        public void Divide(float value)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                _values[i] /= value;
            }
        }

        public float Product(Single1D value)
        {
            int count = Count;
            if (count != value.Count) throw new UnequalValueException(count, value.Count, 282607);
            float sum = 0f;
            for (int i = 0; i < count; i++)
            {
                sum += _values[i] * value._values[i];
            }
            return sum;
        }


        #endregion
    }
}
