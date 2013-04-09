using System;

namespace SSOWebSite.skins
{
    public partial class HeaderFA : System.Web.UI.UserControl
    {
        public string ParamList;

        protected void Page_Load(object sender, EventArgs e)
        {
            //ControlLanguageType = LanguageType.Fa;
            //const string xmlUrl = "http://www.bmi.ir/Fa/HomePageLogo_Provider.aspx?Lang=0";
            //ParamList = string.Format("url={0}", Server.UrlEncode(xmlUrl));
            //Globals.CurrentLanguage = LanguageType.Fa;
        }
    }
}