using System;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.KidsGame;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class KidsBirthday : KidsSecureFormBaseClass
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (OnlineKidsUser != null && OnlineKidsUser.Kids_UserInfo != null &&
                OnlineKidsUser.Kids_UserInfo.IsBirthDay())
            {
                var user = OnlineKidsUser.Kids_UserInfo;

                imgKidPic.ImageUrl = string.Format("~/JpegImage.aspx?act=1&dt={0}", DateTime.Now);
                lblKidsUserName.Text = string.Format("{0} {1}", user.ChildName, user.ChildFamily);
                lblKidsUserAge.Text = user.ChildAge.ToInt32().ToString();
                string BirthDayFileAddress = string.Format("~/KidsGame/GameFileManager.aspx?lid={0}",
                                                           GetTemproryLink(user));
                string GlobalSWfurl = ResolveUrl(BirthDayFileAddress);
                lblSWF.Text = UserControlBaseClass.SWF_tag(GlobalSWfurl, 500, 500);

            }
            else
                Response.Redirect("~/جزیره آرزوها.aspx");
        }

        private Guid GetTemproryLink(KidsUser user)
        {
            return TempLinkManager.Instanse.AddLink(MapPath(string.Format("/AdminCP/Files/BirthDay/{0}.swf", user.ChildAge.ToInt32())));
        }

        protected override void CheckKidsUser()
        {

        }
    }
}