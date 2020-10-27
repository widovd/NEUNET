using System;
using System.Globalization;

namespace Neulib.Extensions
{
    public static class StringExtensions
    {
        private static readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture.NumberFormat;

        public static string Last(this string s, char c)
        {
            int i = s.LastIndexOf(c);
            return (i >= 0 && i < s.Length - 1) ? s.Substring(i + 1) : s;
        }

        public static bool ReadBoolean(this string text, bool defaultValue = false)
        {
            return bool.TryParse(text, out bool x) ? x : defaultValue;
        }

        public static int ReadInt32(this string text, IFormatProvider provider, int defaultValue = 0)
        {
            return int.TryParse(text, NumberStyles.Integer, provider, out int x) ? x : defaultValue;
        }

        public static float ReadFloat(this string text, IFormatProvider provider, float defaultValue = 0f)
        {
            return float.TryParse(text, NumberStyles.Float, provider, out float x) ? x : defaultValue;
        }

        public static double ReadDouble(this string text, IFormatProvider provider, double defaultValue = 0d)
        {
            return double.TryParse(text, NumberStyles.Float, provider, out double x) ? x : defaultValue;
        }

        public static decimal ReadDecimal(this string text, IFormatProvider provider, decimal defaultValue = 0m)
        {
            return decimal.TryParse(text, NumberStyles.Float, provider, out decimal x) ? x : defaultValue;
        }

        public static DateTime ReadDateTime(this string text, IFormatProvider provider, DateTime defaultValue)
        {
            return DateTime.TryParse(text, provider, DateTimeStyles.None, out DateTime x) ? x : defaultValue;
        }

    }
}
