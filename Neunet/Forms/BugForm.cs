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
using Neulib.Visuals;
using Neulib.Visuals.Bugs;
using Neulib.MultiArrays;
using Neulib.Serializers;
using Neunet.Extensions;

namespace Neunet.Forms
{
    public partial class BugForm : BaseForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private WorldSettings Settings { get; set; } = new WorldSettings();

        private ProgressReporter Reporter { get => new ProgressReporter(this, (reportData) => { ReportProgress(reportData); }); }

        private CancellationTokenSource TokenSource { get; set; }

        private readonly string _worldFilePathId = "WorldFilePath";

        private string WorldFilePath
        {
            get { return Program.XmlSettings.GlobalsElement.ReadString(_worldFilePathId, string.Empty); }
            set
            {
                Program.XmlSettings.GlobalsElement.WriteString(_worldFilePathId, value);
                Text = Path.GetFileNameWithoutExtension(value);
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

        private VisualWorld _bugWorld;
        private VisualWorld BugWorld
        {
            get { return _bugWorld; }
            set
            {
                _bugWorld = value;
                bugWorldImage.World = value;
            }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public BugForm()
        {
            InitializeComponent();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseForm

        private const string _CalculationSettingsId = "CalculationSettings";

        public override void LoadFromSettings(XmlElement rootElement)
        {
            base.LoadFromSettings(rootElement);
            Settings.LoadFromSettings(rootElement.GetOrCreateElement(_CalculationSettingsId));
        }

        public override void SaveToSettings(XmlElement rootElement)
        {
            base.SaveToSettings(rootElement);
            Settings.SaveToSettings(rootElement.GetOrCreateElement(_CalculationSettingsId));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region MainForm events


        private void LoadWorld()
        {
            BugWorld = (VisualWorld)LoadFileForm.LoadFile(WorldFilePath, out Version fileVersion);
            SaveReminder = false;
        }

        private void LoadWorldDialog()
        {
            do
            {
                openFileDialog.Filter = "bin files|*.bin|All files|*.*";
                openFileDialog.Title = "Open world file";
                if (!string.IsNullOrEmpty(WorldFilePath))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(WorldFilePath);
                    openFileDialog.FileName = Path.GetFileNameWithoutExtension(WorldFilePath);
                }
                if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;
                WorldFilePath = openFileDialog.FileName;
            }
            while (!File.Exists(WorldFilePath));
            LoadWorld();
        }

        private void SaveWorld()
        {
            SaveFileForm.SaveFile(WorldFilePath, BugWorld);
            SaveReminder = false;
        }

        private void SaveWorldDialog()
        {
            saveFileDialog.Filter = "bin files|*.bin|All files|*.*";
            saveFileDialog.Title = "Save world file";
            if (!string.IsNullOrEmpty(WorldFilePath))
            {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(WorldFilePath);
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(WorldFilePath);
            }
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                WorldFilePath = saveFileDialog.FileName;
                SaveWorld();
            }
        }

        private void SaveWorldElseDialog()
        {
            string filePath = WorldFilePath;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                SaveWorld();
            else
                SaveWorldDialog();
        }

        private bool CancelOnClose()
        {
            const string message = "The world has changed. Save before closing?";
            const string caption = "Closing";
            const MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            switch (result)
            {
                case DialogResult.No:
                    return false;
                case DialogResult.Yes:
                    SaveWorldElseDialog();
                    return false;
            }
            return true; // cancelled
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(WorldFilePath))
                {
                    LoadWorld();
                    Text = Path.GetFileNameWithoutExtension(WorldFilePath);
                }
                else
                {
                    NewWorld();
                    RandomizeWorld();
                }
                SetStatusText(string.Empty);
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
                LoadWorldDialog();
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
                SaveWorldElseDialog();
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
                SaveWorldDialog();
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


        private void NewWorld()
        {
            BugWorld = new VisualWorld()
            {
                new Bug(),
                new Bug(),
                new Bug(),
                new Bug(),
                new Bug(),
                new Bug(),
                new Bug(),
            };
            BugWorld.Randomize(Mersenne);
            bugWorldImage.RefreshImage();
            SaveReminder = false;
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveReminder && CancelOnClose()) return;
                NewWorld();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void EditWorld()
        {
            using (WorldDialog dialog = new WorldDialog() { World = (VisualWorld)BugWorld.Clone() })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    BugWorld = dialog.World;
                    SaveReminder = false;
                }
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveReminder && CancelOnClose()) return;
                EditWorld();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void RandomizeWorld()
        {
            BugWorld.Randomize(Mersenne);
            bugWorldImage.RefreshImage();
            SaveReminder = false;
        }

        private void RandomizeMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveReminder && CancelOnClose()) return;
                RandomizeWorld();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private async Task LearnAsync()
        {
            SaveReminder = true;
            await RunAsync((settings, reporter, tokenSource) =>
            {
                reporter?.WriteStart($"Learning...");
                Stopwatch timer = new Stopwatch();
                timer.Start();
                BugWorld.Learn(settings, reporter, tokenSource);
                reporter?.WriteEnd($"Learning ended after {timer.Elapsed.TotalSeconds} s.");
            });
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

        private async Task PlayAsync()
        {
            updateTimer.Enabled = true;
            await RunAsync((settings, reporter, tokenSource) =>
            {
                reporter?.WriteStart($"Running...");
                Stopwatch timer = new Stopwatch();
                timer.Start();
                BugWorld.Run(settings, reporter, tokenSource);
                reporter?.WriteEnd($"Run ended after {timer.Elapsed.TotalSeconds} s.");
            });
            updateTimer.Enabled = false;

            SetProgress(0);
            SetStatusText(string.Empty);
        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            try
            {
                await PlayAsync();
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
                using (SettingsDialog dialog = new SettingsDialog() { Settings = (WorldSettings)Settings.Clone() })
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                        Settings = dialog.Settings as WorldSettings;
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


        private async Task RunAsync(Action<WorldSettings, ProgressReporter, CancellationTokenSource> action)
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

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            bugWorldImage.RefreshImage();
        }

        #endregion
    }
}
