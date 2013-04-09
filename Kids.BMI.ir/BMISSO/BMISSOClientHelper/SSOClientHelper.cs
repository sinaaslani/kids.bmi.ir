using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.Security;
using BMISSOClientHelper.BMISSOService;
using Microsoft.Web.Services3.Security.Tokens;


namespace BMISSOClientHelper
{
    public class SSOClientHelper
    {
        private readonly String _username;
        private readonly String _password;

        private readonly BMISSOServiceWse objServiceWse = new BMISSOServiceWse();


        public SSOClientHelper(String SSOUsername, String SSOPassword, String SSOWebsite, string SSOWebService)
        {
            objServiceWse.Url = SSOWebService;
            SingleSignOnURL = SSOWebsite;
            _username = SSOUsername;
            _password = SSOPassword;
            WebServiceUserLogin();
        }

        private string SingleSignOnURL { get; set; }

        private void WebServiceUserLogin()
        {
            UsernameToken ut = new UsernameToken(_username, CryptographyHelper.CalculateHash(_password, CryptographyHelper.HashMode.SHA1));
            objServiceWse.RequestSoapContext.Security.Tokens.Clear();
            objServiceWse.RequestSoapContext.Security.Tokens.Add(ut);
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

        }

        private static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #region GetUserInfo

        public UserProfile GetBMIUserProfile(String BMIUsername)
        {
           
            
                return objServiceWse.GetUserInfo(BMIUsername);
         
          
           
        }

        public static bool IsBMIUserLoggedIn()
        {
            DateTime? ExpirationDateTime;
            return GetLoginCoockie(out ExpirationDateTime) != null;
        }
        public static bool IsBMIUserLoggedIn(out UserProfile user)
        {
            DateTime? ExpirationDateTime;
            user = GetLoginCoockie(out ExpirationDateTime);
            return user != null;
        }
        public interface IUserProfile
        {
            UserProfile SSOUser { get; set; }
            DateTime? ExpiretionDate { get; set; }
        }

        public delegate T FillUser<out T>(UserProfile User, bool RefreshSSOUser);
        public delegate string Srializer<in T>(T obj);
        public delegate T DeSrializer<out T>(string SerialStr);

        public static bool IsBMIUserLoggedIn<T>(out T User, FillUser<T> FillAction, bool RefreshSSOUser,
                                                Srializer<T> s = null, DeSrializer<T> ds = null)
                      where T : IUserProfile
        {
            if (IsBMIUserLoggedIn())
            {
                DateTime? ExpirationDateTime;
                UserProfile u = GetLoginCoockie(out ExpirationDateTime);

                if (HttpContext.Current != null)
                {
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["co"]))
                    {
                        var url = HttpContext.Current.Request.Url.AbsolutePath;
                        HttpContext.Current.Response.Redirect(url);
                    }
                }

                var SessionValue = GetSessionValue(ds);

