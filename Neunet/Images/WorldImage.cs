using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neulib.Exceptions;
using Neulib.Visuals;
using Neulib.Numerics;
using Neulib.Instructions;

namespace Neunet.Images
{
    public partial class WorldImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private World _world;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public World World
        {
            get { return _world; }
            set
            {
                _world = value;
                RefreshImage();
            }
        }

        private Single2 _origin = Single2.Zero;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Single2 Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
                RefreshImage();
            }
        }

        private const float _zoomDefault = 0.9f;
        private float _zoom = _zoomDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public WorldImage()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        private float _pixelsPerUnit = 0f;

        public override void DrawImage(Bitmap bitmap)
        {
            base.DrawImage(bitmap);
            if (World == null) return;
            UpdatePixelsPerUnit();
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                InstructionList instructions = World.GetInstructions();
                instructions.Draw(graphics, ToScreen);
            }
        }

        private Single2 _mouseDownOrigin;

        protected override void ImageMouseDown(int x0, int y0, MouseButtons buttons)
        {
            base.ImageMouseDown(x0, y0, buttons);
            _mouseDownOrigin = Origin;
        }

        protected void PictureMoving(int dx, int dy)
        {
            float zoom = Zoom;
            Single2 moveVector = new Single2(-dx / (zoom * _pixelsPerUnit), dy / (zoom * _pixelsPerUnit));
            Origin = _mouseDownOrigin + moveVector;
        }

        protected override void ImageMouseMove(int x0, int y0, int dx, int dy, MouseButtons buttons)
        {
            switch (buttons)
            {
                case MouseButtons.Right:
                    PictureMoving(dx, dy);
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
        #region WorldImage

        private void UpdatePixelsPerUnit()
        {
            Size size = pictureBox.Size;
            float a = size.Width / (World.XHi - World.XLo);
            float b = size.Height / (World.YHi - World.YLo);
            _pixelsPerUnit = a < b ? a : b;
        }

        public PointF ToScreen(Single2 pos)
        {
            Size size = pictureBox.Size;
            float x = 0.5f * size.Width + (_zoom * _pixelsPerUnit * (pos.X - _origin.X));
            float y = 0.5f * size.Height - (_zoom * _pixelsPerUnit * (pos.Y - _origin.Y));
            return new PointF(x, y);
        }

        [Obsolete]
        private PointF ToScreenOld(Single2 pos)
        {
            const float margin = 3f;
            Size size = pictureBox.Size;
            float a = (size.Width - 2f * margin) / (World.XHi - World.XLo);
            float b = (size.Height - 2f * margin) / (World.YHi - World.YLo);
            float x, y;
            if (a > b)
            {
                x = margin + b * (pos.X - World.XLo);
                y = margin + b * (pos.Y - World.YLo);
            }
            else
            {
                x = margin + a * (pos.X - World.XLo);
                y = margin + a * (pos.Y - World.YLo);
            }
            return new PointF(x, y);
        }


        #endregion
    }
}
