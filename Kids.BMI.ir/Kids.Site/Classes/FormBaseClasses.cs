using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BMISSOClientHelper;
using BMISSOClientHelper.BMISSOService;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.EntitiesModel.Scores;
using Kids.LoggingHelper;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Masters;
using Site.Kids.bmi.ir.Scores;
using Site.Kids.bmi.ir.UserControls;

namespace Site.Kids.bmi.ir.Classes
{

    public class UserControlBaseClass : UserControl
    {
        protected void RefreshOnlineKidsUserInfo()
        {
            KidsSecureFormBaseClass.RefreshOnlineKidsUserInfo(Page);
        }

        protected OnlineSystemUserInfo AdminOnlineUser
        {
            get { return AdminSecureFormBaseClass.OnlineSystemUser; }
        }
        protected OnlineKidsUserInfo KidsOnlineUser
        {
            get { return KidsSecureFormBaseClass.OnlineKidsUser; }
        }

        protected void ShowMessageBox(string Message, string BoxTitle, MessageBoxType type = MessageBoxType.Warning)
        {
            MessageBoxHelper.ShowMessageBox(Page, Message, BoxTitle, type);
        }
        static public string SWF_tag(string url, int width, int height)
        {

            string tag = "<script type=\"text/javascript\">";

            tag += " var FlashVersion=new String(); FlashVersion=GetSwfVer();var MajorVersion;";
            tag += " if(FlashVersion !=-1){{ if(FlashVersion.indexOf(\",\")>=0)";
            tag += "  MajorVersion=FlashVersion.split(\" \")[1].split(\",\")[0];";
            tag += "  else if(FlashVersion.indexOf(\".\")>=0)  MajorVersion=FlashVersion.split(\".\")[0]; }}";
            tag += " if(FlashVersion==-1 || MajorVersion<9 ) {{ ";
            tag += " var msgInstallFlash = \"<BR>فلش بر روي دستگاه شما نصب نميباشد.<a style='text-decoration:none' href='http://http://get.adobe.com/flashplayer/'>دريافت آخرين نسخه از سايت adobe</a>\"; ";
            tag += " msgInstallFlash += \"<br><a href='http://www.bmi.ir/images/FlashPlayerIE.zip'>دريافت نسخه 10 ويژه IE</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href='http://www.bmi.ir/images/FlashPlayerFireFox.zip'>دريافت نسخه 10 ويژه Firefox</a>\"; ";
            tag += " document.getElementById('divBanner').innerHTML = msgInstallFlash; ";
            tag += "}} ";
            tag += "else {{ ";
            tag += "AC_FL_RunContent('codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0', 'width', '{1}', 'height', '{2}', 'src', '{0}', 'quality', 'high', 'pluginspage', 'http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash', 'movie', '{0}', 'divBanner', 'divBanner', 'menu', 'false', 'wmode', 'transparent');";
            tag += "}}";
            tag += "</script>";
            tag += "<noscript>";
            tag +=
                "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\">";
            tag += "<param name=\"movie\" value=\"{0}\" />";
            tag += "<param name=\"quality\" value=\"high\" />";
            tag += "<param name=\"wmode\" value=\"transparent\" />";
            tag +=
                "<embed src=\"{0}\" quality=\"high\" wmode=\"transparent\" pluginspage=\"http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash\"";
            tag += "type=\"application/x-shockwave-flash\" ></embed></object></noscript>";

            return string.Format(tag, url, width, height);

        }

    }

    public class FormBaseClass : Page
    {
        protected FormBaseClass(bool p)
        {
        }

        protected FormBaseClass()
        {
            PreInit += FormBaseClass_PreInit;
            Error += FormBaseClasses_Error;
        }

        void FormBaseClass_PreInit(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.CacheControl = "no-cache";
            Response.Expires = -1;

            if (KidsSecureFormBaseClass.OnlineKidsUser == null)
                KidsSecureFormBaseClass.OnlineKidsUser = KidsSecureFormBaseClass.SetUserInfo(this, true, false);

            if (KidsSecureFormBaseClass.OnlineKidsUser == null && GeustKidsUser == null)
                Response.Redirect("~/ورود.aspx?ReturnUrl=" + Server.UrlEncode(HttpContext.Current.Request.Url.ToString()), true);
        }

