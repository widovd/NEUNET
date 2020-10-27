using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace Neunet.UserControls
{
    public class NumericUpDownToolStripButton : ToolStripControlHost
    //https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-wrap-a-windows-forms-control-with-toolstripcontrolhost
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private NumericUpDown NumericUpDown { get { return Control as NumericUpDown; } }
        public int DecimalPlaces { get { return NumericUpDown.DecimalPlaces; } set { NumericUpDown.DecimalPlaces = value; } }
        public bool Hexadecimal { get { return NumericUpDown.Hexadecimal; } set { NumericUpDown.Hexadecimal = value; } }
        public decimal Increment { get { return NumericUpDown.Increment; } set { NumericUpDown.Increment = value; } }
        public decimal Maximum { get { return NumericUpDown.Maximum; } set { NumericUpDown.Maximum = value; } }
        public decimal Minimum { get { return NumericUpDown.Minimum; } set { NumericUpDown.Minimum = value; } }
        public bool ThousandsSeparator { get { return NumericUpDown.ThousandsSeparator; } set { NumericUpDown.ThousandsSeparator = value; } }
        public decimal Value { get { return NumericUpDown.Value; } set { NumericUpDown.Value = value; } }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Events

        public event EventHandler ValueChanged;

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, new EventArgs());
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public NumericUpDownToolStripButton() : base(new NumericUpDown(), "NumericUpDown")
        {
            NumericUpDown.ValueChanged += new EventHandler(NumericUpDown_ValueChanged);
            //numericUpDown.Size = new Size(75, 24);
        }

        public NumericUpDownToolStripButton(decimal minimum, decimal maximum, decimal increment, decimal value, EventHandler valueChanged) : this()
        {
            Minimum = minimum;
            Maximum = maximum;
            Increment = increment;
            Value = value;
            ValueChanged = valueChanged;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NumericUpDownToolStripButton

        public void BeginInit()
        {
            NumericUpDown.BeginInit();
        }

        public void DownButton()
        {
            NumericUpDown.DownButton();
        }

        public void EndInit()
        {
            NumericUpDown.EndInit();
        }

        public override string ToString()
        {
            return NumericUpDown.ToString();
        }

        public void UpButton()
        {
            NumericUpDown.UpButton();
        }

        #endregion
    }
}
