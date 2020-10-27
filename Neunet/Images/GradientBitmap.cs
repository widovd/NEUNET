using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Neulib.Exceptions;
using Neunet.Extensions;

using static System.Convert;

namespace Neunet.Images
{
    public static class GradientBitmap
    {
        // ----------------------------------------------------------------------------------------
        #region GradientBitmap

        public static void GetMinMax(float[,] vArr, out float vMin, out float vMax)
        {
            vMin = float.MaxValue;
            vMax = float.MinValue;
            int n = vArr.GetLength(0), m = vArr.GetLength(1);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    float v = vArr[i, j];
                    if (float.IsNaN(v)) continue;
                    if (v < vMin) vMin = v;
                    if (v > vMax) vMax = v;
                }
        }

        public static float[,] ToValues(Bitmap bitmap, Rectangle rectangle)
        {
            int pixelByteCount = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8; // = 4
            ColorPalette palette = bitmap.Palette;
            if (pixelByteCount == 2) throw new InvalidValueException(nameof(pixelByteCount), pixelByteCount, 374948);
            int n = rectangle.Width, m = rectangle.Height;
            float[,] newValues = new float[n, m];
            BitmapData data = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            try
            {
                IntPtr ptr = data.Scan0;
                int nBytes = pixelByteCount * n;
                byte[] bytes = new byte[nBytes];
                for (int j = 0; j < m; j++)
                {
                    Marshal.Copy(ptr, bytes, 0, nBytes);
                    int k = 0;
                    for (int i = 0; i < n; i++)
                    {
                        byte b, g, r;
                        if (pixelByteCount == 1)
                        {
                            Color C = palette.Entries[bytes[k++]];
                            b = C.B;
                            g = C.G;
                            r = C.R;
                        }
                        else
                        {
                            b = bytes[k++];
                            g = bytes[k++];
                            r = bytes[k++];
                            if (pixelByteCount == 4) _ = bytes[k++];
                        }
                        int W = r + g + b;
                        newValues[i, m - 1 - j] = W; // Image is swapped vertically!
                    }
                    ptr += data.Stride;
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
            return newValues;
        }

        public static void FromValues(Bitmap bitmap, Rectangle rectangle, Func<int, int, Color> func)
        {
            int pixelByteCount = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8; // = 4
            if (pixelByteCount < 3) throw new InvalidValueException(nameof(pixelByteCount), pixelByteCount, 418795);
            BitmapData data = bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            try
            {
                int width = rectangle.Width, height = rectangle.Height;
                IntPtr ptr = data.Scan0;
                int nBytes = pixelByteCount * width;
                byte[] bytes = new byte[nBytes];
                for (int j = 0; j < height; j++)
                {
                    int k = 0;
                    for (int i = 0; i < width; i++)
                    {
                        Color Pixel = func(i, height - 1 - j); // Image is swapped vertically!
                        bytes[k++] = Pixel.B;
                        bytes[k++] = Pixel.G;
                        bytes[k++] = Pixel.R;
                        if (pixelByteCount == 4) bytes[k++] = Pixel.A;
                    }
                    Marshal.Copy(bytes, 0, ptr, nBytes);
                    ptr += data.Stride;
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
        }

        private static Bitmap GenerateBitmap16x16(ColorGradientEnum gradient)
        {
            int n = 16, m = 16;
            Bitmap bitmap = new Bitmap(n, m, PixelFormat.Format32bppArgb);
            float[,] values = new float[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    values[i, j] = ((float)i + (float)j) / (float)(n * m);
                }
            GetMinMax(values, out float vMin, out float vMax);
            Rectangle rectangle = new Rectangle(0, 0, n, m);
            GradientBitmap.FromValues(bitmap, rectangle, (int i, int j) =>
            {
                float z = (values[i, j] - vMin) / (vMax - vMin);
                return ColorUtilities.GetColor(z, gradient, false);
            });
            return bitmap;
        }

        public static void AddColorGradientDropDownItems(ToolStripItemCollection itemCollection, EventHandler onClick)
        {
            itemCollection.Clear();
            Array values = Enum.GetValues(typeof(ColorGradientEnum));
            foreach (ColorGradientEnum colorGradient in values)
            {
                Bitmap bitmap = GradientBitmap.GenerateBitmap16x16(colorGradient); // bitmap implements IDisposable
                ToolStripMenuItem menuItem = new ToolStripMenuItem(colorGradient.GetShowName(), bitmap, onClick) // menuItem implements IDisposable
                {
                    ImageScaling = ToolStripItemImageScaling.None,
                    Tag = colorGradient
                };
                itemCollection.Add(menuItem);
            }
        }

        public static void DrawLegend(
            Bitmap bitmap,
            float minValue, float maxValue, string format,
            ColorGradientEnum colorGradient, bool inverseGradient,
            params string[] lines)
        {
            if (float.IsNaN(minValue) || float.IsNaN(maxValue)) return;
            const int a = 12; // distance from right legend to right bitmap
            const int b = 12; // distance from bottom legend to bottom bitmap
            const int l = 16; // height of a text line
            const int h = 60; // height of the color gradient indicator
            const int w = 64; // width
            const int d = 2; // distance from y to text string
            int n = lines != null ? lines.Length : 0;
            float x1 = bitmap.Width - a, x0 = x1 - w, x2 = x0;
            float y4 = bitmap.Height - b, y3 = y4 - l, y2 = y3 - h, y1 = y2 - l, y0 = y1 - n * l;

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // background
                using (SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(0xF0, 0xF0, 0xF0)))
                {
                    graphics.FillRectangle(backgroundBrush, x0, y0, w, h + (2 + n) * l);
                }

                // color gradient
                using (Pen gradientPen = new Pen(Color.Black, 1f))
                {
                    for (int i = 0; i <= h; i++)
                    {
                        float z = maxValue > minValue ? (float)i / h : 0.5f;
                        gradientPen.Color = ColorUtilities.GetColor(z, colorGradient, inverseGradient);
                        float y = y2 + i;
                        graphics.DrawLine(gradientPen, x0, y, x1, y);
                    }
                }

                // text
                using (Pen textPen = new Pen(Color.Black, 1f))
                using (SolidBrush textBrush = new SolidBrush(Color.Black))
                using (StringFormat stringFormat = new StringFormat 
                { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near })
                {
                    Font font = Control.DefaultFont;
                    textBrush.Color = Color.Black;
                    textPen.Color = Color.Black;
                    for (int i = 0; i < n; i++)
                    {
                        graphics.DrawString(lines[i], font, textBrush, x2, y0 + i * l + d, stringFormat);
                        graphics.DrawRectangle(textPen, x0, y0 + i * l, w, l);
                    }
                    graphics.DrawString(minValue.ToString(format), font, textBrush, x2, y1 + d, stringFormat);
                    graphics.DrawString(maxValue.ToString(format), font, textBrush, x2, y3 + d, stringFormat);
                    graphics.DrawRectangle(textPen, x0, y1, w, h + 2 * l);
                }
            }
        }


        #endregion
    }
}
