using System;
using System.Web.UI;
using Kids.Utility.RSS.NET.RSS;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir
{
    public partial class RSSFeedFa : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RssFeed r = RSSUtility.GenerateNewsRSS( DateTime.Now.AddDays(-60), DateTime.Now);

            Response.Clear();
            Response.ContentType = "text/xml";
            r.Write(Response.OutputStream);
            Response.End();
        }
    }
}
