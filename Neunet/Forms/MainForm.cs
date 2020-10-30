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
                Coefficients = value.GetCoefficients();
            }
        }

        private SampleList _samples;
        public SampleList Samples
        {
            get { return _samples; }
            set
            {
                _samples = value;
                samplesImage.Samples = value;
                samplesLabel.Text = $"{value.Count} samples";
            }
        }


        private MeasurementList _measurements;
        private MeasurementList Measurements
        {
            get { return _measurements; }
            set { SetMeasurements(value); }
        }

        private void SetMeasurements(MeasurementList value)
        {
            _measurements = value;
            samplesImage.Measurements = value;
            if (value == null) return;
            int nSample = Samples.Count;
            if (nSample != Measurements.Count) throw new UnequalValueException(nSample, Measurements.Count, 703443);
            int okCount = 0;
            int nokCount = 0;
            for (int iSample = 0; iSample < nSample; iSample++)
            {
                Sample sample = Samples[iSample];
                Single1D measurement = Measurements[iSample];
                int ny = sample.Requirements.Count;
                if (ny != measurement.Count) throw new UnequalValueException(ny, measurement.Count, 331388);
                for (int i = 0; i < ny; i++)
                {
                    bool required = sample.Requirements[i] > 0.5f;
                    bool measured = measurement[i] > 0.5f;
                    if (!required && !measured) continue; // trivial
                    bool ok = !(required ^ measured);
                    if (ok) okCount++; else nokCount++;
                }
            }
            int totalCount = okCount + nokCount;
            string text = $"{value.Count} samples";
            if (totalCount > 0)
                text += $"; {okCount}/{totalCount} = {(float)okCount / totalCount:P1} is ok, and {nokCount}/{totalCount} =  {(float)nokCount / totalCount:P1} is not ok";
            samplesLabel.Text = text;
        }


        private Single1D _coefficients;
        private Single1D Coefficients
        {
            get { return _coefficients; }
            set
            {
                _coefficients = value;
                coefficientsImage.Values = value;
                coefficientsLabel.Text = value != null ? $"{value.Count} coefficients" : string.Empty; 
            }
        }

        private Single1D _derivatives;
        private Single1D Derivatives
        {
            get { return _derivatives; }
            set
            {
                _derivatives = value;
                derivativesImage.Values = value;

                derivativesLabel.Text = value != null ? $"{value.Count} derivatives" : string.Empty;
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
            float biasMagnitude = Settings.BiasMagnitude;
            float weightMagnitude = Settings.WeightMagnitude;
            Network network = new Network();
            network.AddLayer(28 * 28, Mersenne, biasMagnitude, weightMagnitude);
            network.AddLayer(100, Mersenne, biasMagnitude, weightMagnitude);
            network.AddLayer(30, Mersenne, biasMagnitude, weightMagnitude);
            network.AddLayer(10, Mersenne, biasMagnitude, weightMagnitude);
            historyImage.ClearItems();
            Derivatives = null;
            Measurements = null;
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
                Samples = GetRandomSamples(Network, nSampleTextBox.Value);
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
                const string message = "Are you sure you want to clear settings.xml in the AppData folder?";
                const string caption = "Settings";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            newMenuItem.Enabled = !running;
            openMenuItem.Enabled = !running;
            saveMenuItem.Enabled = !running;
            saveAsMenuItem.Enabled = !running;
            openTrainingSetImageFileMenuItem.Enabled = !running;
            openTrainingSetLabelFileMenuItem.Enabled = !running;
            exitMenuItem.Enabled = !running;


            learnButton.Enabled = !running;
            stopButton.Enabled = stopenabled;
            randomSamplesButton.Enabled = !running;
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

        private void GetXs(int h, Single1D xs)
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

        private void GetYs(int h, Single1D ys)
        {
            int ny = ys.Count;
            for (int i = 0; i < ny; i++)
            {
                ys[i] = TrainingSetLabels[h] == i ? 1f : 0f;
            }
        }

        private SampleList GetRandomSamples(Network network, int nSample)
        {
            int nu = TrainingSetImages.GetLength(1);
            int nv = TrainingSetImages.GetLength(2);
            int nx = network.First.Neurons.Count;
            if (nu * nv != nx) throw new UnequalValueException(nu * nv, nx, 292526);
            int ny = network.Last.Neurons.Count;
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
                GetXs(h, sample.Inputs);
                GetYs(h, sample.Requirements);
                samples.Add(sample);
            }
            return samples;
        }

        private void RandomSamplesButton_Click(object sender, EventArgs e)
        {
            try
            {
                Network network = Network; // Clone ?
                Samples = GetRandomSamples(network, nSampleTextBox.Value);
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
            Single1D xs = new Single1D(nx);
            int ny = Network.Last.Neurons.Count;
            Single1D output = new Single1D(ny);
            SingleArray trainingSetResults = new SingleArray(nh, ny);
            for (int h = 0; h < nh; h++)
            {
                GetXs(h, xs);
                Network.FeedForward(xs, output);
                for (int i = 0; i < ny; i++)
                {
                    trainingSetResults[h, i] = output[i];
                }
            }
            TrainingSetResults = trainingSetResults;
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

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Learn

        private void Learn(CalculationArguments arguments)
        {
            Network network = Network; // Clone
            while (true)
            {
                SampleList samples = GetRandomSamples(network, arguments.settings.SampleCount);
                arguments.reporter?.ReportSamples(samples);
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
            else if (reportData is SamplesReportData samplesData)
            {
                Samples = samplesData.Samples;
            }
            else if (reportData is CoefficientsReportData coefficientsData)
            {
                Coefficients = coefficientsData.Coefficients;
            }
            else if (reportData is CostAndDerivativesReportData iterationData)
            {
                float cost = iterationData.Cost;
                historyImage.AddValue(this, (float)Math.Sqrt(cost));
                Derivatives = iterationData.Derivatives;
                Measurements = iterationData.Measurements;
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
                NetworkChanged = true;
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
