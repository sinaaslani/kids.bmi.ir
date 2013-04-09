using System;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class KidsBirthDayWidget : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (KidsOnlineUser != null && KidsOnlineUser.Kids_UserInfo != null)
            {
                var user = KidsOnlineUser.Kids_UserInfo;
                if (!user.IsBirthDay())
                    Visible = false;
                //imgBirthDayPic.ImageUrl = string.Format("/AdminCP/Files/BirthDay/{0}.PNG",user.ChildAge.ToInt32());
            }
        }
    }
}