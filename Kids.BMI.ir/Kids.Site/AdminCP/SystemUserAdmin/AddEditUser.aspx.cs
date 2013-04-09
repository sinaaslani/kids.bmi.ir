using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Threading;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.SystemUserAdmin
{
    public partial class AddEditUser : AdminSecureFormBaseClass
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
                BindRoles();
                string action = UtilityMethod.GetRequestParameter("act");
                if (action.ToLower() == "edit")
                {
                    int userId = -1;
                    if (UtilityMethod.GetRequestParameter("uid").IsInt32())
                        userId = UtilityMethod.GetRequestParameter("uid").ToInt32();

                    SystemUser user = SystemUser_DataProvider.GetSystemUser(UserId: userId).FirstOrDefault();

                    if (user!=null)
                    {
                        txtFName.Text = user.Name;
                        txtLName.Text = user.Family;
                        txtUserName.Text = user.SSOUserName;
                        ApprovedChkBox.Checked = user.Active;

                        foreach (ListItem roleItem in chkBoxObjectRoles.Items)
                        {
                            if (user.SystemRoles.Any(o => o.RoleId == Convert.ToInt32(roleItem.Value)))
                                roleItem.Selected = true;
                        }
                    }
                }
                else if (action.ToLower() == "new")
                {
                }
            }
        }

        private void BindRoles()
        {
            List<SystemRole> rolesList = SystemUser_DataProvider.GetRoles();
            foreach (SystemRole role in rolesList)
            {
                ListItem li = new ListItem(role.RoleDescription, role.RoleId.ToString());
                li.Attributes.Add("title", role.RoleName);
                chkBoxObjectRoles.Items.Add(li);
            }
        }


        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("AdminUsers.aspx");
        }


        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SystemUser user = null;
                if (UtilityMethod.GetRequestParameter("uid").IsInt32())
                {
                    int? userId = UtilityMethod.GetRequestParameter("uid").ToInt32();
                    user = SystemUser_DataProvider.GetSystemUser(UserId: userId).FirstOrDefault();
                }

                if (user != null)
                {
                    user.MarkAsModified();

                    user.SSOUserName = txtUserName.Text;
                    user.Name = txtFName.Text;
                    user.Family = txtLName.Text;
                    user.Active = ApprovedChkBox.Checked;

                    foreach (ListItem li in chkBoxObjectRoles.Items)
                    {
                        var RoleId = Convert.ToInt32(li.Value);
                        if (li.Selected)
                        {
                            SystemRole Role = SystemUser_DataProvider.GetRoles(RoleId).First();
                            if (user.SystemRoles.All(o => o.RoleId != Role.RoleId))
                                user.SystemRoles.Add(Role);
                        }
                        else
                        {
                            if (user.SystemRoles.Any(o => o.RoleId == RoleId))
                                user.SystemRoles.Remove(user.SystemRoles.First(o => o.RoleId == RoleId));
                        }
                    }
                    SystemUser_DataProvider.SaveSystemUser(user);
                }
                else
                {
                    user = new SystemUser
                                        {
                                            SSOUserName = txtUserName.Text.Trim(),
                                            Name = txtFName.Text,
                                            Family = txtLName.Text,
                                            Active = ApprovedChkBox.Checked,
                                        };
                    foreach (ListItem li in chkBoxObjectRoles.Items)
                    {
                        var RoleId = Convert.ToInt32(li.Value);
                        if (li.Selected)
                        {
                            SystemRole Role = SystemUser_DataProvider.GetRoles(RoleId).First();
                            user.SystemRoles.Add(Role);
                        }
                    }
                    SystemUser_DataProvider.SaveSystemUser(user);
                }

                Page.Response.Redirect("AdminUsers.aspx");
            }
            catch (ThreadAbortException) { }
            catch (Exception exp)
            {
                string msg;
                if (exp.InnerException != null && exp.InnerException.ToString().Contains("IX_SystemUser_UniqueUserName"))
                    msg = "این نام کاربری قبلا انتخاب شده است.";
                else
                    msg = exp.Message + " :::  " + exp.InnerException;
                lblError.Text = msg;
            }
        }


    }
}