using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using Neulib.Exceptions;
using Neunet.Forms;
using Neunet.Utils;
using Neunet.Extensions;
using Neunet.Serializers;

namespace Neunet.UserControls
{
    public partial class RunToolStrip : UserControl
    {
        // ----------------------------------------------------------------------------------------
        #region Reset

        public event EventHandler ResetClick;

        private void ResetButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetClick?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        public bool ResetButtonEnabled
        {
            get { return resetButton.Enabled; }
            set { resetButton.Enabled = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Delete

        public event EventHandler DeleteClick;

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteClick?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        public bool DeleteButtonEnabled
        {
            get { return deleteButton.Enabled; }
            set { deleteButton.Enabled = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Step

        public event EventHandler StepClick;

        private void StepButton_Click(object sender, EventArgs e)
        {
            try
            {
                StepClick?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        public bool StepButtonEnabled
        {
            get { return stepButton.Enabled; }
            set { stepButton.Enabled = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Run

        public event EventHandler RunClick;

        private void RunButton_Click(object sender, EventArgs e)
        {
            try
            {
                RunClick?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        public bool RunButtonEnabled
        {
            get { return runButton.Enabled; }
            set { runButton.Enabled = value; }
        }

        public string RunButtonText
        {
            get { return runButton.Text; }
            set { runButton.Text = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Stop

        public event EventHandler StopClick;

        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                StopClick?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        public bool StopButtonEnabled
        {
            get { return stopButton.Enabled; }
            set { stopButton.Enabled = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Settings

        public event EventHandler SettingsClick;

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsClick?.Invoke(this, new EventArgs());
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        public bool SettingsButtonEnabled
        {
            get { return settingsButton.Enabled; }
            set { settingsButton.Enabled = value; }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public RunToolStrip()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) { components.Dispose(); components = null; }
            }
            base.Dispose(disposing);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IXmlSettings

        public void LoadFromSettings(XmlElement element)
        {
        }

        public void SaveToSettings(XmlElement element)
        {
        }

        #endregion
    }
}
