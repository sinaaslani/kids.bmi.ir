using System;
using System.Configuration;
using System.Web.UI;
using BMISSOClientHelper;
using BMISSOClientHelper.BMISSOService;
using Microsoft.Web.Services3.Security.Tokens;

namespace SSOWebSite
{
    public partial class _Default : Page
    {
        protected const string NavigationScript = "";

        readonly string SSOUserName = ConfigurationManager.AppSettings["SSOUserName"];
        readonly string SSOPassword = ConfigurationManager.AppSettings["SSOPassword"];
        readonly string SSOWebServiceURL = ConfigurationManager.AppSettings["SSOWebServiceURL"];
        readonly string Domain = ConfigurationManager.AppSettings["Domain"];
        readonly int ExpirationMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["ExpirationMinutes"]);
        protected readonly string DefaultRedirect = ConfigurationManager.AppSettings["DefaultRedirect"];

        readonly BMISSOServiceWse objServiceWse = new BMISSOServiceWse();


        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1000));
            objServiceWse.Url = SSOWebServiceURL;

            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["act"]))
            {
                string ActParam;
                try
                {
                    ActParam = CryptographyHelper.Decrypt(Request.QueryString["act"]);
                }
                catch
                {
                    ActParam = "";
                }

                if (ActParam == "logout")
                    logout();
            }

            SetLoginComment();


        }

        private void SetLoginComment()
        {
            bool isBMIEmployee = false;
            try
            {
                if (!String.IsNullOrEmpty(Request["chkE"]) && Convert.ToBoolean(Convert.ToInt32(Request["chkE"])))
                    isBMIEmployee = true;
            }
            catch { }

            Control chld;
            if (isBMIEmployee)
            {
                chld = LoadControl("skins/LoginCommentEmployee.ascx");
                lblUserTypeTitle.Text = "مخصوص كارمندان بانك ملي ايران";
            }
            else
            {
                chld = LoadControl("skins/LoginCommentDefault.ascx");

            } if (chld != null)
                pnlLoginComment.Controls.Add(chld);
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            objServiceWse.RequestSoapContext.Security.Tokens.Add(
                new UsernameToken(SSOUserName
                                  , CryptographyHelper.CalculateHash(SSOPassword, CryptographyHelper.HashMode.SHA1)
                                  , PasswordOption.SendHashed));

            // Authenticating the BMI user.
            bool checkIsBMIEmployee = false;
            try
            {
                UserProfile AU = objServiceWse.Authenticate(TxtUserID.Text, TxtPass.Text);
                if (!String.IsNullOrEmpty(Request["chkE"]) && Convert.ToBoolean(Convert.ToInt32(Request["chkE"])))
                    checkIsBMIEmployee = true;

                if (AU != null)
                {
                    if (!AU.IsBMIEmployee && checkIsBMIEmployee) // user must be BMI Employee
                    {
                        lblError.Text = "براي مشاهده اطلاعات درخواستي بايد كارمند بانك ملي باشيد. اطلاعات شما به عنوان كارمند بانك ملي ثبت نشده است";
                        lblError.Visible = true;

                    }
                    else
                    {
                        //if (SSOClientHelper.IsWeakPassword(TxtPass.Text, TxtUserID.Text))
                        //    Page.Response.Redirect();


                        string cookieContent = SSOClientHelper.SetLoginCookie(AU, Domain, ExpirationMinutes);
                        string URL = string.IsNullOrEmpty(Request["returnURL"])
                                         ? CryptographyHelper.Encrypt(DefaultRedirect)
                                         : Request["returnURL"];

                        //IF Url Domain <> SSO Domain 
                        Uri url_uri = new Uri(CryptographyHelper.Decrypt(URL));
                        string cookieContentEnc = "";
                        if (!url_uri.Host.ToLower().EndsWith("bmi.ir"))
                            cookieContentEnc = Server.UrlEncode(cookieContent);

                        URL = Server.UrlEncode(URL);

                        URL = string.Format("Redirect.aspx?returnURL={0}&co={1}", URL, cookieContentEnc);
                        Response.Redirect(URL);
                    }
                }
                else
                {
                    lblError.Text = "نام کاربری یا کلمه عبور نادرست میباشد";
                    lblError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }


        }

        private void logout()
        {
            SSOClientHelper.ClearLoginCoockie(Domain);
        }
    }
}