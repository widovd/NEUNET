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
    public class InstructionList : IList<Instruction>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The drawing instructions.
        /// </summary>
        private List<Instruction> Instructions { get; set; } = new List<Instruction>();

        /// <summary>
        /// The number of drawing instructions.
        /// </summary>
        public int Count { get => Instructions.Count; }

        public bool IsReadOnly => ((ICollection<Instruction>)Instructions).IsReadOnly;

        /// <summary>
        /// The drawing instruction.
        /// </summary>
        /// <param name="index">The index of the drawing instruction.</param>
        /// <returns>The drawing instruction.</returns>
        public Instruction this[int index] { get => Instructions[index]; set => Instructions[index] = value; }


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
            ForEach(instruction => instruction.Draw(graphics, toScreen));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Instruction item)
        {
            return Instructions.IndexOf(item);
        }

        public void Insert(int index, Instruction item)
        {
            Instructions.Insert(index, item);
        }

        public void Add(Instruction item)
        {
            Instructions.Add(item);
        }

        public void RemoveAt(int index)
        {
            Instructions.RemoveAt(index);
        }

        public bool Remove(Instruction item)
        {
            return Instructions.Remove(item);
        }

        public void Clear()
        {
            Instructions.Clear();
        }

        public bool Contains(Instruction item)
        {
            return Instructions.Contains(item);
        }

        public void CopyTo(Instruction[] array, int arrayIndex)
        {
            Instructions.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Instruction> GetEnumerator()
        {
            return Instructions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Instructions.GetEnumerator();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region InstructionList

        public void ForEach(Action<Instruction> action)
        {
            int count = Instructions.Count;
            for (int i = 0; i < count; i++)
            {
                action(Instructions[i]);
            }
        }

        #endregion
    }
}
