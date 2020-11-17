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
    public class Moveable : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Visual Parent { get; set; }

        public Transform Transform { get; set; } = Transform.Default;

        public Visual _visual;
        public Visual Visual
        {
            get { return _visual; }
            private set
            {
                _visual = value;
                _visual.Parent = this;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Moveable()
        {
            Visual = new Visual();
        }

        public Moveable(Visual visual)
        {
            Visual = visual;
        }

        public Moveable(params Moveable[] moveables)
        {
            Visual = new Visual();
            int count = moveables.Length;
            for (int i = 0; i < count; i++)
            {
                Moveable moveable = moveables[i];
                Visual.Add(moveable);
            }
        }

        public Moveable(Stream stream, Serializer serializer) : base(stream, serializer)
        {
            Transform = stream.ReadTransform();
            Visual = (Visual)stream.ReadValue(serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Moveable value = o as Moveable ?? throw new InvalidTypeException(o, nameof(Moveable), 610504);
            Transform = value.Transform;
            Visual = (Visual)value.Visual.Clone();
        }

        public override void WriteToStream(Stream stream, Serializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteTransform(Transform);
            stream.WriteValue(Visual, serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Moveable

        public virtual void Randomize(Random random)
        {
            Visual.Randomize(random);
        }

        public void Step(float dt, WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            Visual.Step(dt, settings, reporter, tokenSource);
        }

        public void AddInstructions(InstructionList instructions, Transform transform)
        {
            Visual.AddInstructions(instructions, transform * Transform);
        }

        #endregion
    }
}
