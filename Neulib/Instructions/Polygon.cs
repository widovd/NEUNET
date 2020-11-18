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
    public class Polygon : Instruction,  IList<Single2>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Color Color { get; set; } = Color.Black;

        public float Width { get; set; } = 1f;

        /// <summary>
        /// The drawing instructions.
        /// </summary>
        private List<Single2> Points2 { get; set; } = new List<Single2>();

        /// <summary>
        /// The number of drawing instructions.
        /// </summary>
        public int Count { get => Points2.Count; }

        public bool IsReadOnly => ((ICollection<Single2>)Points2).IsReadOnly;

        /// <summary>
        /// The drawing instruction.
        /// </summary>
        /// <param name="index">The index of the drawing instruction.</param>
        /// <returns>The drawing instruction.</returns>
        public Single2 this[int index] { get => Points2[index]; set => Points2[index] = value; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Polygon()
        { }

        public Polygon(Color color, float width)
        {
            Color = color;
            Width = width;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Instruction

        //graphics.DrawPolygon(pen, points.ToArray());
        //PointF p1 = points[0];
        //PointF p2 = points[1];
        //graphics.DrawEllipse(pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
        //graphics.DrawRectangle(pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);

        public override void Draw(Graphics graphics, Func<Single2, PointF> toScreen)
        {
            int count = Count;
            PointF[] pointsF = new PointF[count];
            for (int i = 0; i < count; i++)
            {
                pointsF[i] = toScreen(Points2[i]);
            }
            using (Pen pen = new Pen(Color, Width))
            {
                graphics.DrawPolygon(pen, pointsF);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Polygon

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Single2 item)
        {
            return Points2.IndexOf(item);
        }

        public void Insert(int index, Single2 item)
        {
            Points2.Insert(index, item);
        }

        public void Add(Single2 item)
        {
            Points2.Add(item);
        }

        public void Add(float x, float y)
        {
            Points2.Add(new Single2(x, y));
        }

        public void RemoveAt(int index)
        {
            Points2.RemoveAt(index);
        }

        public bool Remove(Single2 item)
        {
            return Points2.Remove(item);
        }

        public void Clear()
        {
            Points2.Clear();
        }

        public bool Contains(Single2 item)
        {
            return Points2.Contains(item);
        }

        public void CopyTo(Single2[] array, int arrayIndex)
        {
            Points2.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Single2> GetEnumerator()
        {
            return Points2.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Points2.GetEnumerator();
        }

        #endregion
    }
}
