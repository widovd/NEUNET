using System;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;
using Neulib.Exceptions;
using Neulib.Serializers;

namespace Neulib
{
    public enum CostFunctionEnum
    {
        Quadratic,
        CrossEntropy,
    }

    public class CalculationSettings : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        [
            RandomizeCategory,
            DisplayName("Bias magnitude"),
            Description("The standard deviation of the gaussian distribution used to generate the initial bias values."),
        ]
        public float BiasMagnitude { get; set; } = 1f;

        [
            RandomizeCategory,
            DisplayName("Weight magnitude"),
            Description("Sqrt(connection_count) standard deviations of the gaussian distribution used to generate the initial weight values."),
        ]
        public float WeightMagnitude { get; set; } = 1f;


        [
            LearnCategory,
            DisplayName("Max iter"),
            Description("The maximum number of iterations."),
        ]
        public int MaxIter { get; set; } = 100;

        [
            LearnCategory,
            DisplayName("Epsilon"),
            Description("The iteration will stop when 2 * Abs(f2 - f1) <= Tolerance * (Abs(f1) + Abs(f2) + Epsilon)."),
        ]
        public float Epsilon { get; set; } = (float)1.0e-10;


        [
            Category("Learning"),
            DisplayName("Tolerance"),
            Description("The iteration will stop when 2 * Abs(f2 - f1) <= Tolerance * (Abs(f1) + Abs(f2) + Epsilon)."),
        ]
        public float Tolerance { get; set; } = (float)1e-5;

        [
            LearnCategory,
            DisplayName("Cost function"),
            Description("The learning algorithm aims to minimize the cost function values."),
        ]
        public CostFunctionEnum CostFunction { get; set; } = CostFunctionEnum.CrossEntropy;


        [
            LearnCategory,
            DisplayName("Training sample count"),
            Description("The number of unique random samples for training."),
        ]
        public int TrainingSampleCount { get; set; } = 100;


        [
            LearnCategory,
            DisplayName("Learning rate"),
            Description("The rate at which learning occurs."),
        ]
        public float LearningRate { get; set; } = 1f;

        [
            LearnCategory,
            DisplayName("Lambda"),
            Description("The rate at which weight regularization occurs."),
        ]
        public float Lambda { get; set; } = 0.1f;

        [
            TestCategory,
            DisplayName("Test sample count"),
            Description("The number of unique random samples for testing."),
        ]
        public int TestSampleCount { get; set; } = 100;

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
            BiasMagnitude = value.BiasMagnitude;
            WeightMagnitude = value.WeightMagnitude;
            MaxIter = value.MaxIter;
            Epsilon = value.Epsilon;
            Tolerance = value.Tolerance;
            CostFunction = value.CostFunction;
            TrainingSampleCount = value.TrainingSampleCount;
            LearningRate = value.LearningRate;
            Lambda = value.Lambda;
            TestSampleCount = value.TestSampleCount;
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region CalculationSettings

        private const string _BiasMagnitudeId = "BiasMagnitude";
        private const string _WeightMagnitudeId = "WeightMagnitude";
        private const string _MaxIterId = "MaxIter";
        private const string _EpsilonId = "Epsilon";
        private const string _ToleranceId = "Tolerance";
        private const string _CostFunctionId = "CostFunction";
        private const string _LearningSampleCountId = "LearningSampleCount";
        private const string _LearningRateId = "LearningRate";
        private const string _LambdaId = "Lambda";
        private const string _VerificationSampleCountId = "VerificationSampleCount";

        public void LoadFromSettings(XmlElement rootElement)
        {
            BiasMagnitude = rootElement.ReadSingle(_BiasMagnitudeId, BiasMagnitude);
            WeightMagnitude = rootElement.ReadSingle(_WeightMagnitudeId, WeightMagnitude);
            MaxIter = rootElement.ReadInt(_MaxIterId, MaxIter);
            Epsilon = rootElement.ReadSingle(_EpsilonId, Epsilon);
            Tolerance = rootElement.ReadSingle(_ToleranceId, Tolerance);
            CostFunction = rootElement.ReadEnum(_CostFunctionId, CostFunction);
            TrainingSampleCount = rootElement.ReadInt(_LearningSampleCountId, TrainingSampleCount);
            LearningRate = rootElement.ReadSingle(_LearningRateId, LearningRate);
            Lambda = rootElement.ReadSingle(_LambdaId, Lambda);
            TestSampleCount = rootElement.ReadInt(_VerificationSampleCountId, TestSampleCount);
        }

        public void SaveToSettings(XmlElement rootElement)
        {
            rootElement.WriteSingle(_BiasMagnitudeId, BiasMagnitude);
            rootElement.WriteSingle(_WeightMagnitudeId, WeightMagnitude);
            rootElement.WriteInt(_MaxIterId, MaxIter);
            rootElement.WriteSingle(_EpsilonId, Epsilon);
            rootElement.WriteSingle(_ToleranceId, Tolerance);
            rootElement.WriteEnum(_CostFunctionId, CostFunction);
            rootElement.WriteInt(_LearningSampleCountId, TrainingSampleCount);
            rootElement.WriteSingle(_LearningRateId, LearningRate);
            rootElement.WriteSingle(_LambdaId, Lambda);
            rootElement.WriteInt(_VerificationSampleCountId, TestSampleCount);
        }

        #endregion
    }
}
