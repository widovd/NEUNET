using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;

namespace Neulib.Bugs
{

    public class VisualList : Visual, IList<Visual>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The connections with the neurons in the previous layer.
        /// </summary>
        private List<Visual> Visuals { get; set; } = new List<Visual>();

        /// <summary>
        /// The number of connections of this buglist with the previous layer.
        /// </summary>
        public int Count { get => Visuals.Count; }

        public bool IsReadOnly => ((ICollection<Visual>)Visuals).IsReadOnly;

        /// <summary>
        /// Returns the connection with the buglist in the previous layer.
        /// </summary>
        /// <param name="index">The index of the buglist in the previous layer.</param>
        /// <returns>The connection with the buglist in the previous layer.</returns>
        public Visual this[int index] { get => Visuals[index]; set => Visuals[index] = value; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new buglist.
        /// </summary>
        public VisualList()
        {
        }

        /// <summary>
        /// Creates a new buglist from the stream.
        /// </summary>
        public VisualList(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Visual visual = (Visual)stream.ReadValue(serializer);
                visual.Parent = this;
                Visuals.Add(visual);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            VisualList value = o as VisualList ?? throw new InvalidTypeException(o, nameof(VisualList), 166408);
            int count = value.Visuals.Count;
            Visuals.Clear();
            for (int i = 0; i < count; i++)
            {
                Visual visual = (Visual)value.Visuals[i].Clone();
                visual.Parent = this;
                Visuals.Add(visual);
            }
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            int count = Visuals.Count;
            stream.WriteInt(count);
            for (int i = 0; i < count; i++)
            {
                stream.WriteValue(Visuals[i], serializer);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BugList


        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Visual item)
        {
            return Visuals.IndexOf(item);
        }

        public void Insert(int index, Visual item)
        {
            Visuals.Insert(index, item);
        }

        public void Add(Visual item)
        {
            Visuals.Add(item);
        }

        public void RemoveAt(int index)
        {
            Visuals.RemoveAt(index);
        }

        public bool Remove(Visual item)
        {
            return Visuals.Remove(item);
        }

        public void Clear()
        {
            Visuals.Clear();
        }

        public bool Contains(Visual item)
        {
            return Visuals.Contains(item);
        }

        public void CopyTo(Visual[] array, int arrayIndex)
        {
            Visuals.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Visual> GetEnumerator()
        {
            return Visuals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Visuals.GetEnumerator();
        }

        public void ForEach(Action<Visual> action, bool parallel = false)
        {
            int count = Visuals.Count;
            if (parallel)
                Parallel.For(0, count, (int i) => action(Visuals[i]));
            else
                for (int i = 0; i < count; i++)
                {
                    action(Visuals[i]);
                }
        }

        #endregion
    }
}
