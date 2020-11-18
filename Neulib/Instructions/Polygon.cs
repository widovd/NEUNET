using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Numerics;
using static System.Math;

namespace Neulib.Instructions
{
    /// <summary>
    /// A polygon which is a collection of vertices.
    /// </summary>
    public class Polygon : Instruction, IList<Single2>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The color of the polygon lines.
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        /// <summary>
        /// The width of the polygon lines.
        /// </summary>
        public float Width { get; set; } = 1f;

        /// <summary>
        /// The vertices.
        /// </summary>
        private List<Single2> Vertices { get; set; } = new List<Single2>();

        /// <summary>
        /// The number of vertices.
        /// </summary>
        public int Count { get => Vertices.Count; }

        public bool IsReadOnly => ((ICollection<Single2>)Vertices).IsReadOnly;

        /// <summary>
        /// The vertex.
        /// </summary>
        /// <param name="index">The index of the vertex.</param>
        /// <returns>The vertex.</returns>
        public Single2 this[int index] { get => Vertices[index]; set => Vertices[index] = value; }

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
            PointF[] points = new PointF[count];
            for (int i = 0; i < count; i++)
            {
                points[i] = toScreen(Vertices[i]);
            }
            using (Pen pen = new Pen(Color, Width))
            {
                graphics.DrawPolygon(pen, points);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Polygon

        /// <summary>
        /// Adds a part of a circle.
        /// </summary>
        /// <param name="x">The x coordinate of the center.</param>
        /// <param name="y">The y coordinate of the center.</param>
        /// <param name="r">The radius of the circle.</param>
        /// <param name="i1">The first index (inclusive).</param>
        /// <param name="i2">The last index (inclusive).</param>
        /// <param name="n">The circle is divided into this number of parts.</param>
        /// <param name="transform">The transform.</param>
        private void AddCirclePart(float x, float y, float r, int i1, int i2, int n, Transform transform)
        {
            for (int i = i1; i <= i2; i++)
            {
                double a = 2d * PI * i / n;
                Add(transform.Apply(x + r * (float)Cos(a), y + r * (float)Sin(a)));
            }
        }

        /// <summary>
        /// Returns a new polygon as a rounded rectangle.
        /// </summary>
        /// <param name="x1">The x coordinate of the first point of the rectangle.</param>
        /// <param name="y1">The y coordinate of the first point of the rectangle.</param>
        /// <param name="x2">The x coordinate of the second point of the rectangle.</param>
        /// <param name="y2">The y coordinate of the second point of the rectangle.</param>
        /// <param name="r">The radius of the circle.</param>
        /// <param name="n">The circle is divided into this number of parts.</param>
        /// <param name="transform">The transform.</param>
        /// <param name="color">The color of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <returns></returns>
        public static Polygon RoundedRectangle(
            float x1, float y1, float x2, float y2, float r, int n,
            Transform transform, Color color, float width)
        {
            Polygon polygon = new Polygon(color, width);
            polygon.AddCirclePart(x2 - r, y2 - r, r, 0, n / 4, n, transform);
            polygon.Add(transform.Apply(x1 + r, y2));
            polygon.AddCirclePart(x1 + r, y2 - r, r, n / 4, n / 2, n, transform);
            polygon.Add(transform.Apply(x1, y1 + r));
            polygon.AddCirclePart(x1 + r, y1 + r, r, n / 2, 3 * n / 4, n, transform);
            polygon.Add(transform.Apply(x2 - r, y1));
            polygon.AddCirclePart(x2 - r, y1 + r, r, 3 * n / 4, n, n, transform);
            return polygon;
        }
        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Single2 vertex)
        {
            return Vertices.IndexOf(vertex);
        }

        public void Insert(int index, Single2 vertex)
        {
            Vertices.Insert(index, vertex);
        }

        public void Add(Single2 vertex)
        {
            Vertices.Add(vertex);
        }

        public void Add(float x, float y)
        {
            Vertices.Add(new Single2(x, y));
        }

        public void RemoveAt(int index)
        {
            Vertices.RemoveAt(index);
        }

        public bool Remove(Single2 vertex)
        {
            return Vertices.Remove(vertex);
        }

        public void Clear()
        {
            Vertices.Clear();
        }

        public bool Contains(Single2 vertex)
        {
            return Vertices.Contains(vertex);
        }

        public void CopyTo(Single2[] array, int arrayIndex)
        {
            Vertices.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Single2> GetEnumerator()
        {
            return Vertices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Vertices.GetEnumerator();
        }

        #endregion
    }
}
