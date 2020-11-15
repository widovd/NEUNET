using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neulib.Bugs;
using Neulib.Numerics;

namespace Neunet.Images
{
    public partial class BugWorldImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private VisualWorld _bugWorld;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public VisualWorld BugWorld
        {
            get { return _bugWorld; }
            set
            {
                _bugWorld = value;
                RefreshImage();
            }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public BugWorldImage()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        public override void DrawImage(Bitmap bitmap)
        {
            base.DrawImage(bitmap);
            if (BugWorld == null) return;

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                BugWorld.ForEach(bug => bug.GetInstructions().Draw(graphics, ToScreen));
                BugWorld.GetInstructions().Draw(graphics, ToScreen);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BugWorldImage

        private PointF ToScreen(Single2 pos)
        {
            const float margin = 3f;
            Size size = pictureBox.Size;
            float a = (size.Width - 2f * margin) / (BugWorld.XHi - BugWorld.XLo);
            float b = (size.Height - 2f * margin) / (BugWorld.YHi - BugWorld.YLo);
            float x, y;
            if (a > b)
            {
                x = margin + b * (pos.X - BugWorld.XLo);
                y = margin + b * (pos.Y - BugWorld.YLo);
            }
            else
            {
                x = margin + a * (pos.X - BugWorld.XLo);
                y = margin + a * (pos.Y - BugWorld.YLo);
            }
            return new PointF(x, y);
        }


        #endregion
    }
}
