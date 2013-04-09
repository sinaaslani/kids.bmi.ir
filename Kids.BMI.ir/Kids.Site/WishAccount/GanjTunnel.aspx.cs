using System;
using Kids.EntitiesModel;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class GanjTunnel : KidsSecureFormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (OnlineKidsUser != null && OnlineKidsUser.Kids_UserInfo.CanUseWishAccBenefits())
            {
                if (!IsPostBack)
                {
                    pnlMain.Visible = true;
                    BindGanjTunelGrid();
                }
            }
        }

        private void BindGanjTunelGrid()
        {
            int Count;
            var lst = DynamicPages_DataProvider.GetDynamicPage(out Count, PageTypeIds: new[] { 3 });
            DataList1.DataSource = lst;
            DataList1.DataBind();
        }

        protected override void CheckKidsUser()
        {
            if (OnlineKidsUser == null || !OnlineKidsUser.Kids_UserInfo.CanUseWishAccBenefits())
            {
                ShowMessageBox("کاربر گرامی مراحل عضویت شما در این سامانه هنوز تکمیل نگردیده است.شما میبایست حساب آرزو معتبر خود را در سایت ثبت نمایید.",
                    "", MessageBoxType.Information);

            }
        }
    }
}