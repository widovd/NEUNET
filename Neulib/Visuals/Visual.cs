using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;
using Neulib.Numerics;
using Neulib.Instructions;

namespace Neulib.Visuals
{
    public class Visual : BaseObject, IList<Moveable>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Moveable Parent { get; set; }

        /// <summary>
        /// The connections with the neurons in the previous layer.
        /// </summary>
        private List<Moveable> Moveables { get; set; } = new List<Moveable>();

        /// <summary>
        /// The number of connections of this buglist with the previous layer.
        /// </summary>
        public int Count { get => Moveables.Count; }

        public bool IsReadOnly => ((ICollection<Visual>)Moveables).IsReadOnly;

        /// <summary>
        /// Returns the connection with the buglist in the previous layer.
        /// </summary>
        /// <param name="index">The index of the buglist in the previous layer.</param>
        /// <returns>The connection with the buglist in the previous layer.</returns>
        public Moveable this[int index] { get => Moveables[index]; set => Moveables[index] = value; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Visual()
        {
        }

        public Visual(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Moveable moveable = (Moveable)stream.ReadValue(serializer);
                moveable.Parent = this;
                Moveables.Add(moveable);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Visual value = o as Visual ?? throw new InvalidTypeException(o, nameof(Visual), 951859);
            int count = value.Moveables.Count;
            Moveables.Clear();
            for (int i = 0; i < count; i++)
            {
                Moveable moveable = (Moveable)value.Moveables[i].Clone();
                moveable.Parent = this;
                Moveables.Add(moveable);
            }
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            int count = Moveables.Count;
            stream.WriteInt(count);
            for (int i = 0; i < count; i++)
                stream.WriteValue(Moveables[i], serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Moveable item)
        {
            return Moveables.IndexOf(item);
        }

        public void Insert(int index, Moveable item)
        {
            Moveables.Insert(index, item);
        }

        public void Add(Moveable item)
        {
            Moveables.Add(item);
        }

        public void RemoveAt(int index)
        {
            Moveables.RemoveAt(index);
        }

        public bool Remove(Moveable item)
        {
            return Moveables.Remove(item);
        }

        public void Clear()
        {
            Moveables.Clear();
        }

        public bool Contains(Moveable item)
        {
            return Moveables.Contains(item);
        }

        public void CopyTo(Moveable[] array, int arrayIndex)
        {
            Moveables.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Moveable> GetEnumerator()
        {
            return Moveables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Moveables.GetEnumerator();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region VisualList

        public void ForEach(Action<Moveable> action, bool parallel = false)
        {
            int count = Moveables.Count;
            if (parallel)
                Parallel.For(0, count, (int i) => action(Moveables[i]));
            else
                for (int i = 0; i < count; i++)
                {
                    action(Moveables[i]);
                }
        }

        public virtual void Randomize(Random random)
        {
            ForEach(moveable => moveable.Visual.Randomize(random));
        }

        public virtual void Step(WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            ForEach(moveable => moveable.Visual.Step(settings, reporter, tokenSource));
        }

        public virtual void AddInstructions(InstructionList instructions, Transform transform)
        {
            ForEach(moveable => moveable.Visual.AddInstructions(instructions, transform * moveable.Transform));
        }

        #endregion
    }
}
