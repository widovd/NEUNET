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
            int count = Network.Count;
            layersListBox.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                SingleLayer layer = Network[i];
                layersListBox.Items.Add(layer);
            }
        }

        private SingleLayer Layer { get; set; } = new SingleLayer();

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

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            bool isSelected = layersListBox.SelectedIndex >= 0;
            deleteButton.Enabled = isSelected;
            editButton.Enabled = isSelected;
            insertButton.Enabled = isSelected;
            addButton.Enabled = true;
            clearButton.Enabled = layersListBox.Items.Count > 0;
        }

        private void Delete(int index)
        {
            Layer = (SingleLayer)layersListBox.Items[index];
            Network.Remove(Layer);
            layersListBox.Items.Remove(Layer);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = layersListBox.SelectedIndex;
                if (index < 0) return;
                Delete(index);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Edit(int index)
        {
            SingleLayer layer = (SingleLayer)layersListBox.Items[index];
            using (LayerDialog dialog = new LayerDialog() { Layer = (SingleLayer)layer.Clone() })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Network[index] = dialog.Layer;
                    layersListBox.Items[index] = dialog.Layer;
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = layersListBox.SelectedIndex;
                if (index < 0) return;
                Edit(index);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Insert(int index)
        {
            using (LayerDialog dialog = new LayerDialog() { Layer = Layer })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Network.Insert(index, dialog.Layer);
                    layersListBox.Items.Insert(index, dialog.Layer);
                }
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = layersListBox.SelectedIndex;
                if (index < 0) return;
                Insert(index);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Add()
        {
            using (LayerDialog dialog = new LayerDialog() { Layer = Layer })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Network.Add(dialog.Layer);
                    layersListBox.Items.Add(dialog.Layer);
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                Add();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Clear()
        {
            Network.Clear();
            layersListBox.Items.Clear();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
    }
}
