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
            layersListBox.Items.Clear();
            Network.ForEach(layer => layersListBox.Items.Add(layer));
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

        private void Delete(Layer layer)
        {
            layer.Remove();
            layersListBox.Items.Remove(layer);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Layer layer = (Layer)layersListBox.SelectedItem;
                if (layer == null) return;
                Delete(layer);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Edit(Layer layer)
        {
            using (LayerDialog dialog = new LayerDialog() { Layer = layer.Clone() as SingleLayer })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    layer.Replace(dialog.Layer);
                    layersListBox.Items[layersListBox.Items.IndexOf(layer)] = dialog.Layer;
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                Layer layer = (Layer)layersListBox.SelectedItem;
                if (layer == null) return;
                Edit(layer);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Insert(Layer layer)
        {
            using (LayerDialog dialog = new LayerDialog() { Layer = Layer })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    layer.Insert(dialog.Layer);
                    layersListBox.Items.Insert(layersListBox.Items.IndexOf(layer), dialog.Layer);
                }
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                Layer layer = (Layer)layersListBox.SelectedItem;
                if (layer == null) return;
                Insert(layer);
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
