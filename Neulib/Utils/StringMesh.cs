using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Neulib.Exceptions;
using static System.Math;
using static System.Globalization.CultureInfo;

namespace Neulib.Utils
{

    /// <summary>
    /// 2D array of strings
    /// </summary>
    public class StringTable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private string[,] _values;

        public int RowCapacity { get { return _values != null ? _values.GetLength(0) : 0; } }

        public int ColumnCapacity { get { return _values != null ? _values.GetLength(1) : 0; } }

        public int RowCount { get; private set; }

        public int ColumnCount { get; private set; }

        public int RowIndex { get; private set; }

        public int ColumnIndex { get; private set; }

        public string this[int rowIndex, int columnIndex]
        {
            get { return _values[rowIndex, columnIndex]; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public StringTable()
        {
            Clear();
        }

        public StringTable(int rowCapacity, int columnCapacity)
        {
            Clear(rowCapacity, columnCapacity);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Public methods

        private void SetCapacity(int newRowCapacity, int newColumnCapacity)
        {
            if (newRowCapacity <= 0) newRowCapacity = 1;
            if (newColumnCapacity <= 0) newColumnCapacity = 1;
            int n = Min(RowCapacity, newRowCapacity), m = Min(ColumnCapacity, newColumnCapacity);
            string[,] oldValues = _values;
            _values = new string[newRowCapacity, newColumnCapacity];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    _values[i, j] = oldValues[i, j];
        }

        public void Reset()
        {
            RowIndex = 0;
            ColumnIndex = 0;
        }

        public void Clear(int rowCapacity = 1, int columnCapacity = 1)
        {
            //RowCount = 1;
            //ColumnCount = 0;
            _values = new string[rowCapacity, columnCapacity];
            //SetCapacity(rowCapacity, columnCapacity);
            Reset();
        }

        public void StartLine()
        {
            ColumnIndex = 0;
        }

        public void NextLine()
        {
            RowIndex++;
            StartLine();
        }

        public void NextColumn()
        {
            ColumnIndex++;
        }

        public string Read()
        {
            if (RowIndex < 0 || RowIndex >= RowCount)
                throw new InvalidValueException(nameof(RowIndex), RowIndex, 434208);
            if (ColumnIndex < 0 || ColumnIndex >= ColumnCount)
                throw new InvalidValueException(nameof(ColumnIndex), ColumnIndex, 399572);
            string value = _values[RowIndex, ColumnIndex];
            NextColumn();
            return value;
        }

        public string ReadLine()
        {
            string value = Read();
            NextLine();
            return value;
        }

        public void Write(params string[] values)
        {
            foreach (string value in values)
            {
                while (ColumnIndex >= ColumnCapacity) SetCapacity(RowCapacity, 2 * ColumnCapacity);
                while (RowIndex >= RowCapacity) SetCapacity(2 * RowCapacity, ColumnCapacity);
                if (ColumnIndex >= ColumnCount) ColumnCount = ColumnIndex + 1;
                if (RowIndex >= RowCount) RowCount = RowIndex + 1;
                _values[RowIndex, ColumnIndex] = value;
                NextColumn();
            }
        }

        public void WriteLine()
        {
            NextLine();
        }

        public void WriteLine(params string[] values)
        {
            Write(values);
            NextLine();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Read and Write

        public void ReadFromString(string text, string pattern)
        {
            // next statement is 10x faster than: ... = Regex.Split(text, "\r\n|\r|\n", RegexOptions.None);
            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int rowCount = lines.Length;
            while (rowCount > 0 && string.IsNullOrWhiteSpace(lines[rowCount - 1])) rowCount--;
            string[] line;
            int columnCount = 0;
            for (int i = 0; i < rowCount; i++)
            {
                line = Regex.Split(lines[i], pattern);
                int length = line.Length;
                if (length > columnCount) columnCount = length;
            }
            Clear(rowCount, columnCount);
            for (int i = 0; i < rowCount; i++)
            {
                line = Regex.Split(lines[i], pattern);
                int length = line.Length;
                for (int j = 0; j < length; j++)
                {
                    _values[i, j] = line[j];
                }
            }
            RowCount = rowCount;
            RowIndex = rowCount;
            ColumnCount = columnCount;
            ColumnIndex = 0;
        }

        private string WriteToString(string seperator)
        {
            StringBuilder builder = new StringBuilder();
            int rowCount = RowCount, columnCount = ColumnCount;
            for (int i = 0; i < rowCount; i++)
            {
                bool b = false;
                int pos = builder.Length;
                for (int j = columnCount - 1; j >= 0; j--)
                {
                    string value = _values[i, j];
                    if (!b && string.IsNullOrEmpty(value)) continue;
                    if (b) builder.Insert(pos, seperator);
                    builder.Insert(pos, value);
                    b = true;
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        public void ReadFromFile(string filePath, string pattern)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string text = reader.ReadToEnd();
                ReadFromString(text, pattern);
            }
        }

        public void WriteToFile(string filePath, string seperator)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string text = WriteToString(seperator);
                writer.Write(text);
            }
        }

        //private const string _tabSeperator = "\t";

        //public void ReadFromClipboard()
        //// Excel compatible format
        //{
        //    string text = Clipboard.GetText();
        //    ReadFromString(text, _tabSeperator);
        //}

        //public void WriteToClipboard()
        //// Excel compatible format
        //{
        //    string text = WriteToString(_tabSeperator);
        //    Clipboard.SetText(text);
        //}

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Count rows & columns

        /// <summary>
        /// Count the number of rows from the start position until the first non-float value.
        /// </summary>
        /// <param name="rowIndex">The start row index.</param>
        /// <param name="columnIndex">The start column index.</param>
        /// <returns>The number of rows with float values from the start position.</returns>
        public int CountRows(int rowIndex, int columnIndex)
        {
            int i = 0;
            string value;
            while (
                rowIndex + i < RowCapacity &&
                !string.IsNullOrEmpty(value = _values[rowIndex + i, columnIndex]) &&
                double.TryParse(value, NumberStyles.Float, InvariantCulture.NumberFormat, out _)
                ) i++;
            return i;
        }

        public int CountRows()
        {
            return CountRows(RowIndex, ColumnIndex);
        }

        /// <summary>
        /// Counts the number of columns from the start position until the first non-float value.
        /// </summary>
        /// <param name="rowIndex">The start row index.</param>
        /// <param name="columnIndex">The start column index.</param>
        /// <returns>The number of columns with float values from the start position.</returns>
        public int CountColumns(int rowIndex, int columnIndex)
        {
            int j = 0;
            string value;
            while (
                columnIndex + j < ColumnCapacity &&
                !string.IsNullOrEmpty(value = _values[rowIndex, columnIndex + j]) &&
                double.TryParse(value, NumberStyles.Float, InvariantCulture.NumberFormat, out _)
                ) j++;
            return j;
        }

        public int CountColumns()
        {
            return CountColumns(RowIndex, ColumnIndex);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region 1: tables with only cell values

        public void ReadTable1(
            int rowCount, int columnCount,
            Action<int, int, string> cellAction
            )
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    string value = Read();
                    cellAction(i, j, value);
                }
                NextLine();
            }
        }

        public void ReadTable1_T(
            int columnCount, int rowCount,
            Action<int, int, string> cellAction
            )
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    string value = Read();
                    cellAction(j, i, value);
                }
                NextLine();
            }
        }

        public void ReadTable1(
            int uCount, int vCount,
            Action<int, int, string> cellAction,
            bool transpose
            )
        {
            if (transpose)
                ReadTable1_T(uCount, vCount, cellAction);
            else
                ReadTable1(uCount, vCount, cellAction);
        }

        public void WriteTable1(
            int rowCount, int columnCount,
            Func<int, int, string> cellFunc
            )
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    Write(cellFunc(i, j));
                }
                WriteLine();
            }
        }

        public void WriteTable1_T(
            int columnCount, int rowCount,
            Func<int, int, string> cellFunc
            )
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    Write(cellFunc(j, i));
                }
                WriteLine();
            }
        }

        public void WriteTable1(
            int uCount, int vCount,
            Func<int, int, string> cellFunc,
            bool transpose
            )
        {
            if (transpose)
                WriteTable1_T(uCount, vCount, cellFunc);
            else
                WriteTable1(uCount, vCount, cellFunc);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region 1: tables with 1 row and 1 column header and cell values

        public void ReadTable2(
        int rowCount, int columnCount,
        out string header,
        Action<int, int, string> cellAction,
        Action<int, string> column0Action,
        Action<int, string> row0Action
        )
        {
            header = Read();
            for (int j = 0; j < columnCount; j++)
            {
                string value = Read();
                row0Action(j, value);
            }
            NextLine();
            for (int i = 0; i < rowCount; i++)
            {
                string value = Read();
                column0Action(i, value);
                for (int j = 0; j < columnCount; j++)
                {
                    value = Read();
                    cellAction(i, j, value);
                }
                NextLine();
            }
        }

        public void ReadTable2_T(
            int rowCount, int columnCount,
            out string header,
            Action<int, int, string> cellAction,
            Action<int, string> column0Action,
            Action<int, string> row0Action
            )
        {
            header = Read();
            for (int i = 0; i < rowCount; i++)
            {
                string value = Read();
                column0Action(i, value);
            }
            NextLine();
            for (int j = 0; j < columnCount; j++)
            {
                string value = Read();
                row0Action(j, value);
                for (int i = 0; i < rowCount; i++)
                {
                    value = Read();
                    cellAction(i, j, value);
                }
                NextLine();
            }
        }
        public void ReadTable2(
            int rowCount, int columnCount,
            out string header,
            Action<int, int, string> cellAction,
            Action<int, string> column0Action,
            Action<int, string> row0Action,
            bool transpose)
        {
            if (transpose)
                ReadTable2_T(rowCount, columnCount, out header, cellAction, column0Action, row0Action);
            else
                ReadTable2(rowCount, columnCount, out header, cellAction, column0Action, row0Action);
        }

        public void WriteTable2(
            int rowCount, int columnCount,
            string header,
            Func<int, int, string> cellFunc,
            Func<int, string> column0Func,
            Func<int, string> row0Func
            )
        {
            Write(header);
            for (int j = 0; j < columnCount; j++)
            {
                Write(row0Func(j));
            }
            WriteLine();
            for (int i = 0; i < rowCount; i++)
            {
                Write(column0Func(i));
                for (int j = 0; j < columnCount; j++)
                {
                    Write(cellFunc(i, j));
                }
                WriteLine();
            }
        }

        public void WriteTable2_T(
            int rowCount, int columnCount,
            string header,
            Func<int, int, string> cellFunc,
            Func<int, string> column0Func,
            Func<int, string> row0Func
            )
        {
            Write(header);
            for (int i = 0; i < rowCount; i++)
            {
                Write(column0Func(i));
            }
            WriteLine();
            for (int j = 0; j < columnCount; j++)
            {
                Write(row0Func(j));
                for (int i = 0; i < rowCount; i++)
                {
                    Write(cellFunc(i, j));
                }
                WriteLine();
            }
        }

        public void WriteTable2(
            int rowCount, int columnCount,
            string header,
            Func<int, int, string> cellFunc,
            Func<int, string> column0Func,
            Func<int, string> row0Func,
            bool transpose)
        {
            if (transpose)
                WriteTable2_T(rowCount, columnCount, header, cellFunc, column0Func, row0Func);
            else
                WriteTable2(rowCount, columnCount, header, cellFunc, column0Func, row0Func);
        }

        #endregion
    }
}
