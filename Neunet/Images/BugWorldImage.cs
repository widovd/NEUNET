using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neulib.Visuals;
using Neulib.Numerics;
using Neulib.Instructions;

namespace Neunet.Images
{
    public partial class BugWorldImage : BaseImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private VisualWorld _world;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public VisualWorld World
        {
            get { return _world; }
            set
            {
                _world = value;
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
            if (World == null) return;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                InstructionList instructions = new InstructionList();
                World.AddInstructions(instructions, World.Transform);
                instructions.Draw(graphics, ToScreen);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BugWorldImage

        private PointF ToScreen(Single2 pos)
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
