using System;
using System.Linq;
using Kids.Utility.UtilExtension.StringExtensions;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class WishWidget : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                UpdateWish();
        }

        private void UpdateWish()
        {
            var ouser = KidsSecureFormBaseClass.OnlineKidsUser;
            if (ouser != null && ouser.Kids_UserInfo != null && ouser.Kids_UserInfo.Kids_Wishes.Any())
            {
                var user = KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo;
                var wish = user.Kids_Wishes.First().Wish;
                imgWishPicSmall.ImageUrl = string.Format("~/AdminCP/Files/Wish/{0}", wish.WiShPicSmall);
                lblWishAmount.Text = wish.WishAmount.ToString().Money3Dispaly().ToPersinDigit();
                lblWishDescription.Text = wish.WishDescription.Length > 50
                                              ? wish.WishDescription.Substring(0, 50) + "..."
                                              : wish.WishDescription;
                lblWishName.Text = wish.WishName;
            }
            else
            {
                lnkWish.Visible = false;
                pnlMessage.Visible = true;
            }
        }
    }
}