using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Neunet.Forms;
using Neulib.Exceptions;

namespace Neunet.Images
{
    /// <summary>
    /// Base class derived from UserControl.
    /// It contains a PictureBox to display a buffered image or chart.
    /// </summary>
    public partial class BaseImage : UserControl, IMessageFilter
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private Color _backgroundColor = Color.White;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color BackgroundColor // do not use in the ctor!
        {
            get { return _backgroundColor; }
            set
            {
                if (value == BackgroundColor) return;
                _backgroundColor = value;
                RefreshImage(); // virtual!
            }
        }

        private Bitmap _bitmap = null;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public BaseImage()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            MouseWheel += new MouseEventHandler(PictureBox_MouseWheel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) { components.Dispose(); components = null; }
                if (_bitmap != null) { _bitmap.Dispose(); _bitmap = null; }
            }
            base.Dispose(disposing);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IApplicationIdle

        public virtual void Idle()
        {
            bool b1 = _bitmap != null;
            copyToolStripMenuItem.Enabled = b1;
            saveAsToolStripMenuItem.Enabled = b1;
            resizeToolStripMenuItem.Enabled = b1;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        /// <summary>
        /// The current number of display suspend levels.
        /// </summary>
        private int _suspendLevel;

        /// <summary>
        /// Disposes the old Bitmap and initializes a new Bitmap with the PictureBox size.
        /// Redraws on the Bitmap buffer and invalidates the PictureBox
        /// </summary>
        public void RefreshImage()
        {
            if (_suspendLevel != 0) return;
            if (_bitmap != null) { _bitmap.Dispose(); _bitmap = null; }
            int w = pictureBox.Width, h = pictureBox.Height;
            if (w > 0 && h > 0)
            {
                _bitmap = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                DrawImage(_bitmap);
            }
            pictureBox.Refresh();
        }

        /// <summary>
        /// Increases the suspend level by one.
        /// </summary>
        public void SuspendImage()
        {
            if (_suspendLevel >= 0) _suspendLevel += 1;
        }

        /// <summary>
        /// Decreases the suspend level by one. If suspendlevel is 0 then UpdateImage is called.
        /// </summary>
        public void ResumeImage()
        {
            if (_suspendLevel > 0) _suspendLevel -= 1;
            if (_suspendLevel == 0) RefreshImage();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        public virtual void DrawImage(Bitmap bufferBitmap)
        {
            if (BackgroundColor == Color.Transparent) return;
            using (var graphics = Graphics.FromImage(bufferBitmap))
            {
                graphics.Clear(BackgroundColor);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        protected virtual void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                if (_bitmap != null) graphics.DrawImage(_bitmap, 0, 0);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        protected virtual void ImageSizeChanged()
        {
            RefreshImage();
        }

        private void PictureBox_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                ImageSizeChanged();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ImageContextMenuStrip

        private const string _fileFilter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg|PNG|*.png|TIFF|*.tiff|All Files(*.*)|*.*";

        public virtual void SaveImage()
        {
            if (_bitmap == null) return;
            //_bitmap.Save(filePath);
        }

        public virtual void CopyImage()
        {
            if (_bitmap == null) return;
            Clipboard.SetImage(_bitmap);
        }

        public virtual void ResizeImage()
        {
            using (ResizeForm form = new ResizeForm())
            {
                form.ControlSize = pictureBox.Size;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Size S2 = form.ControlSize;
                    Form parentForm = FindForm();
                    if (parentForm == null) return;
                    try
                    {
                        SuspendImage();
                        parentForm.Visible = false;
                        bool ok = false;
                        for (int i = 0; i < 20; i++)
                        {
                            Size t1 = parentForm.Size;
                            Size s1 = pictureBox.Size;
                            Size dd = S2 - s1;
                            if ((dd.Height == 0) && (dd.Width == 0))
                            {
                                ok = true;
                                break;
                            }
                            Size t2 = t1 + S2 - s1;
                            parentForm.Size = t2;
                        }
                        if (!ok)
                        {
                            Size s1 = pictureBox.Size;
                            MessageBox.Show(
                                $"Unable to resize the image.\nSpecified size: ({S2.Width}, {S2.Height})\nCurrent size: ({s1.Width}, {s1.Height})",
                                "Resize image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    finally
                    {
                        ResumeImage();
                        parentForm.Visible = true;
                    }
                }
            }
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                SaveImage();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            try
            {
                CopyImage();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Resize_Click(object sender, EventArgs e)
        {
            try
            {
                ResizeImage();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Mouse methods

        private bool _mouseIsDown = false;
        private int _x0;
        private int _y0;

        /// <summary>
        /// The mouse wheel is moved.
        /// </summary>
        /// <param name="delta">The amount the mouse wheel has been moved.</param>
        protected virtual void ImageMouseWheel(int delta)
        {
        }

        /// <summary>
        /// The mouse button is moved down.
        /// </summary>
        /// <param name="x0">The x-coordinate of the mouse.</param>
        /// <param name="y0">The y-coordinate of the mouse.</param>
        /// <param name="buttons">The mouse button which was pressed.</param>
        protected virtual void ImageMouseDown(int x0, int y0, MouseButtons buttons)
        {
        }

        /// <summary>
        /// While the mouse button is moved down, the mouse is moved.
        /// </summary>
        /// <param name="x0">The x-coordinate of the mouse when it was moved down.</param>
        /// <param name="y0">The y-coordinate of the mouse when it was moved down.</param>
        /// <param name="dx">The delta x-coordinate of the mouse.</param>
        /// <param name="dy">The delta y-coordinate of the mouse.</param>
        /// <param name="buttons">The mouse button which was pressed.</param>
        protected virtual void ImageMouseMove(int x0, int y0, int dx, int dy, MouseButtons buttons)
        {
        }

        /// <summary>
        /// The mouse button is moved up.
        /// </summary>
        /// <param name="x0">The x-coordinate of the mouse when it was moved down.</param>
        /// <param name="y0">The y-coordinate of the mouse when it was moved down.</param>
        /// <param name="dx">The delta x-coordinate of the mouse.</param>
        /// <param name="dy">The delta y-coordinate of the mouse.</param>
        /// <param name="buttons">The mouse button which was pressed.</param>
        protected virtual void ImageMouseUp(int x0, int y0, int dx, int dy, MouseButtons buttons)
        {
        }

        /// <summary>
        /// The mouse button is moved up, but the mouse was not moved.
        /// </summary>
        /// <param name="x0">The x-coordinate of the mouse when it was moved down.</param>
        /// <param name="y0">The y-coordinate of the mouse when it was moved down.</param>
        /// <param name="buttons">The mouse button which was pressed.</param>
        protected virtual void ImageMouseUpNoMove(int x0, int y0, MouseButtons buttons)
        {
            if (buttons == MouseButtons.Right)
                imageContextMenuStrip.Show(this, x0, y0);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Mouse events

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                int delta = e.Delta / 120;
                ImageMouseWheel(delta);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                _mouseIsDown = true;
                _x0 = e.X;
                _y0 = e.Y;
                ImageMouseDown(_x0, _y0, e.Button);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseIsDown) return;
            try
            {
                int dx = e.X - _x0;
                int dy = e.Y - _y0;
                ImageMouseMove(_x0, _y0, dx, dy, e.Button);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_mouseIsDown) return;
            try
            {
                _mouseIsDown = false;
                int dx = e.X - _x0;
                int dy = e.Y - _y0;
                if (dx == 0 && dy == 0)
                    ImageMouseUpNoMove(_x0, _y0, e.Button);
                else
                    ImageMouseUp(_x0, _y0, dx, dy, e.Button);
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IMessageFilter for the mouse wheel event

        // Send mouse wheel event to the control the mouse is hovering above, not to the control who has focus
        // https://social.msdn.microsoft.com/Forums/windows/en-US/eb922ed2-1036-41ca-bd15-49daed7b637c/outlookstyle-wheel-mouse-behavior?forum=winforms

        public bool PreFilterMessage(ref Message m)
        {
            int MOUSEWHEEL = 0x20a;
            if (m.Msg == MOUSEWHEEL)
            {
                //tagPOINT POINT = new tagPOINT(System.Windows.Forms.Cursor.Position);
                var hWnd = NativeMethods.WindowFromPoint(System.Windows.Forms.Cursor.Position);
                if ((hWnd != IntPtr.Zero) && (hWnd != m.HWnd) && (Control.FromHandle(hWnd) != null))
                {
                    NativeMethods.SendMessage(hWnd, MOUSEWHEEL, m.WParam, m.LParam);
                    return true;
                }
            }
            return false;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NativeMethods

        private static class NativeMethods
        {
            //https://msdn.microsoft.com/en-us/library/windows/desktop/ms633558(v=vs.85).aspx
            // Any other type than Point (Int32) will cause a stack inbalance. Code Analyses is Wrong about this!
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "0")]
            [DllImport("user32.dll")]
            public static extern IntPtr WindowFromPoint(Point pt);

            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        }


        #endregion
    }
}
