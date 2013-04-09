using System;
using System.Configuration;
using System.Web.UI;
using BMISSOClientHelper;

namespace SSOWebSite
{
    public partial class _Redirect : Page
    {
        protected string NavigationScript = "";
        readonly string DefaultRedirect = ConfigurationManager.AppSettings["DefaultRedirect"];
        string _ReturnURL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1000));
            try
            {
                ReturnURL = string.IsNullOrEmpty(Request["returnURL"])
                                ? CryptographyHelper.Encrypt(DefaultRedirect)
                                : Request["returnURL"];
                ReturnURL = CryptographyHelper.Decrypt(ReturnURL);

            }
            catch
            {
                ReturnURL = DefaultRedirect;
            }

            if (Request["co"] != null && Request["co"].Trim().Length > 0)
            {
                Uri ReturnURL_uri = new Uri(ReturnURL);
                if (ReturnURL_uri.Query.Trim().Length > 0)
                    ReturnURL += "&";
                else
                    ReturnURL += "?";
                ReturnURL += "co=" + Server.UrlEncode(Request["co"]);
            }

            lnkRedirectUrl.NavigateUrl = ReturnURL;
            NavigationScript = string.Format(@"setTimeout(""navigate('{0}')"",wait);", ReturnURL);
            Page.ClientScript.RegisterStartupScript(GetType(), "Redeirect", NavigationScript, true);
        }

        private string ReturnURL
        {
            get { return _ReturnURL; }
            set { _ReturnURL = value; }
        }
    }
}