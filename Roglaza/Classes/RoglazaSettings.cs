using Roglaza.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roglaza
{
   public enum GeneralErros 
   {
       Not_found 
   }
   public enum RoglazaSettingsParameters
   {
       password, GeneralSettingsFilePath, WaitInPasswordFailed
   }
   public class RoglazaSettings
    {
       public string LogsPath = RoglazaInstaller.GetRoglazaAPPDataPath()+"\\Logs";
       public string GeneralSettingsFilePath = "config.rog";
       public static string DefaultPassword = "weloveyou";    
       public string RoglazaIconPath = "";
       public string RoglazaName = "Roglaza";
       public string KeyLoggerStorePath = "kslogs.mp3";
       public bool showDadMessage = false;
       public bool AllowPornoBlocker = true;
       public string DadMessage = "";

       public int WaitInPasswordFailed = 60;
       public int ScreenShotInterValMinutes = 5;

       public bool Loaded = false;
       public bool AllowKeyLogger = true;
       public bool AllowScreenShots = true;
       public bool AllowCamShots = true;
       public bool AllowBrowserHistory = true;
       public bool ShouldBewrittenToFile = true;
       public RoglazaSettings()
       {
         //  PornMatches = MessageStrings.StaticPornMatches;
           matches_lists = new List<string>();
           GeneralSettingsFilePath = RoglazaInstaller.GetRoglazaAPPDataPath() + "\\config.rog";
           LogsPath = RoglazaInstaller.GetRoglazaAPPDataPath() + "\\Logs";
           KeyLoggerStorePath = RoglazaInstaller.GetRoglazaAPPDataPath() + "\\kslogs.rog";
           ContentMatchesPath = RoglazaInstaller.GetRoglazaAPPDataPath() + "\\matches.rog";

           if (RoglazaHelper.IsExistedFile(ContentMatchesPath) == false)
               SetupContentMatchesFile();
         
       }

       private void SetupContentMatchesFile()
       {
           string x = "";
           matches_lists.Clear();
           foreach (string s in MessageStrings.PornMatches)
           {
               x += s + "\r\n";
               matches_lists.Add(s);
           }
           RoglazaHelper.FileWriteText(ContentMatchesPath, x);
       }

       public RoglazaSettings(bool p)
       {
           ShouldBewrittenToFile = p;
       }
       public List<string> matches_lists;
       public bool LoadFromFile()
       {
           //Load settings from file as text
           try
           {
               string data=  RoglazaHelper.ReadTextFile(Program.ProgramSettings.GeneralSettingsFilePath);
               if (data == GeneralErros.Not_found.ToString())
                   return false;
               else
               {
                  return TranslateSettings(data);              
               }
           }
           catch
           {
               return false;
           }
       }
       private bool TranslateSettings(string data)
       {
           // unserializing text settings
           if (data.Contains("\n"))
           {
               string[] lines = data.Split('\n');
               foreach (string l in lines)
               {
                   if (!l.Contains(":"))
                       continue;
                   string[] key_value = l.Split(':');
                   string key = key_value[0].Trim();
                   string value = key_value[1].Trim();
                   switch (key)
                   {
                           // boolean values
                       case "allowbrowserhistory":
                       case "AllowBrowserHistory": AllowBrowserHistory = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowcamshots":
                       case "AllowCamShots": AllowCamShots = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowkeylogger":
                       case "AllowKeyLogger": AllowKeyLogger = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowscreenshots":
                       case "AllowScreenShots": AllowScreenShots = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowpornoblocker":
                       case "AllowPornoBlocker": AllowPornoBlocker = RoglazaHelper.ReverseToBoolean(value); break;
                      
                       case "showdadmessage":
                       case "ShowDadMessage":
                       case "showDadMessage": showDadMessage = RoglazaHelper.ReverseToBoolean(value); break;
                       case "dadmessage":
                       case "DadMessage": DadMessage = Decode(value); break;


                           //strings
                       case "ContentMatchesPath":
                       case "contentmatchespath": ContentMatchesPath = System.IO.Path.GetFullPath(Decode(value)); LoadContentMatches(); break;
                       case "LogsPath":
                       case "logspath": LogsPath = System.IO.Path.GetFullPath(Decode(value)); break;
                       case "KeyLoggerPath":
                       case "KeyLoggerStore":
                       case "KeyLoggerStorePath":
                       case "kslogs":
                       case "kslogspath":
                       case "keyloggerstorepath": KeyLoggerStorePath = System.IO.Path.GetFullPath(Decode(value)); InitKeylogger(); break;
                       case "RoglazaName":
                       case "roglazaname": RoglazaName = Decode(value); break;
                       case "RoglazaIconPath":
                       case "roglazaiconpath": RoglazaIconPath = Decode(value); break;
                       case "generalsettingsfilepath":
                       case "GeneralSettingsFilePath": GeneralSettingsFilePath = System.IO.Path.GetFullPath(Decode(value)); break;
                       case "password":
                       case "key":
                       case "access":
                       case "credit":
                           {
                               if (value == "") 
                                   value = Crypter.GetMd5Hash(DefaultPassword);
                               else
                                   PasswordHash = RoglazaHelper.ReverseText(value);
                               break;
                           }
                           //int values
                       case "WaitInPasswordFailed":
                       case "waitinpasswordfailed": WaitInPasswordFailed = RoglazaHelper.TextToInt(value, 60); break;
                       case "ScreenShotInterValMinutes":
                       case "screenshotIntervalminutes": ScreenShotInterValMinutes = RoglazaHelper.TextToInt(value, 5); break;
                   }

               }

               return true;
           }
           else return false;
       }

       private void InitKeylogger()
       {
           string olddata = RoglazaHelper.ReadTextFile(KeyLoggerStorePath);
           olddata += "\r\r\r\r" + DateTime.Now.ToShortDateString() + "\r\n-----------------------\r\n";
       }

       private void LoadContentMatches()
       {
           string[] lines = RoglazaHelper.ReadFileLines(ContentMatchesPath);
           if (lines == null)
               return;
           if (lines.Length < 1)
               return;

           Program.ProgramSettings.matches_lists.Clear();
           foreach (string s in lines)
           {
               Program.ProgramSettings.matches_lists.Add(s.ToLower());
           }

       }
       public bool SaveSettings()
       {
           if (ShouldBewrittenToFile == false)
               return false;
           string revHash = RoglazaHelper.ReverseText(PasswordHash);
           string data = "";
           data += "ContentMatchesPath:" + Encode(ContentMatchesPath) + "\r\n";
           data += "GeneralSettingsFilePath:" + Encode(GeneralSettingsFilePath)+"\r\n";
           data += "DadMessage:" + Encode(DadMessage) + "\r\n";
           data += "key:" + revHash + "\r\n";
           data += "WaitInPasswordFailed:" + WaitInPasswordFailed.ToString() + "\r\n";
           data += "ScreenShotInterValMinutes:" + ScreenShotInterValMinutes.ToString()+"\n\r";
           data += "AllowBrowserHistory:" + AllowBrowserHistory + "\r\n";
           data += "AllowCamShots:" + AllowCamShots + "\r\n";
           data += "AllowKeyLogger:" + AllowKeyLogger + "\r\n";
           data += "AllowPornoBlocker:" + showDadMessage + "\r\n";
           data += "AllowPornoBlocker:" + AllowPornoBlocker + "\r\n";
           data += "AllowScreenShots:" + AllowScreenShots + "\r\n";
           data += "RoglazaIconPath:" + Encode(RoglazaIconPath)+"\r\n";
           data += "RoglazaName:" + RoglazaName + "\r\n";
           data += "LogsPath:" + Encode(LogsPath)+"\r\n";
           data += "KeyLoggerStorePath:" + Encode(KeyLoggerStorePath) + "\r\n";
           data += "DadMessage:" + Encode(DadMessage) + "\r\n";
           data += "showDadMessage:" + showDadMessage + "\r\n";



           
           return RoglazaHelper.FileWriteText(GeneralSettingsFilePath, data);         
            
       }

       private  static string Encode(string s)
       {
           return s.Replace(":", "$COLON$");
       }
       private static string Decode(string s)
       {
           return s.Replace("$COLON$",":");
       }
       public string PasswordHash =Crypter.GetMd5Hash(DefaultPassword);
       public static string ConvertTextPasswordToRevHash(string s)
       {
           string hash = Crypter.GetMd5Hash(s);
           hash = RoglazaHelper.ReverseText(hash);
           return hash;
       }
       internal void SETScreenShotInterValMinutes(int p)
       {
           this.ScreenShotInterValMinutes = p;
           SaveSettings();
       }



       public string ContentMatchesPath { get; set; }

       internal void saveMatches(System.Windows.Forms.ListBox.ObjectCollection objectCollection)
       {
           if (objectCollection.Count < 1)
               return;

           string z = "";
           foreach (string s in objectCollection)
           {
               z += s + "\r\n";
           }
           if (RoglazaHelper.FileWriteText(ContentMatchesPath, z))
               System.Windows.Forms.MessageBox.Show("Success");
           
       }

      //public  List<string> PornMatches = new List<string>();
    }
}
