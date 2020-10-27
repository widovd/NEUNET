using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using Neulib.Serializers;
using Neulib;
using Neunet.Utils;

namespace Neunet.Forms
{
    public partial class LoadFileForm : FileForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Version FileVersion { get; private set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public LoadFileForm()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region FileForm

        protected override void SetFilePath(string value)
        {
            base.SetFilePath(value);
            Text = "Load solution";
            SetMessageText($"Loading ...\n{value}");
        }

        private void LoadGraph(Stream stream)
        {
            stream.Position = 0;
            Serializer serializer = GetSerializer();
            Graph = serializer.Deserialize(stream);
            FileVersion = serializer.Version;
        }

        protected override void FileAction(CalculationArguments arguments)
        {
            using MemoryStream memoryStream = new MemoryStream();
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                if (Zip)
                {
                    using GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Decompress);
                    //gZipStream.Position = 0; // runtime error: 'this operation is not supported'
                    gZipStream.CopyTo(memoryStream);
                }
                else
                {
                    fileStream.Position = 0;
                    fileStream.CopyTo(memoryStream);
                }
            }
            LoadGraph(memoryStream);
        }

        private void ShowCancelledError()
        {
            string msg = $"Cancelled loading file:\n{FilePath}.";
            MessageBox.Show(msg, "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowLoadSaveError()
        {
            string msg = $"Error loading file:\n{FilePath}.";
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region LoadFileForm

        public static object LoadFile(string filePath, out Version fileVersion)
        {
            using LoadFileForm form = new LoadFileForm()
            {
                FilePath = filePath,
                FileFormat = FileForm.GetFileFormat(filePath),
            };
            object graph = form.ShowDialog() == DialogResult.OK ? form.Graph : null;
            fileVersion = form.FileVersion;
            return graph;
        }

        public static object LoadDialog(ref string filePath, out Version fileVersion)
        {
            if (FileUtils.GetOpenFileName(ref filePath, FileForm.FreshFilter))
                return LoadFile(filePath, out fileVersion);
            else
            {
                fileVersion = null;
                return null;
            }
        }


        #endregion
    }
}
