using Kids.EntitiesModel;
using System;
using System.Web.UI;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.DynPage
{
    public partial class _DynamicPage : FormBaseClass
    {
        private long PageId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialises my dirty hack to remove the leading slash from all web reference files.
         


            if (RouteData.Values["Id"].ToString().IsInt64())
            {
                PageId = Convert.ToInt64(RouteData.Values["Id"]);
                DynamicPage dynamicPage =DynamicPages_DataProvider.GetDynamicPageById(PageId);
                if ((dynamicPage != null) )
                {
                    TitleLbl.Text = dynamicPage.Title;
                    Page.Title = dynamicPage.PageName;
                    BodyLbl.Text = dynamicPage.Body;
                    return;
                }
            }
            Response.Redirect("~");
        }
    }
}

