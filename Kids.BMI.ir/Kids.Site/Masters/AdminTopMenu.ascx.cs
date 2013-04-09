using System;
using System.Web.Security;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Masters
{
    public partial class AdminTopMenu : UserControlBaseClass
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (AdminOnlineUser != null)
                {
                    var user = AdminOnlineUser.System_UserInfo;
                    lblCurrentUser.Text = string.Format("{0} ( {1} )", AdminOnlineUser.SSOUser.Name, user.SSOUserName);
                }
            }
            catch (ApplicationException)
            {
                Response.Redirect("~/Error/NotAccess.aspx");
            }

        }


        protected void logOffLink_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            AdminSecureFormBaseClass.BMIUserInteraction.LogOutCurrentUser(Request.Url.ToString());
        }


    }
}