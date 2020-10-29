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
using Neulib.MultiArrays;
using Neulib.Serializers;
using Neunet.Extensions;
using Neunet.Images.Charts2D;
using Neunet.Serializers;

namespace Neunet.Forms
{
    public partial class MainForm : BaseForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        //private readonly string _workingFolderId = "WorkingFolder";

        //private string WorkingFolder
        //{
        //    get { return Program.XmlSettings.GlobalsElement.ReadString(_workingFolderId, string.Empty); }
        //    set { Program.XmlSettings.GlobalsElement.WriteString(_workingFolderId, value); }
        //}

        private CalculationSettings Settings { get; set; } = new CalculationSettings();

        private readonly string _networkFilePathId = "NetworkFilePath";

        private string NetworkFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_networkFilePathId, string.Empty); }
            set { Program.XmlSettings.GlobalsElement.WriteString(_networkFilePathId, value); }
        }

        private readonly string _trainingSetImageFilePathId = "TrainingSetImageFilePath";

        private string TrainingSetImageFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_trainingSetImageFilePathId, string.Empty); }
            set { Program.XmlSettings.GlobalsElement.WriteString(_trainingSetImageFilePathId, value); }
        }

        private readonly string _trainingSetLabelFilePathId = "TrainingSetLabelFilePath";

        private string TrainingSetLabelFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_trainingSetLabelFilePathId, string.Empty); }
            set { Program.XmlSettings.GlobalsElement.WriteString(_trainingSetLabelFilePathId, value); }
        }


        private Mersenne Mersenne { get; set; } = new Mersenne();

        private bool _networkChanged = false;
        private bool NetworkChanged
        {
            get { return _networkChanged; }
            set
            {
                _networkChanged = value;
                changedStatusLabel.Visible = value;
            }
        }

        private Network _network = new Network();
        private Network Network
        {
            get { return _network; }
            set
            {
                _network = value;
                NetworkChanged = true;
                UpdateCoefficients();
            }
        }

        private SampleList _samples;
        public SampleList Samples
        {
            get { return _samples; }
            set
            {
                _samples = value;
                UpdateCoefficients();
            }
        }
        
        private CancellationTokenSource _tokenSource = null;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        private const string _SplitterDistanceId = "SplitterDistance";
        private const string _CalculationSettingsId = "CalculationSettings";

        public override void LoadFromSettings(XmlElement rootElement)
        {
            base.LoadFromSettings(rootElement);
            splitContainer.SetSplitterDistance(rootElement, _SplitterDistanceId);
            Settings.LoadFromSettings(rootElement.GetOrCreateElement(_CalculationSettingsId));
        }

        public override void SaveToSettings(XmlElement rootElement)
        {
            base.SaveToSettings(rootElement);
            rootElement.WriteInt(_SplitterDistanceId, splitContainer.SplitterDistance);
            Settings.SaveToSettings(rootElement.GetOrCreateElement(_CalculationSettingsId));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region MainForm events

        private void LoadTraingSetImageFile()
        {
            // train-images.idx3-ubyte
            using (FileStream stream = new FileStream(TrainingSetImageFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                TrainingSetImages = BaseArray.ReadFromStream(stream) as ByteArray;
            }
        }

        private bool LoadTrainingSetImageFileDialog()
        {
            do
            {
                openFileDialog.Filter = "idx3-ubyte files|*.idx3-ubyte|All files|*.*";
                openFileDialog.Title = "Open training set image file";
                openFileDialog.FileName = TrainingSetImageFilePath;
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return false;
                TrainingSetImageFilePath = openFileDialog.FileName;
            }
            while (!File.Exists(TrainingSetImageFilePath));
            LoadTraingSetImageFile();
            return true;
        }

        private void LoadTraingSetLabelFile()
        {
            using (FileStream stream = new FileStream(TrainingSetLabelFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                TrainingSetLabels = BaseArray.ReadFromStream(stream) as ByteArray;
            }
        }

        private bool LoadTrainingSetLabelFileDialog()
        {
            do
            {
                // train-labels.idx1-ubyte
                openFileDialog.Filter = "idx1-ubyte files|*.idx1-ubyte|All files|*.*";
                openFileDialog.Title = "Open training set label file";
                openFileDialog.FileName = TrainingSetLabelFilePath;
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return false;
                TrainingSetLabelFilePath = openFileDialog.FileName;
            }
            while (!File.Exists(TrainingSetLabelFilePath));
            LoadTraingSetLabelFile();
            return true;
        }


        private void NewNetwork()
        {
            const float biasMagnitude = 0.1f;
            const float weightMagnitude = 0.1f;
            Network network = new Network();
            network.AddLayer(28 * 28, Mersenne, biasMagnitude, weightMagnitude);
            network.AddLayer(100, Mersenne, biasMagnitude, weightMagnitude);
            network.AddLayer(30, Mersenne, biasMagnitude, weightMagnitude);
            network.AddLayer(10, Mersenne, biasMagnitude, weightMagnitude);
            Network = network;
        }


        private void LoadNetwork()
        {
            Network = (Network)LoadFileForm.LoadFile(NetworkFilePath, out Version fileVersion);
            NetworkChanged = false;
        }

        private void LoadNetworkDialog()
        {
            do
            {
                openFileDialog.Filter = "bin files|*.bin|All files|*.*";
                openFileDialog.Title = "Open network file";
                if (!string.IsNullOrEmpty(NetworkFilePath))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(NetworkFilePath);
                    openFileDialog.FileName = Path.GetFileNameWithoutExtension(NetworkFilePath);
                }
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;
                NetworkFilePath = openFileDialog.FileName;
            }
            while (!File.Exists(NetworkFilePath));
            LoadNetwork();
        }

        private void SaveNetwork()
        {
            SaveFileForm.SaveFile(NetworkFilePath, Network);
            NetworkChanged = false;
        }

        private void SaveNetworkDialog()
        {
            saveFileDialog.Filter = "bin files|*.bin|All files|*.*";
            saveFileDialog.Title = "Save network file";
            if (!string.IsNullOrEmpty(NetworkFilePath))
            {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(NetworkFilePath);
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(NetworkFilePath);
            }
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                NetworkFilePath = saveFileDialog.FileName;
                SaveNetwork();
            }
        }

        private void SaveNetworkElseDialog()
        {
            string filePath = NetworkFilePath;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                SaveNetwork();
            else
                SaveNetworkDialog();
        }

        private bool CancelOnClose()
        {
            const string message = "The network has changed. Save before closing?";
            const string caption = "Closing";
            const MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            switch (result)
            {
                case DialogResult.No:
                    return false;
                case DialogResult.Yes:
                    SaveNetworkElseDialog();
                    return false;
            }
            return true; // cancelled
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //while (!Directory.Exists(WorkingFolder))
                //{
                //    folderBrowserDialog.SelectedPath = WorkingFolder;
                //    if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK) Close();
                //    WorkingFolder = folderBrowserDialog.SelectedPath;
                //}

                if (File.Exists(TrainingSetImageFilePath))
                    LoadTraingSetImageFile();
                else
                    if (!LoadTrainingSetImageFileDialog()) Close();

                if (File.Exists(TrainingSetLabelFilePath))
                    LoadTraingSetLabelFile();
                else
                    if (!LoadTrainingSetLabelFileDialog()) Close();

                if (File.Exists(NetworkFilePath))
                    LoadNetwork();
                else
                    NewNetwork();
                RandomSamples();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = NetworkChanged && CancelOnClose();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
                e.Cancel = false;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewNetwork();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNetworkDialog();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveNetworkElseDialog();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveNetworkDialog();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void OpenTrainingSetImageFileMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTrainingSetImageFileDialog();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void OpenTrainingSetLabelFileMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTrainingSetLabelFileDialog();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void CalculationSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (CalculationSettingsDialog dialog = new CalculationSettingsDialog() { Settings = Settings })
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                        Settings = dialog.Settings;
                }
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void ClearSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to clear settings.xml in the AppData folder?",
        "Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
                Program.XmlSettings.Clear();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void ApplicationDataFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer", Program.ApplicationData);
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void CommonAppDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer", Program.CommonApplicationData);
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void ProgramFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer", Program.ProgramFiles);
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        public override void Idle()
        {
            base.Idle();
            bool running = _tokenSource != null;
            bool stopenabled = running && !_tokenSource.IsCancellationRequested;
            newButton.Enabled = !running;
            learnButton.Enabled = !running;
            stopButton.Enabled = stopenabled;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Training set

        private ByteArray _trainingSetImages;
        private ByteArray TrainingSetImages
        {
            get { return _trainingSetImages; }
            set
            {
                _trainingSetImages = value ?? throw new VarNullException(nameof(TrainingSetImages), 229440);
                //matrixImage1.ImageArray = TrainingSetImages;
            }
        }

        private ByteArray _trainingSetLabels;
        private ByteArray TrainingSetLabels
        {
            get { return _trainingSetLabels; }
            set
            {
                _trainingSetLabels = value ?? throw new VarNullException(nameof(TrainingSetLabels), 682702);
                //matrixImage1.LabelArray = TrainingSetLabels;
            }
        }

        private SingleArray _trainingSetResults;
        private SingleArray TrainingSetResults
        {
            get { return _trainingSetResults; }
            set
            {
                _trainingSetResults = value ?? throw new VarNullException(nameof(TrainingSetResults), 134871);
                //matrixImage1.ResultsArray = TrainingSetResults;
            }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region ImageIndex

        private void GetXs(int h, float[] xs)
        {
            int nu = TrainingSetImages.GetLength(1);
            int nv = TrainingSetImages.GetLength(2);
            int k = 0;
            for (int i = 0; i < nu; i++)
                for (int j = 0; j < nv; j++)
                {
                    xs[k++] = (float)TrainingSetImages[h, i, j] / 255f;
                }
        }

        private void GetYs(int h, float[] ys)
        {
            int ny = ys.Length;
            for (int i = 0; i < ny; i++)
            {
                ys[i] = TrainingSetLabels[h] == i ? 1f : 0f;
            }
        }

        private SampleList GetSamples(int nSample)
        {
            int nu = TrainingSetImages.GetLength(1);
            int nv = TrainingSetImages.GetLength(2);
            int nx = Network.First.Neurons.Count;
            if (nu * nv != nx) throw new UnequalValueException(nu * nv, nx, 292526);
            int ny = Network.Last.Neurons.Count;
            SampleList samples = new SampleList(nu, nv, ny);
            int nSource = TrainingSetImages.GetLength(0);
            for (int iSample = 0; iSample < nSample; iSample++)
            {
                int h = Mersenne.Next(nSource);
                Sample sample = new Sample(nx, ny)
                {
                    Index = h,
                    Label = TrainingSetLabels[h],
                };
                GetXs(h, sample.Xs);
                GetYs(h, sample.Ys);
                Network.FeedForward(sample.Xs, sample.Zs);
                samples.Add(sample);
            }
            return samples;
        }

        private void RandomSamples()
        {
            samplesImage.Samples = GetSamples(nSampleTextBox.Value);
        }

        private void RandomSamplesButton_Click(object sender, EventArgs e)
        {
            try
            {
                RandomSamples();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        private void NewButton_Click(object sender, EventArgs e)
        {
            try
            {
                NewNetwork();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Test()
        {
            if (Network?.First == null) return;
            int nh = TrainingSetImages.GetLength(0);
            int nu = TrainingSetImages.GetLength(1);
            int nv = TrainingSetImages.GetLength(2);

            int nx = Network.First.Neurons.Count;
            if (nx != nu * nv) throw new UnequalValueException(nx, nu * nv, 527884);
            float[] xs = new float[nx];
            int ny = Network.Last.Neurons.Count;
            float[] ys = new float[ny];
            SingleArray trainingSetResults = new SingleArray(nh, ny);
            for (int h = 0; h < nh; h++)
            {
                GetXs(h, xs);
                Network.FeedForward(xs, ys);
                for (int i = 0; i < ny; i++)
                {
                    trainingSetResults[h, i] = ys[i];
                }
            }
            TrainingSetResults = trainingSetResults;
            //ysLabel.Text = $"{ys[0]:F3}, {ys[1]:F3}, {ys[2]:F3}, {ys[3]:F3}, {ys[4]:F3}, {ys[5]:F3}, {ys[6]:F3}, {ys[7]:F3}, {ys[8]:F3}, {ys[9]:F3}";
            //UpdateCoefficients();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            try
            {
                Test();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }


        private enum NeuronValueTypes { Bias, Sum, Activation, Delta }

        /// <summary>
        /// Fills a float array with the biasses and weights of all neurons in this network.
        /// </summary>
        /// <param name="p">The float array which must have been created.</param>
        private float[] GetNeuronValues(NeuronValueTypes t)
        {
            const bool skipFirst = true;
            int n = 0;
            Network.ForEach(layer =>
            {
                n += layer.Neurons.Count;
            }, skipFirst);
            float[] p = new float[n];
            int h = 0;
            Network.ForEach(layer =>
            {
                layer.ForEach((Neuron neuron) =>
                {
                    p[h++] = t switch
                    {
                        NeuronValueTypes.Bias => neuron.Bias,
                        NeuronValueTypes.Sum => neuron.Sum,
                        NeuronValueTypes.Activation => neuron.Activation,
                        NeuronValueTypes.Delta => neuron.Delta,
                        _ => 0f
                    };
                });
            }, skipFirst);
            return p;
        }

        private void UpdateCoefficients()
        {
            int n = Network.CountCoefficients();
            float[] values = new float[n];
            Network.GetCoefficients(values);
            //float[] values = GetNeuronValues(NeuronValueTypes.Activation);
            coefficientsImage.Coefficients = values;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Learn

        private void Learn(CalculationArguments arguments)
        {
            for (int g = 0; g < 100; g++)
            {
                SampleList samples = GetSamples(arguments.settings.SampleCount);
                NetworkChanged = true;
                Network.Learn(samples, arguments);
            }
        }

        private void SetProgress(double value)
        {
            int a = progressBar.Minimum + (int)(value * (progressBar.Maximum - progressBar.Minimum));
            if (a < progressBar.Minimum) a = progressBar.Minimum;
            if (a > progressBar.Maximum) a = progressBar.Maximum;
            progressBar.Value = a;
        }

        private void SetStatusText(string value)
        {
            if (messageStatusLabel.Text != value) messageStatusLabel.Text = value;
        }

        private void ReportProgress(ReportData reportData)
        {
            if (reportData is ProgressBarReportData progressData)
                SetProgress(progressData.X);
            else if (reportData is MessageReportData debugData)
            {
                if (debugData.MessageIndent == MessageIndentEnum.Start) SetStatusText(debugData.Message);
            }
            else if (reportData is IterationReportData iterationData)
            {
                label6.Text = $"{iterationData.Merit:F5}";
                UpdateCoefficients();
            }
        }

        private void HandleException(Exception ex)
        {
            Exception ex2 = ex;
            while (ex2.InnerException != null) ex2 = ex2.InnerException;
            if (ex2 is OperationCanceledException)
                MessageBox.Show("The operation is cancelled.", "Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                ExceptionDialog.Show(ex);
        }

        private async void LearnButton_Click(object sender, EventArgs e)
        {
            try
            {
                ProgressReporter reporter = new ProgressReporter(this, (reportData) => { ReportProgress(reportData); });
                _tokenSource = new CancellationTokenSource();
                Exception ex = await Task.Run(() =>
                {
                    try
                    {
                        Learn(new CalculationArguments(Settings, reporter, _tokenSource));
                        return null;
                    }
                    catch (Exception ex)
                    {
                        return ex;
                    }
                });
                if (ex != null) HandleException(ex);
                UpdateCoefficients();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
            finally
            {
                if (_tokenSource != null) { _tokenSource.Dispose(); _tokenSource = null; }
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_tokenSource != null && !_tokenSource.IsCancellationRequested)
                    _tokenSource.Cancel();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion

    }
}
