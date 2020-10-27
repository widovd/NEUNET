using System;
using System.ComponentModel;
using System.Windows.Forms;
using Neulib.Exceptions;
using Neunet.Forms;

namespace Neunet.Images.Charts2D
{
    public partial class GradientToolStrip : UserControl
    {
        // ----------------------------------------------------------------------------------------
        #region ResetImage

        public event EventHandler ResetImageClick;

        private void ResetImageToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetImageClick?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region InverseGradient

        public event EventHandler InverseGradientChanged;

        private bool _inverseGradient;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool InverseGradient
        {
            get { return _inverseGradient; }
            set
            {
                if (value == _inverseGradient) return;
                SetInverseGradient(value);
            }
        }

        protected virtual void SetInverseGradient(bool value)
        {
            _inverseGradient = value;
            InverseGradientChanged?.Invoke(this, new EventArgs());
        }

        private void InverseGradientToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                InverseGradient = inverseGradientToolStripButton.Checked;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ColorGradient

        public event EventHandler ColorGradientChanged;

        private ColorGradientEnum _colorGradient;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public ColorGradientEnum ColorGradient
        {
            get { return _colorGradient; }
            set
            {
                if (value == _colorGradient) return;
                SetColorGradient(value);
            }
        }

        protected virtual void SetColorGradient(ColorGradientEnum value)
        {
            _colorGradient = value;
            foreach (ToolStripMenuItem item in colorGradientDropDownButton.DropDownItems)
            {
                ColorGradientEnum colorGradient = (ColorGradientEnum)item.Tag;
                item.Checked = colorGradient == value;
                if (item.Checked)
                    colorGradientDropDownButton.Image = item.Image;
            }
            ColorGradientChanged?.Invoke(this, new EventArgs());
        }

        private void ColorGradientButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is ToolStripMenuItem menuItem)) return;
                ColorGradient = (ColorGradientEnum)menuItem.Tag;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public GradientToolStrip()
        {
            InitializeComponent();
            GradientBitmap.AddColorGradientDropDownItems(colorGradientDropDownButton.DropDownItems, ColorGradientButton_Click);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) { components.Dispose(); components = null; }
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
