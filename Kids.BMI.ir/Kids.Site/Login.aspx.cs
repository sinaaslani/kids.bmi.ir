using System;
using System.Web;
using System.Web.UI;
using Kids.EntitiesModel;
using Kids.Utility.UtilExtension.StringExtensions;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (FormBaseClass.GeustKidsUser != null)
                //Response.Redirect("~/جزیره آرزوها.aspx");
                FormBaseClass.GeustKidsUser = null;

                KidsSecureFormBaseClass.RefreshOnlineKidsUserInfo(this);
                if (KidsSecureFormBaseClass.OnlineKidsUser != null)
                {
                    var user = KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo;
                    if (user == null || !user.IsBirthDay())
                        Response.Redirect("~/جزیره آرزوها.aspx");
                    else
                        Response.Redirect("~/KidsBirthday.aspx");
                }
            }
        }

        protected void btnKidsIslandHomeUser_Click(object sender, ImageClickEventArgs e)
        {
            KidsSecureFormBaseClass.BMIUserInteraction.SendToLoginPage(Request["ReturnUrl"] ?? HttpContext.Current.Request.Url.ToString());
        }

        protected void btnLoginTravelers_Click(object sender, ImageClickEventArgs e)
        {
            if (!txtMelliCode.Text.IsValidMelliCode())
            {
                MessageBoxHelper.ShowMessageBox(this, "کد ملی نامعتبراست", "", MessageBoxType.Information);
                return;
            }
            if (!txtMobileNumber.Text.IsValidMobileNumber())
            {
                MessageBoxHelper.ShowMessageBox(this, "شماره همراه نامعتبراست", "", MessageBoxType.Information);
                return;
            }
            var gUser = new GeustKidsUser
            {
                GeustUserId = Guid.NewGuid(),
                EmailAddress = txtEmailAddress.Text,
                Name = txtName.Text,
                Family = txtFamily.Text,
                MelliCode = txtMelliCode.Text,
                MobileNumber = txtMobileNumber.Text,
                CreateDateTime = DateTime.Now
            };
            KidsUser_DataProvider.SaveGeustKidsUser(gUser);
            FormBaseClass.GeustKidsUser = gUser;

            Response.Redirect(Request["ReturnUrl"] ?? "~/جزیره آرزوها.aspx");
        }



    }
}