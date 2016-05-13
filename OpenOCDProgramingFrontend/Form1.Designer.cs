namespace OpenOCDProgramingFrontend
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmbDrive = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEnableBoolLoader = new System.Windows.Forms.CheckBox();
            this.txtCmdEraseBoot = new System.Windows.Forms.TextBox();
            this.labOpenOCDRemoteName = new System.Windows.Forms.Label();
            this.picConfig = new System.Windows.Forms.PictureBox();
            this.picDownloadInProgress = new System.Windows.Forms.PictureBox();
            this.txtOpenOCDAddr = new System.Windows.Forms.TextBox();
            this.labOutPut = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtGDBOutput = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtConfig = new System.Windows.Forms.TextBox();
            this.btnDefConfig = new System.Windows.Forms.Button();
            this.labConfigFile = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadInProgress)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDrive
            // 
            this.cmbDrive.DisplayMember = "1";
            this.cmbDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrive.FormattingEnabled = true;
            this.cmbDrive.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cmbDrive.Location = new System.Drawing.Point(25, 27);
            this.cmbDrive.Name = "cmbDrive";
            this.cmbDrive.Size = new System.Drawing.Size(80, 21);
            this.cmbDrive.TabIndex = 0;
            this.cmbDrive.ValueMember = "0";
            this.cmbDrive.SelectedIndexChanged += new System.EventHandler(this.cmbDrive_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Programing drive";
            // 
            // chkEnableBoolLoader
            // 
            this.chkEnableBoolLoader.AutoSize = true;
            this.chkEnableBoolLoader.Location = new System.Drawing.Point(12, 106);
            this.chkEnableBoolLoader.Name = "chkEnableBoolLoader";
            this.chkEnableBoolLoader.Size = new System.Drawing.Size(159, 17);
            this.chkEnableBoolLoader.TabIndex = 5;
            this.chkEnableBoolLoader.Text = "NRF51 Include Boot Loader";
            this.chkEnableBoolLoader.UseVisualStyleBackColor = true;
            this.chkEnableBoolLoader.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtCmdEraseBoot
            // 
            this.txtCmdEraseBoot.Location = new System.Drawing.Point(25, 122);
            this.txtCmdEraseBoot.Name = "txtCmdEraseBoot";
            this.txtCmdEraseBoot.Size = new System.Drawing.Size(210, 20);
            this.txtCmdEraseBoot.TabIndex = 6;
            this.txtCmdEraseBoot.TextChanged += new System.EventHandler(this.txtCmdEraseBoot_TextChanged);
            // 
            // labOpenOCDRemoteName
            // 
            this.labOpenOCDRemoteName.AutoSize = true;
            this.labOpenOCDRemoteName.Location = new System.Drawing.Point(12, 62);
            this.labOpenOCDRemoteName.Name = "labOpenOCDRemoteName";
            this.labOpenOCDRemoteName.Size = new System.Drawing.Size(136, 13);
            this.labOpenOCDRemoteName.TabIndex = 9;
            this.labOpenOCDRemoteName.Text = "OpenOCD Server IP/Name";
            // 
            // picConfig
            // 
            this.picConfig.Image = global::OpenOCDProgramingFrontend.Properties.Resources.settings;
            this.picConfig.Location = new System.Drawing.Point(172, 0);
            this.picConfig.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picConfig.Name = "picConfig";
            this.picConfig.Size = new System.Drawing.Size(66, 63);
            this.picConfig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picConfig.TabIndex = 14;
            this.picConfig.TabStop = false;
            this.picConfig.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // picDownloadInProgress
            // 
            this.picDownloadInProgress.Image = global::OpenOCDProgramingFrontend.Properties.Resources.loading42;
            this.picDownloadInProgress.Location = new System.Drawing.Point(130, 78);
            this.picDownloadInProgress.Name = "picDownloadInProgress";
            this.picDownloadInProgress.Size = new System.Drawing.Size(108, 28);
            this.picDownloadInProgress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDownloadInProgress.TabIndex = 7;
            this.picDownloadInProgress.TabStop = false;
            this.picDownloadInProgress.Visible = false;
            // 
            // txtOpenOCDAddr
            // 
            this.txtOpenOCDAddr.Location = new System.Drawing.Point(25, 78);
            this.txtOpenOCDAddr.Name = "txtOpenOCDAddr";
            this.txtOpenOCDAddr.Size = new System.Drawing.Size(100, 20);
            this.txtOpenOCDAddr.TabIndex = 8;
            this.txtOpenOCDAddr.Text = "0.0.0.0";
            this.txtOpenOCDAddr.TextChanged += new System.EventHandler(this.IpAddrBox_TextChanged);
            // 
            // labOutPut
            // 
            this.labOutPut.AutoSize = true;
            this.labOutPut.Location = new System.Drawing.Point(253, 7);
            this.labOutPut.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labOutPut.Name = "labOutPut";
            this.labOutPut.Size = new System.Drawing.Size(86, 13);
            this.labOutPut.TabIndex = 15;
            this.labOutPut.Text = "output from DBG";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(255, 81);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 25);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save Config File";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 144);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(562, 219);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtGDBOutput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(554, 193);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DBG Output";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtGDBOutput
            // 
            this.txtGDBOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGDBOutput.Location = new System.Drawing.Point(2, 2);
            this.txtGDBOutput.Multiline = true;
            this.txtGDBOutput.Name = "txtGDBOutput";
            this.txtGDBOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGDBOutput.Size = new System.Drawing.Size(550, 189);
            this.txtGDBOutput.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtConfig);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(554, 193);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Config File";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtConfig
            // 
            this.txtConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConfig.Location = new System.Drawing.Point(2, 2);
            this.txtConfig.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtConfig.Multiline = true;
            this.txtConfig.Name = "txtConfig";
            this.txtConfig.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConfig.Size = new System.Drawing.Size(550, 189);
            this.txtConfig.TabIndex = 18;
            // 
            // btnDefConfig
            // 
            this.btnDefConfig.Location = new System.Drawing.Point(255, 111);
            this.btnDefConfig.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDefConfig.Name = "btnDefConfig";
            this.btnDefConfig.Size = new System.Drawing.Size(77, 24);
            this.btnDefConfig.TabIndex = 19;
            this.btnDefConfig.Text = "Default Config ";
            this.btnDefConfig.UseVisualStyleBackColor = true;
            this.btnDefConfig.Click += new System.EventHandler(this.btnDefConfig_Click);
            // 
            // labConfigFile
            // 
            this.labConfigFile.AutoSize = true;
            this.labConfigFile.Location = new System.Drawing.Point(253, 27);
            this.labConfigFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labConfigFile.Name = "labConfigFile";
            this.labConfigFile.Size = new System.Drawing.Size(36, 13);
            this.labConfigFile.TabIndex = 20;
            this.labConfigFile.Text = "config";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 362);
            this.Controls.Add(this.labConfigFile);
            this.Controls.Add(this.btnDefConfig);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labOutPut);
            this.Controls.Add(this.picConfig);
            this.Controls.Add(this.labOpenOCDRemoteName);
            this.Controls.Add(this.txtOpenOCDAddr);
            this.Controls.Add(this.picDownloadInProgress);
            this.Controls.Add(this.txtCmdEraseBoot);
            this.Controls.Add(this.chkEnableBoolLoader);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDrive);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "OpenOCD mbed™ Frontend";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.picConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadInProgress)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cmbDrive;
        private System.Windows.Forms.CheckBox chkEnableBoolLoader;
        private System.Windows.Forms.TextBox txtCmdEraseBoot;
        private System.Windows.Forms.PictureBox picDownloadInProgress;
        private System.Windows.Forms.TextBox txtOpenOCDAddr;
        private System.Windows.Forms.Label labOpenOCDRemoteName;
        private System.Windows.Forms.PictureBox picConfig;
        private System.Windows.Forms.Label labOutPut;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtGDBOutput;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtConfig;
        private System.Windows.Forms.Button btnDefConfig;
        private System.Windows.Forms.Label labConfigFile;
    }
}

