using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using Neunet.Images.Charts2D;
using static System.Convert;
using static System.Math;

namespace Neunet.Images
{
    internal enum ConnectionTypeEnum
    {
        None,
        Line,
        StepLeft,
        StepRight
    }

    internal enum VerticalScaleEnum { Lin, Log }

    internal partial class HistoryImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private HistoryDictionary Items { get; set; }

        public int TickCount
        {
            get { return Items.TickCount; }
        }

        public int CategoryCount
        {
            get { return Items.CategoryCount; }
        }

        private string _vAxisText;
        public string VAxisText
        {
            get { return _vAxisText; }
            set
            {
                _vAxisText = value;
                RefreshImage();
            }
        }

        private Padding _chartPadding = new Padding(10, 10, 60, 20);
        private Padding ChartPadding
        {
            get { return _chartPadding; }
            set
            {
                _chartPadding = value;
                RefreshImage();
            }
        }

        private int _magnification = 1;
        public int Magnification
        {
            get { return _magnification; }
            set
            {
                _magnification = value;
                RefreshImage();
            }
        }

        public event EventHandler ZoomChanged;

        private void RaiseZoomChanged()
        {
            ZoomChanged?.Invoke(this, new EventArgs());
        }


        private float _zoom = 0.95f;
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (value == _zoom) return;
                _zoom = value;
                RefreshImage();
                RaiseZoomChanged();
            }
        }

        private ConnectionTypeEnum _outerConnection = ConnectionTypeEnum.Line;
        public ConnectionTypeEnum OuterConnection
        {
            get { return _outerConnection; }
            set
            {
                _outerConnection = value;
                RefreshImage();
            }
        }

        private ConnectionTypeEnum _innerConnection = ConnectionTypeEnum.Line;
        public ConnectionTypeEnum InnerConnection
        {
            get { return _innerConnection; }
            set
            {
                _innerConnection = value;
                RefreshImage();
            }
        }

        private bool _viewPoints = false;
        public bool ViewPoints
        {
            get { return _viewPoints; }
            set
            {
                _viewPoints = value;
                RefreshImage();
            }
        }


        private float _pointSize = 14f;
        public float PointSize
        {
            get { return _pointSize; }
            set
            {
                _pointSize = value;
                RefreshImage();
            }
        }

        private float _scrollBarValue;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float ScrollBarValue
        {
            get { return _scrollBarValue; }
            set
            {
                _scrollBarValue = value;
                RefreshImage();
            }
        }

        public SizeF InnerSize
        {
            get
            {
                return new SizeF(
                    pictureBox.Width - ChartPadding.Left - ChartPadding.Right,
                    pictureBox.Height - ChartPadding.Top - ChartPadding.Bottom
                    );
            }
        }

        public const VerticalScaleEnum verticalScaleDefault = VerticalScaleEnum.Lin;
        private VerticalScaleEnum _verticalScale = verticalScaleDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public VerticalScaleEnum VerticalScale
        {
            get { return _verticalScale; }
            set
            {
                _verticalScale = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public HistoryImage()
        {
            InitializeComponent();
            Items = new HistoryDictionary();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        protected override void ImageMouseWheel(int delta)
        {
            bool swapMouseWheel = true;
            float zoomFactor = 1.1f;
            bool b = delta < 0;
            if (b ^ swapMouseWheel)
                Zoom *= zoomFactor;
            else
                Zoom /= zoomFactor;
        }

        public override void DrawImage(Bitmap bufferBitmap)
        {
            base.DrawImage(bufferBitmap);
            GetMinMax(out _, out float vMax);
            float vMin = 0f;
            //if (vMin == vMax) vMin = 0f;
            if (vMin < vMax)
            {
                vMax = vMin + (vMax - vMin) / Zoom;
                float vMajor = (float)CalculateMajor(vMin, vMax);
                vMin = (float)FloorMajor(vMajor, vMin);
                vMax = (float)CeilingMajor(vMajor, vMax);
                DrawSeries(bufferBitmap, vMin, vMax);
                DrawTicks(bufferBitmap, vMin, vMax, vMajor);
            }
            DrawAxes(bufferBitmap);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region HistoryImage

        private const double tiny = double.Epsilon; // compensate round-off errors

        private static double FloorMajor(double major, double zLo)
        {
            return Floor(zLo / major + tiny) * major;
        }

        private static double CeilingMajor(double major, double zHi)
        {
            return Ceiling(zHi / major - tiny) * major;
        }

        private static double CalculateMajor(double lo, double hi)
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

        private static string MajorLabelFormat(double major, int minDigits = 0)
        {
            int a = ToInt32(Max(minDigits, -Floor(Log10(major))));
            return $"F{a}";
        }

        private void DrawConnection(Graphics graphics, Pen pen1, Pen pen2, PointF q1, PointF q2, ConnectionTypeEnum connection)
        {
            switch (connection)
            {
                case ConnectionTypeEnum.Line:
                    graphics.DrawLine(pen1, q1, q2);
                    break;
                case ConnectionTypeEnum.StepLeft:
                    graphics.DrawLine(pen2, q1.X, q1.Y, q1.X, q2.Y);
                    graphics.DrawLine(pen2, q1.X, q2.Y, q2.X, q2.Y);
                    break;
                case ConnectionTypeEnum.StepRight:
                    graphics.DrawLine(pen2, q1.X, q1.Y, q2.X, q1.Y);
                    graphics.DrawLine(pen2, q2.X, q1.Y, q2.X, q2.Y);
                    break;
                default:
                    break;
            }

        }

        private void DrawPoint(Graphics graphics, Pen pen1, Brush brush1, Brush brush2, StringFormat stringFormat2, PointF q2, int j)
        {
            float d = PointSize, r = d / 2;
            if (d > 10)
            {
                graphics.FillEllipse(brush2, q2.X - r, q2.Y - r, d, d);
                graphics.DrawString($"{j + 1}", DefaultFont, brush1, q2.X, q2.Y, stringFormat2);
                graphics.DrawEllipse(pen1, q2.X - r, q2.Y - r, d, d);
            }
            else
            {
                graphics.FillEllipse(brush1, q2.X - r, q2.Y - r, d, d);
            }
        }

        private float GetValue(List<float> values, int j)
        {
            bool diverging = false;
            float v = values[j];
            switch (VerticalScale)
            {
                case VerticalScaleEnum.Lin:
                    break;
                case VerticalScaleEnum.Log:
                    if (j == 0) return float.NaN;
                    int i = j - 1;
                    while (i > 0 && float.IsNaN(values[i])) i--;
                    float vp = values[i];
                    if (float.IsNaN(vp)) return float.NaN;
                    if (vp == v) return float.NaN;
                    diverging = v > vp;
                    v = -(float)Log10(Abs(vp - v));
                    break;
                default:
                    break;
            }
            return v;
        }

        private static bool PointValid(PointF point)
        {
            if (float.IsNaN(point.X)) return false;
            if (float.IsNaN(point.Y)) return false;
            return true;
        }

        protected void DrawSeries(Bitmap bufferBitmap, float vMin, float vMax)
        {
            if (bufferBitmap == null) return;
            int w = bufferBitmap.Width;
            int h = bufferBitmap.Height;
            float x1 = ChartPadding.Left, x2 = w - ChartPadding.Right;
            float y1 = ChartPadding.Top, y2 = h - ChartPadding.Bottom;
            int n = TickCount, m = CategoryCount;

            using (Graphics graphics = Graphics.FromImage(bufferBitmap))
            using (StringFormat stringFormat1 = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far })
            using (StringFormat stringFormat2 = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (SolidBrush brush1 = new SolidBrush(Color.Black))
            using (SolidBrush brush2 = new SolidBrush(Color.White))
            using (Pen pen1 = new Pen(Color.Black, 2f))
            using (Pen pen2 = new Pen(Color.Gray, 1f) { DashStyle = DashStyle.Dash })
            {
                Padding chartPadding = ChartPadding;
                SizeF innerSize = InnerSize;
                graphics.Clip = new Region(new RectangleF(chartPadding.Left, chartPadding.Top, innerSize.Width + 1f, innerSize.Height + 1f));
                int i = 0;
                foreach (KeyValuePair<object, List<float>> keyValuePair in Items)
                {
                    object key = keyValuePair.Key;
                    List<float> values = keyValuePair.Value;

                    Color color = ColorUtilities.GetDarkColor(i);
                    pen1.Color = color;
                    brush1.Color = color;
                    {
                        PointF q1, q2 = new PointF(float.NaN, float.NaN);
                        bool b3 = false;
                        for (int j = 0; j < n; j++) // tick
                        {
                            float y = GetValue(values, j);
                            if (float.IsNaN(y))
                            {
                                b3 = false;
                            }
                            else
                            {
                                q1 = q2;
                                q2 = ToPointF(bufferBitmap, j, n, ToSingle((y - vMin) / (vMax - vMin)));
                                if (PointValid(q1) && PointValid(q2))
                                {
                                    DrawConnection(graphics, pen1, pen2, q1, q2, b3 ? InnerConnection : OuterConnection);
                                }
                                else if (PointValid(q2))
                                {
                                    DrawPoint(graphics, pen1, brush1, brush2, stringFormat2, q2, j);
                                }
                                b3 = true;
                            }
                        }
                        graphics.DrawString($"[{i + 1}]", DefaultFont, brush1, q2.X, q2.Y, stringFormat1);
                    }
                    if (ViewPoints)
                    {
                        PointF q2 = new PointF(0, 0);
                        for (int j = 0; j < n; j++) // tick
                        {
                            float y = GetValue(values, j);
                            if (float.IsNaN(y)) continue;
                            q2 = ToPointF(bufferBitmap, j, n, ToSingle((y - vMin) / (vMax - vMin)));
                            if (PointValid(q2))
                                DrawPoint(graphics, pen1, brush1, brush2, stringFormat2, q2, j);

                        }
                    }
                    i++;
                }
            }
        }


        public void DrawTicks(Bitmap bufferBitmap, float vMin, float vMax, float vMajor)
        {
            int n = TickCount;
            if (bufferBitmap == null) return;
            int w = bufferBitmap.Width;
            int h = bufferBitmap.Height;
            float x1 = ChartPadding.Left, x2 = w - ChartPadding.Right;
            float y1 = ChartPadding.Top, y2 = h - ChartPadding.Bottom;
            string format = MajorLabelFormat(vMajor, 1);
            Font font = DefaultFont;
            using (SolidBrush brush = new SolidBrush(Color.Black))
            using (Pen pen1 = new Pen(Color.Gray, 1f))
            using (Pen pen2 = new Pen(Color.Black, 1f))
            using (Graphics graphics = Graphics.FromImage(bufferBitmap))
            {
                // horizontal axis
                using (StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near })
                {
                    graphics.Clip = new Region(new RectangleF(ChartPadding.Left, 0, InnerSize.Width + 1f, h));
                    for (int i = 0; i < n; i += 10)
                    {
                        float x = J2x(i, n);
                        graphics.DrawLine(pen1, x, y2 - 2f, x, y2 + 2f);
                    }
                    for (int i = 0; i < n; i += 100)
                    {
                        float x = J2x(i, n);
                        graphics.DrawLine(pen2, x, y2 - 2f, x, y2 + 2f);
                        graphics.DrawString($"{i}", font, brush, x, y2 + 3, stringFormat);
                    }
                }
                // vertical axis
                using (StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                {
                    graphics.Clip = new Region(new RectangleF(0, ChartPadding.Top, w, InnerSize.Height + 1f));
                    for (float v = vMin; v <= vMax; v += vMajor)
                    {
                        float y = V2y(bufferBitmap, ToSingle((v - vMin) / (vMax - vMin)));
#if false
                        try
                        {
                            graphics.DrawLine(pen2, x2 - 2f, y, x2 + 2f, y);
                        }
                        catch
                        {
                            MessageBox.Show($"graphics.DrawLine(pen2, {x2 - 2f}, {y}, {x2 + 2f}, {y})");
                        }
#else
                        graphics.DrawLine(pen2, x2 - 2f, y, x2 + 2f, y);
#endif
                        graphics.DrawString(v.ToString(format), font, brush, x2 + 5f, y, stringFormat);
                    }
                }
            }
        }

        public void DrawAxes(Bitmap bufferBitmap)
        {
            int w = bufferBitmap.Width;
            int h = bufferBitmap.Height;
            float x1 = ChartPadding.Left, x2 = w - ChartPadding.Right;
            float y1 = ChartPadding.Top, y2 = h - ChartPadding.Bottom;
            using (Graphics graphics = Graphics.FromImage(bufferBitmap))
            {
                using (Pen pen = new Pen(Color.Black, 2f))
                {
                    // horizontal axis
                    graphics.DrawLine(pen, x1, y2, x2, y2);
                    // vertical axis
                    graphics.DrawLine(pen, x2, y1, x2, y2);
                }
                Font font = DefaultFont;
                bool reverseAxisY = true;
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    using (StringFormat stringFormat = new StringFormat(StringFormatFlags.DirectionVertical)
                    { Alignment = reverseAxisY ? StringAlignment.Center : StringAlignment.Near, LineAlignment = StringAlignment.Far })
                    {
                        graphics.DrawString(VAxisText, font, brush, w, h / 2f, stringFormat);
                    }
                }
            }
        }

        public void GetMinMax(out float vMin, out float vMax)
        {
            vMin = float.NaN;
            vMax = float.NaN;
            int n = TickCount;
            foreach (KeyValuePair<object, List<float>> keyValuePair in Items)
            {
                List<float> values = keyValuePair.Value;
                for (int j = 0; j < n; j++)
                {
                    float y = GetValue(values, j);
                    if (float.IsNaN(y)) continue;
                    if (float.IsNaN(vMin) || y < vMin) vMin = y;
                    if (float.IsNaN(vMax) || y > vMax) vMax = y;
                }
            }
        }

        private float J2x(int i, int n)
        {
            return ChartPadding.Left + Magnification * i - (1f - ScrollBarValue) * (Magnification * n - InnerSize.Width);

        }

        private float V2y(Bitmap bufferBitmap, float v)
        {
            return bufferBitmap.Height - ChartPadding.Bottom - InnerSize.Height * v;
        }

        private PointF ToPointF(Bitmap bufferBitmap, int i, int n, float v)
        {
            float x = J2x(i, n);
            float y = V2y(bufferBitmap, v);
            return new PointF(x, y);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region This

        public void ClearItems()
        {
            Items.ClearItems();
            RefreshImage();
        }

        /// <summary>
        /// Adds a new tick to the end of all categories, initializes the new tick values with NaN, and returns the index of the new tick.
        /// </summary>
        /// <returns>The index of the new tick.</returns>
        public void AddOneTick()
        {
            Items.AddOneTick();
            RefreshImage();
        }

        public void SetLastValue(object key, float merit)
        {
            if (!Items.ContainsKey(key))
                Items.AddOneCategory(key);
            Items.SetLastValue(key, merit);
            RefreshImage();
        }

        public void AddValue(object key, float merit)
        {
            try
            {
                SuspendImage();
                Items.AddOneTick();
                SetLastValue(key, merit);
            }
            finally
            {
                ResumeImage();
            }
        }

        #endregion
    }
}
