using System;
using System.ComponentModel;
using Neulib.Neurons;
using Neulib.Numerics;
using static System.Convert;

namespace Neulib
{

    public class ProgressReporter
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private ISynchronizeInvoke Control { get; set; }

        Action<ReportData> Action { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ProgressReporter(ISynchronizeInvoke control, Action<ReportData> action)
        {
            Control = control;
            Action = action;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region this

        public void Report(ReportData data)
        // usage example: settings.Reporter.ReportProgress(new ProgressBarReportData(i, n));

        {
            if (Control.InvokeRequired) // check Control.IsDisposed ?
            {
                CalculationCallbackDelegate d = new CalculationCallbackDelegate(Report);
                Control.Invoke(d, new object[] { data });
            }
            else
            {
                Action(data);
            }
        }

        public void ReportSamples(SampleList samples)
        {
            Report(new SamplesReportData(samples));
        }

        public void ReportCoefficients(Single1D coefficients)
        {
            Report(new CoefficientsReportData(coefficients));
        }


        public void ReportCostAndDerivatives(float cost, Single1D derivatives, MeasurementList measurements)
        {
            Report(new CostAndDerivativesReportData(cost, derivatives, measurements));
        }

        public void ReportNetwork()
        {
            Report(new NetworkReportData());
        }

        public void ReportProgress(int i, int n)
        {
            Report(new ProgressBarReportData(n > 1 ? ToDouble(i) / ToDouble(n - 1) : 0d));
        }

        public void ReportProgress(long i, long n)
        {
            Report(new ProgressBarReportData(n > 1 ? ToDouble(i) / ToDouble(n - 1) : 0d));
        }


        public void WriteStart(string message)
        {
            Report(new MessageReportData(message, MessageIndentEnum.Start));
        }

        public void Write(string message, MessageTypeEnum messageType = MessageTypeEnum.InfoText)
        {
            Report(new MessageReportData(message, MessageIndentEnum.None, messageType));
        }

        public void WriteEnd(string message)
        {
            Report(new MessageReportData(message, MessageIndentEnum.End));
        }

        public delegate void CalculationCallbackDelegate(ReportData data);

        #endregion

    }

}
