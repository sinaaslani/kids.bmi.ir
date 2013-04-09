using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DirectoryInfo myImageDir = new DirectoryInfo(MapPath("~/images/fullscreen/"));
            try
            {
                galleryDataList.DataSource = myImageDir.GetFiles();
                galleryDataList.DataBind();
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Response.Write("<script language =Javascript> alert('Error!');</script>");
            }
        } 
    }
}
