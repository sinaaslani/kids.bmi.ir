using System;
using Kids.EntitiesModel;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class WishAccHome : KidsSecureFormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (OnlineKidsUser != null && OnlineKidsUser.Kids_UserInfo.CanUseWishAccBenefits())
            {
                PageMaster.NewsMarque.NewsCategoryId = News_DataProvider.PreDefinedNewsCategory.WishAccount;

                string AccNumber = OnlineKidsUser.Kids_UserInfo.ChildAccNo;
                ucAccBill.AccountNumber = AccNumber;
                if (!string.IsNullOrWhiteSpace(AccNumber))
                    pnlmain.Visible = true;

            }
        }

        protected override void CheckKidsUser()
        {
            if (OnlineKidsUser == null ||
               !OnlineKidsUser.Kids_UserInfo.CanUseWishAccBenefits())
            {
                ShowMessageBox(
                    "کاربر گرامی مراحل عضویت شما در این سامانه هنوز تکمیل نگردیده است.شما میبایست حساب آرزو معتبر خود را در سایت ثبت نمایید.",
                    "", MessageBoxType.Information);
                // ClientRedirect("~/Register.aspx", 3000);
            }
        }

        protected void btnBill_Click(object sender, EventArgs e)
        {
            ucAccBill.Visible = true;
            ucWishAccountCalculator.Visible = false;
            ucWishList.Visible = false;
        }

        protected void btnWish_Click(object sender, EventArgs e)
        {

            ucAccBill.Visible = false;
            ucWishAccountCalculator.Visible = false;
            ucWishList.Visible = true;
            ucWishList.BindWishes();
        }

        protected void btnWishCalculator_Click(object sender, EventArgs e)
        {
            ucWishList.Visible = false;
            ucAccBill.Visible = false;
            ucWishAccountCalculator.Visible = true;
            ucWishAccountCalculator.Initialize();

        }
    }
}