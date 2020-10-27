using System.Globalization;
using static System.Math;
using static System.Convert;

namespace Neulib.Extensions
{
    public static class DoubleExtensions
    {

        /// <summary>
        /// Returns the square value.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>The square value</returns>
        public static double Sqr(this double val)
        {
            return val * val;
        }

        /// <summary>
        /// Converts the value from radians to degrees.
        /// </summary>
        /// <param name="val">The radians value.</param>
        /// <returns>The degrees value.</returns>
        public static double ToDegrees(this double val)
        {
            return (180d / PI) * val;
        }

        /// <summary>
        /// Converts the value from degrees to radians.
        /// </summary>
        /// <param name="val">The degrees value.</param>
        /// <returns>The radians value.</returns>
        public static double ToRadians(this double val)
        {
            return (PI / 180d) * val;
        }

        /// <summary>
        /// Generates a fixed point format string based on the size of the absolute value of the number
        /// Example: FormatString(2648, 3) = "0", FormatString(264.8, 3) = "0.0",
        /// FormatString(26.48, 3) = "0.00", FormatString(2.648, 3) = "0.000", FormatString(0.2648, 3) = "0.0000", 
        /// </summary>
        /// <param name="value">The decimal number</param>
        /// <param name="digits">The number of required digits.</param>
        /// <returns>Format string.</returns>
        public static string FormatString(this double value, int digits)
        {
            string s = "0";
            value = Abs(value);
            if (value > 0d)
            {
                int a = ToInt32(Ceiling(Max(0d, digits - Log10(value) - 1))); // 20180329: -1 toegevoegd om #digits te laten kloppen, bijv: 123.456 is 6 digits
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
        /// <param name="value">The value.</param>
        /// <param name="digits">The number of digits.</param>
        /// <returns>String with specified number of digits.</returns>
        public static string ToString(this double value, int digits)
        {
            return value.ToString(FormatString(value, digits));
        }

        public static string ToString(this double value, int digits, CultureInfo culture)
        {
            return value.ToString(FormatString(value, digits), culture);
        }

    }

}
