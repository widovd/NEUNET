using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neulib.Exceptions;

namespace Neunet.Forms
{
    public partial class NotificationForm : Form
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public string Notification
        {
            get { return notificationLabel.Text; }
            set { notificationLabel.Text = value; }
        }

        public int Duration { get; set; } = 1000;


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public NotificationForm()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NotificationForm

        private async void NotificationForm_Load(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.None;
                Task task = Task.Run(() => Thread.Sleep(Duration));
                await task;
                DialogResult = DialogResult.OK;
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
            finally
            {
                //Close();
            }
        }

        public static void ShowNotification(string notification, int duration)
        {
            using (NotificationForm form = new NotificationForm()
            {
                Notification = notification,
                Duration = duration,
            })
            {
                form.ShowDialog();
            }
        }
        #endregion

    }
}
