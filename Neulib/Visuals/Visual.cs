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
using Neulib.Extensions;
using Neulib.Serializers;
using Neulib.Numerics;
using Neulib.Instructions;
using static System.Math;

namespace Neulib.Visuals
{
    public class Visual : BaseObject, IList<Visual>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Visual Parent { get; set; }

        public Transform Transform { get; set; } = Transform.Default;

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
        public Visual this[int index]
        {
            get => Visuals[index];
            set
            {
                value.Parent = this;
                Visuals[index] = value;
            }
        }

        public Visual Top
        {
            get
            {
                Visual visual = this;
                while (visual.Parent != null)
                    visual = visual.Parent;
                return visual;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Visual()
        {
        }

        public Visual(params Visual[] visuals)
        {
            int count = visuals.Length;
            for (int i = 0; i < count; i++)
            {
                Add(visuals[i]);
            }
        }


        public Visual(Stream stream, Serializer serializer) : base(stream, serializer)
        {
            Transform = stream.ReadTransform();
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
                Add((Visual)stream.ReadValue(serializer));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Visual value = o as Visual ?? throw new InvalidTypeException(o, nameof(Visual), 951859);
            Transform = value.Transform;
            int count = value.Visuals.Count;
            Visuals.Clear();
            for (int i = 0; i < count; i++)
                Add((Visual)value.Visuals[i].Clone());
        }

        public override void WriteToStream(Stream stream, Serializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteTransform(Transform);
            int count = Visuals.Count;
            stream.WriteInt(count);
            for (int i = 0; i < count; i++)
                stream.WriteValue(Visuals[i], serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Visual item)
        {
            return Visuals.IndexOf(item);
        }

        public void Insert(int index, Visual item)
        {
            item.Parent = this;
            Visuals.Insert(index, item);
        }

        public void Add(Visual item)
        {
            item.Parent = this;
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

        #endregion
        // ----------------------------------------------------------------------------------------
        #region VisualList

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

        public void ForEach(Action<Visual, Visual> action)
        {
            int count = Visuals.Count;
            Visual visual = null;
            for (int i = 0; i < count; i++)
            {
                Visual previous = visual;
                visual = Visuals[i];
                action(previous, visual);
            }
        }

        public virtual void UpdateTransforms()
        {
            ForEach(visual => visual.UpdateTransforms());
        }

        public virtual void Randomize(Random random)
        {
            (double x, double y) = random.BoxMuller(100, 0);
            double a = 2d * PI * random.NextDouble();
            Transform = new Transform(
                new Single2((float)x, (float)y),
                Single2x2.Rotation((float)a)
                );

            ForEach(visual => visual.Randomize(random));
        }

        public virtual void Step(float dt, WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            ForEach(visual => visual.Step(dt, settings, reporter, tokenSource));
        }

        public virtual void AddInstructions(InstructionList instructions, Transform transform)
        {
            ForEach(visual => visual.AddInstructions(instructions, transform * visual.Transform));
        }

        #endregion
    }
}
