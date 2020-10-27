using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace Neulib
{
    // example use:
    // ProgressInterval progressInterval = new ProgressInterval(1d, 100);
    // (x) => { progressInterval.DoIt(x, (z) => arguments.reporter?.ReportProgress(new ProgressBarReportData(z)));

    public class ProgressInterval
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public double Threshold { get; set; }

        public double Delta { get; set; }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ProgressInterval(double delta)
        {
            Threshold = 0d;
            Delta = delta;
        }

        public ProgressInterval(double endValue, int number)
        {
            Threshold = 0d;
            Delta = endValue / number;
        }

        public ProgressInterval(double startValue, double delta)
        {
            Threshold = startValue;
            Delta = delta;
        }

        public ProgressInterval(double startValue, double endValue, int number)
        {
            Threshold = startValue;
            Delta = endValue / number;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ProgressInterval

        public void DoIt(double x, Action<double> action)
        {
            const double tiny = 1e-10;
            if (x >= Threshold - tiny)
            {
                while (x >= Threshold) Threshold += Delta;
                action(x);
            }
        }

        public void DoIt(int i, int n, Action<double> action)
        {
            double x = ToDouble(i) / ToDouble(n - 1);
            DoIt(x, action);
        }

        public void DoIt(int i, int j, int n, int m, Action<double> action)
        {
            double x = (ToDouble(i) * ToDouble(j)) / (ToDouble(n - 1) * ToDouble(m - 1));
            DoIt(x, action);
        }


        #endregion
    }
}
