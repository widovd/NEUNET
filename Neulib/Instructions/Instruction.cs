using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Numerics;

namespace Neulib.Instructions
{
    public abstract class Instruction
    {
        // ----------------------------------------------------------------------------------------
        #region Instruction

        public abstract void Draw(Graphics graphics, Func<Single2, PointF> toScreen);

        #endregion
    }
}
