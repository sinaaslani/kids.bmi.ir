using System;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;


namespace Site.Kids.bmi.ir.AdminCP.KidsUserAdmin
{
    public partial class KidsUser_Admin_Branch : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsKidsUserManager || OnlineSystemUser.IsSiteAdministrator || OnlineSystemUser.IsBranchAdmin || OnlineSystemUser.IsBranchUser))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
            pnlResult.Visible = true;

        }

        private void BindGrid(int CurrentPage = 1)
        {
            int RecordCount;
            var kids = KidsUser_DataProvider.GetKidsUser(out RecordCount,
                                                                         SSOUserName: null,
                                                                         ChildMelliCode: txtSearchChildMelliCode.Text,
                                                                         ParentMelliCode: txtSearchParentMelliCode.Text,
                                                                         ChildName: txtSearchChildName.Text,
                                                                         ChildFamily: txtSearchChildFamily.Text,
                                                                         CurrentStatus: null,
                                                                        PageSize: dgPlans.PageSize,
                                                                        Currentpage: CurrentPage);
            GridViewFiller ObjGridFilter = new GridViewFiller();
            ObjGridFilter.PagingGridView(dgPlans, kids, RecordCount.ToInt32());


        }


        protected void dgPlans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgPlans.PageIndex = e.NewPageIndex;
            BindGrid(e.NewPageIndex + 1);
        }

        protected void dgPlans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var KidsUser = e.Row.DataItem as KidsUser;
                if (KidsUser != null)
                {
                    (e.Row.FindControl("lblCurrentState") as Label).Text = KidsUser.CurrentStatus.ToString();
                    (e.Row.FindControl("lblCurrentState") as Label).ToolTip = KidsUser.KidsUserState.StateName;
                }
            }
        }

    }
}