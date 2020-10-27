using System;
using System.Drawing;
using System.Windows.Forms;
using Neulib.Exceptions;

namespace Neunet.Forms
{

    public partial class ResizeForm : Form
    {

        public Size ControlSize
        {
            get
            {
                int.TryParse(WidthTextBox.Text, out int w);
                int.TryParse(HeightTextBox.Text, out int h);
                return new Size(w, h);
            }
            set
            {
                WidthTextBox.Text = value.Width.ToString();
                HeightTextBox.Text = value.Height.ToString();
            }
        }

        public ResizeForm()
        {
            InitializeComponent();
        }

        private void OKButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        public static bool ResizeDialog(ref Size ControlSize)
        {
            bool Result = false;
            using (ResizeForm Form = new ResizeForm())
            {
                Form.ControlSize = ControlSize;
                Result = (Form.ShowDialog() == DialogResult.OK);
                if (Result)
                {
                    ControlSize = Form.ControlSize;
                    return true;
                }
            }
            return Result;
        }

    }
}
