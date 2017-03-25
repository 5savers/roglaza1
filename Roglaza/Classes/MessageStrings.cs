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
            string m=" if you want to re-activate it "+Environment.NewLine+"Please Delete the kill file at " +Environment.NewLine;
            string s =  RoglazaInstaller.getKillFilePath();
           
            s = s.Replace("//", "\\");
             s=s.Replace("\\Temp\\\\..","");
             return m+System.IO.Path.GetFullPath(s);
        }
        public static string GetTurned_off_reactivate_message_caption = "Program is turned off ";

        public static string UnistallationMessage ="Program will stop working Next restart";
        public static string UnistallationMessage_Caption = "Dead Time";


        public static string DoYouWantToStopTheApplication = "Do you want to stop the application";
        public static string KillingError = "Killing Error";

        public static string ProgramWillContinueWorking = "Program Will continue Working";
        public static string Thanks = "Thanks";

        public static string ImHidden = "I'M Hidden , Do you see me ?";

        public static List<string> PornMatches = new List<string>() {"hornyx","4free","4u","accutane","actos","acyclovir","adderall","adipex","allegra","Alprazolam","altace","ambien","Amoxicillin","amoxil","amphetamine","anal","anime","antibiotic","arousal","atfreeforum.com","ativan","attorney","augmentin","Azithromycin","babe","baccarat","bdsm","benadryl","biaxin","bitch","blackjack","blowjob","bondage","boob","boobs","booty","bowflex","bulabital","bupropion","butalbital","camry","car","carisoprodol","cartier","casino","celebrex","celexa","chick","cialis","cipro","citalopram","claritin","clonazepam","cock","codeine","codine","Crestor","crotch","cruise","cruises","cum","cumshot","cumshots","cunt","cyclen","cyclobenzaprine","cymbalta","dada","diazepam","dick","didrex","diovan","directbookmarks.info","dodge","doxycycline","drugstores","edvttj","Effexor","elavil","ephedra","ephedrine","erotica","escort","estate","facial","famvir","finland","Fioricet","forex","freewebs","fuck","gambling","gay","glucophage","gucci","helsinki","hentai","holdem","honda","hoodia","horny","hummer","hydrochlorothiazide","hydrocodone","incest","indianapolis","jaguar","jewelry","Lamictal","lasix","lesbian","lesbians","levaquin","levitra","Lexapro","lexus","lipitor","loan","lopressor","lorazepam","masterbating","mazda","medication","meridia","metalica","mevacor","Minolche","myfreedir.info","mysex","MILF","Murder","necklace","Nexium","Nicotine","nissan","norvasc","nude","orgasm","orgy","Oxycodone","Oxycontin","p0tassium","panties","panty","paxil","penis","percocet","pharmacy","phentermine","phpbb","plavix","poker","Potassium","pravachol","prednisone","prevacid","prilosec","propecia","Protonix","prozac","pussy","rape","refinance","ringtones","ritalin","rolex","roulette","seroquel","sex","sexy","shemale","silveno","slot","soma","sphost","swinger","tadalafil","tadalis","tawnee","teen","testosterone","tetracycline","tissot","tits","tity","titfuck","toon","toyota","Tramadol","Trazodone","twinks","ultracet","ultram","valerian","Valium","viagra","Vicodin","vioxx","Wellbutrin","wholesale","Xanax","xenical","xxx","zanaflex","Zenegra","zithromax","zocor","Zoloft","zolus","zovirax","Zyprexa","9maza.com","allsexvideos.tumblr.com","disneypornfakes.com","freetamilsexvideos.net","fuckbook.nl","promeporn.com","repo-nl.mobi.xtub.mobi","sexwap.ws","xxxindiansex.net","sexdatingzone.nl","XVIDEOS","XXX","Nargis Fakhri","Priyanka Chopra","Deepika Padukone","Kareena Kapoor","Bipasha Basu","Katrina Kaif","Jacqueline Fernandez","Shruti K. Haasan","Ayesha Takia","Malaika Arora","Neha Sharma","Genelia D'Souza","Sunny Leone","Priya Rai","Huma Qureshi","Sonakshi Sinha","Sonam Kapoor","Aruna Shields","Anushka Sharma","Sherlyn Chopra","Ileana D'Cruz","Diana Penty","sexy","xnxx","Elham","el7ad","الحاد" , "سكس", "مقاطع ساخن", "سياسة" };
    }
}
