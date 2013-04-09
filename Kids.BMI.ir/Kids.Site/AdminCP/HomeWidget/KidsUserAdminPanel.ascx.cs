using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.HomeWidget
{

    public partial class KidsUserAdminPanel : UserControlBaseClass
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (AdminOnlineUser != null && (AdminOnlineUser.IsKidsUserManager || AdminOnlineUser.IsSiteAdministrator))
            {
                lnkManageGeustUsers.Visible = true;
                lnkManageUsers.Visible = true;
            }

        }


    }
}
