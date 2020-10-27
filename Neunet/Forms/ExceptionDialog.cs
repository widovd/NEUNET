using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Neulib.Extensions;
using Neulib.Exceptions;
using Neulib.Serializers;
using Neunet.Extensions;

namespace Neunet.Forms
{
    public partial class ExceptionDialog : BaseForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Exception Exception { get; set; }

        public string CallerName { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ExceptionDialog()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        private const string _SplitterDistanceId = "SplitterDistance";

        public override void LoadFromSettings(XmlElement rootElement)
        {
            base.LoadFromSettings(rootElement);
            splitContainer.SetSplitterDistance(rootElement, _SplitterDistanceId);
        }

        public override void SaveToSettings(XmlElement rootElement)
        {
            base.SaveToSettings(rootElement);
            rootElement.WriteInt(_SplitterDistanceId, splitContainer.SplitterDistance);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ExceptionDialog

        private void ExceptionDialog_Load(object sender, EventArgs e)
        {
            bool callerValid = !string.IsNullOrEmpty(CallerName);
            errorLabel.Visible = callerValid;
            errorLabel.Text = callerValid ? $"Method '{CallerName}' caught exception:" : string.Empty;
            Exception exception = Exception;
            int index = 1;
            while (exception != null)
            {
                ListViewItem item = exceptionListView.Items.Add(index++.ToString());
                item.Tag = exception;
                item.SubItems.Add(exception.GetType().ToString().Last('.'));
                int errorCode = exception is BaseException baseException ? baseException.ErrorCode : 0;
                item.SubItems.Add(errorCode.ToString());
                item.SubItems.Add(exception.Message);
                exception = exception.InnerException;
            }
            if (exceptionListView.Items.Count > 0)
            {
                ListViewItem item = exceptionListView.Items[exceptionListView.Items.Count - 1];
                item.Focused = true;
                item.Selected = true;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(CallerName);
            try
            {
                int index = 1;
                foreach (ListViewItem item in exceptionListView.Items)
                {
                    if (builder.Length > 0) builder.AppendLine();
                    Exception exception = (Exception)item.Tag;
                    builder.AppendLine($"[{index++}] {exception}");
                    string stackTrace = exception.StackTrace;
                    if (!string.IsNullOrEmpty(stackTrace))
                    {
                        builder.AppendLine("StackTrace:");
                        builder.AppendLine($"{stackTrace}");
                    }
                }
                Clipboard.SetText(builder.ToString());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void ExceptionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = exceptionListView.SelectedItems;
            if (selectedItems == null) return;
            if (selectedItems.Count == 0) return;
            Exception exception = (Exception)selectedItems[0].Tag;
            if (exception == null) return;
            string stackTrace = exception.StackTrace;
            stackTraceTextBox.Text = !string.IsNullOrEmpty(stackTrace) ? exception.StackTrace : "StackTrace is null";
        }

        public static void Show(Exception exception, [CallerMemberName] string callerName = null)
        {
            if (string.IsNullOrEmpty(callerName))
            {
                StackTrace stackTrace = new StackTrace();
                MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                callerName = methodBase.Name;
            }
            bool debugging = Debugger.IsAttached;
            if (debugging)
            {
                using (ExceptionDialog exceptionDialog = new ExceptionDialog
                {
                    Exception = exception,
                    CallerName = callerName,
                })
                {
                    exceptionDialog.ShowDialog();
                }

            }
            else
            {
                while (exception.InnerException != null) exception = exception.InnerException;
                string caption = exception is BaseException baseException ? $"Error {baseException.ErrorCode}" : "Error";
                string message = exception.Message;
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
