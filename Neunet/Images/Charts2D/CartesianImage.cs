using System.Drawing;
using Neulib.Numerics;
using System.ComponentModel;

namespace Neunet.Images.Charts2D
{
    public class CartesianImage : Chart2DImage
    {
        // ----------------------------------------------------------------------------------------
        #region Grid

        private bool _viewMajorGrid = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewMajorGrid
        {
            get { return _viewMajorGrid; }
            set
            {
                if (value == ViewMajorGrid) return;
                _viewMajorGrid = value;
                RefreshImage();
            }
        }

        private const bool _viewMinorGridDefault = true;
        private bool _viewMinorGrid = _viewMinorGridDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewMinorGrid
        {
            get { return _viewMinorGrid; }
            set
            {
                if (value == ViewMinorGrid) return;
                _viewMinorGrid = value;
                RefreshImage();
            }
        }

        private static readonly Color _majorGridLineColorDefault = Color.Black;
        private Color _majorGridLineColor = _majorGridLineColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color MajorGridLineColor
        {
            get { return _majorGridLineColor; }
            set
            {
                if (value == MajorGridLineColor) return;
                _majorGridLineColor = value;
                RefreshImage();
            }
        }

        private static readonly Color _minorGridLineColorDefault = Color.LightGray;
        private Color _minorGridLineColor = _minorGridLineColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color MinorGridLineColor
        {
            get { return _minorGridLineColor; }
            set
            {
                if (value == MinorGridLineColor) return;
                _minorGridLineColor = value;
                RefreshImage();
            }
        }

        private const float _majorGridLineWidthDefault = 1f;
        private float _majorGridLineWidth = _majorGridLineWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float MajorGridLineWidth
        {
            get { return _majorGridLineWidth; }
            set
            {
                if (value == MajorGridLineWidth) return;
                _majorGridLineWidth = value;
                RefreshImage();
            }
        }

        private const float _minorGridLineWidthDefault = 1f;
        private float _minorGridLineWidth = _minorGridLineWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float MinorGridLineWidth
        {
            get { return _minorGridLineWidth; }
            set
            {
                if (value == MinorGridLineWidth) return;
                _minorGridLineWidth = value;
                RefreshImage();
            }
        }

        private const double _majorGridUnitXDefault = 1d;
        private double _majorGridUnitX = _majorGridUnitXDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public double MajorGridUnitX
        {
            get { return _majorGridUnitX; }
            set
            {
                if (value == MajorGridUnitX) return;
                _majorGridUnitX = value;
                RefreshImage();
            }
        }

        private const double _minorGridUnitXDefault = 0.2d;
        private double _minorGridUnitX = _minorGridUnitXDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public double MinorGridUnitX
        {
            get { return _minorGridUnitX; }
            set
            {
                if (value == MinorGridUnitX) return;
                _minorGridUnitX = value;
                RefreshImage();
            }
        }

        private const double _majorGridUnitYDefault = 1d;
        private double _majorGridUnitY = _majorGridUnitYDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public double MajorGridUnitY
        {
            get { return _majorGridUnitY; }
            set
            {
                if (value == MajorGridUnitY) return;
                _majorGridUnitY = value;
                RefreshImage();
            }
        }

