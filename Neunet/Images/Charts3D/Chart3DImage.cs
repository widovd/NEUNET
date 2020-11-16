using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using Neunet.Attributes;
using Neulib.Numerics;
using Neulib.Extensions;
using static System.Math;
using static System.Convert;

namespace Neunet.Images.Charts3D
{
    public enum View3DOrientationEnum
    {
        [ShowName("XZ"), XmlText("XZ"), Description("XZ")] XZ,
        [ShowName("YX"), XmlText("YX"), Description("YX")] YX,
        [ShowName("ZY"), XmlText("ZY"), Description("ZY")] ZY,
        [ShowName("ZX"), XmlText("ZX"), Description("ZX")] ZX,
        [ShowName("YZ"), XmlText("YZ"), Description("YZ")] YZ,
        [ShowName("XY"), XmlText("XY"), Description("XY")] XY,
    }

    public partial class Chart3DImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Grid properties

        public const bool viewGridDefault = true;
        private bool _viewGrid = viewGridDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// Enable the grid.
        /// </summary>
        public bool ViewGrid
        {
            get { return _viewGrid; }
            set
            {
                if (value == ViewGrid) return;
                _viewGrid = value;
                RefreshImage();
            }
        }

        public static readonly Color majorGridLineColorDefault = Color.Black;
        private Color _majorGridLineColor = majorGridLineColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The color of the major grid lines.
        /// </summary>
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

        public static readonly Color minorGridLineColorDefault = Color.LightGray;
        private Color _minorGridLineColor = minorGridLineColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The color of the minor grid lines.
        /// </summary>
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

        public const float majorGridLineWidthDefault = 1f;
        private float _majorGridLineWidth = majorGridLineWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The width of the major grid lines.
        /// </summary>
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

        public const float minorGridLineWidthDefault = 1f;
        private float _minorGridLineWidth = minorGridLineWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The width of the minor grid lines.
        /// </summary>
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

