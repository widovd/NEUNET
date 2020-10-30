using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Neulib;
using Neulib.Numerics;
using Neulib.Exceptions;
using Neulib.Neurons;
using Neulib.MultiArrays;
using Neulib.Serializers;
using Neunet.Extensions;
using Neunet.Images.Charts2D;
using Neunet.Serializers;

namespace Neunet.Forms
{
    public partial class SettingsDialog : Neunet.Forms.BaseDialog
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public CalculationSettings Settings
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
                Settings = new CalculationSettings();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
    }
}