                if (SessionValue == null)
                {
                    User = FillAction(u, RefreshSSOUser);
                    if (User.SSOUser == null)
                        User.SSOUser = u;
                    User.ExpiretionDate = ExpirationDateTime;
                    SetSessionValue(s, User);
                    return true;
                }
                else
                {
                    var SessionUser = (T)GetSessionValue(ds);
                    if (SessionUser.SSOUser.UserID != u.UserID)
                    {
                        HttpContext.Current.Session.Abandon();
                        User = FillAction(u, RefreshSSOUser);
                        User.SSOUser = u;
                        User.ExpiretionDate = ExpirationDateTime;
                        SetSessionValue(s, User);
                        return true;
                    }
                    else
                    {
                        User = SessionUser;
                        User.ExpiretionDate = ExpirationDateTime;
                        return true;
                    }
                }
            }
            SetSessionValue(s);
            User = default(T);
            return false;
        }


        private static void SetSessionValue<T>(Srializer<T> s, T obj = default(T)) where T : IUserProfile
        {
            if (s != null)
                HttpContext.Current.Session["CurrentSSOUser"] = s(obj);
            else
                HttpContext.Current.Session["CurrentSSOUser"] = obj;
        }

        private static object GetSessionValue<T>(DeSrializer<T> ds) where T : IUserProfile
        {
            if (HttpContext.Current.Session["CurrentSSOUser"] != null)
            {
                object SessionValue = ds != null
                                          ? ds(HttpContext.Current.Session["CurrentSSOUser"].ToString())
                                          : HttpContext.Current.Session["CurrentSSOUser"];
                return SessionValue;
            }
            return null;
        }

        #endregion


        #region Coocki Management

        public static UserProfile GetLoginCoockie(out DateTime? ExpirationDateTime)
        {

            HttpCookie coocki = HttpContext.Current.Request.Cookies["ssolog"];

            if ((coocki == null || String.IsNullOrEmpty(coocki.Value)) && HttpContext.Current.Request["co"] != null) // Have Passed Current Cooki
            {
                coocki = new HttpCookie("ssolog")
                {
                    Value = HttpContext.Current.Request["co"],
                    Domain = HttpContext.Current.Request.Url.Host
                };

            }


            if (coocki != null && !String.IsNullOrEmpty(coocki.Value))
            {
                string FinalValue = CryptographyHelper.Decrypt(coocki.Value);
                string[] ArrFinalValue = FinalValue.Split(new[] { "<SEP>" }, StringSplitOptions.None);

                string coockieDateStr = ArrFinalValue[4];

                DateTime CoockiTimeStamp = DateTime.ParseExact(coockieDateStr, "yyyy/MM/dd HH:mm:ss", null);

                int ExpirationMinutes = Convert.ToInt32(ArrFinalValue[5]);
                if (CoockiTimeStamp.AddMinutes(ExpirationMinutes) < DateTime.Now)
                {
                    ClearLoginCoockie("/");
                    ClearLoginCoockie(HttpContext.Current.Request.Url.Host);
                    ExpirationDateTime = null;
                    return null;
                }


                UserProfile AU = new UserProfile
                {
                    UserID = ArrFinalValue[0],
                    PersonalNo = ArrFinalValue[1],
                    NationalCode = ArrFinalValue[2],
                    Email = ArrFinalValue[3],
                    Name = ArrFinalValue[6]
                };

                //Renew coocki
                SetLoginCookie(AU, HttpContext.Current.Request.Url.Host, ExpirationMinutes);
                ExpirationDateTime = DateTime.Now.AddMinutes(ExpirationMinutes);
                return AU;

            }

            ExpirationDateTime = null;
            return null;
        }

        public static string SetLoginCookie(UserProfile AU, string Domain, int ExpirationMinutes)
        {
            string EncUserID = AU.UserID;
            string EncPersonalNo = AU.PersonalNo;
            string EncNationalCode = AU.NationalCode;
            string EncEmail = AU.Email;
            string fnameLname = AU.Name;

            string coockieDateStr = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string FinalValue =
                CryptographyHelper.Encrypt(String.Format("{0}<SEP>{1}<SEP>{2}<SEP>{3}<SEP>{4}<SEP>{5}<SEP>{6}", EncUserID,
                                                        EncPersonalNo, EncNationalCode, EncEmail, coockieDateStr,
                                                        ExpirationMinutes, fnameLname));

            HttpCookie currentCookie_Def;
            if (HttpContext.Current.Request.Url.Host.ToLower().EndsWith("bmi.ir"))
                currentCookie_Def = new HttpCookie("ssolog") { Value = FinalValue, Domain = "bmi.ir" };
            else
                currentCookie_Def = new HttpCookie("ssolog") { Value = FinalValue, Domain = Domain };

            HttpContext.Current.Response.Cookies.Add(currentCookie_Def);
            return FinalValue;
        }

        public static void ClearLoginCoockie(string Domain)
        {
            HttpCookie currentCookie_Def = new HttpCookie("ssolog", "")
                                               {
                                                   Expires = DateTime.Now.AddDays(-1000),
                                                   Domain = Domain
                                               };

            HttpCookie bmi_currentCookie_Def = new HttpCookie("ssolog", "")
                                                {
                                                    Expires = DateTime.Now.AddDays(-1000),
                                                    Domain = "bmi.ir"
                                                };
            HttpContext.Current.Response.Cookies.Add(currentCookie_Def);
            HttpContext.Current.Response.Cookies.Add(bmi_currentCookie_Def);
        }

        #endregion

        #region logout & Redirect

        public void LogOutCurrentUser(string ReturnUrl, bool GoToSSO = true, bool forceRedirect = true, SessionLogoutAction Action = SessionLogoutAction.Abandon)
        {
            ClearLoginCoockie(HttpContext.Current.Request.Url.Host);
            string actParam = CryptographyHelper.Encrypt("logout");

            string EncReturnUrl = CryptographyHelper.Encrypt(ReturnUrl);
            EncReturnUrl = HttpContext.Current.Server.UrlEncode(EncReturnUrl);


            string URL = GoToSSO ? String.Format("{0}?act={1}&returnurl={2}", SingleSignOnURL, actParam, EncReturnUrl) : ReturnUrl;

            switch (Action)
            {
                case SessionLogoutAction.Abandon:
                    HttpContext.Current.Session.Abandon();
                    break;
                case SessionLogoutAction.Clear:
                    HttpContext.Current.Session.Clear();
                    break;
            }
            FormsAuthentication.SignOut();

            if (forceRedirect)
                HttpContext.Current.Response.Redirect(URL, true);
        }

        public enum SessionLogoutAction
        {
            Abandon = 0,
            Clear = 1,
        }

        public void SendToLoginPage(string ReturnURL, bool checkIsEmployee = false)
        {
            ReturnURL = CryptographyHelper.Encrypt(ReturnURL);
            ReturnURL = HttpContext.Current.Server.UrlEncode(ReturnURL);
            string URL = String.Format("{0}?returnurl={1}", SingleSignOnURL, ReturnURL);
            if (checkIsEmployee)
                URL += "&chkE=1";
            HttpContext.Current.Response.Redirect(URL, true);
        }
        #endregion
    }
}