        void FormBaseClasses_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                LogUtility.WriteEntryEventLog("FormBaseClasses_Error", ex, EventLogEntryType.Error);
                ShowMessageBox(ex);
            }
        }


        public static string ShowAssemblyVersion()
        {
            try
            {
                string Path = HttpContext.Current.Server.MapPath("~/bin/Site.Kids.bmi.ir.dll");
                return UtilityMethod.GetAssemblyVersion(Path).Version.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static void DeleteOldFiles(string dirPath)
        {
            try
            {
                DirectoryInfo TemDir = new DirectoryInfo(dirPath);
                foreach (FileInfo f in TemDir.GetFiles())
                {
                    if (f.Extension.Contains("xls") && f.CreationTime.AddMinutes(15) <= DateTime.Now)
                    {
                        if ((f.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            f.Attributes = FileAttributes.Normal;
                        f.Delete();
                    }
                }
            }
            catch { }

        }

        public Fa PageMaster
        {
            get { return (Master as Fa); }
        }

        public static void ClientRedirect(Page p, string Url, int Timeout)
        {
            ScriptManager.RegisterStartupScript(p, p.GetType(), "redirect",
                                                string.Format("var ptr; ptr=setInterval(function(){{clearInterval(ptr);window.location='{0}'}},{1});", p.ResolveUrl(Url), Timeout)
                                                , true);
        }
        protected void ClientRedirect(string Url, int Timeout)
        {
            ClientRedirect(Page, Url, Timeout);
        }
        protected void ShowMessageBox(string Message)
        {
            MessageBoxHelper.ShowMessageBox(Page, Message, null, MessageBoxType.Warning);
        }
        protected void ShowMessageBox(string Message, string BoxTitle, MessageBoxType type = MessageBoxType.Warning)
        {
            MessageBoxHelper.ShowMessageBox(Page, Message, BoxTitle, type);
        }


        protected void ShowMessageBox(Exception ex)
        {
            string Message = "";
            while (ex != null)
            {
                Message += ex.Message + "\r\n";
                ex = ex.InnerException;
            }
            ShowMessageBox(Message, "خطا در سیستم", MessageBoxType.Error);
        }

        private static void ClearControlForControlType(Control ctrl)
        {
            switch (ctrl.GetType().Name)
            {
                #region  CheckBox
                case "CheckBox":
                    CheckBox objchb = ctrl as CheckBox;
                    objchb.Checked = false;
                    break;
                #endregion

                #region HtmlInputText
                case "HtmlInputText":
                    HtmlInputText objHtmltxt = ctrl as HtmlInputText;
                    objHtmltxt.Value = string.Empty;
                    break;
                #endregion

                #region HtmlTextArea
                case "HtmlTextArea":
                    HtmlTextArea objtxtArea = ctrl as HtmlTextArea;
                    objtxtArea.Value = string.Empty;
                    break;
                #endregion

                #region HtmlInputHidden
                case "HtmlInputHidden":
                    HtmlInputHidden objhd = ctrl as HtmlInputHidden;
                    objhd.Value = string.Empty;
                    break;
                case "HiddenField":
                    HiddenField objhd1 = ctrl as HiddenField;
                    objhd1.Value = string.Empty;
                    break;
                #endregion

                #region HtmlSelect
                case "HtmlSelect":
                    HtmlSelect objsl = ctrl as HtmlSelect;
                    objsl.Value = string.Empty;
                    break;
                #endregion

                #region DropDownList
                case "DropDownList":
                    DropDownList objDropDownList = ctrl as DropDownList;
                    objDropDownList.SelectedIndex = 0;
                    break;
                #endregion

                #region HtmlInputCheckBox
                case "HtmlInputCheckBox":
                    HtmlInputCheckBox objchk = ctrl as HtmlInputCheckBox;
                    objchk.Checked = false;
                    break;
                #endregion

                #region TextBox
                case "TextBox":
                    TextBox objtxt = ctrl as TextBox;
                    objtxt.Text = string.Empty;
                    break;
                #endregion

                #region RadioButtonList
                case "RadioButtonList":
                    RadioButtonList objrbl = ctrl as RadioButtonList;
                    objrbl.SelectedIndex = -1;
                    break;
                #endregion

                #region Label
                case "Label":

                    Label objlbl = ctrl as Label;
                    if (objlbl.ID.Contains("Numlbl"))
                    {
                        objlbl.Text = "0";
                    }
                    else
                    {
                        objlbl.Text = string.Empty;
                    }
                    break;
                #endregion

                #region CheckBoxList
                case "CheckBoxList":
                    CheckBoxList objCheckBoxList = ctrl as CheckBoxList;
                    objCheckBoxList.SelectedIndex = -1;
                    break;
                #endregion

                #region ListBox
                case "ListBox":
                    ListBox objListBox = ctrl as ListBox;
                    objListBox.Items.Clear();
                    break;
                #endregion

                #region Image
                case "Image":
                    Image objImage = ctrl as Image;
                    objImage.ImageUrl = "";
                    break;
                #endregion

                case "securepages_ucdatepicker_ascx":
                    ucDatePicker objucDatePicker = ctrl as ucDatePicker;
                    objucDatePicker.SelectedDateTime = null;
                    break;
            }
        }

        public static void ClearControl(HtmlGenericControl objContainer)
        {

            foreach (Control ctrl in objContainer.Controls)
            {
                Control PassedControl = ctrl;
                ClearControlForControlType(PassedControl);
            }
        }




        public static GeustKidsUser GeustKidsUser
        {
            get
            {
                if (HttpContext.Current.Session["GeustKidsUser"] != null)
                    return SerializeHelper.DataContract_ToObject<GeustKidsUser>(HttpContext.Current.Session["GeustKidsUser"].ToString());

                return null;
            }
            set
            {
                HttpContext.Current.Session["GeustKidsUser"] = SerializeHelper.DataContract_ToString(value);
            }
        }


    }

    public abstract class KidsSecureFormBaseClass : FormBaseClass
    {
        private static readonly string SSOUserName = SystemConfigs.SSOUserName;
        private static readonly string SSOPassword = SystemConfigs.SSOPassword;
        private static readonly string SSOWebSiteURL = SystemConfigs.SSOWebSiteURL;
        private static readonly string SSOWebServiceURL = SystemConfigs.SSOWebServiceURL;


        public readonly static SSOClientHelper BMIUserInteraction = new SSOClientHelper(SSOUserName, SSOPassword, SSOWebSiteURL, SSOWebServiceURL);

        protected abstract void CheckKidsUser();

        protected KidsSecureFormBaseClass()
            : base(true)
        {
            PreInit += FormBaseClasses_PreInit;
            Load += FormBaseClasses_Load;
            if (SystemConfigs.DoResponseFilter)
                HttpContext.Current.Response.Filter = new WebResourceResponseFilter(HttpContext.Current.Response.Filter);
        }

        void FormBaseClasses_PreInit(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.CacheControl = "no-cache";
            Response.Expires = -1;

            OnlineKidsUser = SetUserInfo(this, true, true);
        }

        void FormBaseClasses_Load(object sender, EventArgs e)
        {

            if (OnlineKidsUser != null)
                CheckKidsUser();
            else
                SendToLoginPage(this);
        }

        protected void RefreshOnlineKidsUserInfo()
        {
            RefreshOnlineKidsUserInfo(this);
        }

        public static void RefreshOnlineKidsUserInfo(Object o)
        {
            Page p = o as Page;
            HttpContext.Current.Session["CurrentSSOUser"] = null;
            OnlineKidsUser = SetUserInfo(p, false, false);
        }
        public static void RefreshKidsUserScores()
        {
            if (OnlineKidsUser != null && OnlineKidsUser.Kids_UserInfo != null)
            {
                List<scoreListItem> DailyscoreList, MonthlyscoreList;
                var score = ScoreHelper.CalculateScore(OnlineKidsUser.Kids_UserInfo, true, out DailyscoreList, out MonthlyscoreList);
                HttpContext.Current.Session["CurrentDailyScoreList"] = SerializeHelper.DataContract_ToString(DailyscoreList);
                HttpContext.Current.Session["CurrentMonthlyScoreList"] = SerializeHelper.DataContract_ToString(MonthlyscoreList);
                HttpContext.Current.Session["CurrentScore"] = score;
            }
        }
        public static OnlineKidsUserInfo SetUserInfo(Page p, bool RefreshSSOUser, bool MustSendToLoginPage)
        {

            if (HttpContext.Current.Session["CurrentSSOUser"] != null && HttpContext.Current.Session["CurrentSSOUser"].ToString().Contains("OnlineSystemUserInfo"))
                HttpContext.Current.Session["CurrentSSOUser"] = null;


            OnlineKidsUserInfo outuser;
            if (!SSOClientHelper.IsBMIUserLoggedIn(out outuser, GetKidsUser, RefreshSSOUser, Serializaer, DeSerializaer))
                if (MustSendToLoginPage)
                    SendToLoginPage(p);
                else
                    return null;

            return outuser;
        }



        private static OnlineKidsUserInfo GetKidsUser(UserProfile userinfo, bool RefreshSSOUser)
        {
            try
            {
                KidsUser u = KidsUser_DataProvider.GetKidsUser(SSOUserName: userinfo.UserID).FirstOrDefault();
                if (RefreshSSOUser)
                    userinfo = BMIUserInteraction.GetBMIUserProfile(userinfo.UserID);

                if (u == null)
                {
                    //var reqUrl = HttpContext.Current.Request.Url.ToString();
                    //if (
                    //    reqUrl.Contains("Register.aspx") ||
                    //    reqUrl.Contains("JpegImage.aspx")
                    //   )
                    return new OnlineKidsUserInfo { Kids_UserInfo = null, SSOUser = userinfo };

                    //throw new ApplicationException("Invalid User!");
                }

                return new OnlineKidsUserInfo { Kids_UserInfo = u, SSOUser = userinfo };

            }
            catch (ApplicationException)
            {
                //HttpContext.Current.Response.Redirect("~/Register.aspx", true);
                return null;
            }
        }


        public static OnlineKidsUserInfo OnlineKidsUser
        {
            get
            {
                if (HttpContext.Current.Session["OnlineKidsUser"] != null)
                    return SerializeHelper.DataContract_ToObject<OnlineKidsUserInfo>(HttpContext.Current.Session["OnlineKidsUser"].ToString());

                return null;
            }
            set
            {
                HttpContext.Current.Session["OnlineKidsUser"] = SerializeHelper.DataContract_ToString(value);
            }
        }

        private static string Serializaer(OnlineKidsUserInfo user)
        {
            return SerializeHelper.DataContract_ToString(user);
        }

        private static OnlineKidsUserInfo DeSerializaer(string serialuser)
        {
            return SerializeHelper.DataContract_ToObject<OnlineKidsUserInfo>(serialuser);
        }


        private static void SendToLoginPage(Page p)
        {
            var c = HttpContext.Current;
            if (GeustKidsUser != null)
                MessageBoxHelper.ShowMessageBox(p, "کاربر گرامی :<BR>جهت استفاده از امکانات این بخش شما میبایست یکی از کاربران ساکن جزیره باشید", "خطای دسترسی", MessageBoxType.Information);
            else
                c.Response.Redirect(string.Format("~/ورود.aspx?ReturnUrl={0}", c.Server.UrlEncode(HttpContext.Current.Request.Url.ToString())));

        }
    }

    public abstract class AdminSecureFormBaseClass : FormBaseClass
    {
        private static readonly string SSOUserName = SystemConfigs.SSOUserName;
        private static readonly string SSOPassword = SystemConfigs.SSOPassword;
        private static readonly string SSOWebSiteURL = SystemConfigs.SSOWebSiteURL;
        private static readonly string SSOWebServiceURL = SystemConfigs.SSOWebServiceURL;


        public readonly static SSOClientHelper BMIUserInteraction = new SSOClientHelper(SSOUserName, SSOPassword, SSOWebSiteURL, SSOWebServiceURL);

        protected abstract void CheckAdminUser();

        protected AdminSecureFormBaseClass()
            : base(true)
        {
            PreInit += FormBaseClasses_PreInit;
            Load += FormBaseClasses_Load;
            if (SystemConfigs.DoResponseFilter)
                HttpContext.Current.Response.Filter = new WebResourceResponseFilter(HttpContext.Current.Response.Filter);
        }

        void FormBaseClasses_PreInit(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.CacheControl = "no-cache";
            Response.Expires = -1;

            OnlineSystemUser = SetUserInfo(true);
        }

        void FormBaseClasses_Load(object sender, EventArgs e)
        {
            if (OnlineSystemUser != null)
                CheckAdminUser();
            else
                SendToLoginPage();
        }

        private static OnlineSystemUserInfo SetUserInfo(bool MustSendToLoginPage)
        {
            if (HttpContext.Current.Session["CurrentSSOUser"] != null && HttpContext.Current.Session["CurrentSSOUser"].ToString().Contains("OnlineKidsUserInfo"))
                HttpContext.Current.Session["CurrentSSOUser"] = null;

            OnlineSystemUserInfo outuser;
            if (!SSOClientHelper.IsBMIUserLoggedIn(out outuser, GetSystemUser, false, Serializaer, DeSerializaer))
            {
                if (MustSendToLoginPage)
                    SendToLoginPage();
                else
                    return null;
            }
            return outuser;
        }



        private static OnlineSystemUserInfo GetSystemUser(UserProfile userinfo, bool RefreshAdminSSOUser)
        {
            try
            {
                SystemUser u = SystemUser_DataProvider.GetValidSystemUser(userinfo.UserID);

                if (u == null)
                    throw new ApplicationException("Invalid User");

                if (RefreshAdminSSOUser)
                    userinfo = BMIUserInteraction.GetBMIUserProfile(userinfo.UserID);

                return new OnlineSystemUserInfo { System_UserInfo = u, SSOUser = userinfo };
            }
            catch (ApplicationException)
            {
                HttpContext.Current.Response.Redirect("~/Error/NotAccess.aspx");
                return null;
            }
        }


        public static OnlineSystemUserInfo OnlineSystemUser
        {
            get
            {
                if (HttpContext.Current.Session["OnlineSystemUser"] != null)
                    return SerializeHelper.DataContract_ToObject<OnlineSystemUserInfo>(HttpContext.Current.Session["OnlineSystemUser"].ToString());

                return null;
            }
            private set
            {
                HttpContext.Current.Session["OnlineSystemUser"] = SerializeHelper.DataContract_ToString(value);
            }
        }

        private static string Serializaer(OnlineSystemUserInfo user)
        {
            return SerializeHelper.DataContract_ToString(user);
        }

        private static OnlineSystemUserInfo DeSerializaer(string serialuser)
        {
            return SerializeHelper.DataContract_ToObject<OnlineSystemUserInfo>(serialuser);
        }

        protected static void SendToLoginPage()
        {
            BMIUserInteraction.SendToLoginPage(HttpContext.Current.Request.Url.ToString());
        }

        protected Action PageState
        {
            get { return (Action)ViewState["PageState"]; }
            set { ViewState["PageState"] = value; }
        }

        protected enum Action
        {
            Add = 1,
            Update = 2,

        };
    }



    public class OnlineSystemUserInfo : SSOClientHelper.IUserProfile
    {
        public SystemUser System_UserInfo { get; set; }
        public UserProfile SSOUser { get; set; }
        public DateTime? ExpiretionDate { get; set; }

        public bool IsSiteAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "SiteAdministrator"); }
        }

        internal bool IsNewsAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "NewsAdministrator"); }
        }

        public bool IsNewsOperator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "NewsOperator"); }
        }


        internal bool IsDynamicPageAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "DynamicPageAdministrator"); }
        }

        internal bool IsSystemUserManager
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "SystemUserManager"); }
        }

        public bool IsPollAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "PollAdministrator"); }
        }

        public bool IsKidsUserManager
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "KidsUserManager"); }
        }

        public bool IsGameAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "GameAdministrator"); }
        }

        public bool IsFAQAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "FAQAdministrator"); }
        }

        public bool IsExamAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "ExamAdministrator"); }
        }

        public bool IsBranchUser
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "BranchUser"); }
        }

        public bool IsBranchAdmin
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "BranchAdmin"); }
        }

        public bool IsScoreTypeAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "ScoreTypeAdministrator"); }
        }

        public bool IsWishesAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "WishesAdministrator"); }
        }

        public bool IsPostalCardAdministrator
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "PostalCardAdministrator"); }
        }

        public bool IsKidsUserAdmin
        {
            get { return System_UserInfo.SystemRoles.Any(o => o.RoleName == "KidsUserAdmin"); }
        }
    }

    public class OnlineKidsUserInfo : SSOClientHelper.IUserProfile
    {
        public KidsUser Kids_UserInfo { get; set; }
        public UserProfile SSOUser { get; set; }


        public DateTime? ExpiretionDate { get; set; }

    }
}