using System;
using System.Globalization;
using System.IO;
using Neulib.Exceptions;
using static System.Convert;
using static System.Globalization.CultureInfo;

namespace Neulib.Utils
{
    public class LineReader : IDisposable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public StreamReader Reader { get; private set; }

        public string Line { get; private set; }

        public char[] TrimChars { get; set; }

        //private readonly IFormatProvider _formatInfo = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        public bool EndOfStream
        {
            get { return Reader.EndOfStream; }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public LineReader(string fileName, char[] trimChars)
        {
            Reader = new StreamReader(fileName);
            TrimChars = trimChars;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Reader != null) { Reader.Dispose(); Reader = null; }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region LineReader

        public bool LineIsEmpty()
        {
            return string.IsNullOrEmpty(Line);
        }

        private void ReadLine()
        {
            while (!Reader.EndOfStream && string.IsNullOrEmpty(Line))
            {
                Line = Reader.ReadLine()?.Trim(TrimChars);
            }
        }

        public string ReadString()
        {
            ReadLine();
            string s = Line;
            Line = string.Empty;
            return s;
        }

        public string ReadString(char delimiter)
        {
            ReadLine();
            if (string.IsNullOrEmpty(Line)) return null;
            int k = Line.IndexOf(delimiter);
            string s;
            if (k >= 0)
            {
                s = Line.Substring(0, k).TrimEnd(TrimChars);
                Line = Line.Substring(k + 1).TrimStart(TrimChars);
            }
            else
            {
                s = Line;
                Line = string.Empty;
            }
            return s;
        }

        private double WordToDouble(string w)
        {
            if (string.IsNullOrEmpty(w))
                throw new InvalidDataException("Missing double value.");
            if (!double.TryParse(w, NumberStyles.Float, InvariantCulture.NumberFormat, out double v))
                throw new InvalidDataException($"Double value has wrong number format: {w}.");
            return v;
        }

        public double ReadDouble()
        {
            string w = ReadString();
            return WordToDouble(w);
        }

        public double ReadDouble(char delimiter)
        {
            string w = ReadString(delimiter);
            return WordToDouble(w);
        }

        private float WordToSingle(string w)
        {
            if (string.IsNullOrEmpty(w))
                throw new InvalidDataException("Missing single value.");
            {
                if (float.TryParse(w, NumberStyles.Float, InvariantCulture.NumberFormat, out float v)) return v;
            }
            {
                if (double.TryParse(w, NumberStyles.Float, InvariantCulture.NumberFormat, out double v)) return ToSingle(v);
            }

            throw new InvalidDataException($"Single value has wrong number format: {w}.");

        }

        public float ReadSingle()
        {
            string w = ReadString();
            return WordToSingle(w);
        }

        public float ReadSingle(char delimiter)
        {
            string w = ReadString(delimiter);
            return WordToSingle(w);
        }

        private int WordToInt32(string w)
        {
            if (string.IsNullOrEmpty(w))
                throw new InvalidDataException("Missing integer value.");
            {
                if (int.TryParse(w, NumberStyles.Integer, InvariantCulture.NumberFormat, out int v)) return v;
            }
            {
                if (float.TryParse(w, NumberStyles.Float, InvariantCulture.NumberFormat, out float v)) return ToInt32(v);
            }
            {
                if (double.TryParse(w, NumberStyles.Float, InvariantCulture.NumberFormat, out double v)) return ToInt32(v);
            }
            throw new InvalidDataException($"Integer value has wrong number format: {w}.");
        }

        public int ReadInt32()
        {
            string w = ReadString();
            return WordToInt32(w);
        }

        public int ReadInt32(char delimiter)
        {
            string w = ReadString(delimiter);
            return WordToInt32(w);
        }

        #endregion
    }
}
