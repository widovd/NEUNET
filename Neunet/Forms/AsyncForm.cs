using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib;
using System.Runtime.Remoting.Messaging;

namespace Neunet.Forms
{
    public partial class AsyncForm : BaseForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Settings Settings { get; set; }

        public Action<Settings, ProgressReporter, CancellationTokenSource> Action { get; set; }

        protected CancellationTokenSource _tokenSource = null;

        public string Message
        {
            get => messageLabel.Text;
            set => messageLabel.Text = value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public AsyncForm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) { components.Dispose(); components = null; }
                if (_tokenSource != null) { _tokenSource.Dispose(); _tokenSource = null; }

            }
            base.Dispose(disposing);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region PreviewForm

        private void SetProgress(double value)
        {
            int a = progressBar.Minimum + (int)(value * (progressBar.Maximum - progressBar.Minimum));
            if (a < progressBar.Minimum) a = progressBar.Minimum;
            if (a > progressBar.Maximum) a = progressBar.Maximum;
            progressBar.Value = a;
        }

        private void SetStatusText(string value)
        {
            if (statusLabel.Text != value) statusLabel.Text = value;
        }

        private void ReportProgress(ReportData reportData)
        {
            if (reportData is ProgressBarReportData progressData)
                SetProgress(progressData.X);
            else if (reportData is MessageReportData debugData)
            {
                if (debugData.MessageIndent == MessageIndentEnum.Start) SetStatusText(debugData.Message);
            }
        }

        private void HandleException(Exception ex)
        {
            Exception ex2 = ex;
            while (ex2.InnerException != null) ex2 = ex2.InnerException;
            if (ex2 is OperationCanceledException)
                MessageBox.Show("The operation is cancelled.", "Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                ExceptionDialog.Show(ex);
        }

        private async void AsyncForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            ProgressReporter reporter = new ProgressReporter(this, (reportData) => { ReportProgress(reportData); });
            try
            {
                DialogResult = DialogResult.None;
                _tokenSource = new CancellationTokenSource();
                Exception ex = await Task.Run<Exception>(() =>
                {
                    try
                    {
                        Action(Settings, reporter, _tokenSource);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        return ex;
                    }
                });
                if (ex == null)
                    DialogResult = DialogResult.OK;
                else
                    HandleException(ex);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
            finally
            {
                if (_tokenSource != null) { _tokenSource.Dispose(); _tokenSource = null; }
                Close();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_tokenSource != null && !_tokenSource.IsCancellationRequested)
                    _tokenSource.Cancel();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Static

        public static bool RunAsync(Action<Settings, ProgressReporter, CancellationTokenSource> action, IWin32Window owner)
        {
            using (AsyncForm form = new AsyncForm { Action = action })
            {
                return form.ShowDialog(owner) == DialogResult.OK;
            }
        }


        #endregion
    }
}
