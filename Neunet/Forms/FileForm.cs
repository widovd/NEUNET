using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Neulib.Exceptions;
using Neulib.Serializers;
using Neulib;

namespace Neunet.Forms
{
    public enum FileFormatEnum
    {
        BinarySerializer, // binary, smaller files
        XmlDocSerializer, // readable text, large files, can not contain calculation data
    }

    public partial class FileForm : AsyncForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public FileFormatEnum FileFormat { get; set; } = FileFormatEnum.BinarySerializer;

        protected bool Zip
        {
            get => FileFormat == FileFormatEnum.BinarySerializer;
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (Equals(value, _filePath)) return;
                SetFilePath(value);
            }
        }

        protected virtual void SetFilePath(string value)
        {
            _filePath = value;
        }

        // The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized.
        public ISerializable Serializable { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public FileForm()
        {
            InitializeComponent();
            Action = FileAction;
            SetFilePath(string.Empty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) { components.Dispose(); components = null; }
            }
            base.Dispose(disposing);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        public override void Idle()
        {
            base.Idle();
            //stopButton.Enabled = CancellationTokenSource != null && !CancellationTokenSource.IsCancellationRequested;
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region LoadForm

        protected void SetMessageText(string text)
        {
            Message = text;
        }

        public const string ext1 = "bin";
        public const string ext2 = "xml";
        public const string FreshFilter = "Binary files|*." + ext1 + "|Xml files|*." + ext2 + "|All files|*.*";

        public static FileFormatEnum GetFileFormat(string filePath)
        {
            string ext = Path.GetExtension(filePath);
            ext = ext.Substring(1);
            FileFormatEnum fileFormat = ext switch
            {
                ext1 => FileFormatEnum.BinarySerializer,
                ext2 => FileFormatEnum.XmlDocSerializer,
                _ => throw new InvalidCaseException("ext", ext, 549860),
            };
            return fileFormat;
        }

        protected Serializer GetSerializer()
        {
            Serializer serializer = FileFormat switch
            {
                FileFormatEnum.BinarySerializer => new BinarySerializer(_tokenSource),
                //FileFormatEnum.XmlDocSerializer => new XmlDocSerializer(_tokenSource),
                _ => throw new InvalidCaseException(nameof(FileFormat), FileFormat, 802008),
            };
            return serializer;
        }

        protected virtual void FileAction(Settings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
        }

        #endregion
    }
}