using System;
using System.Globalization;

namespace Neunet.UserControls
{
    public class DoubleTextBox : NumericTextBox<double>
    {
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public DoubleTextBox() : base()
        {
            FormatProvider = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            Format = "G";
            SetValue(0d);
            SetMinValue(double.NaN);
            SetMaxValue(double.NaN);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Control

        protected override void OnValidated(EventArgs e)
        {
            Value = double.Parse(Text);
            base.OnValidated(e);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NumericTextBox

        protected override bool IsValidText(string text, out string message)
        {
            message = string.Empty;
            if (!double.TryParse(text, out double f))
            {
                message = "This is not a double.";
                return false;
            }
            if (!double.IsNaN(MinValue) && f < MinValue)
            {
                message = $"{f} is smaller than {MinValue}.";
                return false;
            }
            if (!double.IsNaN(MaxValue) && f > MaxValue)
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
