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
    public partial class LayerDialog : BaseDialog
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private Layer _layer = new Layer();
        public Layer Layer
        {
            get { return _layer; }
            set { SetLayer(value); }
        }

        private void SetLayer(Layer value)
        {
            _layer = value;
            neuronsTextBox.Value = value.Count;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public LayerDialog()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseDialog

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NetworkDialog

        private void NeuronsTextBox_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int n = neuronsTextBox.Value;
                Layer.Clear();
                for (int i = 0; i < n; i++)
                {
                    Layer.Add(new Sigmoid());
                }
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
    }
}
