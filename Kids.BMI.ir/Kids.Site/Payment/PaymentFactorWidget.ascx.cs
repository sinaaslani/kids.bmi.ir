using System;
using System.Web;
using BMIBranch.ServiceProxy;
using EPS.MerchantHelper;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Payment
{
    public partial class PaymentFactorWidget : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            if (!txtAmount.Text.IsInt64())
            {
                ShowMessageBox("مبلغ وارد شده نامعتبر است", "خطا", MessageBoxType.Error);
                return;
            }
            if (txtAmount.Text.ToLong() < 100000)
            {
                ShowMessageBox("مبلغ وارد شده نامعتبر است", "خطا", MessageBoxType.Error);
                return;
            }

            if (!txtChildBranchCode.Text.IsInt32() ||
                BMIBranchProvider.FindUnits(txtChildBranchCode.Text.ToInt32()) == null)
            {
                ShowMessageBox("کد شعبه وارد شده نامعتبر است", "خطا", MessageBoxType.Error);
                return;
            }

            var currentuser = KidsOnlineUser.Kids_UserInfo;

            string RequestKey;
            long Amount = SystemConfigs.IsInTestMode ? 1 : txtAmount.Text.ToLong() + 40000;
            long OrderId = OrderIdGenerator.Instance.GetNextOrderId();
            string AdditionalData = string.Format("پرداخت مبلغ افتتاح حساب آرزو" + "==>{0}", KidsOnlineUser.SSOUser.UserID);
            string CustomerEmailAddress = KidsOnlineUser.SSOUser.Email;

            string POSTHTML = MerchantHelpers.PaymentUtility(SystemConfigs.ServiceUrl, true, SystemConfigs.PaymentUrl, SystemConfigs.MerchantId,
                                                             Amount, OrderId, SystemConfigs.TransactionKey, SystemConfigs.TerminalId, SystemConfigs.ReturnURL,
                                                             AdditionalData, CustomerEmailAddress, "", out RequestKey);
            POSTHTML += @"</form>";


            currentuser.ChildAccBranchNo = txtChildBranchCode.Text.ToInt32();
            var CurrentTransaction = new KidsUsers_Payments
                {
                    Amount = Amount,
                    CreateDateTime = DateTime.Now,
                    OrderId = OrderId,
                    RequestKey = RequestKey,
                    UserIPAddress = HttpContext.Current.Request.UserHostAddress,

                };
            currentuser.KidsUsers_Payments.Add(CurrentTransaction);
            KidsUser_DataProvider.SaveKidsUser(currentuser, this, KidsSecureFormBaseClass.RefreshOnlineKidsUserInfo);


            SessionItems.CurrentTransaction = CurrentTransaction;
            SessionItems.FormBody = POSTHTML;

            Response.Redirect("~/Payment/MerchantBeforePost.aspx", true);

            /////////////////////////////////

        }

        protected void txtChildBranchCode_TextChanged(object sender, EventArgs e)
        {
            lblUnitInfo.Text = "";
            if (txtChildBranchCode.Text.Length >= 3)
            {
                lblUnitInfo.Text = "کد شعبه نامعتبر است";
                if (txtChildBranchCode.Text.IsInt32())
                {
                    var unit = BMIBranchProvider.FindUnits(txtChildBranchCode.Text.ToInt32());
                    if (unit != null)
                        lblUnitInfo.Text = string.Format("{0}({1})", unit.Title, unit.UnitId);
                    
                }
            }

        }
    }
}