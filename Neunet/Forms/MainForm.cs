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

namespace Neunet.Forms
{
    public partial class MainForm : BaseForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region MainForm

        private void DigitsButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (DigitsForm form = new DigitsForm())
                {
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void BugsButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (BugForm form = new BugForm())
                {
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion

    }
}
