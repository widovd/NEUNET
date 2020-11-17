using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
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
    public sealed class World : Moveable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float XLo { get; set; } = -600;
        public float XHi { get; set; } = 600f;
        public float YLo { get; set; } = -400;
        public float YHi { get; set; } = 400f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public World()
        {
        }

        public World(Visual items) : base(items)
        {
        }

        public World(params Moveable[] items) : base(items)
        {
        }

        public World(Stream stream, Serializer serializer) : base(stream, serializer)
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
            World value = o as World ?? throw new InvalidTypeException(o, nameof(World), 550727);
            XLo = value.XLo;
            XHi = value.XHi;
            YLo = value.YLo;
            YHi = value.YHi;
        }

        public override void WriteToStream(Stream stream, Serializer serializer)
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

        public override void UpdateTransforms()
        {
            base.UpdateTransforms();
        }

        public override void Randomize(Random random)
        {
            base.Randomize(random);
        }

        public override void Step(float dt, WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            base.Step(dt, settings, reporter, tokenSource);
        }

        public override void AddInstructions(InstructionList instructions, Transform transform)
        {
            base.AddInstructions(instructions, transform);
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region World


        public InstructionList GetInstructions()
        {
            InstructionList instructions = new InstructionList();
            Visual.AddInstructions(instructions, Transform);
            instructions.Add(new Instruction(new Single2(XLo, YLo), InstructionEnum.Add));
            instructions.Add(new Instruction(new Single2(XHi, YHi), InstructionEnum.Rectangle));
            return instructions;
        }

        public void Run(WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            long ticks = timer.Elapsed.Ticks;
            while (true)
            {
                tokenSource?.Token.ThrowIfCancellationRequested();
                long prevTicks = ticks;
                ticks = timer.Elapsed.Ticks;
                float dt = (float)(ticks - prevTicks) / TimeSpan.TicksPerSecond;
                Step(dt, settings, reporter, tokenSource);
                UpdateTransforms();
            }
        }

        #endregion
    }
}
