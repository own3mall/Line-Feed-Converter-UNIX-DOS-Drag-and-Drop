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

namespace ConvertFilesToFormat
{
    public partial class FileLineFeedConverter : Form
    {

        #region GlobalVars
        // Global vars
        List<string> filesToProcess = new List<string>();
        int count = 0;
        string settingsFile = AppDomain.CurrentDomain.BaseDirectory + "\\user_settings";
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
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsFile))
            {
                FileConversionArgs args = GenericHelper.ReadFromBinaryFile<FileConversionArgs>(settingsFile);
                if (args.BackupFiles)
                {
                    bkFilesCB.Checked = true;
                }

                if (!string.IsNullOrEmpty(args.BackupPath))
                {
                    folderPathToBackupTB.Text = args.BackupPath;
                }

                if (args.LFMode >= 0)
                {
                    actionDL.SelectedIndex = args.LFMode;
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
                if (!backgroundWorker.IsBusy)
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
                    resetDragView();
                }
            }
            else
            {
                resetDragView();
            }
        }
        #endregion

        #region UI Manipulation

        private void ddPic_Click(object sender, EventArgs e)
        {
            showFolderPicker();
        }

        private void showFolderPicker()
        {
            if (inputsValid())
            {
                if (!backgroundWorker.IsBusy)
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
            rtbFilesToProcess.Text = "";
            ddLabel.Show();
            ddPic.Show();
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
            }
            else
            {
                folderPathToBackupTB.Visible = false;
                browseBackupDirButton.Visible = false;
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

        #endregion

        #region Logic

        private void clearFilesTextAndResetCounter()
        {
            count = 0;
            filesToProcess = new List<string>();
            rtbFilesToProcess.Text = "";
        }

        private void processFoldersAndFiles(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                FileAttributes attr = File.GetAttributes(file);
                bool isFolder = (attr & FileAttributes.Directory) == FileAttributes.Directory;
                if (isFolder)
                {
                    addFolderToRTB(file);
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

                FileConversionArgs args = new FileConversionArgs { LFMode = actionDL.SelectedIndex, BackupPath = folderPathToBackupTB.Text, BackupFiles = bkFilesCB.Checked };

                //Console.WriteLine("---------------------------------------------");
                Console.WriteLine(date + " - Running conversion " + action + " on " + filesToProcess.Count.ToString() + " " + filesStr + ".");
                backgroundWorker.RunWorkerAsync(args);
            }
            else
            {

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
            FileConversionArgs args = new FileConversionArgs { LFMode = actionDL.SelectedIndex, BackupPath = folderPathToBackupTB.Text, BackupFiles = bkFilesCB.Checked };
            GenericHelper.WriteToBinaryFile(AppDomain.CurrentDomain.BaseDirectory + "\\user_settings", args);
        }

        #endregion

        #region Threading

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var options = (FileConversionArgs)e.Argument;
            options.FilesToProcess = filesToProcess;
            if (filesToProcess.Any())
            {
                TextFileConversionUtilities tc = new TextFileConversionUtilities(ref backgroundWorker, options);
            }
            Console.WriteLine("\nOperations completed.\n");
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            if (e.ProgressPercentage >= 100)
            {
                resetDragView();
            }
        }

        #endregion
    }
}