        public static readonly Color gridTextColorDefault = Color.Black;
        private Color _gridTextColor = gridTextColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The color of the grid text.
        /// </summary>
        public Color GridTextColor
        {
            get { return _gridTextColor; }
            set
            {
                if (value == GridTextColor) return;
                _gridTextColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region XYZ-Axes properties

        public const bool viewAxesDefault = true;
        private bool _viewAxes = viewAxesDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// Enables viewing the xyz-axes.
        /// </summary>
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

        public const float xyzAxisPenWidthDefault = 2f;
        private float _xyzAxisPenWidth = xyzAxisPenWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The pen width of the xyz-axes.
        /// </summary>
        public float XYZAxisPenWidth
        {
            get { return _xyzAxisPenWidth; }
            set
            {
                if (value == XYZAxisPenWidth) return;
                _xyzAxisPenWidth = value;
                RefreshImage();
            }
        }

        public const float xyzAxisCapSizeDefault = 5f;
        private float _xyzAxisCapSize = xyzAxisCapSizeDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The cap size of the xyz-axes.
        /// </summary>
        public float XYZAxisCapSize
        {
            get { return _xyzAxisCapSize; }
            set
            {
                if (value == XYZAxisCapSize) return;
                _xyzAxisCapSize = value;
                RefreshImage();
            }
        }

        public const float xyzAxisLengthDefault = 1f;
        private float _xyzAxisLength = xyzAxisLengthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The length of the xyz-axes.
        /// </summary>
        public float XYZAxisLength
        {
            get { return _xyzAxisLength; }
            set
            {
                if (value == XYZAxisLength) return;
                _xyzAxisLength = value;
                RefreshImage();
            }
        }

        public static readonly Color xAxisColorDefault = Color.Red;
        private Color _xAxisColor = xAxisColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The color of the x-axis.
        /// </summary>
        public Color XAxisColor
        {
            get { return _xAxisColor; }
            set
            {
                if (value == XAxisColor) return;
                _xAxisColor = value;
                RefreshImage();
            }
        }

        public static readonly Color yAxisColorDefault = Color.Green;
        private Color _yAxisColor = yAxisColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The color of the y-axis.
        /// </summary>
        public Color YAxisColor
        {
            get { return _yAxisColor; }
            set
            {
                if (value == YAxisColor) return;
                _yAxisColor = value;
                RefreshImage();
            }
        }

        public static readonly Color zAxisColorDefault = Color.Blue;
        private Color _zAxisColor = zAxisColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The color of the z-axis.
        /// </summary>
        public Color ZAxisColor
        {
            get { return _zAxisColor; }
            set
            {
                if (value == ZAxisColor) return;
                _zAxisColor = value;
                RefreshImage();
            }
        }

        public const bool viewOriginDefault = true;
        private bool _viewOrigin = viewOriginDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// Enables viewing the origin which is the center point of rotation.
        /// </summary>
        public bool ViewOrigin
        {
            get { return _viewOrigin; }
            set
            {
                if (value == ViewOrigin) return;
                _viewOrigin = value;
                RefreshImage();
            }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Origin

        public static readonly Single3 originDefault = Single3.Zero;
        private Single3 _origin = originDefault;
        /// <summary>
        /// The rotation point and origin of the 3D image which is located in the center of the control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Single3 Origin
        {
            get { return _origin; }
            set
            {
                if (Equals(value, Origin)) return;
                if (Single3.IsNaN(value)) return;
                SetOrigin(value);
            }
        }

        public event EventHandler OriginChanged;

        private void SetOrigin(Single3 value)
        {
            _origin = value;
            RefreshImage();
            OriginChanged?.Invoke(this, new EventArgs());
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region RotMat

        public static readonly Single3x3 rotMatDefault = new Single3x3(1);
        private Single3x3 _rotMat = rotMatDefault;
        /// <summary>
        /// The rotation matrix for the 3D image. This matrix is changed by mouse movements.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Single3x3 RotMat
        {
            get { return _rotMat; }
            set
            {
                if (Equals(value, RotMat)) return;
                if (Single3x3.IsNaN(RotMat)) return;
                SetRotMat(value);
            }
        }

        public event EventHandler RotMatChanged;

        private void SetRotMat(Single3x3 value)
        {
            _rotMat = value.Orthogonalize(new Single3x3(1));
            RefreshImage();
            RotMatChanged?.Invoke(this, new EventArgs());
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Imaging properties

        private static readonly Single3x3 _swapAxesMatrix = new Single3x3(0, 0, 1f, 0, 1f, 0, 1f, 0, 0);

        public const View3DOrientationEnum orientationDefault = View3DOrientationEnum.ZX;
        private View3DOrientationEnum _orientation = orientationDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// Sets the orientation of the xyz-axes with respect to the control, or gets the last applied orientation.
        /// </summary>
        public View3DOrientationEnum Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                switch (Orientation)
                {
                    case View3DOrientationEnum.XZ:
                        RotMat = new Single3x3(0f, 0f, 1f, 0f, 1f, 0f, 1f, 0f, 0f);
                        break;
                    case View3DOrientationEnum.YX:
                        RotMat = new Single3x3(1f, 0f, 0f, 0f, 0f, 1f, 0f, 1f, 0f);
                        break;
                    case View3DOrientationEnum.ZY:
                        RotMat = new Single3x3(0f, 1f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
                        break;
                    case View3DOrientationEnum.ZX:
                        RotMat = _swapAxesMatrix * new Single3x3(0f, 0f, 1f, 0f, 1f, 0f, 1f, 0f, 0f);
                        break;
                    case View3DOrientationEnum.YZ:
                        RotMat = _swapAxesMatrix * new Single3x3(0f, 1f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
                        break;
                    case View3DOrientationEnum.XY:
                        RotMat = _swapAxesMatrix * new Single3x3(1f, 0f, 0f, 0f, 0f, 1f, 0f, 1f, 0f);
                        break;
                }
            }
        }

        public const float perspectiveDefault = 0f;
        private float _perspective = perspectiveDefault; // 0d ~ 1d: 0 = flat
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The perspective: 0 ~ 1. If this value is 0 then the image has no perspective (i.e. flat).
        /// </summary>
        public float Perspective
        {
            get { return _perspective; }
            set
            {
                if (value == Perspective) return;
                _perspective = value.Clip(0f, 1f);
                RefreshImage();
            }
        }

        public const float perspectiveStepDefault = 0.1f;
        private float _perspectiveStep = perspectiveStepDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// Perspective is changed by increments or decrements of this value: 0 ~ 1.
        /// </summary>
        public float PerspectiveStep
        {
            get { return _perspectiveStep; }
            set { _perspectiveStep = value.Clip(0f, 1f); }
        }

        public virtual float BiggestSize
        {
            get { return float.NaN; }
        }

        public const float zoomDefault = 1f;
        private float _zoom = zoomDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The zoom value.
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (value == Zoom) return;
                if (float.IsNaN(value)) return;
                SetZoom(value);
            }
        }

        public event EventHandler ZoomChanged;

        private void SetZoom(float value)
        {
            _zoom = Max(0f, value);
            RefreshImage();
            ZoomChanged?.Invoke(this, new EventArgs());
        }


        public const float zoomFactorDefault = 1.1f;
        private float _zoomFactor = zoomFactorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// The zoom value is changed by multiplications or divisions of this value: > 1.
        /// </summary>
        public float ZoomFactor
        {
            get { return _zoomFactor; }
            set { _zoomFactor = Max(1.001f, value); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Mouse properties

        //False: Left = Rotate, Right = Move
        public const bool swapMouseButtonsDefault = false;
        private bool _swapMouseButtons = swapMouseButtonsDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// Swap the function of the left and right mouse buttons.
        /// </summary>
        public bool SwapMouseButtons
        {
            get { return _swapMouseButtons; }
            set { _swapMouseButtons = value; }
        }

        public const bool swapMouseWheelDefault = false;
        private bool _swapMouseWheel = swapMouseWheelDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        /// <summary>
        /// Swap the function of moving the mouse wheel up or down.
        /// </summary>
        public bool SwapMouseWheel
        {
            get { return _swapMouseWheel; }
            set { _swapMouseWheel = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors


        public Chart3DImage()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Cleans up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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

        private float _centerX = 0f;
        private float _centerY = 0f;

        public override void DrawImage(Bitmap bufferBitmap)
        {
            base.DrawImage(bufferBitmap);
            float zoom = Zoom;
            float biggestSize = BiggestSize;
            if (float.IsNaN(zoom) || zoom <= 0f || float.IsNaN(biggestSize) || biggestSize <= 0d)
            {
                //DrawMessage($"Zoom = {zoom}; BiggestSize = {biggestSize}");
                return;
            }
            RotMat = RotMat.Orthogonalize(new Single3x3(1));
            _centerX = bufferBitmap.Width / 2f;
            _centerY = bufferBitmap.Height / 2f;
            if (ViewGrid) DrawGrid(bufferBitmap);
            if (ViewAxes) DrawAxes(bufferBitmap);
            if (ViewOrigin) DrawOrigin(bufferBitmap);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ResetImage

        #endregion
        // ----------------------------------------------------------------------------------------
        #region View3DToolStrip

        /// <summary>
        /// True if Origin can be set to the default value.
        /// </summary>
        [Browsable(false)]
        public virtual bool CanResetOrigin
        {
            get { return true; }
        }

        /// <summary>
        /// Sets Origin to the default value.
        /// </summary>
        public virtual void ResetOrigin()
        {
            Origin = originDefault;
        }

        /// <summary>
        /// True if Zoom can be set to the default value.
        /// </summary>
        [Browsable(false)]
        public virtual bool CanResetZoom
        {
            get { return true; }
        }

        /// <summary>
        /// Resets Zoom to a value such that the image just fits into the PictureBox bitmap.
        /// </summary>
        public virtual void ResetZoom(float fit)
        {
            Zoom = zoomDefault;
        }

        [Browsable(false)]
        public virtual bool CanZoomIn
        {
            get { return true; }
        }

        public void ZoomIn()
        {
            Zoom *= ZoomFactor;
        }

        [Browsable(false)]
        public virtual bool CanZoomOut
        {
            get { return true; }
        }

        public void ZoomOut()
        {
            Zoom /= ZoomFactor;
        }

        [Browsable(false)]
        public virtual bool CanResetPerspective
        {
            get { return true; }
        }

        public void ResetPerspective()
        {
            Perspective = perspectiveDefault;
        }

        [Browsable(false)]
        public virtual bool CanDecreasePerspective
        {
            get { return Perspective > 0d; }
        }

        public void DecreasePerspective()
        {
            if (Perspective > 0d) Perspective -= PerspectiveStep;
        }

        [Browsable(false)]
        public virtual bool CanIncreasePerspective
        {
            get { return Perspective < 1d; }
        }

        public void IncreasePerspective()
        {
            if (Perspective < 1d) Perspective += PerspectiveStep;
        }

        public void ImageZoomIn()
        {
            bool shift = ModifierKeys == Keys.Shift;
            if (shift)
                DecreasePerspective();
            else
                ZoomIn();
        }

        public void ImageZoomOut()
        {
            bool shift = ModifierKeys == Keys.Shift;
            if (shift)
                IncreasePerspective();
            else
                ZoomOut();
        }

        protected override void ImageMouseWheel(int delta)
        {
            bool b = delta > 0;
            if (b ^ SwapMouseWheel)
                ImageZoomIn();
            else
                ImageZoomOut();
        }

        [Browsable(false)]
        public virtual bool CanChangeRotMat
        {
            get { return true; }
        }

        public void SwapLeftRight()
        {
            RotMat = new Single3x3(-1, 0, 0, 0, 1, 0, 0, 0, 1) * RotMat;
        }

        public void SwapFrontRear()
        {
            RotMat = new Single3x3(1, 0, 0, 0, -1, 0, 0, 0, 1) * RotMat;
        }

        public void SwapTopBottom()
        {
            RotMat = new Single3x3(1, 0, 0, 0, 1, 0, 0, 0, -1) * RotMat;
        }

        public void SwapAxes()
        {
            RotMat = _swapAxesMatrix * RotMat;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region View3DImage mouse

        private Single3 _mouseDownOrigin;
        private Single3x3 _mouseDownRotMat;
        private Single3x3 _mouseDownInvMat;

        protected override void ImageMouseDown(int x0, int y0, MouseButtons buttons)
        {
            base.ImageMouseDown(x0, y0, buttons);
            _mouseDownOrigin = Origin;
            _mouseDownRotMat = RotMat;
            _mouseDownInvMat = _mouseDownRotMat.Inverse();
        }

        protected void PictureRotating(int x0, int y0, int dx, int dy)
        {
            int x1 = x0 + dx;
            int y1 = y0 + dy;
            double w2 = pictureBox.Width / 2d;
            double h2 = pictureBox.Height / 2d;
            double dx1 = ToDouble(x1 - w2);
            double dy1 = ToDouble(y1 - h2);
            double dx0 = ToDouble(x0 - w2);
            double dy0 = ToDouble(y0 - h2);
            double wh = Min(w2, h2);
            double a = (Atan(dx1 / wh) - Atan(dx0 / wh)).ToDegrees();
            double b = (Atan(dy1 / wh) - Atan(dy0 / wh)).ToDegrees();
            Single3x3 RotZ = new Single3x3(new Single3(0f, 0f, 1f), ToSingle(a));
            Single3x3 RotX = new Single3x3(new Single3(1f, 0f, 0f), ToSingle(b));
            RotMat = RotX * (RotZ * _mouseDownRotMat);
        }

        protected void PictureMoving(int x0, int y0, int dx, int dy)
        {
            float zoom = Zoom;
            Single3 moveVector = _mouseDownInvMat * (new Single3(-dx / zoom, 0, dy / zoom));
            Origin = _mouseDownOrigin + moveVector;
        }

        protected override void ImageMouseMove(int x0, int y0, int dx, int dy, MouseButtons buttons)
        {
            switch (buttons)
            {
                case MouseButtons.Left:
                    if (SwapMouseButtons) PictureMoving(x0, y0, dx, dy); else PictureRotating(x0, y0, dx, dy);
                    break;
                case MouseButtons.Right:
                    if (SwapMouseButtons) PictureRotating(x0, y0, dx, dy); else PictureMoving(x0, y0, dx, dy);
                    break;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Draw

        public PointF Project(Single3 point)
        {
            PointF result = new PointF(float.NaN, float.NaN); // Default value
            if (!Single3.IsNaN(point))
            {
                float zoom = Zoom;
                if (!float.IsNaN(zoom) && zoom > 0d)
                {
                    float biggestSize = BiggestSize;
                    if (!float.IsNaN(biggestSize) && biggestSize > 0d)
                    {
                        Single3 p2 = RotMat * (point - Origin); // 9 multiplications, 6 additions
                        if (Perspective > 0d)
                        {
                            float b = Perspective * p2.Y / biggestSize;
                            zoom /= (1f + b);
                        }
                        result = new PointF(ToSingle(_centerX + zoom * p2.X), ToSingle(_centerY - zoom * p2.Z)); // 2 multiplications, 2 additions
                    }
                }
            }
            return result;
        }

        private float GetDelta()
        {
            double a = Log10(ToDouble(Zoom));
            double b = Floor(a);
            double c = a - b;
            double d = Pow(10, c); // <1~10]
            if (d <= 2d) d *= 5d;
            else if (d <= 5d) d *= 2d;
            else if (d <= 10d) d *= 1d;
            return (float)d;
        }

        protected float UnitVectorLength
        {
            get { return 10f * GetDelta() / Zoom; }
        }

        private void DrawGrid(Bitmap bitmap)
        {
            const string floatFormat = "0.0";
            const float tiny = 0.0001f;
            int w = bitmap.Width;
            int h = bitmap.Height;
            PointF p1 = Project(Single3.Zero);
            float x0 = p1.X;
            float y0 = p1.Y;

            float d1 = 10f * GetDelta();
            using (var graphics = Graphics.FromImage(bitmap))
            using (Pen pen = new Pen(Color.Gray, 1))
            using (SolidBrush brush = new SolidBrush(_gridTextColor))
            {
                using (StringFormat stringFormat = new StringFormat()
                { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far })
                {
                    float x1 = (float)(d1 * Ceiling(-x0 / d1));
                    while (x0 + x1 < w)
                    {
                        bool major = (Abs(x1) < tiny);
                        pen.Color = major ? MajorGridLineColor : MinorGridLineColor;
                        pen.Width = major ? MajorGridLineWidth : MinorGridLineWidth;
                        graphics.DrawLine(pen, ToSingle(x0 + x1), 0f, ToSingle(x0 + x1), h);
                        float u = x1 / Zoom;
                        string s = u.ToString(floatFormat);
                        float x = ToSingle(x0 + x1);
                        float y = 15f;
                        graphics.DrawString(s, DefaultFont, brush, x, y, stringFormat);
                        x1 += d1;
                    }
                }
                using (StringFormat SF = new StringFormat()
                { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                {

                    float y = (float)(d1 * Ceiling(-y0 / d1));
                    while (y0 + y < h)
                    {
                        bool Major = (Abs(y) < tiny);
                        pen.Color = Major ? MajorGridLineColor : MinorGridLineColor;
                        pen.Width = Major ? MajorGridLineWidth : MinorGridLineWidth;
                        graphics.DrawLine(pen, 0f, ToSingle(y0 + y), w, ToSingle(y0 + y));
                        float u = -y / Zoom;
                        graphics.DrawString(u.ToString(floatFormat), DefaultFont, brush, 3f, ToSingle(y0 + y), SF);
                        y += d1;
                    }
                }
            }
        }

        private void DrawAxis(Graphics graphics, Color color, PointF p1, PointF p2, string text)
        {
            float c = 1.2f, w = 20f;
            using (Pen pen = new Pen(color, XYZAxisPenWidth))
            {
                float capSize = XYZAxisCapSize;
                pen.CustomEndCap = new AdjustableArrowCap(capSize, capSize);
                graphics.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
                float x = (1f - c) * p1.X + c * p2.X - w / 2f;
                float y = (1f - c) * p1.Y + c * p2.Y - w / 2f;
                RectangleF rectangle = new RectangleF(x, y, w, w);
                using (SolidBrush brush = new SolidBrush(color))
                using (StringFormat stringFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    graphics.DrawString(text, DefaultFont, brush, rectangle, stringFormat);
                }
            }

        }

        private void DrawAxes(Bitmap bufferBitmap)
        {
            PointF p1 = Project(new Single3(0, 0, 0));
            float axisSize = XYZAxisLength * UnitVectorLength;
            using (Graphics graphics = Graphics.FromImage(bufferBitmap))
            {
                DrawAxis(graphics, XAxisColor, p1, Project(new Single3(axisSize, 0, 0)), "X");
                DrawAxis(graphics, YAxisColor, p1, Project(new Single3(0, axisSize, 0)), "Y");
                DrawAxis(graphics, ZAxisColor, p1, Project(new Single3(0, 0, axisSize)), "Z");
            }
        }

        private void DrawOrigin(Bitmap bufferBitmap)
        {
            Single3 origin = Origin;
            PointF p0 = Project(Single3.Zero);
            PointF p1 = Project(new Single3(origin.X, 0, 0));
            PointF p2 = Project(new Single3(origin.X, origin.Y, 0));
            PointF originF = Project(origin);
            float pointSize = 8f;
            Font font = DefaultFont;
            using (Graphics graphics = Graphics.FromImage(bufferBitmap))
            using (SolidBrush brush = new SolidBrush(Color.LightGray))
            using (Pen pen = new Pen(Color.Gray, 1f))
            using (StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Far,
            })
            {
                pen.DashStyle = DashStyle.Dash;
                graphics.DrawLine(pen, p0, p1);
                graphics.DrawLine(pen, p1, p2);
                graphics.DrawLine(pen, p2, originF);
                pen.DashStyle = DashStyle.Solid;
                graphics.FillEllipse(brush, originF.X - pointSize / 2, originF.Y - pointSize / 2, pointSize, pointSize);
                graphics.DrawEllipse(pen, originF.X - pointSize / 2, originF.Y - pointSize / 2, pointSize, pointSize);
                brush.Color = Color.Black;
                graphics.DrawString($"({origin.X:F3}, {origin.Y:F3}, {origin.Z:F3})", font, brush, originF.X, originF.Y - 10, stringFormat);
            }
        }

        #endregion
    }
}
