using System;
using System.Web.Security;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Masters
{
    public partial class TopMenu : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lnkLogout.Visible = true;

                if (KidsOnlineUser != null &&
                    KidsOnlineUser.SSOUser != null &&
                    KidsOnlineUser.Kids_UserInfo != null &&
                    KidsOnlineUser.Kids_UserInfo.CurrentStatus != (int)KidsUserStatus.RegisterWithoutConfirmation

                    )
                {
                    var user = KidsOnlineUser.Kids_UserInfo;

                    lblCurrentUser.Text = string.Format("{0} {1} ", user.ChildName, user.ChildFamily);
                    imgKidPic.ImageUrl = string.Format("~/JpegImage.aspx?act=1&dt={0}", DateTime.Now);
                    imgKidPic.Visible = true;
                    lnkInviteFriend.Visible = true;
                    lnkProfile.Visible = true;

                    if (KidsOnlineUser.Kids_UserInfo != null)
                        lnkRegisterAccount.Visible = !KidsOnlineUser.Kids_UserInfo.CanUseWishAccBenefits();

                    if (user.IsBirthDay())
                        ucKidsBirthDayWidget.Visible = true;
                    else
                        ucWishWidget.Visible = true;

                }
                else
                {
                    if (FormBaseClass.GeustKidsUser != null)
                    {
                        var guser = FormBaseClass.GeustKidsUser;
                        lblCurrentGuestUser.Text = string.Format("{0} {1} :کاربر جاری", guser.Name, guser.Family);
                        //lnkLogout.Visible = true;
                    }

                    lnkRegister.Visible = true;

                }
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            if (KidsSecureFormBaseClass.OnlineKidsUser != null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                KidsSecureFormBaseClass.BMIUserInteraction.LogOutCurrentUser(Request.Url.ToString());
            }
            else if (FormBaseClass.GeustKidsUser != null)
            {
                FormBaseClass.GeustKidsUser = null;
                Response.Redirect("~/ورود.aspx");
            }
        }

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            KidsSecureFormBaseClass.BMIUserInteraction.SendToLoginPage(Request.Url.ToString());
        }

    }
}

