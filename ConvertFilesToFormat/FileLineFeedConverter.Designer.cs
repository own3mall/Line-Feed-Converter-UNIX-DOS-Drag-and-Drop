namespace ConvertFilesToFormat
{
    partial class FileLineFeedConverter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.ddPanel = new System.Windows.Forms.Panel();
            this.ddLabel = new System.Windows.Forms.Label();
            this.ddPic = new System.Windows.Forms.PictureBox();
            this.rtbFilesToProcess = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.actionDL = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.consoleTB = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFolderToConvertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.copyToLogButton = new System.Windows.Forms.Button();
            this.bkFilesCB = new System.Windows.Forms.CheckBox();
            this.folderPathToBackupTB = new System.Windows.Forms.TextBox();
            this.browseBackupDirButton = new System.Windows.Forms.Button();
            this.folderBrowserDialogBKDir = new System.Windows.Forms.FolderBrowserDialog();
            this.extensionsTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numThreadsTB = new System.Windows.Forms.TextBox();
            this.mainTainStructureCB = new System.Windows.Forms.CheckBox();
            this.ddPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddPic)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(109, 484);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(320, 23);
            this.progressBar.TabIndex = 0;
            // 
            // ddPanel
            // 
            this.ddPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ddPanel.BackColor = System.Drawing.SystemColors.Window;
            this.ddPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ddPanel.Controls.Add(this.ddLabel);
            this.ddPanel.Controls.Add(this.ddPic);
            this.ddPanel.Controls.Add(this.rtbFilesToProcess);
            this.ddPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ddPanel.Location = new System.Drawing.Point(34, 342);
            this.ddPanel.Name = "ddPanel";
            this.ddPanel.Size = new System.Drawing.Size(395, 114);
            this.ddPanel.TabIndex = 1;
            // 
            // ddLabel
            // 
            this.ddLabel.AutoSize = true;
            this.ddLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddLabel.Location = new System.Drawing.Point(75, 72);
            this.ddLabel.Name = "ddLabel";
            this.ddLabel.Size = new System.Drawing.Size(239, 20);
            this.ddLabel.TabIndex = 3;
            this.ddLabel.Text = "Drag and Drop Files and Folders";
            // 
            // ddPic
            // 
            this.ddPic.BackColor = System.Drawing.Color.Transparent;
            this.ddPic.Image = global::ConvertFilesToFormat.Properties.Resources.ddPic;
            this.ddPic.InitialImage = global::ConvertFilesToFormat.Properties.Resources.ddPic;
            this.ddPic.Location = new System.Drawing.Point(171, 19);
            this.ddPic.Name = "ddPic";
            this.ddPic.Size = new System.Drawing.Size(50, 50);
            this.ddPic.TabIndex = 1;
            this.ddPic.TabStop = false;
            this.ddPic.Click += new System.EventHandler(this.ddPic_Click);
            // 
            // rtbFilesToProcess
            // 
            this.rtbFilesToProcess.BackColor = System.Drawing.SystemColors.Window;
            this.rtbFilesToProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rtbFilesToProcess.Location = new System.Drawing.Point(-1, -1);
            this.rtbFilesToProcess.Name = "rtbFilesToProcess";
            this.rtbFilesToProcess.ReadOnly = true;
            this.rtbFilesToProcess.Size = new System.Drawing.Size(395, 114);
            this.rtbFilesToProcess.TabIndex = 2;
            this.rtbFilesToProcess.TabStop = false;
            this.rtbFilesToProcess.Text = "";
            this.rtbFilesToProcess.Enter += new System.EventHandler(this.rtbFilesToProcess_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 484);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Progress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Files and Folders to Process";
            // 
            // actionDL
            // 
            this.actionDL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionDL.FormattingEnabled = true;
            this.actionDL.Items.AddRange(new object[] {
            "Unix LF Mode",
            "Windows CRLF Mode"});
            this.actionDL.Location = new System.Drawing.Point(243, 106);
            this.actionDL.Name = "actionDL";
            this.actionDL.Size = new System.Drawing.Size(186, 28);
            this.actionDL.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Convert Files and Folders to";
            // 
            // consoleTB
            // 
            this.consoleTB.Location = new System.Drawing.Point(35, 559);
            this.consoleTB.Name = "consoleTB";
            this.consoleTB.Size = new System.Drawing.Size(394, 58);
            this.consoleTB.TabIndex = 50;
            this.consoleTB.Text = "";
            this.consoleTB.TextChanged += new System.EventHandler(this.consoleTB_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 540);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Status";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(419, 31);
            this.label5.TabIndex = 8;
            this.label5.Text = "Line Feed Recursive Converter";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(111, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "To UNIX LF and Windows DOS formats";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(461, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFolderToConvertToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // selectFolderToConvertToolStripMenuItem
            // 
            this.selectFolderToConvertToolStripMenuItem.Name = "selectFolderToConvertToolStripMenuItem";
            this.selectFolderToConvertToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.selectFolderToConvertToolStripMenuItem.Text = "&Select Folder to Convert";
            this.selectFolderToConvertToolStripMenuItem.Click += new System.EventHandler(this.selectFolderToConvertToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // copyToLogButton
            // 
            this.copyToLogButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyToLogButton.Location = new System.Drawing.Point(354, 623);
            this.copyToLogButton.Name = "copyToLogButton";
            this.copyToLogButton.Size = new System.Drawing.Size(75, 28);
            this.copyToLogButton.TabIndex = 51;
            this.copyToLogButton.Text = "Copy &Log";
            this.copyToLogButton.UseVisualStyleBackColor = true;
            this.copyToLogButton.Click += new System.EventHandler(this.CopyLogToClipboard_Click);
            // 
            // bkFilesCB
            // 
            this.bkFilesCB.AutoSize = true;
            this.bkFilesCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bkFilesCB.Location = new System.Drawing.Point(34, 222);
            this.bkFilesCB.Name = "bkFilesCB";
            this.bkFilesCB.Size = new System.Drawing.Size(105, 20);
            this.bkFilesCB.TabIndex = 6;
            this.bkFilesCB.Text = "Backup Files";
            this.bkFilesCB.UseVisualStyleBackColor = true;
            this.bkFilesCB.CheckedChanged += new System.EventHandler(this.bkFilesCB_CheckedChanged);
            // 
            // folderPathToBackupTB
            // 
            this.folderPathToBackupTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderPathToBackupTB.Location = new System.Drawing.Point(138, 219);
            this.folderPathToBackupTB.Name = "folderPathToBackupTB";
            this.folderPathToBackupTB.ReadOnly = true;
            this.folderPathToBackupTB.Size = new System.Drawing.Size(199, 26);
            this.folderPathToBackupTB.TabIndex = 15;
            this.folderPathToBackupTB.TabStop = false;
            this.folderPathToBackupTB.Visible = false;
            // 
            // browseBackupDirButton
            // 
            this.browseBackupDirButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseBackupDirButton.Location = new System.Drawing.Point(338, 218);
            this.browseBackupDirButton.Name = "browseBackupDirButton";
            this.browseBackupDirButton.Size = new System.Drawing.Size(91, 28);
            this.browseBackupDirButton.TabIndex = 7;
            this.browseBackupDirButton.Text = "&Browse...";
            this.browseBackupDirButton.UseVisualStyleBackColor = true;
            this.browseBackupDirButton.Visible = false;
            this.browseBackupDirButton.Click += new System.EventHandler(this.browseBackupDirButton_Click);
            // 
            // extensionsTB
            // 
            this.extensionsTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extensionsTB.Location = new System.Drawing.Point(206, 152);
            this.extensionsTB.Name = "extensionsTB";
            this.extensionsTB.Size = new System.Drawing.Size(223, 26);
            this.extensionsTB.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(30, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "Extensions to Process";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(226, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(203, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "(Separate Multiple Extensions w/ Comma)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(315, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Example: php,txt,js,css";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(33, 277);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(284, 16);
            this.label10.TabIndex = 52;
            this.label10.Text = "Number of Threads to Use (Advanced Option):";
            // 
            // numThreadsTB
            // 
            this.numThreadsTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numThreadsTB.Location = new System.Drawing.Point(338, 273);
            this.numThreadsTB.Name = "numThreadsTB";
            this.numThreadsTB.Size = new System.Drawing.Size(91, 26);
            this.numThreadsTB.TabIndex = 9;
            this.numThreadsTB.Text = "10";
            this.numThreadsTB.TextChanged += new System.EventHandler(this.numThreadsTB_TextChanged);
            // 
            // mainTainStructureCB
            // 
            this.mainTainStructureCB.AutoSize = true;
            this.mainTainStructureCB.Checked = true;
            this.mainTainStructureCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mainTainStructureCB.Enabled = false;
            this.mainTainStructureCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTainStructureCB.Location = new System.Drawing.Point(34, 248);
            this.mainTainStructureCB.Name = "mainTainStructureCB";
            this.mainTainStructureCB.Size = new System.Drawing.Size(285, 20);
            this.mainTainStructureCB.TabIndex = 8;
            this.mainTainStructureCB.Text = "Maintain Original Folder Structure in Backup";
            this.mainTainStructureCB.UseVisualStyleBackColor = true;
            // 
            // FileLineFeedConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(461, 666);
            this.Controls.Add(this.mainTainStructureCB);
            this.Controls.Add(this.numThreadsTB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.extensionsTB);
            this.Controls.Add(this.browseBackupDirButton);
            this.Controls.Add(this.folderPathToBackupTB);
            this.Controls.Add(this.bkFilesCB);
            this.Controls.Add(this.copyToLogButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ddPanel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.consoleTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.actionDL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.menuStrip1);
            this.Icon = global::ConvertFilesToFormat.Properties.Resources.text_file;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(477, 700);
            this.Name = "FileLineFeedConverter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Line Feed (Newlines) Recursive Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileLineFeedConverter_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ddPanel.ResumeLayout(false);
            this.ddPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddPic)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel ddPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox actionDL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox consoleTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox ddPic;
        private System.Windows.Forms.RichTextBox rtbFilesToProcess;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Label ddLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem selectFolderToConvertToolStripMenuItem;
        private System.Windows.Forms.Button copyToLogButton;
        private System.Windows.Forms.CheckBox bkFilesCB;
        private System.Windows.Forms.TextBox folderPathToBackupTB;
        private System.Windows.Forms.Button browseBackupDirButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogBKDir;
        private System.Windows.Forms.TextBox extensionsTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox numThreadsTB;
        private System.Windows.Forms.CheckBox mainTainStructureCB;
    }
}

