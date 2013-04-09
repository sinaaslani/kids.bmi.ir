using System;
using System.Web.UI;
using Kids.Utility.WebMessageBox;

namespace Site.Kids.bmi.ir
{
    public partial class Entry : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnKidsIsland_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/ورود.aspx");
        }

        protected void btnJavanan_Click(object sender, ImageClickEventArgs e)
        {
            MessageBoxHelper.ShowMessageBox(this, @"کاربر محترم :<br>
                               این بخش در حال ایجاد میباشد و به محض آماده سازی شما از طریق همین صفحه قادر به استفاده از امکانات آن خواهید بود"
                             , "ورود به سایت نوجوانان", MessageBoxType.Information);
        }
    }
}
