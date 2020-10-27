using System;
using static System.Math;

namespace Neulib.Extensions
{
    /// <summary>
    /// Static class with integer extention methods.
    /// </summary>
    public static class IntExtensions
    {

        /// <summary>
        /// Sets a bit of an integer value to 0 or 1 and returns the new value.
        /// </summary>
        /// <param name="flags">The integer value.</param>
        /// <param name="bitNumber">The number of the bit to change: 0, 1, ..., 31.</param>
        /// <param name="value">Set bit to: 0 = false, 1 = true.</param>
        /// <returns>The changed Int32.</returns>
        public static int SetBit(this int flags, byte bitNumber, bool value)
        {
            if (value)
                flags |= (1 << bitNumber);
            else
                flags &= ~(1 << bitNumber);
            return flags;
        }

        /// <summary>
        /// Returns the bit of the integer value.
        /// </summary>
        /// <param name="val">The integer value.</param>
        /// <param name="Bit">The number of the bit to change: 0, 1, ..., 31.</param>
        /// <returns>The value of the bit: 0 = false, 1 = true.</returns>
        public static bool GetBit(this int val, byte Bit)
        {
            return (val & (1 << Bit)) != 0;
        }

        /// <summary>
        /// Returns the square value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>The square value</returns>
        public static long Sqr(this int val)
        {
            long n = Convert.ToInt64(val);
            return n * n;
        }

        /// <summary>
        /// Returns the value but clipped between MinValue and MaxValue.
        /// </summary>
        /// <param name="val">The integer value.</param>
        /// <param name="MinValue">The lower clip bound.</param>
        /// <param name="MaxValue">The upper clip bound.</param>
        /// <returns>The clipped value.</returns>
        public static int Clip(this int val, int MinValue, int MaxValue)
        {
            return Min(Max(MinValue, val), MaxValue);
        }

        /// <summary>
        /// Returns true if the number is a power of 2.
        /// </summary>
        /// <param name="val">The integer value.</param>
        /// <returns>True if the number is a power of 2.</returns>
        public static bool IsPowerOfTwo(this int val)
        {
            return (val & (val - 1)) == 0;
        }

        /// <summary>
        /// If value > 0 then the 2Log of the value is returned.
        /// If value == 0 then the -1 is returned.
        /// </summary>
        /// <param name="val">The integer value.</param>
        /// <returns>The 2Log of the value</returns>
        public static int Log2(this int val)
        {
            int i = -1;
            while (val > 0)
            {
                val >>= 1;
                i++;
            }
            return i;
        }

        public static int Pow2(this int val)
        {
            return val == 0 ? 1 : 1 << val;
        }

        public static int Trim(this int i, int n)
        {
            while (i < 0) i += n;
            while (i >= n) i -= n;
            return i;
        }


    }

}
