using System;
using System.IO;
using System.Collections.Generic;
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
    public class VisualWorld : Visual
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float XLo { get; set; } = 0;
        public float XHi { get; set; } = 1200f;
        public float YLo { get; set; } = 0;
        public float YHi { get; set; } = 800f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public VisualWorld()
        {
        }

        public VisualWorld(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            XLo = stream.ReadSingle();
            XHi = stream.ReadSingle();
            YLo = stream.ReadSingle();
            YHi = stream.ReadSingle();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            VisualWorld value = o as VisualWorld ?? throw new InvalidTypeException(o, nameof(VisualWorld), 550727);
            XLo = value.XLo;
            XHi = value.XHi;
            YLo = value.YLo;
            YHi = value.YHi;
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteSingle(XLo);
            stream.WriteSingle(XHi);
            stream.WriteSingle(YLo);
            stream.WriteSingle(YHi);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Visual

        public override void Randomize(Random random)
        {
            base.Randomize(random);
        }

        public override void Step(WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            base.Step(settings, reporter, tokenSource);
        }

        public override void AddInstructions(InstructionList instructions, Transform transform)
        {
            base.AddInstructions(instructions, transform);
            instructions.Add(new Instruction(transform * new Single2(XLo, YLo), InstructionEnum.Add));
            instructions.Add(new Instruction(transform * new Single2(XHi, YHi), InstructionEnum.Rectangle));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region VisualWorld

        public void Learn(WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {

        }

        public void Run(WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            while (true)
            {
                tokenSource?.Token.ThrowIfCancellationRequested();
                ForEach(visual => visual.Step(settings, reporter, tokenSource), true);
            }
        }

        #endregion
    }
}
