using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neulib.Numerics;
using Neulib.Exceptions;
using Neulib.Neurons;
using System.IO;
using Neulib.MultiArrays;
using Neunet.Images.Charts2D;
using Neulib;

namespace Neunet.Forms
{
    public partial class MainForm : BaseForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private Mersenne Mersenne { get; set; } = new Mersenne();
        private Network Network { get; set; } = new Network();

        private CancellationTokenSource _tokenSource = null;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public MainForm()
        {
            InitializeComponent();
            ReadTraingSetImageFile();
            ReadTraingSetLabelFile();
            ImageIndex = 0;
        }

        private string _filePath = @"D:\VS Projects\Neunet\NeunetData\Networks\Network.bin";

        private void LoadNetwork()
        {
            if (File.Exists(_filePath))
            {
                Network = (Network)LoadFileForm.LoadFile(_filePath, out Version fileVersion);
                UpdateCoefficients();
            }
            else
                NewNetwork();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadNetwork();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void SaveNetwork()
        {
            SaveFileForm.SaveFile(_filePath, Network);
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                SaveNetwork();
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

        private void ReadTraingSetImageFile()
        {
            string filePath = @"D:\VS Projects\Neunet\NeunetData\Samples\Idx\train-images.idx3-ubyte";
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                TrainingSetImages = BaseArray.ReadFromStream(stream) as ByteArray;
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

        private void ReadTraingSetLabelFile()
        {
            string filePath = @"D:\VS Projects\Neunet\NeunetData\Samples\Idx\train-labels.idx1-ubyte";
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                TrainingSetLabels = BaseArray.ReadFromStream(stream) as ByteArray;
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

        private void NewNetwork()
        {
            const float biasMagnitude = 0.1f;
            const float weightMagnitude = 0.1f;
            Network = new Network();
            Network.AddLayer(28 * 28, Mersenne, biasMagnitude, weightMagnitude);
            Network.AddLayer(100, Mersenne, biasMagnitude, weightMagnitude);
            Network.AddLayer(30, Mersenne, biasMagnitude, weightMagnitude);
            Network.AddLayer(10, Mersenne, biasMagnitude, weightMagnitude);
            UpdateCoefficients();
        }

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
                            sample.Xs[k++] = (float)TrainingSetImages[h, i, j] / 255f;
                        }
                    for (int i = 0; i < ny; i++)
                    {
                        sample.Ys[i] = _trainingSetLabels[h] == i ? 1f : 0f;
                    }
                    samples.Add(sample);
                }
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
            if (statusLabel.Text != value) statusLabel.Text = value;
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
