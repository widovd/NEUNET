using System;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;
using Neulib.Exceptions;
using Neulib.Serializers;

namespace Neulib
{
    public class CalculationSettings : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        [
            Category("Minimization"),
            DisplayName("Max iter"),
            Description("The maximum number of iterations."),
        ]
        public int MaxIter { get; set; } = 100;

        [
            Category("Minimization"),
            DisplayName("Epsilon"),
            Description("The iteration will stop when 2 * Abs(f2 - f1) <= Tolerance * (Abs(f1) + Abs(f2) + Epsilon)."),
        ]
        public float Epsilon { get; set; } = (float)1.0e-10;


        [
            Category("Minimization"),
            DisplayName("Tolerance"),
            Description("The iteration will stop when 2 * Abs(f2 - f1) <= Tolerance * (Abs(f1) + Abs(f2) + Epsilon)."),
        ]
        public float Tolerance { get; set; } = (float)1e-5;

        [
            Category("Calculation"),
            DisplayName("Sample count"),
            Description("The number of random samples."),
        ]
        public int SampleCount { get; set; } = 100;


        [
            Category("Calculation"),
            DisplayName("Learning rate"),
            Description("The rate at which learning occurs."),
        ]
        public float LearningRate { get; set; } = 1f;

        [
            Category("Initialization"),
            DisplayName("Bias magnitude"),
            Description("The neuron bias values are initialized as a random number with this magnitude."),
        ]
        public float BiasMagnitude { get; set; } = 0.1f;

        [
            Category("Initialization"),
            DisplayName("Weight magnitude"),
            Description("The connection weight values are initialized as a random number with this magnitude."),
        ]
        public float WeightMagnitude { get; set; } = 0.1f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public CalculationSettings()
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            CalculationSettings value = o as CalculationSettings ?? throw new InvalidTypeException(o, nameof(CalculationSettings), 473835);
            MaxIter = value.MaxIter;
            Epsilon = value.Epsilon;
            Tolerance = value.Tolerance;
            SampleCount = value.SampleCount;
            LearningRate = value.LearningRate;
            BiasMagnitude = value.BiasMagnitude;
            WeightMagnitude = value.WeightMagnitude;
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region CalculationSettings

        private const string _MaxIterId = "MaxIter";
        private const string _EpsilonId = "Epsilon";
        private const string _ToleranceId = "Tolerance";
        private const string _SampleCountId = "SampleCount";
        private const string _LearningRateId = "LearningRate";
        private const string _BiasMagnitudeId = "BiasMagnitude";
        private const string _WeightMagnitudeId = "WeightMagnitude";

        public void LoadFromSettings(XmlElement rootElement)
        {
            MaxIter = rootElement.ReadInt(_MaxIterId, MaxIter);
            Epsilon = rootElement.ReadSingle(_EpsilonId, Epsilon);
            Tolerance = rootElement.ReadSingle(_ToleranceId, Tolerance);
            SampleCount = rootElement.ReadInt(_SampleCountId, SampleCount);
            LearningRate = rootElement.ReadSingle(_LearningRateId, LearningRate);
            BiasMagnitude = rootElement.ReadSingle(_BiasMagnitudeId, BiasMagnitude);
            WeightMagnitude = rootElement.ReadSingle(_WeightMagnitudeId, WeightMagnitude);
        }

        public void SaveToSettings(XmlElement rootElement)
        {
            rootElement.WriteInt(_MaxIterId, MaxIter);
            rootElement.WriteSingle(_EpsilonId, Epsilon);
            rootElement.WriteSingle(_ToleranceId, Tolerance);
            rootElement.WriteInt(_SampleCountId, SampleCount);
            rootElement.WriteSingle(_LearningRateId, LearningRate);
            rootElement.WriteSingle(_BiasMagnitudeId, BiasMagnitude);
            rootElement.WriteSingle(_WeightMagnitudeId, WeightMagnitude);
        }

        #endregion
    }
}
