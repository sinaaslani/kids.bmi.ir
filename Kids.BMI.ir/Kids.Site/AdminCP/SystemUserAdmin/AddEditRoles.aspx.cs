using System;
using System.Collections.Generic;
using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.SystemUserAdmin
{
    public partial class AddEditRoles : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsSystemUserManager || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            switch (action.ToLower())
            {
                case "new":
                    HeaderLbl.Text = " ايجاد يک نقش مسووليتي ";
                    createEditBtn.Text = " ايجاد  ";
                    break;
                case "edit":
                    createEditBtn.Text = " ويرايش و بهنگام ";
                    //roleNameTxt.ReadOnly = true;
                    HeaderLbl.Text = " ويرايش يک نقش مسووليتي ";
                    if (!Page.IsPostBack)
                        setInitiateValue();
                    break;
                case "delete":
                    createEditBtn.Text = " حذف ";
                    HeaderLbl.Text = " حذف يک نقش مسووليتي ";
                    if (!Page.IsPostBack)
                        setInitiateValue();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void setInitiateValue()
        {
            int? roleId = null;
            if (UtilityMethod.GetRequestParameter("role").IsInt32())
                roleId = UtilityMethod.GetRequestParameter("role").ToInt32();

            List<SystemRole> RoleList = SystemUser_DataProvider.GetRoles(roleId);
            if (RoleList.Count > 0)
            {
                SystemRole role = RoleList[0];
                roleNameTxt.Text = role.RoleName;
                descriptionTxt.Text = role.RoleDescription;
            }
            else
                throw new InvalidOperationException();
        }

        protected void CreateEditBtn_Click(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            if (Page.IsValid)
            {
                if (action.ToLower() == "edit")
                {
                    int roleId = -1;
                    if (UtilityMethod.GetRequestParameter("role").IsInt32())
                        roleId = Convert.ToInt32(UtilityMethod.GetRequestParameter("role"));

                    SystemRole role = SystemUser_DataProvider.GetRoles(roleId).FirstOrDefault();
                    if (role != null)
                    {
                        role.MarkAsModified();
                        role.RoleDescription = descriptionTxt.Text;
                        role.RoleName = roleNameTxt.Text;

                        SystemUser_DataProvider.SaveRoles(role);
                        Page.Response.Redirect("AdminRoles.aspx");
                    }
                    else
                        throw new InvalidOperationException();
                }
                else if (action.ToLower() == "delete")
                {
                    int roleId = -1;
                    if (UtilityMethod.GetRequestParameter("role").IsInt32())
                        roleId = UtilityMethod.GetRequestParameter("role").ToInt32();

                    SystemRole role = SystemUser_DataProvider.GetRoles(roleId).FirstOrDefault();
                    role.MarkAsDeleted();
                    SystemUser_DataProvider.SaveRoles(role);
                    Page.Response.Redirect("AdminRoles.aspx");
                }
                else if (action.ToLower() == "new")
                {
                    SystemRole role = new SystemRole { RoleName = roleNameTxt.Text, RoleDescription = descriptionTxt.Text };
                    SystemUser_DataProvider.SaveRoles(role);
                    Page.Response.Redirect("AdminRoles.aspx");
                }
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("AdminRoles.aspx");
        }


    }
}