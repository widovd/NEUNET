using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Neulib.Exceptions;
using System.Globalization;
using Neulib;
using Neunet.Attributes;

namespace Neunet.UserControls
{
    public class NumericTextBox<T> : TextBox
    {
        // ----------------------------------------------------------------------------------------
        #region Events

        [Description("Occurs when Format changes")]
        public event EventHandler FormatChanged;

        protected virtual void OnFormatChanged(EventArgs e)
        {
            FormatChanged?.Invoke(this, e);
        }

        [Description("Occurs when FormatProvider changes")]
        public event EventHandler FormatProviderChanged;

        protected virtual void OnFormatProviderChanged(EventArgs e)
        {
            FormatProviderChanged?.Invoke(this, e);
        }

        [Description("Occurs when Value changes")]
        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        [Description("Occurs when MinValue changes")]
        public event EventHandler MinValueChanged;

        protected virtual void OnMinValueChanged(EventArgs e)
        {
            MinValueChanged?.Invoke(this, e);
        }

        [Description("Occurs when MaxValue changes")]
        public event EventHandler MaxValueChanged;

        protected virtual void OnMaxValueChanged(EventArgs e)
        {
            MaxValueChanged?.Invoke(this, e);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Properties

        public IFormatProvider _formatProvider;

        [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            EditorBrowsable(EditorBrowsableState.Never),
            Browsable(false),
        ]
        public IFormatProvider FormatProvider
        {
            get { return _formatProvider; }
            set
            {
                if (Equals(value, _formatProvider)) return;
                SetFormatProvider(value);
            }
        }

        private void SetFormatProvider(IFormatProvider value)
        {
            _formatProvider = value;
            UpdateText();
            OnFormatProviderChanged(new EventArgs());
        }

        private string _format;
        //https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
        [NumericCategory, Description("Numeric format string")]
        public string Format
        {
            get { return _format; }
            set
            {
                if (Equals(value, _format)) return;
                SetFormat(value);
            }
        }

        private void SetFormat(string value)
        {
            _format = value;
            UpdateText();
            OnFormatChanged(new EventArgs());
        }

        private T _value;
        [NumericCategory, Description("Numeric value")]
        public T Value
        {
            get { return _value; }
            set
            {
                if (Equals(value, _value)) return;
                SetValue(value);
            }
        }

        protected void SetValue(T value)
        {
            _value = value;
            UpdateText();
            OnValueChanged(new EventArgs());
        }

        private T _minValue;
        [NumericCategory, Description("Minimum numeric value or NaN")]
        public T MinValue
        {
            get { return _minValue; }
            set
            {
                if (Equals(value, _minValue)) return;
                SetMinValue(value);
            }
        }

        protected void SetMinValue(T value)
        {
            _minValue = value;
            OnMinValueChanged(new EventArgs());
        }

        private T _maxValue;
        [NumericCategory, Description("Maximum numeric value or NaN")]
        public T MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (Equals(value, _maxValue)) return;
                SetMaxValue(value);
            }
        }

        protected void SetMaxValue(T value)
        {
            _maxValue = value;
            OnMaxValueChanged(new EventArgs());
        }

        private ErrorProvider errorProvider = new ErrorProvider();

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public NumericTextBox() : base()
        {
            SetFormatProvider(new NumberFormatInfo() { NumberDecimalSeparator = "." });
            SetFormat("G");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;
            if (errorProvider != null) { errorProvider.Dispose(); errorProvider = null; }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Control

        protected override void OnValidating(CancelEventArgs e)
        {
            string text = Text;
            bool valid = IsValidText(text, out string message);
            //Console.WriteLine($"Text '{text}' is valid: {valid}");
            if (!valid)
            {
                e.Cancel = true;
                Select(0, text.Length);
                using (var icon = new NotifyIcon())
                {
                    errorProvider.SetError(this, message);
                    errorProvider.SetIconAlignment(this, ErrorIconAlignment.MiddleLeft);
                }
            }
            base.OnValidating(e);
        }

        protected override void OnValidated(EventArgs e)
        {
            errorProvider.SetError(this, null);
            base.OnValidated(e);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NumericTextBox

        protected virtual bool IsValidText(string text, out string message)
        {
            message = string.Empty;
            return true;
        }

        protected virtual void UpdateText()
        {

        }

        #endregion
    }
}
