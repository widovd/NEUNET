using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neulib.Numerics;
using static System.Math;

namespace Neunet.Images
{
    public partial class CoefficientsImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private Single1D _values;
        public Single1D Values
        {
            get { return _values; }
            set
            {
                _values = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors
        public CoefficientsImage()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        public override void DrawImage(Bitmap bitmap)
        {
            base.DrawImage(bitmap);
            if (Values == null) return;
            DrawCoefficients(bitmap);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region CoefficientsImage

        private float MaxValue()
        {
            int count = Values.Count;
            float max = float.NaN;
            for (int i = 0; i < count; i++)
            {
                float value = Values[i];
                if (float.IsNaN(max) || value > max) max = value;
            }
            return max;
        }

        private float MinValue()
        {
            int count = Values.Count;
            float min = float.NaN;
            for (int i = 0; i < count; i++)
            {
                float value = Values[i];
                if (float.IsNaN(min) || value < min) min = value;
            }
            return min;
        }

        private float Interpolate(float x)
        {
            int n = Values.Count;
            if (n == 0) return 0f;
            float fx = (n - 1) * x;
            int ix = (int)fx;
            if (ix < 0) ix = 0; else if (ix > n - 2) ix = n - 2;
            fx -= ix;
            return (1f - fx) * Values[ix] + fx * Values[ix + 1];
        }

        private void DrawCoefficients(Bitmap bitmap)
        {
            float max = MaxValue();
            float min = MinValue();
            float delta = max - min;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Pen pen = new Pen(Color.Black, 1f))
            using (SolidBrush brush = new SolidBrush(Color.Gray))
            {
                int w = bitmap.Width;
                int h = bitmap.Height;
                for (int i = 0; i < w; i++)
                {
                    float x = (float)i / (w - 1);
                    float z = delta > 0 ? (Interpolate(x) - min) / delta : 0.5f;
                    pen.Color = ColorUtilities.GetColor(z, Color.Black, Color.White);
                    graphics.DrawLine(pen, i, 0, i, h);
                }
            }
        }

        #endregion
    }
}
