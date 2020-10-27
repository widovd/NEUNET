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
            Network = new Network();
            Network.AddLayer(28 * 28, Mersenne, biasMagnitude, weightMagnitude);
            Network.AddLayer(100, Mersenne, biasMagnitude, weightMagnitude);
            Network.AddLayer(30, Mersenne, biasMagnitude, weightMagnitude);
            Network.AddLayer(10, Mersenne, biasMagnitude, weightMagnitude);
        }


        private void LoadNetwork()
        {
            Network = (Network)LoadFileForm.LoadFile(NetworkFilePath, out Version fileVersion);
        }

        private void LoadNetworkDialog()
        {
            do
            {
                openFileDialog.Filter = "bin files|*.bin|All files|*.*";
                openFileDialog.Title = "Open network file";
                openFileDialog.FileName = NetworkFilePath;
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;
                NetworkFilePath = openFileDialog.FileName;
            }
            while (!File.Exists(NetworkFilePath));
            LoadNetwork();
        }

        private void SaveNetwork()
        {
            SaveFileForm.SaveFile(NetworkFilePath, Network);
        }

        private void SaveNetworkDialog()
        {
            saveFileDialog.Filter = "bin files|*.bin|All files|*.*";
            saveFileDialog.Title = "Save network file";
            saveFileDialog.FileName = NetworkFilePath;
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
                ImageIndex = 0;

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
                matrixImage1.ByteArray = TrainingSetImages;
                matrixImage2.ByteArray = TrainingSetImages;
                matrixImage3.ByteArray = TrainingSetImages;
                matrixImage4.ByteArray = TrainingSetImages;
                matrixImage5.ByteArray = TrainingSetImages;
            }
        }

        private ByteArray _trainingSetLabels;
        private ByteArray TrainingSetLabels
        {
            get { return _trainingSetLabels; }
            set
            {
                _trainingSetLabels = value ?? throw new VarNullException(nameof(TrainingSetLabels), 682702);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ImageIndex

        private void UpdateImage(MatrixImage matrixImage, Label label, int k)
        {
            matrixImage.ImageIndex = k;
            int dim0 = TrainingSetImages.GetLength(0);
            bool ok = k >= 0 && k < dim0;
            label.Text = ok ? $"[{k}] = {TrainingSetLabels[k]}" : string.Empty;
        }

        private int _imageIndex = 0;
        private int ImageIndex
        {
            get { return _imageIndex; }
            set
            {
                int dim0 = TrainingSetImages.GetLength(0);
                if (value < 0 || value >= dim0) return;
                _imageIndex = value;
                UpdateImage(matrixImage1, label1, value - 2);
                UpdateImage(matrixImage2, label2, value - 1);
                UpdateImage(matrixImage3, label3, value);
                UpdateImage(matrixImage4, label4, value + 1);
                UpdateImage(matrixImage5, label5, value + 2);
                Test(value);
            }
        }

        private void Left1Button_Click(object sender, EventArgs e)
        {
            ImageIndex--;
        }

        private void Right1Button_Click(object sender, EventArgs e)
        {
            ImageIndex++;
        }

        private void Left2Button_Click(object sender, EventArgs e)
        {
            ImageIndex -= 5;
        }

        private void Right2Button_Click(object sender, EventArgs e)
        {
            ImageIndex += 5;
        }

        private void RandomIndexButton_Click(object sender, EventArgs e)
        {
            ImageIndex = Mersenne.Next(TrainingSetImages.GetLength(0));
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

        private void Test(int h)
        {
            if (Network?.First == null) return;
            int nu = TrainingSetImages.GetLength(1);
            int nv = TrainingSetImages.GetLength(2);
            int nx = Network.First.Neurons.Count;
            if (nx != nu * nv) throw new UnequalValueException(nx, nu * nv, 527884);
            float[] xs = new float[nx];
            int k = 0;
            for (int i = 0; i < nu; i++)
                for (int j = 0; j < nv; j++)
                {
                    xs[k++] = TrainingSetImages[h, i, j] / 255;
                }
            int ny = Network.Last.Neurons.Count;
            float[] ys = new float[ny];
            Network.FeedForward(xs, ys);
            ysLabel.Text = $"{ys[0]:F3}, {ys[1]:F3}, {ys[2]:F3}, {ys[3]:F3}, {ys[4]:F3}, {ys[5]:F3}, {ys[6]:F3}, {ys[7]:F3}, {ys[8]:F3}, {ys[9]:F3}";
            UpdateCoefficients();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            try
            {
                Test(ImageIndex);
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
            int nu = TrainingSetImages.GetLength(1);
            int nv = TrainingSetImages.GetLength(2);
            int nx = Network.First.Neurons.Count;
            int ny = Network.Last.Neurons.Count;

            const int nSamples = 1000;

            for (int g = 0; g < 100; g++)
            {
                SampleList samples = new SampleList();
                for (int l = 0; l < nSamples; l++)
                {
                    int h = Mersenne.Next(TrainingSetImages.GetLength(0));
                    Sample sample = new Sample(nx, ny);
                    int k = 0;
                    for (int i = 0; i < nu; i++)
                        for (int j = 0; j < nv; j++)
                        {
                            sample.Xs[k++] = ((float)TrainingSetImages[h, i, j] - 128f) / 128f;
                        }
                    for (int i = 0; i < ny; i++)
                    {
                        sample.Ys[i] = _trainingSetLabels[h] == i ? 1f : 0f;
                    }
                    samples.Add(sample);
                }
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
                CalculationSettings settings = new CalculationSettings();
                ProgressReporter reporter = new ProgressReporter(this, (reportData) => { ReportProgress(reportData); });
                _tokenSource = new CancellationTokenSource();
                Exception ex = await Task.Run(() =>
                {
                    try
                    {
                        Learn(new CalculationArguments(settings, reporter, _tokenSource));
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
