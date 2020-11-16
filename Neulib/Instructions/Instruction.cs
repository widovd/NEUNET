using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Numerics;

namespace Neulib.Instructions
{
    public enum InstructionEnum
    {
        Add, Rectangle, Ellipse, Polygon
    }
    
    public class Instruction
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public InstructionEnum Code { get; set; }
        public Single2 Point { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Instruction(Single2 point, InstructionEnum code)
        {
            Point = point;
            Code = code;
        }

        public Instruction(float x, float y, InstructionEnum code, Transform transform)
        {
            Point = transform * new Single2(x, y);
            Code = code;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Instruction

        #endregion
    }
}
