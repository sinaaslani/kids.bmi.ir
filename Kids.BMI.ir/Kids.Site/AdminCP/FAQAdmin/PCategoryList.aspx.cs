using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;
using System.Web.UI.WebControls;

namespace Site.Kids.bmi.ir.AdminCP.FAQAdmin
{
    public partial class PCategoryList : AdminSecureFormBaseClass
	{
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsFAQAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ListGrid.DataSource = FAQ_DataProvider.GetFAQCategory();
			ListGrid.DataBind() ;

		}

        protected void ListGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
			{
            ListGrid.PageIndex = e.NewPageIndex;
				ListGrid.DataBind() ;

		}
        
	}
}
