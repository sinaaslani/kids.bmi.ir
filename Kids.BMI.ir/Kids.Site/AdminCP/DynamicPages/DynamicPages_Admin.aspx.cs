using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.DynamicPages
{
    public partial class DynamicPages_Admin : AdminSecureFormBaseClass
    {
        private DynamicPage _CurrentDynamicPage;
        private DynamicPage CurrentDynamicPage
        {
            get
            {
                return _CurrentDynamicPage ??
                       (_CurrentDynamicPage = SerializeHelper.DataContract_ToObject<DynamicPage>(ViewState["CurrentDynamicPage"].ToString()));
            }
            set
            {
                _CurrentDynamicPage = value;
                ViewState["CurrentDynamicPage"] = SerializeHelper.DataContract_ToString(_CurrentDynamicPage);
            }
        }

        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsDynamicPageAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindDynamicPageTypes();
        }

        private void BindDynamicPageTypes()
        {
            IEnumerable<DynamicPageType> lst = DynamicPages_DataProvider.GetDynamicPageType();
            drpDynamicPageType.Items.Add(new ListItem("------", "-1"));
            foreach (var type in lst)
                drpDynamicPageType.Items.Add(new ListItem(type.PageTypeName, type.PageTypeId.ToString()));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPlanGrid();

            pnlResult.Visible = true;
            pnlDetails.Visible = false;
        }

        private void BindPlanGrid(int CurrentPage = 1)
        {
            int RecordCount;
            long? PageId = null;
            if (txtSearchPageId.Text.IsInt32())
                PageId = Convert.ToInt32(txtSearchPageId.Text);

            List<DynamicPage> Plans = DynamicPages_DataProvider.GetDynamicPage(out RecordCount, PageId,
                                                                  PageName: txtSearchPageName.Text, _PageSize: dgDynamicPages.PageSize,
                                                                  Currentpage: CurrentPage);
            GridViewFiller ObjGridFilter = new GridViewFiller();
            ObjGridFilter.PagingGridView(dgDynamicPages, Plans, RecordCount);

        }

        protected void btnNewDynamicPage_Click(object sender, EventArgs e)
        {
            ClearControl(pnlDetails);
            txtbodyCtrl.Text = "";
            PageState = Action.Add;
            CurrentDynamicPage = new DynamicPage();

            pnlResult.Visible = false;
            pnlDetails.Visible = true;

        }

        protected void dgDynamicPages_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            PageState = Action.Update;
            CurrentDynamicPage = DynamicPages_DataProvider.GetDynamicPageById(Convert.ToInt64(dgDynamicPages.DataKeys[e.NewSelectedIndex].Value));
            ClearControl(pnlDetails);

            txtPageName.Text = CurrentDynamicPage.PageName;
            txtPageTitle.Text = CurrentDynamicPage.Title;
            txtbodyCtrl.Text = CurrentDynamicPage.Body;
            drpDynamicPageType.SelectedValue = CurrentDynamicPage.PageTypeId.ToString();
            pnlDetails.Visible = true;
        }

        protected void dgDynamicPages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DynamicPage p = DynamicPages_DataProvider.GetDynamicPageById(Convert.ToInt64(dgDynamicPages.DataKeys[e.RowIndex].Value));
            p.MarkAsDeleted();
            DynamicPages_DataProvider.SaveDynamicPage(p);
            pnlDetails.Visible = false;
            BindPlanGrid();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CurrentDynamicPage.PageName = txtPageName.Text;
            CurrentDynamicPage.Title = txtPageTitle.Text;
            CurrentDynamicPage.Body = txtbodyCtrl.Text;
            CurrentDynamicPage.PageTypeId = drpDynamicPageType.SelectedValue.ToInt32();
            CurrentDynamicPage.CreateDateTime = DateTime.Now;

            if (PageState == Action.Add)
            {
                CurrentDynamicPage.MarkAsAdded();
                DynamicPages_DataProvider.SaveDynamicPage(CurrentDynamicPage);

            }
            else if (PageState == Action.Update)
            {
                CurrentDynamicPage.MarkAsModified();
                DynamicPages_DataProvider.SaveDynamicPage(CurrentDynamicPage);
            }
            pnlDetails.Visible = false;
            BindPlanGrid();

            RouteTable.Routes.Clear();
            Global.RegisterRoutes(RouteTable.Routes);
        }





        protected void dgDynamicPages_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgDynamicPages.PageIndex = e.NewPageIndex;
            BindPlanGrid(e.NewPageIndex + 1);
        }

        protected void dgDynamicPages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lnkDelete") as LinkButton).Attributes.Add("onclick", "return Confirm();");
            }
        }

        protected void btnCancelPlan_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
        }




    }
}