using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using Neunet.Utils;
using Neulib;
using Neulib.Serializers;

namespace Neunet.Forms
{
    public partial class SaveFileForm : FileForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public SaveFileForm()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region FileForm

        protected override void SetFilePath(string value)
        {
            base.SetFilePath(value);
            Text = "Save solution";
            SetMessageText($"Saving ...\n{value}");
        }


        private void SaveGraph(Stream stream)
        {
            Serializer formatter = GetSerializer();
            formatter?.Serialize(stream, Graph);
        }

        protected override void FileAction(CalculationArguments arguments)
        {
            using MemoryStream memoryStream = new MemoryStream();
            SaveGraph(memoryStream);
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                if (Zip)
                {
                    using GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Compress);
                    memoryStream.Position = 0;
                    memoryStream.CopyTo(gZipStream);
                }
                else
                {
                    memoryStream.Position = 0;
                    memoryStream.CopyTo(fileStream);
                }
            }
        }

        private void ShowCancelledError()
        {
            string msg = $"Cancelled saving file:\n{FilePath}.";
            MessageBox.Show(msg, "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowLoadSaveError()
        {
            string msg = $"Error saving file:\n{FilePath}.";
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SaveFileForm

        public static bool SaveFile(string filePath, object graph)
        {
            using SaveFileForm form = new SaveFileForm()
            {
                FilePath = filePath,
                FileFormat = FileForm.GetFileFormat(filePath),
                Graph = graph,
            };
            return form.ShowDialog() == DialogResult.OK;
        }

        public static bool SaveDialog(ref string filePath, object graph)
        {
            return FileUtils.GetSaveFileName(ref filePath, FreshFilter) && SaveFile(filePath, graph);
        }

        #endregion
    }
}
