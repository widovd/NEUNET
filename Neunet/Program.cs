using System;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Neunet.Serializers;
using Neunet.Forms;
using Neulib.Serializers;

namespace Neunet
{
    static class Program
    {
        public static string Name
        {
            get => "NEUNET";
        }

        public static Version Version
        {
            get => Assembly.GetExecutingAssembly().GetName().Version;
        }

        public static string VersionString
        {
            get => Version.ToString();
        }

        public static string AssemblyTitleAndVersion
        {
            get => $"{Name} {Version}";
        }

        public static string ApplicationData
        // The directory that serves as a common repository for application-specific data for the current roaming user.
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Name); }
        }

        public static string CommonApplicationData
        // The directory that serves as a common repository for application-specific data that is used by all users.
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Name); }
        }

        public static string LocalApplicationData
        // The directory that serves as a common repository for application-specific data that is used by the current, non-roaming user.
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Name); }
        }

        public static string ProgramFiles
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), Name); }
        }
        public static XmlSettings XmlSettings { get; private set; }

        public static XmlElement GlobalsElement
        {
            get { return XmlSettings?.GlobalsElement; }
        }

        private const string _imageFilePathId = "ImageFilePath";

        public static string ImageFilePath
        {
            get { return GlobalsElement.ReadString(_imageFilePathId, ""); }
            set { GlobalsElement.WriteString(_imageFilePathId, value); }
        }

        private const string _imageFilterIndexId = "ImageFilterIndex";

        public static int ImageFilterIndex
        {
            get { return GlobalsElement.ReadInt(_imageFilterIndexId, 0); }
            set { GlobalsElement.WriteInt(_imageFilterIndexId, value); }
        }


        public static XmlElement FormsElement
        {
            get { return XmlSettings?.FormsElement; }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
