using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roglaza.Classes
{
    public class MessageStrings
    {
        public static string Warning = "warning !";

        public static string GetTurned_off_reactivate_message()
        {
            string s = " if you want to re-activate it "+Environment.NewLine+"Please Delete the kill file at " +Environment.NewLine+ RoglazaHelper.getKillFilePath();
            s = s.Replace("//", "\\");
            return s.Replace("\\Temp\\\\..","");
        }
        public static string GetTurned_off_reactivate_message_caption = "Program is turned off ";

        public static string UnistallationMessage ="Program will stop working Next restart";
        public static string UnistallationMessage_Caption = "Dead Time";


        public static string DoYouWantToStopTheApplication = "Do you want to stop the application";
        public static string KillingError = "Killing Error";

        public static string ProgramWillContinueWorking = "Program Will continue Working";
        public static string Thanks = "Thanks";

        public static string ImHidden = "I'M Hidden , Do you see me ?";
    }
}
