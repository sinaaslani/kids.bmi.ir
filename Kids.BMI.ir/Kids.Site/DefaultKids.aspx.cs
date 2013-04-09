using System;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir
{
    public partial class DefaultKids : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string GlobalSWfurl = ResolveUrl("~/App_Themes/Default/Movie/KidsIsland.swf");
            lblSWF.Text = UserControlBaseClass.SWF_tag(GlobalSWfurl, 980, 630);

            if (KidsSecureFormBaseClass.OnlineKidsUser != null && KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo != null)
            {
                var user = KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo;
                if (user.CurrentStatus == (int)KidsUserStatus.WaiteForAccCreation_Failed ||
                    user.CurrentStatus == (int)KidsUserStatus.WaiteForAccCreation_FailedSabt
                    )
                {
                    ShowMessageBox("در فرآیند افتتاح حساب خودکار شما مشکلی بروز کرده است.شما به طور خودکار به صفحه پروفایل هدایت خواهید شد تا اطلاعات هویتی خود را کنتری نمایید.");
                    ClientRedirect("~/Profile.aspx", 3000);
                }
            }
        }

    }
}