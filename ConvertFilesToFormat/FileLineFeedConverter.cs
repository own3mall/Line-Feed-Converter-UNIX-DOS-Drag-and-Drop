using ConvertFilesToFormat.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ConvertFilesToFormat.Extensions;

namespace ConvertFilesToFormat
{
    public partial class FileLineFeedConverter : Form
    {

        #region GlobalVars
        // Global vars
        List<string> filesToProcess = new List<string>();
        string settingsFile = AppDomain.CurrentDomain.BaseDirectory + "\\user_settings";
        List<BackgroundWorker> ThreadedWorkers = new List<BackgroundWorker>();
        List<string> FolderParents = new List<string>();
        public double maxThreads = 10;
        public double totalFilesProcessed = 0;
        public double totalFilesToProcess = 0;
        bool completedMessageShown = false;
        DateTime operationsStarted = new DateTime();
        DateTime operationsEnded = new DateTime();
        bool mainTainStructureOnBackup = true;
        #endregion

        #region Init
        public FileLineFeedConverter()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Console.SetOut(new ControlWriter(consoleTB));

            rtbFilesToProcess.AllowDrop = true;
            rtbFilesToProcess.DragDrop += handleFileFolderDragDrop_DragDrop;
            rtbFilesToProcess.DragEnter += handleFileFolderDragDrop_DragEnter;
            rtbFilesToProcess.DragLeave += handleFileFolderDragDrop_DragLeave; 

            ddPic.Parent = rtbFilesToProcess;
            actionDL.SelectedIndex = 0;

