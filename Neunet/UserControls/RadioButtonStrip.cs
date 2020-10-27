using System;
using System.Drawing;
using System.Windows.Forms;
using Neulib.Exceptions;
using Neunet.Forms;

namespace Neunet.UserControls
{

    public partial class RadioButtonStrip : UserControl
    {
        // ----------------------------------------------------------------------------------------
        #region Events

        public delegate void CheckedEventHandler(object sender, ButtonArgs e);

        public event CheckedEventHandler CheckedChanged = null;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Properties

        private int _buttonCount;
        public int ButtonCount
        {
            get { return _buttonCount; }
            set
            {
                _buttonCount = value;
                UpdateButtons();
            }
        }

        private int _checkedIndex;
        public int CheckedIndex
        {
            get { return _checkedIndex; }
            set
            {
                _checkedIndex = value;
                foreach (ToolStripButton Button in RadioToolStrip.Items)
                {
                    if (Button != null)
                        Button.Checked = value == (int)Button.Tag;
                }

            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public RadioButtonStrip()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region This

        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is ToolStripButton Button)
                {
                    int Index = (int)Button.Tag;
                    CheckedIndex = Index;
                    CheckedChanged?.Invoke(this, new ButtonArgs(Index));
                }
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is ToolStripButton Button)
                {
                    if (Button.Checked)
                        Button.Font = new Font(Button.Font, FontStyle.Bold);
                    else
                        Button.Font = new Font(Button.Font, FontStyle.Regular);
                }
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }

        }
        private void RemoveButton()
        {
            RadioToolStrip.Items.RemoveAt(RadioToolStrip.Items.Count - 1);
        }

        private void AddButton()
        {
            ToolStripButton NewButton = new ToolStripButton
            {
                CheckOnClick = false,
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Size = new Size(23, 22)
            };
            int Index = RadioToolStrip.Items.Count;
            NewButton.Tag = Index;
            NewButton.Text = $"{Index + 1}";
            NewButton.Click += new EventHandler(Button_Click);
            NewButton.CheckedChanged += new EventHandler(Button_CheckedChanged);
            RadioToolStrip.Items.Add(NewButton);
        }

        private void UpdateButtons()
        {
            RadioToolStrip.SuspendLayout();
            SuspendLayout();
            while (RadioToolStrip.Items.Count < ButtonCount) AddButton();
            while (RadioToolStrip.Items.Count > ButtonCount) RemoveButton();
            RadioToolStrip.ResumeLayout(false);
            RadioToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }

    public class ButtonArgs : EventArgs
    {
        public int Index { get; private set; }

        public ButtonArgs(int Index)
        {
            this.Index = Index;
        }
    }

}
