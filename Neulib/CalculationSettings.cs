using System;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;
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
        #region CalculationSettings

        private const string _MaxIterId = "MaxIter";

        public void LoadFromSettings(XmlElement rootElement)
        {
            MaxIter = rootElement.ReadInt(_MaxIterId, MaxIter);
        }

        public void SaveToSettings(XmlElement rootElement)
        {
            rootElement.WriteInt(_MaxIterId, MaxIter);
        }

        public void Default()
        {

        }

        #endregion
    }
}
