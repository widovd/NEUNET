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

namespace Neunet.Images.Charts2D
{
    public partial class MatrixImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private ByteArray _byteArray;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public ByteArray ByteArray
        {
            get { return _byteArray; }
            set
            {
                _byteArray = value;
                RefreshImage();
            }
        }

        private int _imageIndex;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int ImageIndex
        {
            get { return _imageIndex; }
            set
            {
                _imageIndex = value;
                RefreshImage();
            }
        }


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
            if (_byteArray == null) return;
            DrawPixels(bitmap);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region MatrixImage

        private void DrawPixels(Bitmap bitmap)
        {
            int dim0 = ByteArray.GetLength(0);
            if (ImageIndex < 0 || ImageIndex >= dim0) return;
            int nu = ByteArray.GetLength(1);
            int nv = ByteArray.GetLength(2);
            PointF[,] points = new PointF[nu + 1, nv + 1];
            float s = Math.Min(bitmap.Width, bitmap.Height);
            for (int i = 1; i <= nu; i++)
            {
                float x = (float)i / nu;
                for (int j = 0; j <= nv; j++)
                {
                    float y = (float)j / nv;
                    points[i, j] = new PointF(x * s, y * s);
                }
            }
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Pen pen = new Pen(Color.Black, 1f))
            using (SolidBrush brush = new SolidBrush(Color.Gray))
            {
                for (int i = 0; i < nu; i++)
                    for (int j = 0; j < nv; j++)
                    {
                        PointF p1 = points[i, j];
                        PointF p2 = points[i + 1, j + 1];
                        float z = ByteArray[ImageIndex, j, i] / 255f;
                        brush.Color = ColorUtilities.GetColor(z, Color.Black, Color.White);
                        graphics.FillRectangle(brush, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
                    }
            }
        }

        #endregion
    }
}
