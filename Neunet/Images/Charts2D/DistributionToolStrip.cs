using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Neunet.UserControls;
using Neulib.Exceptions;
using Neunet.Forms;

namespace Neunet.Images.Charts2D
{
    public partial class DistributionToolStrip : UserControl
    {
        // ----------------------------------------------------------------------------------------
        #region DistributionVisible

        public event EventHandler DistributionVisibleChanged;

        private bool _distributionVisible;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool DistributionVisible
        {
            get { return _distributionVisible; }
            set
            {
                if (value == _distributionVisible) return;
                SetDistributionVisible(value);
            }
        }

        private void SetDistributionVisible(bool value)
        {
            _distributionVisible = value;
            distributionVisibleButton.Checked = value;
            DistributionVisibleChanged?.Invoke(this, new EventArgs());
        }

        private void DistributionVisibleButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DistributionVisible = distributionVisibleButton.Checked;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region LinesVisible

        public event EventHandler ParametricGridVisibleChanged;

        private bool _parametricGridVisible;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ParametricGridVisible
        {
            get { return _parametricGridVisible; }
            set
            {
                if (value == _parametricGridVisible) return;
                SetParametricGridVisible(value);
            }
        }

        private void SetParametricGridVisible(bool value)
        {
            _parametricGridVisible = value;
            parametricGridVisibleButton.Checked = value;
            ParametricGridVisibleChanged?.Invoke(this, new EventArgs());
        }

        private void ParametricGridVisible_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ParametricGridVisible = parametricGridVisibleButton.Checked;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region LinesPenColor

        public event EventHandler ParametricGridPenColorChanged;

        private Color _parametricGridPenColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color ParametricGridPenColor
        {
            get { return _parametricGridPenColor; }
            set
            {
                if (value == _parametricGridPenColor) return;
                SetParametricGridPenColor(value);
            }
        }

        private void SetParametricGridPenColor(Color value)
        {
            _parametricGridPenColor = value;
            ParametricGridPenColorChanged?.Invoke(this, new EventArgs());
        }

        private void ParametricGridPenColorButton_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog.Color = ParametricGridPenColor;
                if (colorDialog.ShowDialog() != DialogResult.OK) return;
                ParametricGridPenColor = colorDialog.Color;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region LinesPenWidth

        public event EventHandler ParametricGridPenWidthChanged;

        private float _parametricGridPenWidth;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float ParametricGridPenWidth
        {
            get { return _parametricGridPenWidth; }
            set
            {
                if (value == _parametricGridPenWidth) return;
                SetParametricGridPenWidth(value);
            }
        }

        private void SetParametricGridPenWidth(float value)
        {
            _parametricGridPenWidth = value;
            _parametricGridPenWidthUpDown.Value = (decimal)value;
            ParametricGridPenWidthChanged?.Invoke(this, new EventArgs());
        }

