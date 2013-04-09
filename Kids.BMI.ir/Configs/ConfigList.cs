using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Kids.Utility;

namespace Kids.Common
{
    public static class SystemConfigs
    {
        
        public static int NewsResultNumber
        {
            get { return Config_DataProvider.GetCacheConfig("NewsResultNumber").ConfigValue.ToInt32(); }
        }
        
        public static string UrlNewsFilesPath
        {
            get { return Config_DataProvider.GetCacheConfig("UrlNewsFilesPath").ConfigValue; }
        }
        

        public static string KidsConnectionString
        {
            get
            {
                return CryptographyHelper.Decrypt(ConfigurationManager.ConnectionStrings["BMIKidsEntities"].ConnectionString);
            }
        }

        public static string SMTPServer
        {
            get { return Config_DataProvider.GetCacheConfig("SMTPServer").ConfigValue; }
        }

        public static object SMTPPort
        {
            get { return Config_DataProvider.GetCacheConfig("SMTPPort").ConfigValue; }
        }

        public static object SMTPIsSSL
        {
            get { return Config_DataProvider.GetCacheConfig("SMTPIsSSL").ConfigValue; }
        }

        public static string SMTPUserName
        {
            get { return Config_DataProvider.GetCacheConfig("SMTPUserName").ConfigValue; }
        }

        public static string SMTPPassword
        {
            get { return Config_DataProvider.GetCacheConfig("SMTPPassword").ConfigValue; }
        }

        public static string FromEmailAddress
        {
            get { return Config_DataProvider.GetCacheConfig("FromEmailAddress").ConfigValue; }
        }



        public static string SSOUserName
        {
            get { return Config_DataProvider.GetCacheConfig("SSOUserName").ConfigValue; }
        }

        public static string SSOWebSiteURL
        {
            get { return Config_DataProvider.GetCacheConfig("SSOWebSiteURL").ConfigValue; }
        }

        public static string SSOWebServiceURL
        {
            get { return Config_DataProvider.GetCacheConfig("SSOWebServiceURL").ConfigValue; }
        }

        public static string SSOPassword
        {
            get { return Config_DataProvider.GetCacheConfig("SSOPassword").ConfigValue; }
        }

        public static string UrlKidsPicFilesPath(string ssoUserName)
        {
            string path = Config_DataProvider.GetCacheConfig("UrlKidsPicFilesPath").ConfigValue + ssoUserName + "/";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            return path;
        }

      

        public static string Cnn_MelliCode
        {
            get
            {
                return CryptographyHelper.Decrypt(ConfigurationManager.ConnectionStrings["Cnn_MelliCode"].ConnectionString);
            }
        }

        public static string Cnn_TashilatSiba
        {
            get
            {
                return CryptographyHelper.Decrypt(ConfigurationManager.ConnectionStrings["Cnn_TashilatSiba"].ConnectionString);
            }
        }

        public static string Cnn_Central_Current
        {
            get
            {
                if (HttpContext.Current.Application["Cnn_Central_Current"] != null)
                    return HttpContext.Current.Application["Cnn_Central_Current"].ToString();
                return null;
            }
            set { HttpContext.Current.Application["Cnn_Central_Current"] = value; }
        }

        public static string Cnn_Central_Current1
        {
            get
            {
                return CryptographyHelper.Decrypt(ConfigurationManager.ConnectionStrings["Cnn_Central_Current1"].ConnectionString);
            }

        }
        public static string Cnn_Central_Current2
        {
            get
            {
                return CryptographyHelper.Decrypt(ConfigurationManager.ConnectionStrings["Cnn_Central_Current2"].ConnectionString);
            }
        }


        public static double DefaultStringSimilarity
        {
            get { return Convert.ToDouble(Config_DataProvider.GetCacheConfig("DefaultStringSimilarity").ConfigValue); }
        }

        public static bool IsInTestMode
        {
            get { return Convert.ToBoolean(Config_DataProvider.GetCacheConfig("IsInTestMode").ConfigValue); }
        }

        public static string ServiceUrl
        {
            get { return Convert.ToString(Config_DataProvider.GetCacheConfig("ServiceUrl").ConfigValue); }
        }

        public static string PaymentUrl
        {
            get { return Convert.ToString(Config_DataProvider.GetCacheConfig("PaymentUrl").ConfigValue); }
        }

        public static string MerchantId
        {
            get { return Convert.ToString(Config_DataProvider.GetCacheConfig("MerchantId").ConfigValue); }
        }

        public static string TransactionKey
        {
            get { return Convert.ToString(Config_DataProvider.GetCacheConfig("TransactionKey").ConfigValue); }
        }

        public static string TerminalId
        {
            get { return Convert.ToString(Config_DataProvider.GetCacheConfig("TerminalId").ConfigValue); }
        }

        public static string ReturnURL
        {
            get { return Convert.ToString(Config_DataProvider.GetCacheConfig("ReturnURL").ConfigValue); }
        }



        public static int[] DynamicPageTypesInRightMenu
        {
            get { return GetListConfig<int>(Config_DataProvider.GetCacheConfig("DynamicPageTypesInRightMenu").ConfigValue).ToArray(); }
        }

        private static List<T> GetListConfig<T>(string p)
        {
            return p.Split(',').Select(item => (T)Convert.ChangeType(item, typeof(T))).ToList();
        }

        public static int ScoreCalculationInterval
        {
            get { return Convert.ToInt32(Config_DataProvider.GetCacheConfig("ScoreCalculationInterval").ConfigValue); }
        }

        public static string UrlGameFilesPath
        {
            get { return Convert.ToString(Config_DataProvider.GetCacheConfig("UrlGameFilesPath").ConfigValue); }
        }

        public static string ContacUsAdmin
        {
            get { return Convert.ToString(Config_DataProvider.GetCacheConfig("ContacUsAdmin").ConfigValue); }
        }

        public static string UrlWishFilesPath
        {
            get { return Config_DataProvider.GetCacheConfig("UrlWishFilesPath").ConfigValue; }
        }

        public static string UrlPostalCardFilesPath
        {
            get { return Config_DataProvider.GetCacheConfig("UrlPostalCardFilesPath").ConfigValue; }
        }

        public static bool DoResponseFilter
        {
            get { return Config_DataProvider.GetCacheConfig("DoResponseFilter").ConfigValue.ToBool(); }
        }

        public static String PublicKeyPath
        {
            get { return Config_DataProvider.GetCacheConfig("PublicKeyPath").ConfigValue; }
        }
    }
}
