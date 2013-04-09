using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;
using System;
using System.Web.UI.WebControls;

namespace Site.Kids.bmi.ir.AdminCP.ScoreAdmin
{

    public partial class ScoreTypeList : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsScoreTypeAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int? cpcategoryId = null;
            if (UtilityMethod.GetRequestParameter("pcId").IsInt64())
                cpcategoryId = UtilityMethod.GetRequestParameter("pcId").ToInt32();

            ListGrid.DataSource = Score_DataProvider.GetScoresTypes(cpcategoryId);
            ListGrid.DataBind();

        }


        protected void ListGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListGrid.PageIndex = e.NewPageIndex;
            ListGrid.DataBind();
        }

    }
}
