using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Neulib.Exceptions;
using static System.Globalization.CultureInfo;

namespace Neulib.Utils
{
    public class LineWriter : IDisposable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties
        public StreamWriter Writer { get; private set; }

        public StringBuilder Line { get; private set; } = new StringBuilder();

        public int MaxLength { get; set; }

        //private readonly IFormatProvider _formatInfo = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public LineWriter(string fileName, char delimiter, int maxLength = 0)
        {
            Writer = new StreamWriter(fileName);
            MaxLength = maxLength;
            //https://msdn.microsoft.com/en-us/library/kfsatb94(v=vs.110).aspx
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Writer != null) { Writer.Dispose(); Writer = null; }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region LineWriter

        public void WriteLine()
        {
            Writer.WriteLine(Line.ToString());
            Line.Clear();
        }

        public void WriteLine(string line)
        {
            if (Line.Length > 0) WriteLine();
            Line.Append(line);
            WriteLine();
        }

        public void WriteString(string value, char delimiter)
        {
            if (Line.Length > 0) Line.Append(delimiter);
            Line.Append(value);
            if ((MaxLength > 0) && (Line.Length > MaxLength)) WriteLine();
        }

        public void WriteInt32(int value, char delimiter)
        {
            WriteString(value.ToString(string.Empty, InvariantCulture.NumberFormat), delimiter);
        }

        public void WriteInt32(int[] values, char delimiter)
        {
            foreach (int value in values)
            {
                WriteInt32(value, delimiter);
            }
        }

        public void WriteSingle(float value, char delimiter, string format)
        {
            WriteString(value.ToString(format, InvariantCulture.NumberFormat), delimiter);
        }

        public void WriteSingle(float[] values, char delimiter, string format)
        {
            foreach (float value in values)
            {
                WriteSingle(value, delimiter, format);
            }
        }

        public void WriteDouble(double value, char delimiter, string format )
        {
            WriteString(value.ToString(format, InvariantCulture.NumberFormat), delimiter);
        }

        public void WriteDouble(double[] values, char delimiter, string format)
        {
            foreach (double value in values)
            {
                WriteDouble(value, delimiter, format);
            }
        }

        #endregion
    }
}
