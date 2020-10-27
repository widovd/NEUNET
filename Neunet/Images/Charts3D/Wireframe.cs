using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using Neulib;
using Neulib.Exceptions;
using Neulib.Numerics;
using Neulib.Extensions;
using Neunet.Attributes;
using static System.Math;
using static System.Convert;

namespace Neunet.Images.Charts3D
{
    public enum SliceModeEnum
    {
        [ShowName("None"), XmlText("None"), Description("None")] None,
        [ShowName("Slice U"), XmlText("SliceU"), Description("SliceU")] SliceU,
        [ShowName("Slice V"), XmlText("SliceV"), Description("SliceV")] SliceV,
    }


    public class Wireframe
    {
        // ----------------------------------------------------------------------------------------
        #region Index

        private struct Index
        {
            public int I { get; }
            public int J { get; }

            public Index(int i, int j)
            {
                I = i;
                J = j;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Properties

        public WireframeNode ParentNode { get; set; }

        private WireframeImage WireframeImage { get { return ParentNode?.WireframeImage; } }

        public bool UPeriodic { get; set; }

        public bool VPeriodic { get; set; }

        private readonly WireframePoint[,,] _wireframePoints;

        public WireframePoint this[int h, int i, int j]
        {
            get
            {
                return
                    i >= 0 && i < UCount && j >= 0 && j < VCount ?
                    _wireframePoints[h, i, j] : null;
            }
            set { _wireframePoints[h, i, j] = value; }
        }

        private readonly float[,,] _values;
        private readonly Color[,,] _colors;

        public int HCount { get; private set; }

        public int UCount { get; private set; }

        public int VCount { get; private set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Wireframe(int hCount, int uCount, int vCount)
        {
            if (hCount <= 0) throw new InvalidValueException(nameof(hCount), hCount, 833183);
            HCount = hCount;
            UCount = uCount;
            VCount = vCount;
            _wireframePoints = new WireframePoint[hCount, uCount, vCount];
            _values = new float[hCount, uCount, vCount];
            _colors = new Color[hCount, uCount, vCount];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"{HCount} x {UCount} x {VCount}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region WireFrame

        public void CalculatePointsF(float length, Func<Single3, PointF> func)
        {
            int hCount = HCount, uCount = UCount, vCount = VCount;
            Parallel.For(0, uCount, (i) =>
            {
                for (int j = 0; j < vCount; j++)
                    for (int h = 0; h < hCount; h++)
                    {
                        WireframePoint w = this[h, i, j];
                        if (w == null) continue;
                        w.CalculatePointsF(length, func);
                    }
            });
        }

        public void CalculateColors(float min, float max)
        {
            WireframeImage wireframeImage = WireframeImage;
            int hCount = HCount, uCount = UCount, vCount = VCount;
            Parallel.For(0, uCount, (i) =>
            {
                for (int j = 0; j < vCount; j++)
                    for (int h = 0; h < hCount; h++)
                    {
                        WireframePoint point = this[h, i, j];
                        float value = _values[h, i, j];
                        Color color;
                        if (double.IsNaN(value))
                            color = wireframeImage.SurfaceColor;
                        else if (double.IsNaN(min) || double.IsNaN(max))
                            color = wireframeImage.InvalidColor;
                        else
                        {
                            float delta = max - min;
                            float z = delta > 0f ? (value - min) / delta : 0.5f;
                            color = ColorUtilities.GetColor(z, wireframeImage.ColorGradient, wireframeImage.InverseGradient);
                        }
                        _colors[h, i, j] = color;
                    }
            });
        }

        public static void DrawString(Graphics graphics, Color color, Font font, PointF p1, PointF p2, string text, float distance)
        {
            using (SolidBrush brush = new SolidBrush(color))
            using (StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                float dx = p2.X - p1.X, dy = p2.Y - p1.Y;
                float l = ToSingle(Sqrt(dx * dx + dy * dy));
                if (l > 0f)
                {
                    dx = distance * dx / l;
                    dy = distance * dy / l;
                }
                float x = p2.X + dx - distance / 2;
                float y = p2.Y + dy - distance / 2;
                RectangleF rectangle = new RectangleF(x, y, distance, distance);
                graphics.DrawString(text, font, brush, rectangle, stringFormat);
            }
        }

        /// <summary>
        /// Calculates the number of display points per direction u or v.
        /// </summary>
        /// <param name="count">The number of available points per direction.</param>
        /// <param name="maxLevel">The maximum level of points to be displayed per direction: Max count = 2^maxLevel.</param>
        /// <param name="shift">The bit shift to be applied to the index of the available points.</param>
        /// <returns>The number of displayed points per direction.</returns>
        private static int CalculatePointCount(int count, int maxLevel, bool periodical, out int shift)
        {
            int uLog2 = count.Log2();
            shift = 0;
            if (uLog2 > maxLevel) shift = uLog2 - maxLevel;
            int n = count >> shift;
            if (shift > 0 && !periodical) n++;
            return n;
        }

        private Index[,] CalculatePointIndices()
        {
            const int minLevel = 2, maxLevel = 8;
            int uCount = UCount, vCount = VCount;
            int delta = WireframeImage.DeltaLevel;
            int uLevel = (uCount.Log2() + delta).Clip(minLevel, maxLevel);
            int vLevel = (vCount.Log2() + delta).Clip(minLevel, maxLevel);
            int nu = CalculatePointCount(uCount, uLevel, UPeriodic, out int uShift);
            int nu2 = nu / 2;
            int nv = CalculatePointCount(vCount, vLevel, VPeriodic, out int vShift);
            int nv2 = nv / 2;

            SliceModeEnum sliceMode = WireframeImage.SliceMode;
            switch (sliceMode)
            {
                case SliceModeEnum.None:
                    break;
                case SliceModeEnum.SliceU:
                    nv = 1;
                    break;
                case SliceModeEnum.SliceV:
                    nu = 1;
                    break;
                default:
                    throw new InvalidCaseException("sliceMode", sliceMode, 561093);
            }
            int sliceValue = WireframeImage.SliceValue;
            //if (sliceValue < 0) sliceValue = 0;
            Index[,] indices = new Index[nu, nv];
            for (int i = 0; i < nu; i++)
                for (int j = 0; j < nv; j++)
                {
                    int i1 = Min(i << uShift, uCount - 1);
                    int j1 = Min(j << vShift, vCount - 1);
                    int i2 = nu2 + sliceValue << vShift;
                    int j2 = nv2 + sliceValue << vShift;
                    indices[i, j] = sliceMode switch
                    {
                        SliceModeEnum.None => new Index(i1, j1),
                        SliceModeEnum.SliceU => new Index(i1, j2),
                        SliceModeEnum.SliceV => new Index(i2, j1),
                        _ => throw new InvalidCaseException(nameof(sliceMode), sliceMode, 681261),
                    };
                }
            return indices;
        }

        private Color GetColor(WireframePoint w1, Color color)
        {

            return w1.HasFlux ? color : WireframeImage.InvalidColor;
        }

        private void DrawSurfaceU(Graphics graphics, Index[,] indices, int h)
        {
            WireframeImage wireframeImage = WireframeImage;
            int uCount = indices.GetLength(0), vCount = indices.GetLength(1);
            int nu = uCount, nv = vCount;
            if (nu < 2) return;
            if (UPeriodic) nu++;
            bool drawAxis = wireframeImage.ViewUVAxes;
            float capSize = wireframeImage.UVAxisCapSize;
            Font axisFont = WireframeImage.DefaultFont;

            using (Pen surfacePen = new Pen(Color.Black, wireframeImage.SurfacePenWidth))
            {
                for (int j = 0; j < nv; j++)
                    for (int i = 1; i < nu; i++)
                    {
                        Index ix1 = indices[i - 1, j];
                        WireframePoint w1 = this[h, ix1.I, ix1.J];
                        if (w1 == null) continue;
                        Index ix2 = indices[i % uCount, j];
                        WireframePoint w2 = this[h, ix2.I, ix2.J];
                        if (w2 == null) continue;
                        Color c1 = w1.HasFlux ? _colors[h, ix1.I, ix1.J] : wireframeImage.InvalidColor;
                        Color c2 = w2.HasFlux ? _colors[h, ix2.I, ix2.J] : wireframeImage.InvalidColor;
                        Color c = ColorUtilities.Mix(c1, c2);
                        PointF p1 = w1.OriginF;
                        PointF p2 = w2.OriginF;
                        surfacePen.Color = c;
                        graphics.DrawLine(surfacePen, p1.X, p1.Y, p2.X, p2.Y);
                    }
            }
            if (drawAxis && nv > 1)
                using (Pen axisPen = new Pen(wireframeImage.UAxisColor, wireframeImage.UVAxisPenWidth))
                {
                    int j = 0;
                    for (int i = 1; i < nu; i++)
                    {
                        Index ix1 = indices[i - 1, j];
                        WireframePoint w1 = this[h, ix1.I, ix1.J];
                        if (w1 == null) continue;
                        Index ix2 = indices[i % uCount, j];
                        WireframePoint w2 = this[h, ix2.I, ix2.J];
                        if (w2 == null) continue;
                        PointF p1 = w1.OriginF;
                        PointF p2 = w2.OriginF;
                        if (i == nu - 1)
                        {
                            axisPen.CustomEndCap = new AdjustableArrowCap(capSize, capSize);
                            DrawString(graphics, axisPen.Color, axisFont, p1, p2, "U", 16f);
                        }
                        graphics.DrawLine(axisPen, p1.X, p1.Y, p2.X, p2.Y);
                    }
                }
        }

        private void DrawSurfaceV(Graphics graphics, Index[,] indices, int h)
        {
            WireframeImage wireframeImage = WireframeImage;
            int uCount = indices.GetLength(0), vCount = indices.GetLength(1);
            int nu = uCount, nv = vCount;
            if (nv < 2) return;
            if (VPeriodic) nv++;
            bool drawAxis = wireframeImage.ViewUVAxes;
            float capSize = wireframeImage.UVAxisCapSize;
            Font axisFont = WireframeImage.DefaultFont;

            using (Pen surfacePen = new Pen(Color.Black, wireframeImage.SurfacePenWidth))
            {
                for (int i = 0; i < nu; i++)
                    for (int j = 1; j < nv; j++)
                    {
                        Index ix1 = indices[i, j - 1];
                        WireframePoint w1 = this[h, ix1.I, ix1.J];
                        if (w1 == null) continue;
                        Index ix2 = indices[i, j % vCount];
                        WireframePoint w2 = this[h, ix2.I, ix2.J];
                        if (w2 == null) continue;
                        Color c1 = w1.HasFlux ? _colors[h, ix1.I, ix1.J] : wireframeImage.InvalidColor;
                        Color c2 = w2.HasFlux ? _colors[h, ix2.I, ix2.J] : wireframeImage.InvalidColor;
                        Color c = ColorUtilities.Mix(c1, c2);
                        PointF p1 = w1.OriginF;
                        PointF p2 = w2.OriginF;
                        surfacePen.Color = c;
                        graphics.DrawLine(surfacePen, p1.X, p1.Y, p2.X, p2.Y);
                    }
            }
            if (drawAxis && nu > 1)
                using (Pen axisPen = new Pen(wireframeImage.VAxisColor, wireframeImage.UVAxisPenWidth))
                {
                    int i = 0;
                    for (int j = 1; j < nv; j++)
                    {
                        Index ix1 = indices[i, j - 1];
                        WireframePoint w1 = this[h, ix1.I, ix1.J];
                        if (w1 == null) continue;
                        Index ix2 = indices[i, j % vCount];
                        WireframePoint w2 = this[h, ix2.I, ix2.J];
                        if (w2 == null) continue;
                        PointF p1 = w1.OriginF;
                        PointF p2 = w2.OriginF;
                        if (j == nv - 1)
                        {
                            axisPen.CustomEndCap = new AdjustableArrowCap(capSize, capSize);
                            DrawString(graphics, axisPen.Color, axisFont, p1, p2, "V", 16f);
                        }
                        graphics.DrawLine(axisPen, p1.X, p1.Y, p2.X, p2.Y);
                    }
                }
        }

        private void DrawSurfaces(Graphics graphics, Index[,] indices)
        {
            int hCount = HCount;
            for (int h = 0; h < hCount; h++)
            {
                DrawSurfaceU(graphics, indices, h);
                DrawSurfaceV(graphics, indices, h);
            }
        }

        private void DrawRays(Graphics graphics, Index[,] indices)
        {
            WireframeImage wireframeImage = WireframeImage;
            int hCount = HCount;
            int uCount = indices.GetLength(0);
            int vCount = indices.GetLength(1);
            float penWidth = wireframeImage.RayPenWidth;
            float capSize = wireframeImage.RayCapSize;
            using (Pen mainPen = new Pen(Color.Black, penWidth))
            using (Pen endPen = new Pen(Color.Black, penWidth)
            {
                CustomEndCap = new AdjustableArrowCap(capSize, capSize),
            })
            {
                for (int i = 0; i < uCount; i++)
                    for (int j = 0; j < vCount; j++)
                    {
                        Index ix = indices[i, j];
                        bool isInvalid = false;
                        bool isReflection = false;
                        bool isTir = false;
                        for (int h = 0; h < hCount; h++)
                        {
                            WireframePoint w = this[h, ix.I, ix.J];
                            isInvalid = isInvalid || !w.HasFlux;
                            isReflection = isReflection || w.IsReflection;
                            isTir = isTir || w.IsTir;
                        }

                        Color rayColor;
                        if (isInvalid) rayColor = wireframeImage.InvalidColor;
                        else if (isTir) rayColor = wireframeImage.TirColor;
                        else if (isReflection) rayColor = wireframeImage.ReflectionColor;
                        else rayColor = wireframeImage.RefractionColor;
                        endPen.Color = rayColor;
                        mainPen.Color = rayColor;

                        WireframePoint w2 = null;
                        bool b = true;
                        for (int h = hCount - 1; h >= 0; h--)
                        {
                            WireframePoint w1 = w2;
                            w2 = this[h, ix.I, ix.J];
                            if (w2 == null) continue;

                            PointF p2 = w2.OriginF;
                            if (b)
                            {
                                PointF p3 = w2.RayEndF;
                                graphics.DrawLine(endPen, p2.X, p2.Y, p3.X, p3.Y);
                                b = false;
                            }
                            if (w1 == null) continue;
                            PointF p1 = w1.OriginF;
                            graphics.DrawLine(mainPen, p1.X, p1.Y, p2.X, p2.Y);
                        }
                    }
            }
        }

        private void DrawNormalVectors(Graphics graphics, Index[,] indices)
        {
            WireframeImage wireframeImage = WireframeImage;
            float capSize = wireframeImage.VectorCapSize;
            Color normalsColor = wireframeImage.NormalsColor;
            using (Pen pen = new Pen(normalsColor, wireframeImage.NormalVectorPenWidth)
            {
                CustomEndCap = new AdjustableArrowCap(capSize, capSize),
            })
            {
                int hCount = HCount;
                int uCount = indices.GetLength(0);
                int vCount = indices.GetLength(1);
                for (int h = 0; h < hCount; h++)
                    for (int i = 0; i < uCount; i++)
                        for (int j = 0; j < vCount; j++)
                        {
                            Index ix = indices[i, j];
                            WireframePoint w = this[h, ix.I, ix.J];
                            if (w == null) continue;
                            PointF originF = w.OriginF;
                            PointF normalEndF = w.NormalEndF;
                            pen.Color = GetColor(w, wireframeImage.NormalsColor);
                            graphics.DrawLine(pen, originF.X, originF.Y, normalEndF.X, normalEndF.Y);
                        }
            }
        }

        [Obsolete]
        private static void FillEllipse(Graphics graphics, SolidBrush brush, PointF point, float pointSize)
        {
            graphics.FillEllipse(brush, point.X - pointSize / 2, point.Y - pointSize / 2, pointSize, pointSize);
        }

        [Obsolete]
        private static void DrawEllipse(Graphics graphics, Pen pen, PointF point, float pointSize)
        {
            graphics.DrawEllipse(pen, point.X - pointSize / 2, point.Y - pointSize / 2, pointSize, pointSize);
        }

        [Obsolete]
        private static void DrawCross(Graphics graphics, Pen pen, PointF point, float pointSize)
        {
            graphics.DrawLine(pen, point.X - pointSize / 2, point.Y - pointSize / 2, point.X + pointSize / 2, point.Y + pointSize / 2);
            graphics.DrawLine(pen, point.X - pointSize / 2, point.Y + pointSize / 2, point.X + pointSize / 2, point.Y - pointSize / 2);
        }

        private void DrawValues(Graphics graphics, Index[,] indices)
        {
            Font font = WireframeImage.DefaultFont;
            using (SolidBrush brush = new SolidBrush(Color.Black))
            using (StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                int hCount = HCount;
                int uCount = indices.GetLength(0);
                int vCount = indices.GetLength(1);
                //int frameIndex = ParentNode.Frames.IndexOf(this);
                for (int h = 0; h < hCount; h++)
                    for (int i = 0; i < uCount; i++)
                        for (int j = 0; j < vCount; j++)
                        {
                            Index ix = indices[i, j];
                            WireframePoint w = this[h, ix.I, ix.J];
                            if (w == null) continue;
                            PointF originF = w.OriginF;
                            double value = _values[h, ix.I, ix.J];
                            string text = double.IsNaN(value) ? $"[{h}, {ix.I}, {ix.J}]" : $"{value:F2}";
                            graphics.DrawString(text, font, brush, originF.X, originF.Y, format);
                        }
            }
        }


        public void DrawFrame(Graphics graphics)
        {
            WireframeImage wireframeImage = WireframeImage;
            Index[,] indices = CalculatePointIndices();
            if (wireframeImage.ViewSurfaces) DrawSurfaces(graphics, indices);
            if (wireframeImage.ViewRays) DrawRays(graphics, indices);
            if (wireframeImage.ViewNormals) DrawNormalVectors(graphics, indices);
            if (wireframeImage.SliceMode != SliceModeEnum.None) DrawValues(graphics, indices);
        }

        public void UpdateDimensions(WireframeDimensions dimensions)
        {
            foreach (WireframePoint wireframePoint in _wireframePoints)
            {
                wireframePoint?.UpdateDimensions(dimensions);
            }
        }

        #endregion
    }
}