            // Set background worker
            backgroundWorker.DoWork += backgroundWorker_DoWork;

            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsFile))
            {
                bool placeDefaultExts = true;
                FileConversionArgs args = GenericHelper.ReadFromBinaryFile<FileConversionArgs>(settingsFile);
                if (args.BackupFiles)
                {
                    bkFilesCB.Checked = true;
                }

                if (args.MaintainFolderStructureOnBackup == false)
                {
                    mainTainStructureCB.Checked = false;
                }

                if (args.NumberOfThreadsToUse > 0 && args.NumberOfThreadsToUse <= 64)
                {
                    numThreadsTB.Text = args.NumberOfThreadsToUse.ToString();
                }

                if (!string.IsNullOrEmpty(args.BackupPath))
                {
                    folderPathToBackupTB.Text = args.BackupPath;
                }

                if (args.LFMode >= 0)
                {
                    actionDL.SelectedIndex = args.LFMode;
                }

                if (args.FileExtensionsToProcess != null)
                {
                    if (args.FileExtensionsToProcess.Any())
                    {
                        for (int i = 0; i < args.FileExtensionsToProcess.Count; i++)
                        {
                            if (i == 0)
                            {
                                extensionsTB.Text += args.FileExtensionsToProcess[i];
                            }
                            else
                            {
                                extensionsTB.Text += "," + args.FileExtensionsToProcess[i];
                            }
                        }
                    }
                    else
                    {
                        placeDefaultExts = false;
                    }
                }

                // Set defaults
                if (string.IsNullOrEmpty(extensionsTB.Text) && placeDefaultExts)
                {
                    extensionsTB.Text = "php,js,css,htm,html,py,pl,txt";
                }
                else
                {
                    if (!string.IsNullOrEmpty(extensionsTB.Text))
                    {
                        extensionsTB.Text = extensionsTB.Text.Replace(".", ""); // Don't show the ugly period before extension
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region DragDrop Events
        private void handleFileFolderDragDrop_DragLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtbFilesToProcess.Text))
            {
                resetDragView();
            }
        }

        private void handleFileFolderDragDrop_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                if (Directory.Exists(path) || File.Exists(path))
                    effects = DragDropEffects.Copy;
            }

            e.Effect = effects;
            hideIndicator();
        }

        private void handleFileFolderDragDrop_DragDrop(object sender, DragEventArgs e)
        {
            if (inputsValid())
            {
                if (!AnyBackgroundWorkerBusy())
                {
                    clearFilesTextAndResetCounter();
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (string file in files)
                    {
                        processFoldersAndFiles(file);
                    }

                    HandleConversion();
                }
                else
                {
                    showPleaseWaitForThreadingMessage();
                    // resetDragView(); // NO, why would you reset it here?
                }
            }
            else
            {
                resetDragView();
            }
        }
        #endregion

        #region UI Manipulation

        private void numThreadsTB_TextChanged(object sender, EventArgs e)
        {
            int n;
            string error = "";
            bool isNumeric = int.TryParse(numThreadsTB.Text, out n);
            if (!isNumeric)
            {
                numThreadsTB.Text = "10";
                error += "You must enter a numeric value!\n";
            }
            else
            {
                if (n <= 0 || n > 20)
                {
                    error += "Please enter a number between 1 and 20. These are software threads, not hardware, so don't go off of how many threads your CPU has.\n";
                    numThreadsTB.Text = "10";
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void updateProgressbar(int percent)
        {
            this.InvokeEx(f => f.progressBar.Value = percent);

            if (percent >= 100 && !completedMessageShown)
            {
                // Set our variables
                completedMessageShown = true;
                DateTime today = DateTime.Now;
                string date = today.ToString("M/dd/yyyy h:mm:ss tt");
                operationsEnded = DateTime.Now;

                // Write out status
                Console.WriteLine("Processed " + totalFilesProcessed.ToString() + " file(s) based on extensions and binary content filters.");
                Console.WriteLine("\nOperations completed on " + date + ".  Total time spent on file conversion: " + (operationsEnded - operationsStarted).TotalSeconds + " seconds\n");

                // Reset UI
                resetDragView();
            }
        }

        private void ddPic_Click(object sender, EventArgs e)
        {
            showFolderPicker();
        }

        private void showFolderPicker()
        {
            if (inputsValid())
            {
                if (!AnyBackgroundWorkerBusy())
                {
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = folderBrowserDialog.SelectedPath;
                        if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                        {
                            hideIndicator();
                            clearFilesTextAndResetCounter();
                            processFoldersAndFiles(path);
                            HandleConversion();
                        }
                        else
                        {
                            resetDragView();
                        }
                    }
                    else
                    {
                        resetDragView();
                    }
                }
                else
                {
                    showPleaseWaitForThreadingMessage();
                    resetDragView();
                }
            }
            else
            {
                resetDragView();
            }
        }

        private void hideIndicator()
        {
            ddPic.Hide();
            ddLabel.Hide();
            progressBar.Value = 0;
        }

        private void showPleaseWaitForThreadingMessage()
        {
            MessageBox.Show("Please wait for the current operations to finish!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void rtbFilesToProcess_Enter(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void resetDragView()
        {
            this.InvokeEx(f => f.rtbFilesToProcess.Text = "");
            this.InvokeEx(f => f.ddLabel.Show());
            this.InvokeEx(f => f.ddPic.Show());
        }

        private void consoleTB_TextChanged(object sender, EventArgs e)
        {
            consoleTB.SelectionStart = consoleTB.Text.Length;
            consoleTB.ScrollToCaret();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addFolderToRTB(string file)
        {
            rtbFilesToProcess.Text += "Processing folder " + file + "\r\n";
        }

        private void addFileToRTB(string file)
        {
            rtbFilesToProcess.Text += "Processing file " + file + "\r\n";
        }
 
        private void selectFolderToConvertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showFolderPicker();
        }

        private void CopyLogToClipboard_Click(object sender, EventArgs e)
        {
            string text = consoleTB.Text;
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("\n", "\r\n");
                Clipboard.SetText(text);
                MessageBox.Show("Copied log successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bkFilesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (bkFilesCB.Checked)
            {
                folderPathToBackupTB.Visible = true;
                browseBackupDirButton.Visible = true;
                mainTainStructureCB.Enabled = true;
            }
            else
            {
                folderPathToBackupTB.Visible = false;
                browseBackupDirButton.Visible = false;
                mainTainStructureCB.Enabled = false;
                mainTainStructureCB.Checked = true;
            }
        }

        private void browseBackupDirButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogBKDir.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialogBKDir.SelectedPath;
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    folderPathToBackupTB.Text = path;                    
                }
            }
        }

        private void FileLineFeedConverter_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveInputs();
        }

        private List<string> processExtensionsTB()
        {
            List<string> extensions = new List<string>();
            if (!string.IsNullOrEmpty(extensionsTB.Text))
            {
                string[] exts = extensionsTB.Text.Split(',');
                foreach (string ext in exts)
                {
                    if (ext.IndexOf('.') == -1)
                    {
                        extensions.Add("." + ext.Trim());
                    }
                    else
                    {
                        extensions.Add(ext.Trim());
                    }
                }
            }
            return extensions;
        }

        #endregion

        #region Logic

        private bool AnyBackgroundWorkerBusy()
        {
            if (!backgroundWorker.IsBusy && (!ThreadedWorkers.Any() || (ThreadedWorkers.Any() && !ThreadedWorkers.Any(c => c.IsBusy))))
            {
                return false;
            }

            return true;
        }

        private void clearFilesTextAndResetCounter()
        {
            filesToProcess = new List<string>();
            rtbFilesToProcess.Text = "";
            consoleTB.Text = ""; // Reset log text
            FolderParents = new List<string>();
        }

        private void processFoldersAndFiles(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                FileAttributes attr = File.GetAttributes(file);
                bool isFolder = (attr & FileAttributes.Directory) == FileAttributes.Directory;
                if (isFolder)
                {
                    // Add it to our list of parents
                    if (!FolderParents.Contains(file))
                    {
                        FolderParents.Add(file);
                    }

                    // Add it to the UI text box
                    addFolderToRTB(file);

                    // Scan the folder recursively and add all files to the list
                    List<string> filesInFolder = FileFolderHelper.DirSearch(file);
                    if (filesInFolder.Any())
                    {
                        List<string> filesNotInCollection = filesInFolder.Where(x => !filesToProcess.Any(y => y == x)).ToList();
                        if (filesNotInCollection.Any())
                        {
                            filesToProcess.AddRange(filesNotInCollection);
                        }
                    }
                }
                else
                {
                    if (!filesToProcess.Contains(file))
                    {
                        addFileToRTB(file);
                        filesToProcess.Add(file);
                    }
                }
            }
        }

        private void HandleConversion()
        {
            if (filesToProcess.Any())
            {
                DateTime today = DateTime.Now;
                string date = today.ToString("M/dd/yyyy h:mm:ss tt");
                string filesStr = "files";
                string action = "to Unix LF mode";
                if (filesToProcess.Count == 1)
                {
                    filesStr = "file";
                }

                if (actionDL.SelectedIndex == 1)
                {
                    action = "to Windows CRLF mode";
                }

                FileConversionArgs args = new FileConversionArgs { LFMode = actionDL.SelectedIndex, BackupPath = folderPathToBackupTB.Text, BackupFiles = bkFilesCB.Checked, FolderParents = FolderParents };
                args.FileExtensionsToProcess = processExtensionsTB();

                //Console.WriteLine("---------------------------------------------");
                Console.WriteLine(date + " - Running conversion " + action + " on " + filesToProcess.Count.ToString() + " " + filesStr + " before applying extension and content filters.");
                backgroundWorker.RunWorkerAsync(args);
            }
            else
            {
                resetDragView();
                MessageBox.Show("No files to process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool inputsValid()
        {
            bool valid = true;
            string message = "";
            if (bkFilesCB.Checked)
            {
                if (string.IsNullOrEmpty(folderPathToBackupTB.Text))
                {
                    message += "Please select a folder where a backup of each processed file will be created.";
                    valid = false;
                }
            }

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valid;
        }

        private void SaveInputs()
        {
            FileConversionArgs args = new FileConversionArgs { LFMode = actionDL.SelectedIndex, BackupPath = folderPathToBackupTB.Text, BackupFiles = bkFilesCB.Checked, NumberOfThreadsToUse = Convert.ToInt32(numThreadsTB.Text), MaintainFolderStructureOnBackup = mainTainStructureCB.Checked };
            args.FileExtensionsToProcess = processExtensionsTB();
            GenericHelper.WriteToBinaryFile(AppDomain.CurrentDomain.BaseDirectory + "\\user_settings", args);
        }

        #endregion

        #region Threading

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var options = (FileConversionArgs)e.Argument;
            maxThreads = Convert.ToInt32(numThreadsTB.Text);
            int filesToHandlePerThread = (int)Math.Ceiling(filesToProcess.Count / maxThreads);
            totalFilesToProcess = filesToProcess.Count;
            totalFilesProcessed = 0;
            ThreadedWorkers = new List<BackgroundWorker>();
            completedMessageShown = false;
            operationsStarted = DateTime.Now;
            operationsEnded = DateTime.Now;

            List<string> filesSplitUp = new List<string>();
            for (int i = 0; i < filesToProcess.Count; i++)
            {
                if (i != 0 && i % filesToHandlePerThread == 0)
                {
                    options.FilesToProcess = filesSplitUp;
                    addThreadedBackgroundWorker(new FileConversionArgs() { FilesToProcess = options.FilesToProcess, BackupFiles = options.BackupFiles, BackupPath = options.BackupPath, FileExtensionsToProcess = options.FileExtensionsToProcess, LFMode = options.LFMode, FolderParents = options.FolderParents, MaintainFolderStructureOnBackup = mainTainStructureCB.Checked});
                    filesSplitUp = new List<string>();
                }
                filesSplitUp.Add(filesToProcess[i]);
            }

            // Get the last batch
            if (filesSplitUp.Any())
            {
                options.FilesToProcess = filesSplitUp;
                addThreadedBackgroundWorker(new FileConversionArgs() { FilesToProcess = options.FilesToProcess, BackupFiles = options.BackupFiles, BackupPath = options.BackupPath, FileExtensionsToProcess = options.FileExtensionsToProcess, LFMode = options.LFMode, FolderParents = options.FolderParents, MaintainFolderStructureOnBackup = mainTainStructureCB.Checked});
                filesSplitUp = new List<string>();
            }            
        }

        private void addThreadedBackgroundWorker(FileConversionArgs args)
        {
            BackgroundWorker bc = new BackgroundWorker();
            bc.DoWork += threadedBackgroundWorker_DoWork;
            ThreadedWorkers.Add(bc);
            bc.RunWorkerAsync(args);
        }

        private void threadedBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var options = (FileConversionArgs)e.Argument;
            if (options.FilesToProcess.Any())
            {
                TextFileConversionUtilities tc = new TextFileConversionUtilities(this, ref backgroundWorker, options);
            }
        }

        #endregion
    }
}
