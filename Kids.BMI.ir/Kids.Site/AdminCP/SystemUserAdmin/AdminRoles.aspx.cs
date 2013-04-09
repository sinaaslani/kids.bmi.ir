using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.SystemUserAdmin
{
    public partial class AdminRoles : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsSystemUserManager || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolesGrid.DataSource = SystemUser_DataProvider.GetRoles();
            RolesGrid.DataBind();
        }


        protected void RolesGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RoleId = (int)RolesGrid.DataKeys[RolesGrid.SelectedIndex].Value;
            List<SystemUser> Users = SystemUser_DataProvider.GetRolesWithUsers(RoleId).FirstOrDefault().SystemUsers.ToList();
            dgUserInRole.DataSource = Users;
            dgUserInRole.DataBind();
        }


        protected void dgUserInRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var RoleId = (int)RolesGrid.DataKeys[RolesGrid.SelectedIndex].Value.ToLong();
            var UserId = dgUserInRole.DataKeys[e.RowIndex].Value.ToLong();
            var u = SystemUser_DataProvider.GetSystemUser(UserId: UserId).First();

            u.MarkAsModified();
            u.SystemRoles.Remove(u.SystemRoles.First(o => o.RoleId == RoleId));
            
            SystemUser_DataProvider.SaveSystemUser(u);

            List<SystemUser> Users = SystemUser_DataProvider.GetRolesWithUsers(RoleId).First().SystemUsers.ToList();
            if (Users.Any())
                dgUserInRole.Focus();

            dgUserInRole.DataSource = Users;
            dgUserInRole.DataBind();
        }



    }
}