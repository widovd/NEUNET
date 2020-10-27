using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Neulib.Exceptions;
using Neulib.Numerics;
using static System.Math;
using static System.Convert;

namespace Neunet.Images.Charts2D
{

    public partial class Chart2DImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region ChartBounds

        private ParamBounds _chartBounds = new ParamBounds(-1d, 1d, -1d, 1d);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public ParamBounds ChartBounds
        {
            get { return _chartBounds; }
            set { _chartBounds = value; RefreshImage(); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region LockedAxes

        private bool _lockedAxes = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool LockedAxes
        {
            get { return _lockedAxes; }
            set { _lockedAxes = value; RefreshImage(); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Origin

        public event EventHandler OriginChanged;

        private Double2 _origin = new Double2(0d, 0d);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Double2 Origin
        {
            get { return _origin; }
            set
            {
                if (Equals(value, Origin)) return;
                _origin = value;
                RefreshImage();
                OriginChanged?.Invoke(this, new EventArgs());
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Zoom

        public event EventHandler ZoomChanged;

        private const float _zoomDefault = 0.9f;
        private float _zoom = _zoomDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (value == Zoom) return;
                _zoom = value;
                RefreshImage();
                ZoomChanged?.Invoke(this, new EventArgs());
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Chart2DImage()
        {
            InitializeComponent();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        private float _centerX;
        private float _centerY;
        private float _pixelsPerUnitX = 0f;
        private float _pixelsPerUnitY = 0f;

        public override void DrawImage(Bitmap bitmap)
        {
            base.DrawImage(bitmap); // background color
            double xDelta = ChartBounds.DeltaX;
            if (xDelta <= 0d) throw new InvalidValueException(nameof(xDelta), xDelta, 866788);
            double yDelta = ChartBounds.DeltaY;
            if (yDelta <= 0d) throw new InvalidValueException(nameof(yDelta), yDelta, 644258);
            float pixelsPerUnitX = ToSingle(bitmap.Width / xDelta);
            float pixelsPerUnitY = ToSingle(bitmap.Height / yDelta);
            if (LockedAxes)
            {
                if (bitmap.Width * yDelta < bitmap.Height * xDelta)
                    pixelsPerUnitY = pixelsPerUnitX;
                else
                    pixelsPerUnitX = pixelsPerUnitY;
            }
            _pixelsPerUnitX = pixelsPerUnitX;
            _pixelsPerUnitY = pixelsPerUnitY;
            _centerX = bitmap.Width / 2f;
            _centerY = bitmap.Height / 2f;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region View3DImage mouse

        private Double2 _mouseDownOrigin;

        protected override void ImageMouseDown(int x0, int y0, MouseButtons buttons)
        {
            base.ImageMouseDown(x0, y0, buttons);
            _mouseDownOrigin = Origin;
        }

        protected void PictureMoving(int x0, int y0, int dx, int dy)
        {
            float zoom = Zoom;
            Double2 moveVector = new Double2(-dx / (zoom * _pixelsPerUnitX), dy / (zoom * _pixelsPerUnitY));
            Origin = _mouseDownOrigin + moveVector;
        }

        protected override void ImageMouseMove(int x0, int y0, int dx, int dy, MouseButtons buttons)
        {
            switch (buttons)
            {
                case MouseButtons.Right:
                    PictureMoving(x0, y0, dx, dy);
                    break;
            }
        }

        protected override void ImageMouseWheel(int delta)
        {
            const bool _swapMouseWheel = true;
            const float zoomFactor = 1.1f;
            bool b = delta < 0;
            if (b ^ _swapMouseWheel)
                Zoom *= zoomFactor;
            else
                Zoom /= zoomFactor;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Draw

        public void ResetOriginAndZoom()
        {
            try
            {
                SuspendImage();
                Zoom = _zoomDefault;
                Origin = ChartBounds.Center;
            }
            finally
            {
                ResumeImage();
            }
        }

        public virtual bool Transform(Double2 p, out Double2 q)
        {
            q = p;
            return true;
        }

        public virtual bool TransformInv(Double2 q, out Double2 p)
        {
            p = q;
            return true;
        }

        public PointF Project(Double2 coord)
        {
            float x = _centerX + (_zoom * _pixelsPerUnitX * (float)(coord.X - _origin.X));
            float y = _centerY - (_zoom * _pixelsPerUnitY * (float)(coord.Y - _origin.Y));
            return new PointF(x, y);
        }

        protected Double2 ProjectInv(PointF point)
        {
            double x = _origin.X + (point.X - _centerX) / (_zoom * _pixelsPerUnitX);
            double y = _origin.Y - (point.Y - _centerY) / (_zoom * _pixelsPerUnitY);
            return new Double2(x, y);
        }

        protected bool CoordToPointF(float x, float y, out PointF qq)
        {
            qq = new PointF(float.NaN, float.NaN);
            Double2 o11 = new Double2(x, y);
            if (!TransformInv(o11, out Double2 p11)) return false;
            qq = Project(p11);
            return true;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Ticks

        private const double tiny = double.Epsilon; // compensate round-off errors

        public static double FloorMajor(double major, double zLo)
        {
            return Floor(zLo / major + tiny) * major;
        }

        public static double CeilingMajor(double major, double zHi)
        {
            return Ceiling(zHi / major - tiny) * major;
        }

        public static double CalculateMajor(double lo, double hi)
        {
            double a = Log10(hi - lo);
            double b = Floor(a);
            double c = a - b;
            double d = Pow(10d, b);
            double e = Pow(10d, c);
            double major;
            if (e < 2) { major = 0.2d; }
            else if (e < 5) { major = 0.5d; }
            else { major = 1.0d; }
            major *= d;
            if (double.IsNaN(major)) major = 1d;
            return major;
        }

        public static string MajorLabelFormat(double major, int minDigits = 0)
        {
            int a = ToInt32(Max(minDigits, -Floor(Log10(major))));
            return $"F{a}";
        }

        protected void RescaleAxisX(out double major)
        {
            major = CalculateMajor(ChartBounds.XLo, ChartBounds.XHi);
            double xLo = FloorMajor(major, ChartBounds.XLo);
            double xHi = CeilingMajor(major, ChartBounds.XHi);
            ChartBounds = new ParamBounds(xLo, xHi, ChartBounds.YLo, ChartBounds.YHi);
        }

        protected void RescaleAxisY(out double major)
        {
            major = CalculateMajor(ChartBounds.YLo, ChartBounds.YHi);
            double yLo = FloorMajor(major, ChartBounds.YLo);
            double yHi = CeilingMajor(major, ChartBounds.YHi);
            ChartBounds = new ParamBounds(ChartBounds.XLo, ChartBounds.XHi, yLo, yHi);
        }

        protected void RescaleAxisBoth(out double major)
        {
            double xMajor = CalculateMajor(ChartBounds.XLo, ChartBounds.XHi);
            double yMajor = CalculateMajor(ChartBounds.YLo, ChartBounds.YHi);
            major = Max(xMajor, yMajor);
            double xLo = FloorMajor(major, ChartBounds.XLo);
            double xHi = CeilingMajor(major, ChartBounds.XHi);
            double yLo = FloorMajor(major, ChartBounds.YLo);
            double yHi = CeilingMajor(major, ChartBounds.YHi);
            ChartBounds = new ParamBounds(xLo, xHi, yLo, yHi);
        }

        protected bool InRange(Double2 p)
        {
            return (ChartBounds.XLo <= p.X && p.X <= ChartBounds.XHi && ChartBounds.YLo <= p.Y && p.Y <= ChartBounds.YHi);
        }

        #endregion

    }

}

