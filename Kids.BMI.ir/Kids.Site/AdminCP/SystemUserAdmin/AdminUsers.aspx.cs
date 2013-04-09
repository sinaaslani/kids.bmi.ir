using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.SystemUserAdmin
{
    public partial class AdminUsers : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsSystemUserManager || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string uname = UserNameTxt.Text;
            string fname = FNameTxt.Text;
            string lname = LNameTxt.Text;
            List<SystemUser> userslist = SystemUser_DataProvider.GetSystemUser(uname, null, fname, lname);
            usersGrid.DataSource = userslist;
            ResultNumberLbl.Text = userslist.Count.ToString();
            try
            {
                usersGrid.DataBind();
            }
            catch
            {
                usersGrid.PageIndex = 0;
                usersGrid.DataBind();
            }
        }

        protected void UsersGrid_PageIndexChanged(object source, GridViewPageEventArgs e)
        {
            string uname = UserNameTxt.Text;
            string fname = FNameTxt.Text;
            string lname = LNameTxt.Text;
            List<SystemUser> userslist = SystemUser_DataProvider.GetSystemUser(uname);
            usersGrid.DataSource = userslist;
            ResultNumberLbl.Text = userslist.Count.ToString();
            try
            {
                usersGrid.PageIndex = e.NewPageIndex;
                usersGrid.DataBind();
            }
            catch
            {
                usersGrid.PageIndex = 0;
                usersGrid.DataBind();
            }
        }

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditUser.aspx?act=new");
        }


    }
}