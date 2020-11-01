using System;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using Neulib.Exceptions;
using Neulib.Extensions;
using Neulib.Serializers;

namespace Neunet.Forms
{
    /// <summary>
    /// Abstract base class for (almost) all forms used in Fresh 3.
    /// </summary>
    public partial class BaseForm : Form
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private string _text;
        public override sealed string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                string title = $"{Program.Name.ToUpper()}";
                string s = string.IsNullOrEmpty(value) ? title : string.Join(" - ", title, value);
                if (s == null) s = string.Empty;
                //base.Text = s;
                try
                {
                    base.Text = s;
                }
                catch
                {
                    base.Text = string.Empty;
                }
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public BaseForm()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IApplicationIdle

        public virtual void Idle()
        {
        }

        private bool disableIdle = false;

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (disableIdle) return;
                Idle();
            }
            catch (Exception ex)
            {
                disableIdle = true;
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IXmlSettings

        private const string _locationXId = "LocationX";
        private const string _locationYId = "LocationY";
        private const string _sizeWidthId = "SizeWidth";
        private const string _sizeHeightId = "SizeHeight";

        public virtual void LoadFromSettings(XmlElement rootElement)
        {
            int x = rootElement.ReadInt(_locationXId, Location.X);
            int y = rootElement.ReadInt(_locationYId, Location.Y);
            int width = rootElement.ReadInt(_sizeWidthId, Size.Width);
            int height = rootElement.ReadInt(_sizeHeightId, Size.Height);
            SetWindowPosition(new Size(width, height), new Point(x, y));
        }

        public virtual void SaveToSettings(XmlElement rootElement)
        {
            rootElement.WriteInt(_locationXId, Location.X);
            rootElement.WriteInt(_locationYId, Location.Y);
            rootElement.WriteInt(_sizeWidthId, Size.Width);
            rootElement.WriteInt(_sizeHeightId, Size.Height);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        /// <summary>
        /// The window location and size is loaded from XmlSettings
        /// </summary>
        //[DebuggingCheck(Action = CheckAction.Exit)]
        //[TamperCheck(Action = CheckAction.Exit)]
        private void BaseForm_Load(object sender, EventArgs e)
        {
            //if (CustomTampering.HasBeenTampered)
            //{
            //    NotificationForm.ShowNotification("BaseForm_Load: Tamper detected", 500);
            //    //Close();
            //    //return;
            //}
            //if (CustomTampering.IsBeingDebugged)
            //{
            //    NotificationForm.ShowNotification("BaseForm_Load: Debugger detected", 500);
            //    //Close();
            //    //return;
            //}
            try
            {
                if (!DesignMode) LoadSettingsFromDocument();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        /// <summary>
        /// The window location and size is saved to XmlSettings
        /// </summary>
        private void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!DesignMode) SaveSettingsToDocument();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region FreshDocument

        private const int _minWidth = 100;
        private const int _minHeight = 100;

        private void SetWindowPosition(Size size, Point location)
        {
            int width = size.Width.Clip(_minWidth, Screen.PrimaryScreen.WorkingArea.Width);
            int height = size.Height.Clip(_minHeight, Screen.PrimaryScreen.WorkingArea.Height);
            int x = location.X.Clip(0, Screen.PrimaryScreen.WorkingArea.Width - width);
            int y = location.Y.Clip(0, Screen.PrimaryScreen.WorkingArea.Height - height);
            if (StartPosition == FormStartPosition.Manual) Location = new Point(x, y);
            if (FormBorderStyle == FormBorderStyle.Sizable) Size = new Size(width, height);
        }

        private void LoadSettingsFromDocument()
        {
            XmlElement formElement = Program.FormsElement;
            if (formElement == null) return;
            XmlElement element = formElement.GetOrCreateElement(Name);
            LoadFromSettings(element);
        }

        private void SaveSettingsToDocument()
        {
            XmlElement formElement = Program.FormsElement;
            if (formElement == null) return;
            XmlElement element = formElement.GetOrCreateElement(Name);
            SaveToSettings(element);
        }


        #endregion

    }
}
