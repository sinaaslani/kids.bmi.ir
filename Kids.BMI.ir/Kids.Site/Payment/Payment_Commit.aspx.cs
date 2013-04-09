using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.LoggingHelper;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;
using EPS.MerchantHelper;
using EPS.MerchantHelper.MerchantUtilityWebRef;

namespace Site.Kids.bmi.ir.Payment
{
    public partial class Payment_Commit : KidsSecureFormBaseClass
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KidsUsers_Payments CurrentTransaction = SessionItems.CurrentTransaction;
                Session.Abandon();
                /////////////Retrivare Transaction Info
                if (CurrentTransaction == null)
                {
                    if (HttpContext.Current.Request.Form["OrderId"].IsInt64())
                    {
                        long PostOrderId = Convert.ToInt64(HttpContext.Current.Request.Form["OrderId"]);
                        var UserOfTransaction = KidsUser_DataProvider.GetKidsUser(OrderId: PostOrderId).FirstOrDefault();
                        if (UserOfTransaction == null)
                        {
                            lblMessage.Text = "اطلاعات خريد موجود نيست";
                            return;
                        }
                        CurrentTransaction = UserOfTransaction.GetPaymentByOrderId(PostOrderId);
                    }
                    else
                    {
                        lblMessage.Text = "اطلاعات خريد ارسال نشده است";
                        return;
                    }
                }
                else
                {
                    CurrentTransaction = KidsUser_DataProvider.GetKidsUser(PaymentId: CurrentTransaction.PaymentId).FirstOrDefault().GetPaymentByPaymentId(CurrentTransaction.PaymentId);
                }

                VerifyTransaction(CurrentTransaction);

            }
        }

        private void VerifyTransaction(KidsUsers_Payments CurrentTransaction)
        {
            if (CurrentTransaction.AppStatusCode == null)
            {
                try
                {
                    CheckStatusResult GetStatus_Result = MerchantHelpers.CheckRequestStatus(SystemConfigs.ServiceUrl, true,
                                                                                          CurrentTransaction.OrderId,
                                                                                          SystemConfigs.MerchantId, SystemConfigs.TerminalId,
                                                                                          SystemConfigs.TransactionKey,
                                                                                          CurrentTransaction.RequestKey,
                                                                                          CurrentTransaction.Amount
                        );
                    if (GetStatus_Result.AppStatusCode == 0 && GetStatus_Result.AppStatusDescription == "COMMIT")
                    {
                        UpdateKidsUserPayment(CurrentTransaction, GetStatus_Result);
                        pnlResult.Visible = true;
                        lblAmount.Text = CurrentTransaction.Amount.ToString();
                        lblAmount_Letter.Text = CurrentTransaction.Amount.ToString().num2str();
                        lblOrderId.Text = CurrentTransaction.OrderId.ToString();
                        lblTraceNo.Text = CurrentTransaction.SystemTraceNo.ToString();
                        lblRefrenceNo.Text = CurrentTransaction.RetrivalRefNo;
                        lblTranDate.Text = PersianDateTime.MiladiToPersian(DateTime.Now).ToLongDateTimeString();

                        lblMessage.Text = @"پرداخت شما با موفقیت انجام گرفته است.<br>
                                              شما از 72 ساعت دیگر با مراجعه به این سایت میتوانید از شماره حساب خود آگاه گردیده و با مراجعه به شعب بانک ملی نسبت به فعالسازی حساب خود و دریافت کارت حساب خود اقدام نمایید";
                    }
                    else
                    {
                        SetFailedTransaction(CurrentTransaction, GetStatus_Result);
                    }
                }
                catch (Exception exp)
                {
                    LogUtility.WriteEntryEventLog("CheckStatusHelper", exp, EventLogEntryType.Error);
                    lblMessage.Text =
                        "(03)با عرض پوزش در حال حاضر امکان سرویس دهی موجود نمی باشد.در صورت کسر مبلغ از حساب شما , مبلغ بطور خودکار به حساب شما بازگشت داده خواهد شد.";
                }
            }
            else
                lblMessage.Text =
                    "عملیات لازم جهت پردازش این تراکنش قبلا انجام شده است.جهت مشاهده مجدد آن به اطلاعات ارسالی به آدرس ایمیل خود مراجعه فرمایید";
        }

        private void SetFailedTransaction(KidsUsers_Payments payment, CheckStatusResult PaymentResult)
        {
            payment.MarkAsModified();
            payment.RetrivalRefNo = PaymentResult.RefrenceNumber;
            payment.SystemTraceNo = PaymentResult.TraceNo;
            payment.AppStatusCode = PaymentResult.AppStatusCode;
            payment.AppStatusDescription = PaymentResult.AppStatusDescription;
            payment.CustomerCardNo = PaymentResult.CustomerCardNumber;


            KidsUser_DataProvider.SaveKidsUserPayment(payment);
            RefreshOnlineKidsUserInfo(false);
            ShowMessageBox(string.Format(@"خطا در پرداخت:<br>
                            کد خطا :{0}<br>
                            شرح خطا :{1}
                            ", payment.AppStatusCode, payment.AppStatusDescription),
                            "خطا در پرداخت", MessageBoxType.Error);
        }

        private void UpdateKidsUserPayment(KidsUsers_Payments payment, CheckStatusResult PaymentResult)
        {
            var currentuser = payment.KidsUser;
            currentuser.MarkAsModified();

            payment.MarkAsModified();
            payment.RetrivalRefNo = PaymentResult.RefrenceNumber;
            payment.SystemTraceNo = PaymentResult.TraceNo;
            payment.AppStatusCode = PaymentResult.AppStatusCode;
            payment.AppStatusDescription = PaymentResult.AppStatusDescription;
            payment.CustomerCardNo = PaymentResult.CustomerCardNumber;


            currentuser.ChildAccNo = null;
            currentuser.StatusHistory = string.Format("{0},{1}", currentuser.CurrentStatus, (int)KidsUserStatus.WaiteForAccCreation);



            List<CustomerAccInfo> childBMIAccounts = BMICustomer_DataProvider.GetAccByMellicode(currentuser.ChildMelliCode);
            List<CustomerAccInfo> parentBMIAccounts = BMICustomer_DataProvider.GetAccByMellicode(currentuser.ParentMelliCode);

            if (parentBMIAccounts.Any())
            {
                if (childBMIAccounts.Any())
                {
                    currentuser.CurrentStatus = (int)KidsUserStatus.WaiteForAccCreation;
                }
                else
                {
                    currentuser.CurrentStatus = (int)KidsUserStatus.WaiteForAccCreation_WithSabtConfirmation;
                }

            }
            else
            {
                throw new ApplicationException("Invalid User State");
            }


            KidsUser_DataProvider.SaveKidsUser(currentuser, this, RefreshOnlineKidsUserInfo);

            ShowMessageBox("اطلاعات حساب شما با موفقیت ثبت شد", "ثبت اطلاعات حساب آرزو", MessageBoxType.Information);
        }


        protected override void CheckKidsUser()
        {

        }
    }
}