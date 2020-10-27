using System;

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

    public class IterationReportData : ReportData
    {
        public double Merit { get; private set; }

        public IterationReportData(float merit)
        {
            Merit = merit;
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
