using System;
using System.IO;
using System.Web.UI;

namespace Site.Kids.bmi.ir
{
    public partial class WebForm2 : Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DirectoryInfo myImageDir = new DirectoryInfo(MapPath("~/AdminCP/Files/Festival/"));
                try
                {
                    galleryDataList.DataSource = myImageDir.GetFiles();
                    galleryDataList.DataBind();
                }
                catch (DirectoryNotFoundException)
                {
                    Response.Write("<script language =Javascript> alert('Error!');</script>");
                }
            } 
        }
    }
}
