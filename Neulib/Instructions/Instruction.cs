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
    /// <summary>
    /// Drawing instruction.
    /// </summary>
    public abstract class Instruction
    {
        // ----------------------------------------------------------------------------------------
        #region Instruction

        /// <summary>
        /// Draws on the canvas of the graphics.
        /// </summary>
        /// <param name="graphics">The System.Drawing.Graphics</param>
        /// <param name="toScreen">Function which converts Single2 coordinates to PointF screen points.</param>
        public abstract void Draw(Graphics graphics, Func<Single2, PointF> toScreen);

        #endregion
    }
}
