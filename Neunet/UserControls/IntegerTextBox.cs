using System;
using System.Globalization;

namespace Neunet.UserControls
{
    public class IntegerTextBox : NumericTextBox<int>
    {
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public IntegerTextBox() : base()
        {
            FormatProvider = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            Format = "G";
            SetValue(0);
            SetMinValue(int.MinValue);
            SetMaxValue(int.MaxValue);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Control

        protected override void OnValidated(EventArgs e)
        {
            Value = int.Parse(Text);
            base.OnValidated(e);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NumericTextBox

        protected override bool IsValidText(string text, out string message)
        {
            if (!base.IsValidText(text, out message)) return false;
            if (!int.TryParse(text, out int f))
            {
                message = "This is not an integer.";
                return false;
            }
            if (f < MinValue)
            {
                message = $"{f} is smaller than {MinValue}.";
                return false;
            }
            if (f > MaxValue)
            {
                message = $"{f} is bigger than {MaxValue}.";
                return false;
            }
            return true;
        }

        protected override void UpdateText()
        {
            Text =
                string.IsNullOrEmpty(Format)
                ? Value.ToString()
                : FormatProvider == null
                ? Value.ToString(Format)
                : Value.ToString(Format, FormatProvider);
        }

        #endregion
    }
}
