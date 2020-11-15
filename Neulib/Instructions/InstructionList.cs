using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Numerics;

namespace Neulib.Instructions
{
    public class InstructionList : List<Instruction>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public InstructionList()
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region InstructionList

        public virtual void Draw(Graphics graphics, Func<Single2, PointF> toScreen)
        {
            using (Pen pen = new Pen(Color.Black, 1f))
            {
                List<PointF> points = new List<PointF>();
                ForEach(instruction =>
                {
                    points.Add(toScreen(instruction.Point));
                    switch (instruction.Code)
                    {
                        case InstructionEnum.Rectangle:
                            PointF p1 = points[0];
                            PointF p2 = points[1];
                            graphics.DrawRectangle(pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
                            points.Clear();
                            break;
                    }
                });

            }
        }

        #endregion
    }
}
