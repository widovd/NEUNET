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
using Neulib.Exceptions;
using Neunet.Forms;
using static System.Math;

namespace Neunet.Images.Charts2D
{
    public partial class SamplesImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private SampleList _samples;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public SampleList Samples
        {
            get { return _samples; }
            set
            {
                _samples = value;
                UpdateScrollBar();
            }
        }

        private float _labelHeight = 14f;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float LabelHeight
        // The height of the rectangular label area
        {
            get { return _labelHeight; }
            set
            {
                _labelHeight = value;
                UpdateScrollBar();
            }
        }

        private float _outputHeight = 30f;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        // The height of the rectangular area where the y values are shown: sample.Ys[i]
        public float OutputHeight
        {
            get { return _outputHeight; }
            set
            {
                _outputHeight = value;
                UpdateScrollBar();
            }
        }

        private float _gapHeight = 4f;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        // The vertical gap between the label, input, and output areas
        public float GapHeight
        {
            get { return _gapHeight; }
            set
            {
                _gapHeight = value;
                UpdateScrollBar();
            }
        }

        private float _gapWidth = 4f;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        // The horizontal gap between the sample items
        public float GapWidth
        {
            get { return _gapWidth; }
            set
            {
                _gapWidth = value;
                UpdateScrollBar();
            }
        }

        private float InputSize
        // The width and the height of the input area where the x values (sample.Xs[i]) are shown as a matrix of dots
        {
            get
            {
                int nu = Samples.NU; // the horizontal size of the matrix of x values
                int nv = Samples.NV; // the vertical size of the matrix of x values
                return nu * (pictureBox.Height - LabelHeight - OutputHeight - 2 * GapHeight) / Max(nu, nv);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public SamplesImage()
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
            float w = InputSize;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {

                float x = 0f;
                for (int iSample = hScrollBar.Value; iSample < hScrollBar.Value + hScrollBar.LargeChange; iSample++)
                {
                    Sample sample = Samples[iSample];
                    //if (x >= bitmap.Width) break;
                    float y = 0f;
                    DrawLabels(graphics, $"{iSample}: [{sample.Index}] = {sample.Label}", x, y, w, LabelHeight);
                    y += LabelHeight + GapHeight;
                    DrawInputs(graphics, sample.Xs, x, y, w, w, Samples.NU, Samples.NV);
                    y += w + GapHeight;
                    DrawOutputs(graphics, sample.Ys, sample.Zs, x, y, w, OutputHeight);
                    x += w + GapWidth;
                }
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region MatrixImage

        private void UpdateScrollBar()
        {
            if (Samples == null) return;
            hScrollBar.Minimum = 0;
            hScrollBar.Maximum = Samples.Count - 1;


            hScrollBar.LargeChange = (int)(pictureBox.Width / (InputSize + GapWidth));
            RefreshImage();
        }

        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            RefreshImage();
        }


        protected override void ImageSizeChanged()
        {
            base.ImageSizeChanged();
            UpdateScrollBar();
        }

        private static void DrawLabels(Graphics graphics, string label, float x, float y, float w, float h)
        {
            using (SolidBrush stringBrush = new SolidBrush(Color.Black))
            using (Pen pen = new Pen(Color.Black, 1f))
            using (StringFormat stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Near,
                Alignment = StringAlignment.Near,
            })
            {
                graphics.DrawString(label, DefaultFont, stringBrush, x, y, stringFormat);
                graphics.DrawRectangle(pen, x, y, w, h);
            }
        }

        private static void DrawInputs(Graphics graphics, float[] xs, float x, float y, float w, float h, int nu, int nv)
        {
            using (Pen pen = new Pen(Color.Black, 1f))
            using (SolidBrush fillBrush = new SolidBrush(Color.Black))
            using (StringFormat stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Near,
                Alignment = StringAlignment.Near,
            })
            {
                float d1 = Min(w / nu, h / nv);
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
                graphics.DrawRectangle(pen, x, y, w, h);
            }
        }

        private static void DrawOutputs(Graphics graphics, float[] ys, float[] zs, float x, float y, float w, float h)
        {
            const float sh = 14f;
            float hb = h - sh;
            int ny = ys.Length;
            float d2 = w / ny;
            float xgap = 1f;
            float dx = d2 - 2 * xgap;
            using (Pen peny = new Pen(Color.DarkRed, 3f)) // required y values
            using (Pen penz = new Pen(Color.DarkGray, 1f)) // feedforward y values
            using (Pen penr = new Pen(Color.Black, 1f)) // rectangle
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
                    //fillBrush.Color = i == iMax ? Color.Red : Color.Gray;
                    float xi = x + xgap + i * d2;
                    float yy = ys[i];
                    float y1 = y + hb * (1f - yy);
                    float zz = zs[i];
                    float z1 = y + hb * (1f - zz);
                    graphics.DrawRectangle(penr, xi, y, dx, hb);
                    graphics.FillRectangle(fillBrush, xi+1f, z1, dx - 1f, hb * zz);
                    //graphics.DrawLine(penz, xi, z1, xi + dx, z1);
                    graphics.DrawLine(peny, xi, y1, xi + dx + 1f, y1);
                    graphics.DrawString($"{i}", DefaultFont, stringBrush, xi, y + hb, stringFormat);
                }
            }
        }

        #endregion

    }
}
