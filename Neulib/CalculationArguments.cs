using System;
using System.Threading;

namespace Neulib
{
    public struct CalculationArguments
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public CalculationSettings settings;
        public ProgressReporter reporter;
        public CancellationTokenSource tokenSource;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public CalculationArguments(CalculationSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            this.settings = settings;
            this.reporter = reporter;
            this.tokenSource = tokenSource;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region CalculationArguments

        public void ThrowIfCancellationRequested()
        {
            tokenSource?.Token.ThrowIfCancellationRequested();
        }

        #endregion
    }
}
