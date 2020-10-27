using System;
using System.Drawing;
using Neulib.Numerics;
using Neulib.Extensions;
using static System.Math;

namespace Neunet.Images.Charts3D
{
    public class WireframePoint
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Single3 Origin { get; set; } = Single3.NaN;

        public Single3 Direction { get; set; } = Single3.NaN;

        public Single3 PrevDirection { get; set; } = Single3.NaN;

        public Single3 Normal { get; set; } = Single3.NaN;

        public float FluxPerUnit { get; set; } = float.NaN;

        private int Flags { get; set; } = 0;

        private const byte _isReflectedBit = 2;
        public bool IsReflection
        {
            get { return Flags.GetBit(_isReflectedBit); }
            set { Flags = Flags.SetBit(_isReflectedBit, value); }
        }

        private const byte _isTirBit = 3;
        public bool IsTir
        {
            get { return Flags.GetBit(_isTirBit); }
            set { Flags = Flags.SetBit(_isTirBit, value); }
        }

        public PointF OriginF { get; protected set; }

        public PointF RayEndF { get; protected set; }

        public PointF NormalEndF { get; protected set; }

        public float Deflection
        {
            get
            {
                if (Single3.IsNaN(PrevDirection) || Single3.IsNaN(Direction)) return float.NaN;
                return (float)Acos(PrevDirection * Direction).ToDegrees();
            }
        }

        public float DrawAngle
        {
            get
            {
                if (Single3.IsNaN(Normal)) return float.NaN;
                return (float)(90d - Acos(Normal.Z).ToDegrees());
            }
        }

        public float NormalAngle
        {
            get
            {
                if (Single3.IsNaN(Normal)) return float.NaN;
                return (float)(Acos(Normal.Z).ToDegrees());
            }
        }

        public float IncidentAngle
        {
            get
            {
                if (Single3.IsNaN(PrevDirection) || Single3.IsNaN(Normal)) return float.NaN;
                return (float)Acos(Abs(PrevDirection * Normal)).ToDegrees();
            }
        }
        public float RefractionAngle
        {
            get
            {
                if (Single3.IsNaN(Direction) || Single3.IsNaN(Normal)) return float.NaN;
                return (float)Acos(Abs(Direction * Normal)).ToDegrees();
            }
        }

        public bool HasFlux
        {
            get { return !double.IsNaN(FluxPerUnit) && FluxPerUnit >= 0f; }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public WireframePoint()
        {
            OriginF = new PointF(float.NaN, float.NaN);
            RayEndF = new PointF(float.NaN, float.NaN);
            NormalEndF = new PointF(float.NaN, float.NaN);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region WireframePointBase

        public virtual void CalculatePointsF(float length, Func<Single3, PointF> func)
        {
            OriginF = new PointF(float.NaN, float.NaN); // default
            RayEndF = new PointF(float.NaN, float.NaN); // default
            NormalEndF = new PointF(float.NaN, float.NaN); // default
            if (!Single3.IsNaN(Origin))
            {
                OriginF = func(Origin);
                if (!double.IsNaN(length))
                {
                    if (!Single3.IsNaN(Direction))
                        RayEndF = func(Origin + length * Direction);
                    if (!Single3.IsNaN(Normal))
                        NormalEndF = func(Origin + length * Normal);
                }
            }
        }

        public void UpdateDimensions(WireframeDimensions dimensions)
        {
            dimensions.UpdateDimensions(Origin);
        }

        #endregion
    }
}
