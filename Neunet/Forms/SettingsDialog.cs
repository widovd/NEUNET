using System;
using System.Windows.Forms;
using Neulib;
using Neulib.Neurons;

namespace Neunet.Forms
{
    public partial class SettingsDialog : Neunet.Forms.BaseDialog
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Settings Settings
        {
            get { return (CalculationSettings)propertyGrid.SelectedObject; }
            set { propertyGrid.SelectedObject = value; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public SettingsDialog()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseDialog

        #endregion
        // ----------------------------------------------------------------------------------------
        #region CalculationSettingsForm

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            try
            {
                const string message = "The settings will be changed to default values. Continue?";
                const string caption = "Settings";
                const MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                if (MessageBox.Show(message, caption, buttons) == DialogResult.Cancel) return;
                Settings = Settings?.DefaultSettings();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
    }
}