        private void ParametricGridPenWidth_Changed(object sender, EventArgs e)
        {
            ParametricGridPenWidth = (float)_parametricGridPenWidthUpDown.Value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region PointsVisible

        public event EventHandler ParametricPointsVisibleChanged;

        private bool _parametricPointsVisible;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ParametricPointsVisible
        {
            get { return _parametricPointsVisible; }
            set
            {
                if (value == _parametricPointsVisible) return;
                SetParametricPointsVisible(value);
            }
        }

        private void SetParametricPointsVisible(bool value)
        {
            _parametricPointsVisible = value;
            parametricPointsVisible.Checked = value;
            ParametricPointsVisibleChanged?.Invoke(this, new EventArgs());
        }


        private void PointsVisibleButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ParametricPointsVisible = parametricPointsVisible.Checked;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region PointSize

        public event EventHandler ParametricPointsSizeChanged;


        private float _parametricPointsSize;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float ParametricPointsSize
        {
            get { return _parametricPointsSize; }
            set
            {
                if (value == _parametricPointsSize) return;
                SetParametricPointsSize(value);
            }
        }

        private void SetParametricPointsSize(float value)
        {
            _parametricPointsSize = value;
            parametricPointsSizeUpDown.Value = (decimal)value;
            ParametricPointsSizeChanged?.Invoke(this, new EventArgs());
        }

        private void ParametricPointsSizeUpDown_Changed(object sender, EventArgs e)
        {
            ParametricPointsSize = (float)parametricPointsSizeUpDown.Value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region PointsPen

        public event EventHandler ParametricPointsPenColorChanged;

        private Color _parametricPointsPenColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color ParametricPointsPenColor
        {
            get { return _parametricPointsPenColor; }
            set
            {
                if (value == _parametricPointsPenColor) return;
                SetParametricPointsPenColor(value);
            }
        }

        private void SetParametricPointsPenColor(Color value)
        {
            _parametricPointsPenColor = value;
            ParametricPointsPenColorChanged?.Invoke(this, new EventArgs());
        }

        private void ParametricPointsPenColorButton_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog.Color = ParametricPointsPenColor;
                if (colorDialog.ShowDialog() != DialogResult.OK) return;
                ParametricPointsPenColor = colorDialog.Color;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region PointsBrush

        public event EventHandler ParametricPointsBrushColorChanged;

        private Color _parametricPointsBrushColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color ParametricPointsBrushColor
        {
            get { return _parametricPointsBrushColor; }
            set
            {
                if (value == _parametricPointsBrushColor) return;
                SetParametricPointsBrushColor(value);
            }
        }

        private void SetParametricPointsBrushColor(Color value)
        {
            _parametricPointsBrushColor = value;
            ParametricPointsBrushColorChanged?.Invoke(this, new EventArgs());
        }

        private void ParametricPointsBrushColorButton_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog.Color = ParametricPointsBrushColor;
                if (colorDialog.ShowDialog() != DialogResult.OK) return;
                ParametricPointsBrushColor = colorDialog.Color;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IntersectionsVisible

        public event EventHandler IntersectionsVisibleChanged;

        private bool _intersectionsVisible;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IntersectionsVisible
        {
            get { return _intersectionsVisible; }
            set
            {
                if (value == _intersectionsVisible) return;
                SetIntersectionsVisible(value);
            }
        }

        private void SetIntersectionsVisible(bool value)
        {
            _intersectionsVisible = value;
            intersectionsVisibleButton.Checked = value;
            IntersectionsVisibleChanged?.Invoke(this, new EventArgs());
        }

        private void IntersectionsVisibleButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                IntersectionsVisible = intersectionsVisibleButton.Checked;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IntersectionsSize

        public event EventHandler IntersectionsPointSizeChanged;

        private float _intersectionsPointSize;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float IntersectionsPointSize
        {
            get { return _intersectionsPointSize; }
            set
            {
                if (value == _intersectionsPointSize) return;
                SetIntersectionsPointSize(value);
            }
        }

        private void SetIntersectionsPointSize(float value)
        {
            _intersectionsPointSize = value;
            _intersectionsPointSizeUpDown.Value = (decimal)value;
            IntersectionsPointSizeChanged?.Invoke(this, new EventArgs());
        }

        private void IntersectionsPointSizeUpDown_Changed(object sender, EventArgs e)
        {
            IntersectionsPointSize = (float)_intersectionsPointSizeUpDown.Value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IntersectionsPen

        public event EventHandler IntersectionsPenColorChanged;

        private Color _intersectionsPenColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color IntersectionsPenColor
        {
            get { return _intersectionsPenColor; }
            set
            {
                if (value == _intersectionsPenColor) return;
                SetIntersectionsPenColor(value);
            }
        }

        private void SetIntersectionsPenColor(Color value)
        {
            _intersectionsPenColor = value;
            IntersectionsPenColorChanged?.Invoke(this, new EventArgs());
        }

        private void IntersectionsPenColorButton_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog.Color = IntersectionsPenColor;
                if (colorDialog.ShowDialog() != DialogResult.OK) return;
                IntersectionsPenColor = colorDialog.Color;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IntersectionsBrushColor

        public event EventHandler IntersectionsBrushColorChanged;

        private Color _intersectionsBrushColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color IntersectionsBrushColor
        {
            get { return _intersectionsBrushColor; }
            set
            {
                if (value == _intersectionsBrushColor) return;
                SetIntersectionsBrushColor(value);
            }
        }

        private void SetIntersectionsBrushColor(Color value)
        {
            _intersectionsBrushColor = value;
            IntersectionsBrushColorChanged?.Invoke(this, new EventArgs());
        }

        private void IntersectionsBrushColorButton_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog.Color = IntersectionsBrushColor;
                if (colorDialog.ShowDialog() != DialogResult.OK) return;
                IntersectionsBrushColor = colorDialog.Color;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        private NumericUpDownToolStripButton _parametricGridPenWidthUpDown;
        private NumericUpDownToolStripButton parametricPointsSizeUpDown;
        private NumericUpDownToolStripButton _intersectionsPointSizeUpDown;

        public DistributionToolStrip()
        {
            InitializeComponent();
            _parametricGridPenWidthUpDown = new NumericUpDownToolStripButton(1, 5, 1, 1, ParametricGridPenWidth_Changed);
            toolStrip.Items.Insert(toolStrip.Items.IndexOf(parametricGridPenColorButton) + 1, _parametricGridPenWidthUpDown);
            parametricPointsSizeUpDown = new NumericUpDownToolStripButton(1, 10, 1, 1, ParametricPointsSizeUpDown_Changed);
            toolStrip.Items.Insert(toolStrip.Items.IndexOf(pointsBrushButton) + 1, parametricPointsSizeUpDown);
            _intersectionsPointSizeUpDown = new NumericUpDownToolStripButton(1, 10, 1, 1, IntersectionsPointSizeUpDown_Changed);
            toolStrip.Items.Insert(toolStrip.Items.IndexOf(intersectionsBrushButton) + 1, _intersectionsPointSizeUpDown);
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
                if (_parametricGridPenWidthUpDown != null) { _parametricGridPenWidthUpDown.Dispose(); _parametricGridPenWidthUpDown = null; }
                if (parametricPointsSizeUpDown != null) { parametricPointsSizeUpDown.Dispose(); parametricPointsSizeUpDown = null; }
                if (_intersectionsPointSizeUpDown != null) { _intersectionsPointSizeUpDown.Dispose(); _intersectionsPointSizeUpDown = null; }
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
