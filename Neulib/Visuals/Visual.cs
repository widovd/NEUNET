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
        private List<Moveable> Visuals { get; set; } = new List<Moveable>();

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
        public Moveable this[int index] { get => Visuals[index]; set => Visuals[index] = value; }

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
                Visuals.Add(moveable);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Visual value = o as Visual ?? throw new InvalidTypeException(o, nameof(Visual), 951859);
            int count = value.Visuals.Count;
            Visuals.Clear();
            for (int i = 0; i < count; i++)
            {
                Moveable moveable = (Moveable)value.Visuals[i].Clone();
                moveable.Parent = this;
                Visuals.Add(moveable);
            }
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            int count = Visuals.Count;
            stream.WriteInt(count);
            for (int i = 0; i < count; i++)
                stream.WriteValue(Visuals[i], serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Moveable item)
        {
            return Visuals.IndexOf(item);
        }

        public void Insert(int index, Moveable item)
        {
            Visuals.Insert(index, item);
        }

        public void Add(Moveable item)
        {
            Visuals.Add(item);
        }

        public void RemoveAt(int index)
        {
            Visuals.RemoveAt(index);
        }

        public bool Remove(Moveable item)
        {
            return Visuals.Remove(item);
        }

        public void Clear()
        {
            Visuals.Clear();
        }

        public bool Contains(Moveable item)
        {
            return Visuals.Contains(item);
        }

        public void CopyTo(Moveable[] array, int arrayIndex)
        {
            Visuals.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Moveable> GetEnumerator()
        {
            return Visuals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Visuals.GetEnumerator();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region VisualList

        public void ForEach(Action<Moveable> action, bool parallel = false)
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
