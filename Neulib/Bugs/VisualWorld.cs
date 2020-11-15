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

namespace Neulib.Bugs
{
    public class VisualWorld : VisualList
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float XLo { get; set; } = 0;
        public float XHi { get; set; } = 600f;
        public float YLo { get; set; } = 0;
        public float YHi { get; set; } = 400f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public VisualWorld()
        {
        }

        public VisualWorld(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Add(new Visual());
            }
        }

        public VisualWorld(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            float XLo = stream.ReadSingle();
            float XHi = stream.ReadSingle();
            float YLo = stream.ReadSingle();
            float YHi = stream.ReadSingle();
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

        protected override void AddInstructions(InstructionList instructions)
        {
            base.AddInstructions(instructions);
            instructions.Add(new Instruction(new Single2(XLo, YLo), InstructionEnum.Add));
            instructions.Add(new Instruction(new Single2(XHi, YHi), InstructionEnum.Rectangle));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region VisualList


        #endregion
        // ----------------------------------------------------------------------------------------
        #region VisualWorld

        public static VisualWorld Randomize(VisualWorld world, Random random)
        {
            return world;
        }

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
