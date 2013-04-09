using System;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class WishList : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void BindWishes()
        {
            var wishList = Wish_DataProvider.GetWish();
            dgWish.DataSource = wishList;
            dgWish.DataBind();
        }

        protected void dgWish_ItemCommand(object source, DataListCommandEventArgs e)
        {
            var wish = Wish_DataProvider.GetWish(e.CommandArgument.ToInt32()).FirstOrDefault();
            if (wish != null)
            {
                pnlWishDetails.Visible = true;
                hdnWishId.Value = wish.WishId.ToString();
                imgWishPic.ImageUrl = string.Format("~/AdminCP/Files/Wish/{0}", wish.WishPic);
                imgWishPicSmall.ImageUrl = string.Format("~/AdminCP/Files/Wish/{0}", wish.WiShPicSmall);
                lblWishAmount.Text = wish.WishAmount.ToString().Money3Dispaly().ToPersinDigit();
                lblWishDescription.Text = wish.WishDescription;
                lblWishName.Text = wish.WishName;
            }
        }

        protected void btnSelectWish_Click(object sender, EventArgs e)
        {
            try
            {
                var user = KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo;

                user.MarkAsModified();
                while (user.Kids_Wishes.Any())
                    user.Kids_Wishes.First().MarkAsDeleted();

                user.Kids_Wishes.Add(new Kids_Wishes { WishId = hdnWishId.Value.ToInt32(), CreateDateTime = DateTime.Now });

                KidsUser_DataProvider.SaveKidsUser(user, this, KidsSecureFormBaseClass.RefreshOnlineKidsUserInfo);
                RefreshOnlineKidsUserInfo();
                ShowMessageBox("آرزوی شما در سیستم ثبت شد", "انتخاب آرزو", MessageBoxType.Information);
                Response.Redirect("~/WishAcc.aspx");
                pnlWishDetails.Visible = false;
            }
            catch (Exception ex)
            {
                ShowMessageBox("خطا در ثبت اطلاعات :" + ex.Message, "خطا");
            }
        }
    }
}