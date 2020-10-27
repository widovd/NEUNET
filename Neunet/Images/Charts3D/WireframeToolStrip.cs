using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using Neulib.Exceptions;
using Neunet.Forms;
using Neunet.Serializers;

namespace Neunet.Images.Charts3D
{
    public partial class WireframeToolStrip : UserControl
    {
        // ----------------------------------------------------------------------------------------
        #region Resolution

        public event EventHandler DefaultResolutionClicked;

        private void DefaultResolutionButton_Click(object sender, EventArgs e)
        {
            try
            {
                DefaultResolutionClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler DecreaseResolutionClicked;

        private void DecreaseResolutionButton_Click(object sender, EventArgs e)
        {
            try
            {
                DecreaseResolutionClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler IncreaseResolutionClicked;

        private void IncreaseResolutionButton_Click(object sender, EventArgs e)
        {
            try
            {
                IncreaseResolutionClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SliceMode

        public event EventHandler SliceModeChanged;

        private SliceModeEnum _SliceMode;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public SliceModeEnum SliceMode
        {
            get { return _SliceMode; }
            set
            {
                if (value == _SliceMode) return;
                SetSliceMode(value);
            }
        }

        private void SetSliceMode(SliceModeEnum value)
        {
            _SliceMode = value;
            sliceNoneButton.Checked = value == SliceModeEnum.None;
            sliceUButton.Checked = value == SliceModeEnum.SliceU;
            sliceVButton.Checked = value == SliceModeEnum.SliceV;
            SliceModeChanged?.Invoke(this, new EventArgs());
        }

        private void SliceNoneButton_Click(object sender, EventArgs e)
        {
            try
            {
                SliceMode = SliceModeEnum.None;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        private void SliceUButton_Click(object sender, EventArgs e)
        {
            try
            {
                SliceMode = SliceModeEnum.SliceU;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        private void SliceVButton_Click(object sender, EventArgs e)
        {
            try
            {
                SliceMode = SliceModeEnum.SliceV;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SliceValue

        public event EventHandler SliceValueChanged;

        private int _SliceValue;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int SliceValue
        {
            get { return _SliceValue; }
            set
            {
                if (value == _SliceValue) return;
                SetSliceValue(value);
            }
        }

        private void SetSliceValue(int value)
        {
            _SliceValue = value;
            SliceValueChanged?.Invoke(this, new EventArgs());
        }

        private void SliceValueDefaultButton_Click(object sender, EventArgs e)
        {
            try
            {
                SliceValue = 0;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        private void SliceValueDownButton_Click(object sender, EventArgs e)
        {
            try
            {
                SliceValue--;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        private void SliceValueUpButton_Click(object sender, EventArgs e)
        {
            try
            {
                SliceValue++;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewRays

        public event EventHandler ViewRaysChanged;

        private bool _ViewRays;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewRays
        {
            get { return _ViewRays; }
            set
            {
                if (value == _ViewRays) return;
                SetViewRays(value);
            }
        }

        private void SetViewRays(bool value)
        {
            _ViewRays = value;
            viewRaysButton.Checked = value;
            ViewRaysChanged?.Invoke(this, new EventArgs());
        }

        private void ViewRays_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ViewRays = viewRaysButton.Checked;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewNormals

        public event EventHandler ViewNormalsChanged;

        private bool _ViewNormals;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewNormals
        {
            get { return _ViewNormals; }
            set
            {
                if (value == _ViewNormals) return;
                SetViewNormals(value);
            }
        }

        private void SetViewNormals(bool value)
        {
            _ViewNormals = value;
            viewNormalsButton.Checked = value;
            ViewNormalsChanged?.Invoke(this, new EventArgs());
        }


        private void ViewNormals_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ViewNormals = viewNormalsButton.Checked;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public WireframeToolStrip()
        {
            InitializeComponent();
            SetSliceMode(WireframeImage.sliceModeDefault);
            SetSliceValue(WireframeImage.sliceValueDefault);
            SetViewRays(WireframeImage.viewRaysDefault);
            SetViewNormals(WireframeImage.viewNormalsDefault);
        }

        #endregion
    }
}
