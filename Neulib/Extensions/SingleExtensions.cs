using System.Globalization;
using static System.Math;
using static System.Convert;

namespace Neulib.Extensions
{
    public static class SingleExtensions
    {

        /// <summary>
        /// Returns the square value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>The square value</returns>
        public static float Sqr(this float val)
        {
            return val * val;
        }

        /// <summary>
        /// Converts the value from radians to degrees.
        /// </summary>
        /// <param name="val">The radians value.</param>
        /// <returns>The degrees value.</returns>
        public static float ToDegrees(this float val)
        {
            return ToSingle((180d / PI) * val);
        }

        /// <summary>
        /// Converts the value from degrees to radians.
        /// </summary>
        /// <param name="val">The degrees value.</param>
        /// <returns>The radians value.</returns>
        public static float ToRadians(this float val)
        {
            return ToSingle((PI / 180d) * val);
        }

        /// <summary>
        /// Returns the value but clipped between MinValue and MaxValue.
        /// </summary>
        /// <param name="val">The double value.</param>
        /// <param name="MinValue">The lower clip bound.</param>
        /// <param name="MaxValue">The upper clip bound.</param>
        /// <returns>The clipped value.</returns>
        public static float Clip(this float val, float MinValue, float MaxValue)
        {
            return Min(Max(MinValue, val), MaxValue);
        }

        /// <summary>
        /// Generates a fixed point format string based on the size of the absolute value of the number
        /// Example: FormatString(2648, 3) = "0", FormatString(264.8, 3) = "0.0", FormatString(26.48, 3) = "0.00", FormatString(2.648, 3) = "0.000", FormatString(0.2648, 3) = "0.0000", 
        /// </summary>
        /// <param name="val">The decimal number</param>
        /// <param name="digits">The number of required digits.</param>
        /// <returns>Format string.</returns>
        private static string FormatString(float val, int digits)
        {
            string s = "0";
            val = Abs(val);
            if (val > 0d)
            {
                int a = ToInt32(Ceiling(Max(0d, digits - Log10(val))));
                if (a > 0)
                {
                    s += ".";
                    while (a > 0) { s += "0"; a--; }
                }
            }
            return s;
        }

        /// <summary>
        /// Converts the value to a string with a specified number of digits.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <param name="Digits">The number of digits.</param>
        /// <returns>String with specified number of digits.</returns>
        public static string ToString(this float val, int Digits)
        {
            return val.ToString(FormatString(val, Digits));
        }

        public static string ToString(this float val, int Digits, CultureInfo Culture)
        {
            return val.ToString(FormatString(val, Digits), Culture);
        }

    }

}
