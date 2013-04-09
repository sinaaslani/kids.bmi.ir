using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;


namespace Kids.Utility
{
    public static class UtilityMethod
    {
        //public static string GetRequestValue(string parameterName)
        //{
        //    if (HttpContext.Current.Request[parameterName] != null)
        //        return HttpContext.Current.Request[parameterName];
        //    return "";
        //}
        public static string GetRequestParameter(string p_Request, string p_Default = null)
        {
            try
            {
                p_Request = HttpContext.Current.Request[p_Request];
                if (string.IsNullOrEmpty(p_Request))
                    return p_Default;
                return p_Request;
            }
            catch
            {
                return p_Default;
            }
        }
        public static string ListToCommaSepStr<T>(IEnumerable<T> TempList)
        {
            string Temp = "";
            foreach (T l in TempList)
                Temp += string.Format("{0},", l);

            return Temp.TrimEnd(",".ToCharArray());
        }

      

       

        

       

       
        

        public static string TransactionKeyGenerator()
        {
            string allowedChars = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            const string ex1 = @"^.*(?=.{10,})(?=.*\d)(?!.*[a-z])(?=.*[A-Z])(?!.*[@#$%^&+=IO]).*$";

            string passwordString;

            do
            {
                passwordString = "";

                Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    string temp = arr[rand.Next(0, arr.Length)];
                    passwordString += temp;
                }
            } while (!Regex.IsMatch(passwordString, ex1));
            return passwordString;
        }



      
        

        public static AssemblyName GetAssemblyVersion(string fileName)
        {
            try
            {
                return AssemblyName.GetAssemblyName(fileName);
            }
            catch 
            {
                return null;
            }

        }
        
        

        public static Dictionary<string, string> CommandLineParser(IEnumerable<string> Args)
        {
            Dictionary<string, string> Col = new Dictionary<string, string>();
            foreach (string arg in Args)
            {
                if (arg.Contains("="))
                {
                    string[] Values = arg.Split("=".ToCharArray());
                    Col.Add(Values[0].ToLower(), Values[1]);
                }
                else
                    Col.Add(arg.ToLower(), arg);
            }
            return Col;
        }

        public static void CopyFolder(string FromPath, string ToPath)
        {
            #region Copy if it is a Folder

            if (Directory.Exists(FromPath))
            {
                #region Copying Files

                string[] files = Directory.GetFiles(FromPath);
                foreach (string s in files)
                {
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(ToPath, fileName);
                    File.Copy(s, destFile, true);
                }

                #endregion

                #region Copying Folders and sub folders recursively

                string[] Dirs = Directory.GetDirectories(FromPath);

                foreach (string dir in Dirs)
                {
                    string FolderName = Path.GetFileName(dir);
                    string ToPathNew = Path.Combine(ToPath, FolderName);
                    if (!Directory.Exists(ToPathNew))
                        Directory.CreateDirectory(ToPathNew);

                    CopyFolder(dir, ToPathNew);
                }

                #endregion
            }
            #endregion

        }
    }

}
