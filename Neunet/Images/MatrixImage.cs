using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neulib.MultiArrays;
using Neulib.Numerics;
using Neulib.Neurons;
using static System.Math;

namespace Neunet.Images.Charts2D
{
    public partial class MatrixImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        //SampleList
        private SampleList _samples;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public SampleList Samples
        {
            get { return _samples; }
            set
            {
                _samples = value;
                RefreshImage();
            }
        }

        //private int _imageIndex;
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        //public int ImageIndex
        //{
        //    get { return _imageIndex; }
        //    set
        //    {
        //        _imageIndex = value;
        //        RefreshImage();
        //    }
        //}

        //private ByteArray _labelArray;
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        //public ByteArray LabelArray
        //    // 1D
        //{
        //    get { return _labelArray; }
        //    set
        //    {
        //        _labelArray = value;
        //        RefreshImage();
        //    }
        //}


        //private ByteArray _imageArray;
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        //public ByteArray ImageArray
        //    // 3D
        //{
        //    get { return _imageArray; }
        //    set
        //    {
        //        _imageArray = value;
        //        RefreshImage();
        //    }
        //}

        //private SingleArray _resultsArray;
        //public SingleArray ResultsArray
        //    // 2D
        //{
        //    get { return _resultsArray; }
        //    set
        //    {
        //        _resultsArray = value;
        //        RefreshImage();
        //    }
        //}


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public MatrixImage()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        public override void DrawImage(Bitmap bitmap)
        {
            base.DrawImage(bitmap);
            if (Samples == null) return;
            DrawPixels(bitmap);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region MatrixImage

        private void DrawLabels(Graphics graphics, int i, Sample sample, float x, float y, float w, float h)
        {
            using (SolidBrush stringBrush = new SolidBrush(Color.Black))
            using (StringFormat stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Near,
                Alignment = StringAlignment.Near,
            })
            {
                graphics.DrawString($"{i}: [{sample.Index}] = {sample.Label}", DefaultFont, stringBrush, x, y, stringFormat);
            }
        }

        private void DrawYs(Graphics graphics, float[] ys, float x, float y, float w, float h)
        {
            int ny = ys.Length;
            float d2 = w / ny;
            float d1 = 1f;
            float dx = d2 - 2 * d1;
            using (Pen pen = new Pen(Color.Black, 1f))
            using (SolidBrush fillBrush = new SolidBrush(Color.Gray))
            using (SolidBrush stringBrush = new SolidBrush(Color.Black))
            using (StringFormat stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Near,
                Alignment = StringAlignment.Near,
            })
            {
                int iMax = 0;
                float vMax = float.NaN;
                for (int i = 0; i < ny; i++)
                {
                    float v = ys[i];
                    if (float.IsNaN(vMax) || v > vMax) { vMax = v; iMax = i; }
                }
                for (int i = 0; i < ny; i++)
                {
                    fillBrush.Color = i == iMax ? Color.Red : Color.Gray;
                    float x1 = x + d1 + i * d2;
                    float dy = h * ys[i];
                    float y1 = y + h - dy;
                    graphics.FillRectangle(fillBrush, x1, y1, dx, dy);
                    graphics.DrawRectangle(pen, x1, y, dx, h - dy);
                    graphics.DrawRectangle(pen, x1, y1, dx, dy);
                    graphics.DrawString($"{i}", DefaultFont, stringBrush, x + i * d2, y + h, stringFormat);
                }
            }
        }

        private void DrawXs(Graphics graphics, float[] xs, float x, float y, float w, float h, int nu, int nv)
        {
            using (Pen pen = new Pen(Color.Black, 1f))
            using (SolidBrush fillBrush = new SolidBrush(Color.Black))
            using (StringFormat stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Near,
                Alignment = StringAlignment.Near,
            })
            {
                float d1 = h / Max(nu, nv);
                for (int i = 0; i < nu; i++)
                {
                    float x2 = (float)i * d1;
                    for (int j = 0; j < nv; j++)
                    {
                        float y2 = (float)j * d1;
                        float z = xs[i + nu * j];
                        fillBrush.Color = ColorUtilities.GetColor(z, Color.Black, Color.White);
                        graphics.FillRectangle(fillBrush, x + x2, y + y2, d1, d1);
                    }
                }
            }
        }

        private void DrawPixels(Bitmap bitmap)
        {
            int nh = Samples.Count;
            int nu = Samples.NU;
            int nv = Samples.NV;
            float xx = 20f, yy = 20f;
            float s = bitmap.Height - yy;
            float d1 = s / Max(nu, nv);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {

                for (int h = 0; h < nh; h++)
                {
                    Sample sample = Samples[h];
                    float x1 = h * (nu * d1 + xx);
                    if (x1 >= bitmap.Width) break;
                    float y1 = 0f;
                    DrawLabels(graphics, h, sample, x1, y1, nu * d1, 15);
                    y1 += 20f;
                    DrawXs(graphics, sample.Xs, x1, y1, nu * d1, nv * d1, nu, nv);
                    y1 += 20f;
                    DrawYs(graphics, sample.Ys, x1, y1, nu * d1, 20f);
                }
            }
        }

        #endregion
    }
}
