using System;
using System.IO;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class InviteFriend : KidsSecureFormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInviteFriend_Click(object sender, EventArgs e)
        {
            var user = OnlineKidsUser.Kids_UserInfo;
            string HTMLBody = File.ReadAllText(Server.MapPath("~/Registration/InvitationEmailTemplate.htm"));
            HTMLBody = HTMLBody.Replace("@@CHILDNAME@@", user.ChildName + " " + user.ChildFamily);
            HTMLBody = HTMLBody.Replace("@@CHILDUSERID@@", user.KidsUserId.ToString());

#if(DEBUG)
            HTMLBody = HTMLBody.Replace("@@REGISTRATIONADDRESS@@", string.Format("http://localhost:7008/Register.aspx?inid={0}", user.KidsUserId));
#else
            HTMLBody = HTMLBody.Replace("@@REGISTRATIONADDRESS@@", string.Format("http://Kids.bmi.ir/Register.aspx?inid={0}", user.KidsUserId));
#endif

            string To = txtFriendEmailAddress.Text;
            try
            {
                MailingHelper.SendEmail("Kids@BMI.IR", "کانون جوانه های بانک ملی ایران", To,
                                        "شما از طرف دوستتان به سایت جوانه های بانک ملی ایران دعوت شدید", HTMLBody);
                ShowMessageBox("دعوتنامه با موفقیت ارسال شد", "ارسال دعوتنامه", MessageBoxType.Information);
                //ClientRedirect("~", 3000);
                pnlMain.Visible = false;
            }
            catch (Exception ex)
            {
                ShowMessageBox("خطا در ارسال ایمیل" + ex.Message, "ارسال دعوتنامه", MessageBoxType.Error);
            }
        }

        protected override void CheckKidsUser()
        {

        }
    }
}