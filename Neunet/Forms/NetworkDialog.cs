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
    public partial class NetworkDialog : BaseDialog
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private Network _network = new Network();
        public Network Network
        {
            get { return _network; }
            set { SetNetwork(value); }
        }

        private void SetNetwork(Network value)
        {
            _network = value;
            int count = Network.Layers.Count;
            layersListBox.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                Layer layer = Network.Layers[i];
                layersListBox.Items.Add(layer);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public NetworkDialog()
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

        #endregion
    }
}