        private const double _minorGridUnitYDefault = 0.2d;
        private double _minorGridUnitY = _minorGridUnitYDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public double MinorGridUnitY
        {
            get { return _minorGridUnitY; }
            set
            {
                if (value == MinorGridUnitY) return;
                _minorGridUnitY = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Axes

        private const bool _viewAxesDefault = true;
        private bool _viewAxes = _viewAxesDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewAxes
        {
            get { return _viewAxes; }
            set
            {
                if (value == ViewAxes) return;
                _viewAxes = value;
                RefreshImage();
            }
        }

        private static readonly Color _majorTickColorDefault = Color.Black;
        private Color _majorTickColor = _majorTickColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color MajorTickColor
        {
            get { return _majorTickColor; }
            set
            {
                if (value == MajorTickColor) return;
                _majorTickColor = value;
                RefreshImage();
            }
        }


        private static readonly Color _axesPenColorDefault = Color.Black;
        private Color _axesPenColor = _axesPenColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color AxesPenColor
        {
            get { return _axesPenColor; }
            set
            {
                if (value == AxesPenColor) return;
                _axesPenColor = value;
                RefreshImage();
            }
        }

        private const float _axesPenWidthDefault = 1f;
        private float _axesPenWidth = _axesPenWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float AxesPenWidth
        {
            get { return _axesPenWidth; }
            set
            {
                if (value == AxesPenWidth) return;
                _axesPenWidth = value;
                RefreshImage();
            }
        }

        private static readonly Color _axesTickColorDefault = Color.Black;
        private Color _axesTickColor = _axesTickColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color AxesTickColor
        {
            get { return _axesTickColor; }
            set
            {
                if (value == AxesTickColor) return;
                _axesTickColor = value;
                RefreshImage();
            }
        }

        private const float _axesTickWidthDefault = 1f;
        private float _axesTickWidth = _axesTickWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float AxesTickWidth
        {
            get { return _axesTickWidth; }
            set
            {
                if (value == AxesTickWidth) return;
                _axesTickWidth = value;
                RefreshImage();
            }
        }

        private static readonly Color _axesTextColorDefault = Color.Black;
        private Color _axesTextColor = _axesTextColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color AxesTextColor
        {
            get { return _axesTextColor; }
            set
            {
                if (value == AxesTextColor) return;
                _axesTextColor = value;
                RefreshImage();
            }
        }

        private const string _xAxisTextDefault = "X-axis";
        private string _xAxisText = _xAxisTextDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string XAxisText
        {
            get { return _xAxisText; }
            set
            {
                if (value == _xAxisText) return;
                _xAxisText = value;
                RefreshImage();
            }
        }

        private const string _yAxisTextDefault = "Y-axis";
        private string _yAxisText = _yAxisTextDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string YAxisText
        {
            get { return _yAxisText; }
            set
            {
                if (value == _yAxisText) return;
                _yAxisText = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public CartesianImage() : base()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        public override void DrawImage(Bitmap bitmap)
        {
            base.DrawImage(bitmap);
            //DrawGridLines(bitmap);
            //if (ViewAxes) DrawAxes(bitmap);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Chart2DImage

        #endregion
        // ----------------------------------------------------------------------------------------
        #region CartesianImage

        private void DrawGridLines(Graphics graphics, Pen pen, double dx, double dy)
        {
            PointF p1, p2;
            // Vertical grid lines
            double xLo = ChartBounds.XLo, xHi = ChartBounds.XHi;
            double yLo = ChartBounds.YLo, yHi = ChartBounds.YHi;
            for (double x = xLo; x <= xHi; x += dx)
            {
                p1 = Project(new Double2(x, yLo));
                p2 = Project(new Double2(x, yHi));
                graphics.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
            }
            // Horizontal grid lines
            for (double y = yLo; y <= yHi; y += dy)
            {
                p1 = Project(new Double2(xLo, y));
                p2 = Project(new Double2(xHi, y));
                graphics.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
            }
        }

        protected void DrawGridLines(Bitmap bitmap)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                if (ViewMinorGrid)
                    using (Pen pen = new Pen(MinorGridLineColor, MinorGridLineWidth))
                    {
                        DrawGridLines(graphics, pen, MinorGridUnitX, MinorGridUnitY);
                    }
                if (ViewMajorGrid)
                    using (Pen pen = new Pen(MajorGridLineColor, MajorGridLineWidth))
                    {
                        DrawGridLines(graphics, pen, MajorGridUnitX, MajorGridUnitY);
                    }
            }
        }

        private void DrawHorizontalAxis(Graphics graphics)
        {
            double xLo = ChartBounds.XLo, xHi = ChartBounds.XHi;
            double yLo = ChartBounds.YLo, yHi = ChartBounds.YHi;
            double yPos = yLo; // YPositionXAxis;
            PointF p1, p2;
            // Horizontal axis
            p1 = Project(new Double2(xLo, yPos));
            p2 = Project(new Double2(xHi, yPos));
            using (Pen axisPen = new Pen(AxesPenColor, AxesPenWidth))
                graphics.DrawLine(axisPen, p1.X, p1.Y, p2.X, p2.Y);
            // Horizontal axis minor units
            double dx = MinorGridUnitX;
            using (Pen tickPen = new Pen(AxesTickColor, AxesTickWidth))
                for (double x = xLo; x <= xHi; x += dx)
                {
                    p2 = Project(new Double2(x, yPos));
                    graphics.DrawLine(tickPen, p2.X, p2.Y - 2, p2.X, p2.Y + 2);
                }
            // Horizontal axis major labels
            string labelFormat = MajorLabelFormat(MajorGridUnitX);
            using (SolidBrush brush = new SolidBrush(MajorTickColor))
            using (StringFormat stringFormat = new StringFormat()
            { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near })
                for (double x = xLo; x <= xHi; x += MajorGridUnitX)
                {
                    if (x == 0d) continue;
                    p2 = Project(new Double2(x, yPos));
                    {
                        graphics.DrawString(x.ToString(labelFormat), DefaultFont, brush, p2.X, p2.Y + 3, stringFormat);
                    }
                }
            // Text near horizontal axis
            bool reverseAxisX = false;
            using (SolidBrush brush = new SolidBrush(AxesTextColor))
            {
                p2 = Project(new Double2(xHi, yPos));
                using (StringFormat stringFormat = new StringFormat()
                { Alignment = reverseAxisX ? StringAlignment.Near : StringAlignment.Far, LineAlignment = StringAlignment.Near })
                {
                    if (!string.IsNullOrEmpty(XAxisText))
                        graphics.DrawString(XAxisText, DefaultFont, brush, p2.X, p2.Y + 16, stringFormat);
                }
            }
        }

        private void DrawVerticalAxis(Graphics graphics)
        {
            double xLo = ChartBounds.XLo, xHi = ChartBounds.XHi;
            double yLo = ChartBounds.YLo, yHi = ChartBounds.YHi;
            double xPos = xLo; // XPositionYAxis;
            PointF p1, p2;
            // Vertical axis
            p1 = Project(new Double2(xPos, yLo));
            p2 = Project(new Double2(xPos, yHi));
            using (Pen axisPen = new Pen(AxesPenColor, AxesPenWidth))
                graphics.DrawLine(axisPen, p1.X, p1.Y, p2.X, p2.Y);
            // Vertical axis minor units
            double dy = MinorGridUnitY;
            using (Pen tickPen = new Pen(AxesTickColor, AxesTickWidth))
                for (double y = yLo; y <= yHi; y += dy)
                {
                    p2 = Project(new Double2(xPos, y));
                    graphics.DrawLine(tickPen, p2.X - 2, p2.Y, p2.X + 2, p2.Y);
                }
            // Vertical axis major labels
            string labelFormat = MajorLabelFormat(MajorGridUnitY);
            using (SolidBrush brush = new SolidBrush(MajorTickColor))
            using (StringFormat stringFormat = new StringFormat()
            { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                for (double y = yLo; y <= yHi; y += MajorGridUnitY)
                {
                    if (y == 0d) continue;
                    p2 = Project(new Double2(xPos, y));
                    {
                        graphics.DrawString(y.ToString(labelFormat), DefaultFont, brush, p2.X - 2, p2.Y, stringFormat);
                    }
                }
            // Text near vertical axis
            bool reverseAxisY = false;
            using (SolidBrush brush = new SolidBrush(AxesTextColor))
            {
                p2 = Project(new Double2(xPos, yHi));
                using (StringFormat stringFormat = new StringFormat(StringFormatFlags.DirectionVertical)
                { Alignment = reverseAxisY ? StringAlignment.Far : StringAlignment.Near, LineAlignment = StringAlignment.Far })
                {
                    if (!string.IsNullOrEmpty(YAxisText))
                        graphics.DrawString(YAxisText, DefaultFont, brush, p2.X - 16, p2.Y, stringFormat);
                }
            }
        }

        protected void DrawAxes(Bitmap bitmap)
        {
            if (!ViewAxes) return;
            using (var graphics = Graphics.FromImage(bitmap))
            {
                DrawHorizontalAxis(graphics);
                DrawVerticalAxis(graphics);
            }
        }

        #endregion
    }
}
