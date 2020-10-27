using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using Neulib.Exceptions;
using Neunet.Forms;
using Neunet.Serializers;
using Neulib.Extensions;

namespace Neunet.Images.Charts3D
{
    public partial class Chart3DToolStrip : UserControl
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewGrid

        public event EventHandler ViewGridChanged;

        private bool _viewGrid;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewGrid
        {
            get { return _viewGrid; }
            set
            {
                if (value == _viewGrid) return;
                SetViewGrid(value);
            }
        }

        private void SetViewGrid(bool value)
        {
            _viewGrid = value;
            viewGridButton.Checked = value;
            ViewGridChanged?.Invoke(this, new EventArgs());
        }

        private void ViewGridButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ViewGrid = viewGridButton.Checked;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewAxes

        public event EventHandler ViewAxesChanged;

        private bool _viewAxes;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewAxes
        {
            get { return _viewAxes; }
            set
            {
                if (value == _viewAxes) return;
                SetViewAxes(value);
            }
        }

        private void SetViewAxes(bool value)
        {
            _viewAxes = value;
            viewAxesButton.Checked = value;
            ViewAxesChanged?.Invoke(this, new EventArgs());
        }

        private void ViewAxesButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ViewAxes = viewAxesButton.Checked;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewOrigin

        public event EventHandler ViewOriginChanged;

        private bool _viewOrigin;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewOrigin
        {
            get { return _viewOrigin; }
            set
            {
                if (value == _viewOrigin) return;
                SetViewOrigin(value);
            }
        }

        private void SetViewOrigin(bool value)
        {
            _viewOrigin = value;
            viewOriginButton.Checked = value;
            ViewOriginChanged?.Invoke(this, new EventArgs());
        }


        private void ViewOriginButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ViewOrigin = viewOriginButton.Checked;
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Orientation

        public event EventHandler OrientationChanged;

        private View3DOrientationEnum _orientation;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public View3DOrientationEnum Orientation
        {
            get { return _orientation; }
            set
            {
                if (value == _orientation) return;
                SetOrientation(value);
            }
        }

        private void SetOrientation(View3DOrientationEnum value)
        {
            _orientation = value;
            projectionToolStripButton.Image = Orientation switch
            {
                View3DOrientationEnum.XZ => Properties.Resources.zxProjection,
                View3DOrientationEnum.YX => Properties.Resources.xyProjection,
                View3DOrientationEnum.ZY => Properties.Resources.yzProjection,
                View3DOrientationEnum.ZX => Properties.Resources.xzProjection,
                View3DOrientationEnum.YZ => Properties.Resources.zyProjection,
                View3DOrientationEnum.XY => Properties.Resources.yxProjection,
                _ => throw new InvalidCaseException(nameof(Orientation), Orientation, 891466),
            };
            OrientationChanged?.Invoke(this, new EventArgs());
        }

        private void XY_Orientation_Click(object sender, EventArgs e)
        {
            try
            {
                Orientation = View3DOrientationEnum.XY;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void YX_Orientation_Click(object sender, EventArgs e)
        {
            try
            {
                Orientation = View3DOrientationEnum.YX;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void YZ_Orientation_Click(object sender, EventArgs e)
        {
            try
            {
                Orientation = View3DOrientationEnum.YZ;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void ZY_Orientation_Click(object sender, EventArgs e)
        {
            try
            {
                Orientation = View3DOrientationEnum.ZY;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void ZX_Orientation_Click(object sender, EventArgs e)
        {
            try
            {
                Orientation = View3DOrientationEnum.ZX;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void XZ_Orientation_Click(object sender, EventArgs e)
        {
            try
            {
                Orientation = View3DOrientationEnum.XZ;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region RepeatOrientation

        public event EventHandler RepeatOrientationClicked;

        private void RepeatOrientation_Click(object sender, EventArgs e)
        {
            try
            {
                RepeatOrientationClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Swap

        public event EventHandler SwapLeftRightClicked;

        private void SwapLeftRightButton_Click(object sender, EventArgs e)
        {
            try
            {
                SwapLeftRightClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler SwapFrontRearClicked;

        private void SwapFrontRearButton_Click(object sender, EventArgs e)
        {
            try
            {
                SwapFrontRearClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler SwapTopBottomClicked;

        private void SwapTopBottomButton_Click(object sender, EventArgs e)
        {
            try
            {
                SwapTopBottomClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler SwapAxesClicked;

        private void SwapAxesButton_Click(object sender, EventArgs e)
        {
            try
            {
                SwapAxesClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Zoom

        public event EventHandler ResetZoomClicked;

        private void ResetZoomButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetZoomClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler ZoomOutClicked;

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            try
            {
                ZoomOutClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler ZoomInClicked;

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            try
            {
                ZoomInClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Perspective

        public event EventHandler ResetPerspectiveClicked;

        private void ResetPerspectiveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetPerspectiveClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler DecreasePerspectiveClicked;

        private void DecreasePerspectiveButton_Click(object sender, EventArgs e)
        {
            try
            {
                DecreasePerspectiveClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        public event EventHandler IncreasePerspectiveClicked;

        private void IncreasePerspectiveButton_Click(object sender, EventArgs e)
        {
            try
            {
                IncreasePerspectiveClicked?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex) { ExceptionDialog.Show(ex); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Chart3DToolStrip()
        {
            InitializeComponent();
            SetViewGrid(Chart3DImage.viewGridDefault);
            SetViewAxes(Chart3DImage.viewAxesDefault);
            SetViewOrigin(Chart3DImage.viewOriginDefault);
            SetOrientation(Chart3DImage.orientationDefault);
        }

        #endregion
    }
}
