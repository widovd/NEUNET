using System;
using Neulib.Neurons;
using Neulib.Numerics;

namespace Neulib
{
    public enum MessageTypeEnum
    {
        InfoText,
        WarningText,
        ErrorText
    }

    public enum MessageIndentEnum
    {
        Start,
        None,
        End,
    }

    public abstract class ReportData
    {
    }

    public class ProgressBarReportData : ReportData
    {
        public double X { get; private set; }

        public ProgressBarReportData(double x)
        {
            X = x;
        }

    }

    public class CoefficientsReportData : ReportData
    {
        public Single1D Coefficients { get; private set; }

        public CoefficientsReportData(Single1D coefficients)
        {
            Coefficients = coefficients;
        }

    }

    public class CostAndDerivativesReportData : ReportData
    {
        public float Cost { get; private set; }

        public Single1D Derivatives { get; private set; }

        public MeasurementList Measurements { get; private set; }

        public CostAndDerivativesReportData(float cost, Single1D derivatives, MeasurementList measurements)
        {
            Cost = cost;
            Derivatives = derivatives;
            Measurements = measurements;
        }
    }

    public class SamplesReportData : ReportData
    {
        public SampleList Samples { get; private set; }

        public SamplesReportData(SampleList samples)
        {
            Samples = samples;
        }

    }

    public class NetworkReportData : ReportData
    {
        public NetworkReportData()
        {
        }

    }

    public class MessageReportData : ReportData
    {
        public string Message { get; set; }

        public MessageIndentEnum MessageIndent { get; set; } = MessageIndentEnum.None;

        public MessageTypeEnum MessageType { get; set; } = MessageTypeEnum.InfoText;

        public MessageReportData(string message)
        {
            Message = message;
        }

        public MessageReportData(string message, MessageIndentEnum messageIndent)
        {
            Message = message;
            MessageIndent = messageIndent;
            MessageType = MessageTypeEnum.InfoText;
        }

        public MessageReportData(string message, MessageIndentEnum messageIndent, MessageTypeEnum messageType)
        {
            Message = message;
            MessageIndent = messageIndent;
            MessageType = messageType;
        }
    }


}
