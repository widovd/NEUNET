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
        Add, Rectangle
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

        public Instruction(Single2 point, InstructionEnum code, Transform transform)
        {
            Point = point;
            Code = code;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Instruction

        #endregion
    }
}
