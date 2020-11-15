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

namespace Neunet.Forms
{
    public partial class DigitsForm : BaseForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private CalculationSettings Settings { get; set; } = new CalculationSettings();

        private ProgressReporter Reporter { get => new ProgressReporter(this, (reportData) => { ReportProgress(reportData); }); }

        private CancellationTokenSource TokenSource { get; set; }

        private readonly string _networkFilePathId = "NetworkFilePath";

        private string NetworkFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_networkFilePathId, string.Empty); }
            set
            {
                Program.XmlSettings.GlobalsElement.WriteString(_networkFilePathId, value);
                Text = Path.GetFileNameWithoutExtension(value);
            }
        }

        private readonly string _trainingSetImageFilePathId = "TrainingSetImageFilePath";

        private string TrainingSetImageFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_trainingSetImageFilePathId, "train-images.idx3-ubyte"); }
            set { Program.XmlSettings.GlobalsElement.WriteString(_trainingSetImageFilePathId, value); }
        }

        private readonly string _trainingSetLabelFilePathId = "TrainingSetLabelFilePath";

        private string TrainingSetLabelFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_trainingSetLabelFilePathId, "train-labels.idx1-ubyte"); }
            set { Program.XmlSettings.GlobalsElement.WriteString(_trainingSetLabelFilePathId, value); }
        }

        private ByteArray _trainingSetImages;
        private ByteArray TrainingSetImages
        {
            get { return _trainingSetImages; }
            set
            {
                _trainingSetImages = value ?? throw new VarNullException(nameof(TrainingSetImages), 229440);
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

        private readonly string _testSetImageFilePathId = "TestSetImageFilePath";

        private string TestSetImageFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_testSetImageFilePathId, "t10k-images.idx3-ubyte"); }
            set { Program.XmlSettings.GlobalsElement.WriteString(_testSetImageFilePathId, value); }
        }

        private readonly string _testSetLabelFilePathId = "TestSetLabelFilePath";

        private string TestSetLabelFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_testSetLabelFilePathId, "t10k-labels.idx1-ubyte"); }
            set { Program.XmlSettings.GlobalsElement.WriteString(_testSetLabelFilePathId, value); }
        }

        private ByteArray _testSetImages;
        private ByteArray TestSetImages
        {
            get { return _testSetImages; }
            set
            {
                _testSetImages = value ?? throw new VarNullException(nameof(TestSetImages), 229440);
            }
        }

        private ByteArray _testSetLabels;
        private ByteArray TestSetLabels
        {
            get { return _testSetLabels; }
            set
            {
                _testSetLabels = value ?? throw new VarNullException(nameof(TestSetLabels), 682702);
            }
        }

        private Mersenne Mersenne { get; set; } = new Mersenne();

        private bool _saveReminder = false;
        private bool SaveReminder
        {
            get { return _saveReminder; }
            set
            {
                _saveReminder = value;
                saveReminderLabel.Visible = value;
            }
        }

        private Network _network;
        private Network NetworkX
        {
            get { return _network; }
            set
            {
                _network = value;
                Coefficients = value?.GetCoefficients();
                historyImage.ClearItems();
                Derivatives = null;
                Measurements = null;
            }
        }

        private SampleList _trainingSamples;
        public SampleList TrainingSamples
        {
            get { return _trainingSamples; }
            set
            {
                _trainingSamples = value;
                trainingSamplesImage.Samples = value;
                trainingSamplesLabel.Text = $"{value.Count} training samples";
            }
        }

        private SampleList _testSamples;
        public SampleList TestSamples
        {
            get { return _testSamples; }
            set
            {
                _testSamples = value;
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
            trainingSamplesImage.Measurements = value;
            if (value == null) return;
            int nSample = TrainingSamples.Count;
            if (nSample != Measurements.Count) throw new UnequalValueException(nSample, Measurements.Count, 703443);
            int okCount = 0;
            int nokCount = 0;
            for (int iSample = 0; iSample < nSample; iSample++)
            {
                Sample sample = TrainingSamples[iSample];
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
            trainingSamplesLabel.Text = text;
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

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public DigitsForm()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        private const string _SplitterDistanceId = "SplitterDistance";
        private const string _SampleCountId = "SampleCount";
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

        private void LoadTrainingSetImageFile()
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
            LoadTrainingSetImageFile();
            return true;
        }

        private void LoadTrainingSetLabelFile()
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
            LoadTrainingSetLabelFile();
            return true;
        }

        private void LoadTestSetImageFile()
        {
            // train-images.idx3-ubyte
            using (FileStream stream = new FileStream(TestSetImageFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                TestSetImages = BaseArray.ReadFromStream(stream) as ByteArray;
            }
        }

        private bool LoadTestSetImageFileDialog()
        {
            do
            {
                openFileDialog.Filter = "idx3-ubyte files|*.idx3-ubyte|All files|*.*";
                openFileDialog.Title = "Open test set image file";
                openFileDialog.FileName = TestSetImageFilePath;
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return false;
                TestSetImageFilePath = openFileDialog.FileName;
            }
            while (!File.Exists(TestSetImageFilePath));
            LoadTestSetImageFile();
            return true;
        }

        private void LoadTestSetLabelFile()
        {
            using (FileStream stream = new FileStream(TestSetLabelFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                TestSetLabels = BaseArray.ReadFromStream(stream) as ByteArray;
            }
        }

        private bool LoadTestSetLabelFileDialog()
        {
            do
            {
                // train-labels.idx1-ubyte
                openFileDialog.Filter = "idx1-ubyte files|*.idx1-ubyte|All files|*.*";
                openFileDialog.Title = "Open test set label file";
                openFileDialog.FileName = TestSetLabelFilePath;
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return false;
                TestSetLabelFilePath = openFileDialog.FileName;
            }
            while (!File.Exists(TestSetLabelFilePath));
            LoadTestSetLabelFile();
            return true;
        }

        private void LoadNetwork()
        {
            Network network = (Network)LoadFileForm.LoadFile(NetworkFilePath, out Version fileVersion);
            NetworkX = network;
            SaveReminder = false;
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
            SaveFileForm.SaveFile(NetworkFilePath, NetworkX);
            SaveReminder = false;
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
                    LoadTrainingSetImageFile();
                else
                    if (!LoadTrainingSetImageFileDialog()) Close();

                if (File.Exists(TrainingSetLabelFilePath))
                    LoadTrainingSetLabelFile();
                else
                    if (!LoadTrainingSetLabelFileDialog()) Close();

                if (File.Exists(TestSetImageFilePath))
                    LoadTestSetImageFile();
                else
                    if (!LoadTestSetImageFileDialog()) Close();

                if (File.Exists(TestSetLabelFilePath))
                    LoadTestSetLabelFile();
                else
                    if (!LoadTestSetLabelFileDialog()) Close();

                if (File.Exists(NetworkFilePath))
                {
                    LoadNetwork();
                    Text = Path.GetFileNameWithoutExtension(NetworkFilePath);
                }
                else
                {
                    NewNetwork();
                    RandomizeNetwork();
                }
                SetStatusText(string.Empty);
                const int outputCount = 10;
                TrainingSamples = GetRandomSamples(
                    TrainingSetImages, TrainingSetLabels,
                    Settings.TrainingSampleCount, outputCount, Mersenne);
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
                e.Cancel = SaveReminder && CancelOnClose();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
                e.Cancel = false;
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

        private void OpenTestSetImageFileMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTestSetImageFileDialog();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void OpenTestSetLabelFileMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTestSetLabelFileDialog();
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


        private void NewNetwork()
        {
            int nx = TrainingSetImages.GetLength(1);
            int ny = TrainingSetImages.GetLength(2);
            NetworkX = Randomize(new Network
            (
                new Layer(nx * ny),
                new Layer(30),
                new Layer(10)
            ));
            SaveReminder = false;
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveReminder && CancelOnClose()) return;
                NewNetwork();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void EditNetwork()
        {
            using (NetworkDialog dialog = new NetworkDialog() { Network = (Network)NetworkX.Clone() })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    NetworkX = dialog.Network;
                    SaveReminder = false;
                }
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveReminder && CancelOnClose()) return;
                EditNetwork();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void RandomizeNetwork()
        {
            NetworkX = Randomize((Network)NetworkX.Clone());
            SaveReminder = false;
        }

        private void RandomizeMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveReminder && CancelOnClose()) return;
                RandomizeNetwork();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Learn(CalculationSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            reporter?.WriteStart("Learning the network...");
            while (true)
            {
                SampleList samples = GetRandomSamples(
                    TrainingSetImages, TrainingSetLabels,
                    Settings.TrainingSampleCount, NetworkX.Output.Count, Mersenne);

                reporter?.ReportSamples(samples);
                NetworkX.Learn(samples, settings, reporter, tokenSource);
            }
        }

        private async Task LearnAsync()
        {
            SaveReminder = true;
            await RunAsync((CalculationSettings, ProgressReporter, CancellationTokenSource) =>
            Learn(CalculationSettings, ProgressReporter, CancellationTokenSource));
            SetProgress(0);
            SetStatusText(string.Empty);
        }

        private async void LearnButton_Click(object sender, EventArgs e)
        {
            try
            {
                await LearnAsync();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private async Task TestAsync()
        {
            Single1D derivatives = NetworkX.CreateCoefficients();
            int ny = NetworkX.Output.Count;
            SampleList samples = GetRandomSamples(
                TestSetImages, TestSetLabels,
                Settings.TestSampleCount, ny, Mersenne);

            MeasurementList measurements = new MeasurementList(samples.Count, ny);
            await RunAsync((settings, reporter, tokenSource) =>
            {
                reporter?.WriteStart($"Calculating {samples.Count} test samples...");
                Stopwatch timer = new Stopwatch();
                timer.Start();
                float finalCost = NetworkX.GetCostAndDerivatives(samples, derivatives, measurements, settings, reporter, tokenSource);
                reporter?.WriteEnd($"The samples are calculated in {timer.Elapsed.TotalSeconds} s, and the final cost value is {finalCost:F4}.");
            });
            TrainingSamples = samples;
            Derivatives = derivatives;
            Measurements = measurements;
            SetProgress(0);
        }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            try
            {
                await TestAsync();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void Stop()
        {
            if (TokenSource != null && !TokenSource.IsCancellationRequested)
                TokenSource.Cancel();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                Stop();
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void CalculationSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SettingsDialog dialog = new SettingsDialog() { Settings = (CalculationSettings)Settings.Clone() })
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                        Settings = dialog.Settings as CalculationSettings;
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
                using (AboutDialog dialog = new AboutDialog())
                {
                    dialog.ShowDialog(this);
                }
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
            bool running = TokenSource != null;
            bool stopEnabled = running && !TokenSource.IsCancellationRequested;
            // File menu
            newMenuItem.Enabled = !running;
            newToolStripButton.Enabled = !running;
            openMenuItem.Enabled = !running;
            openToolStripButton.Enabled = !running;
            saveMenuItem.Enabled = !running;
            saveAsMenuItem.Enabled = !running;
            openTrainingSetImageFileMenuItem.Enabled = !running;
            openTrainingSetLabelFileMenuItem.Enabled = !running;
            openTestSetImageFileMenuItem.Enabled = !running;
            openTestSetLabelFileMenuItem.Enabled = !running;
            exitMenuItem.Enabled = !running;
            // Edit menu
            editToolStripMenuItem1.Enabled = !running;
            editToolStripButton.Enabled = !running;
            // Run menu
            randomizeToolStripMenuItem.Enabled = !running;
            randomizeToolStripButton.Enabled = !running;
            learnToolStripMenuItem.Enabled = !running;
            learnToolStripButton.Enabled = !running;
            verifyToolStripButton.Enabled = !running;
            verifyToolStripMenuItem.Enabled = !running;
            stopToolStripMenuItem.Enabled = stopEnabled;
            stopToolStripButton.Enabled = stopEnabled;
            settingsToolStripMenuItem.Enabled = !running;
            settingsToolStripButton.Enabled = !running;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Training set

        //private Network AddInputs(Network network)
        //{
        //    int nx = TrainingSetImages.GetLength(1);
        //    int ny = TrainingSetImages.GetLength(2);
        //    network.ClearInputs();
        //    network.AddInput(new Layer(nx * ny));
        //    return network;
        //}

        private Network Randomize(Network network)
        {
            network.Randomize(Mersenne, Settings.BiasMagnitude, Settings.WeightMagnitude);
            return network;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region RunAsync

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
                TrainingSamples = samplesData.Samples;
                Measurements = null;
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


        private async Task RunAsync(Action<CalculationSettings, ProgressReporter, CancellationTokenSource> action)
        {
            try
            {
                SaveReminder = true;
                TokenSource = new CancellationTokenSource();
                Exception ex = await Task.Run(() =>
                {
                    try
                    {
                        action(Settings, Reporter, TokenSource);
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
                if (TokenSource != null) { TokenSource.Dispose(); TokenSource = null; }
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Samples

        private static void GetXs(ByteArray images, int h, Single1D xs)
        {
            int nu = images.GetLength(1);
            int nv = images.GetLength(2);
            int k = 0;
            for (int i = 0; i < nu; i++)
                for (int j = 0; j < nv; j++)
                {
                    xs[k++] = (float)images[h, i, j] / 255f;
                }
        }

        private static void GetYs(ByteArray labels, int h, Single1D ys)
        {
            int ny = ys.Count;
            for (int i = 0; i < ny; i++)
            {
                ys[i] = labels[h] == i ? 1f : 0f;
            }
        }

        private static SampleList GetRandomSamples(
            ByteArray images, ByteArray labels, int nSample,
            int ny, Random random)
        {
            int nSource = images.GetLength(0);
            if (nSample < 1) nSample = 1; else if (nSample > nSource) nSample = nSource;
            int nu = images.GetLength(1);
            int nv = images.GetLength(2);
            int nx = nu * nv;
            if (nu * nv != nx) throw new UnequalValueException(nu * nv, nx, 292526);
            SampleList samples = new SampleList(nu, nv);
            List<int> Indices = new List<int>(nSource);
            for (int index = 0; index < nSource; index++)
                Indices.Add(index);
            for (int iSample = 0; iSample < nSample; iSample++)
            {
                // generate unique indices using Mersenne random numbers
                int next = random.Next(Indices.Count);
                int index = Indices[next];
                Indices.RemoveAt(next); // remove the used index
                Sample sample = new Sample(nx, ny)
                {
                    Index = index,
                    Label = labels[index],
                };
                GetXs(images, index, sample.Inputs);
                GetYs(labels, index, sample.Requirements);
                samples.Add(sample);
            }
            return samples;
        }

        #endregion

    }
}
