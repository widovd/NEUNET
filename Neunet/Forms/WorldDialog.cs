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
using Neulib.Visuals;
using Neulib.MultiArrays;
using Neulib.Serializers;
using Neunet.Extensions;
using Neunet.Images.Charts2D;
using Neunet.Serializers;

namespace Neunet.Forms
{
    public partial class WorldDialog : BaseDialog
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private VisualWorld _world;
        public VisualWorld World
        {
            get { return _world; }
            set { SetWorld(value); }
        }

        private void SetWorld(VisualWorld value)
        {
            _world = value;
            UpdateItems();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public WorldDialog()
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

        private void UpdateItems()
        {
            int index = layersListBox.SelectedIndex;
            layersListBox.Items.Clear();
            //World.ForEach(layer => layersListBox.Items.Add(layer));
            if (index >= layersListBox.Items.Count) index = layersListBox.Items.Count - 1;
            if (index >= 0) layersListBox.SelectedIndex = index;
        }

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
            if (index < 0 || index >= layersListBox.Items.Count) return;
            //TemplateLayer = World.Remove(index);
            UpdateItems();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
               Delete(layersListBox.SelectedIndex);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Edit(int index)
        {
            //Layer layer = World[index];
            //using (LayerDialog dialog = new LayerDialog() { Layer = layer.Clone() as Layer })
            //{
            //    if (dialog.ShowDialog(this) == DialogResult.OK)
            //    {
            //        World[index] = dialog.Layer;
            //        UpdateItems();
            //    }
            //}
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                Edit(layersListBox.SelectedIndex);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Insert(int index)
        {
            //using (LayerDialog dialog = new LayerDialog() { Layer = (Layer)TemplateLayer.Clone() })
            //{
            //    if (dialog.ShowDialog(this) == DialogResult.OK)
            //    {
            //        World.Insert(index, dialog.Layer);
            //        UpdateItems();
            //    }
            //}
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                Insert(layersListBox.SelectedIndex);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Add()
        {
            //using (LayerDialog dialog = new LayerDialog() { Layer = (Layer)TemplateLayer.Clone() })
            //{
            //    if (dialog.ShowDialog(this) == DialogResult.OK)
            //    {
            //        World.Add(dialog.Layer);
            //        UpdateItems();
            //    }
            //}
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
            //World.Clear();
            UpdateItems();
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
