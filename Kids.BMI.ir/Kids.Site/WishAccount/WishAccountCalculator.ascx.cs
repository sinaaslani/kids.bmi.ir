using System;
using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class WishAccountCalculatorascx : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Initialize()
        {
            var user = KidsOnlineUser.Kids_UserInfo;
            if (!user.Kids_Wishes.Any())
            {
                ShowMessageBox(@"جهت استفاده از محاسبه گر آرزو میبایست ابتدا از قسمت 
                                    <BR>
                                ""انتخاب حساب آرزو""
                                آرزوی خود را انتخاب نمایید.
                                ", "محاسبه گر آرزو");
                pnlMain.Visible = false;
            }
            else
            {
                pnlMain.Visible = true;

                string lastDate;
                long remain = BMICustomer_DataProvider.GetAccRemain(user, out lastDate);
                lblCurrnrRemain.Text = remain.ToString().Money3Dispaly();
            }
        }

        protected void btnCalculateWishDuration_Click(object sender, EventArgs e)
        {
            var user = KidsOnlineUser.Kids_UserInfo;
            var WishAmount = user.Kids_Wishes.First().Wish.WishAmount;
            var CurrentRemain = lblCurrnrRemain.Text.Replace(",","").ToLong();
            var PaymentperMonth = txtPeymentPerMonth.Text.ToLong();

            int counter = 0;
            double TempSum = 0;
            while (true)
            {
                const double benefitPecent = 0.07 / 12;
                TempSum += CurrentRemain + (benefitPecent * CurrentRemain) + PaymentperMonth;
                if (TempSum >= WishAmount)
                    break;
                counter++;
            }

            lblResult.Text = string.Format("مدت زما لازم جهت دستیابی به آرزو : {0} ماه  ", counter.ToString());

        }
    }
}