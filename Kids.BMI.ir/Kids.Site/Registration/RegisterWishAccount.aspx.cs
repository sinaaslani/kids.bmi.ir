using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility.UtilExtension.StringExtensions;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class RegisterWishAccount : KidsSecureFormBaseClass
    {
        protected override void CheckKidsUser()
        {
            if (OnlineKidsUser == null || OnlineKidsUser.Kids_UserInfo == null)
            {
                Response.Redirect("~/Register.aspx");
                return;
            }

            switch ((KidsUserStatus)OnlineKidsUser.Kids_UserInfo.CurrentStatus)
            {
                case KidsUserStatus.RegisterWithoutConfirmation:
                    Response.Redirect("~/Register.aspx");
                    break;

                case KidsUserStatus.WaiteForAccCreation_Failed:
                case KidsUserStatus.WaiteForAccCreation_FailedSabt:
                    Response.Redirect("~/Profile.aspx");
                    break;


                case KidsUserStatus.RegisterdWithNoAcc:
                    pnlAccRegister.Visible = true;
                    break;

                case KidsUserStatus.RegisterdCompletly:
                    Response.Redirect("~");
                    break;

                case KidsUserStatus.WaiteForAccCreation:
                case KidsUserStatus.WaiteForAccCreation_WithSabtConfirmation:
                case KidsUserStatus.WaiteForAccCreation_SentToSabt:
                    pnlWaiteForAccCreation.Visible = true;
                    ucKidsUserSateWidget.SetUserInfo( OnlineKidsUser.Kids_UserInfo);
                    break;

                case KidsUserStatus.AccCreated_WaiteForDBCR:
                    Response.Redirect("~");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (OnlineKidsUser != null && OnlineKidsUser.Kids_UserInfo != null)
                {
                    //pnlAgreement.Visible = true;
                    var currentuser = OnlineKidsUser.Kids_UserInfo;
                    List<CustomerAccInfo> parentBMIAccounts = BMICustomer_DataProvider.GetAccByMellicode(currentuser.ParentMelliCode);

                    if (parentBMIAccounts.Any())
                    {
                        pnlAccRegisterS12.Visible = true;

                        if (currentuser.HasSuccessPayment())
                            pnlAutoCreateAccount_SuccessPayment.Visible = true;
                        else
                            pnlSelectInterviewAccountMethod.Visible = true;
                    }
                    else
                        pnlAccRegisterS3.Visible = true;

                    foreach (ListItem item in rdoAddAccMethod.Items)
                        item.Text = string.Format(item.Text, currentuser.ChildName + " " + currentuser.ChildFamily, currentuser.ChildMelliCode);

                }
            }

        }

        protected void rdoAddAccMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoAddAccMethod.SelectedValue == "1")
            {
                pnlAutoCreateAccount.Visible = false;
                pnlAutoCreatAccountInformationCompletion.Visible = false;
                pnlAutoCreateAccount_PaymentFactor.Visible = false;
                pnlAutoCreatAccountWithConfirmation.Visible = false;
                pnlInterviewAccount.Visible = true;

            }
            else
            {
                pnlInterviewAccount.Visible = false;
                var currentuser = OnlineKidsUser.Kids_UserInfo;

                if (!currentuser.IsRegistrionInformationComplete())
                {
                    pnlAutoCreatAccountInformationCompletion.Visible = true;
                    return;
                }

                List<CustomerAccInfo> childBMIAccounts = BMICustomer_DataProvider.GetAccByMellicode(currentuser.ChildMelliCode);

                if (childBMIAccounts.Any())
                {
                    pnlAutoCreateAccount.Visible = true;
                }
                else
                {
                    pnlAutoCreatAccountWithConfirmation.Visible = true;
                }
            }
        }

        protected void btnHavingAccountWish_Click(object sender, EventArgs e)
        {
            if (!txtHavingAccountWish.Text.IsBMIValidAccountNo())
            {
                ShowMessageBox("شماره حساب وارد شده نامعتبر است", "ثبت اطلاعات حساب آرزو", MessageBoxType.Error);
                return;
            }

            if (!txtHavingAccountWish.Text.Trim().StartsWith("02"))
            {
                ShowMessageBox("شماره حساب وارد شده میبایست حساب کوتاه مدت باشد", "ثبت اطلاعات حساب آرزو", MessageBoxType.Error);
                return;
            }

            var currentuser = OnlineKidsUser.Kids_UserInfo;
            var acclist = BMICustomer_DataProvider.GetAccByMellicode(currentuser.ChildMelliCode);
            if (acclist.All(o => o.ac_num != txtHavingAccountWish.Text))
            {
                ShowMessageBox("این شماره حساب متعلق به شما نمیباشد.لطفا شماره حساب صحیح خود را وارد نمایید", "خطا در اعتبار سنجی شماره حساب", MessageBoxType.Information);
                return;
            }

            currentuser.MarkAsModified();
            currentuser.ChildAccNo = txtHavingAccountWish.Text;

            currentuser.StatusHistory = string.Format("{0},{1}", currentuser.CurrentStatus, (int)KidsUserStatus.RegisterdCompletly);
            currentuser.CurrentStatus = (int)KidsUserStatus.RegisterdCompletly;


            KidsUser_DataProvider.SaveKidsUser(currentuser, this, RefreshOnlineKidsUserInfo);
            ShowMessageBox("اطلاعات حساب شما با موفقیت ثبت شد", "ثبت اطلاعات حساب آرزو", MessageBoxType.Information);
            pnlAccRegisterS12.Visible = false;
        }

        protected void btnAcceptAgreement_Click(object sender, EventArgs e)
        {
            pnlAutoCreateAccount_PaymentFactor.Visible = true;
            pnlAutoCreatAccountWithConfirmation.Visible = false;
            pnlAutoCreateAccount.Visible = false;
            pnlAgreement.Visible = false;
        }

        protected void btnPayment_CreateAccount_Click(object sender, EventArgs e)
        {
            pnlAgreement.Visible = true;
            pnlAutoCreatAccountWithConfirmation.Visible = false;
        }

        protected void btnCancelCreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RegisterWishAcc.aspx");
        }

        protected void btnVerifyChildWishAccount_Click(object sender, EventArgs e)
        {
            if (!txtHaveNewWishAccount.Text.IsBMIValidAccountNo())
            {
                ShowMessageBox("شماره حساب وارد شده نامعتبر است", "ثبت اطلاعات حساب آرزو", MessageBoxType.Error);
                return;
            }
            if (!txtHaveNewWishAccount.Text.StartsWith("02"))
            {
                ShowMessageBox("شماره حساب وارد شده میبایست حساب کوتاه مدت باشد", "ثبت اطلاعات حساب آرزو", MessageBoxType.Error);
                return;
            }

            var currentuser = OnlineKidsUser.Kids_UserInfo;
            List<CustomerAccInfo> childBMIAccounts = BMICustomer_DataProvider.GetAccByMellicode(currentuser.ChildMelliCode);
            if (childBMIAccounts.Any(o => o.ac_num == txtHaveNewWishAccount.Text))
            {
                currentuser.MarkAsModified();

                currentuser.ChildAccNo = txtHaveNewWishAccount.Text;
                currentuser.StatusHistory = string.Format("{0},{1}", currentuser.CurrentStatus, (int)KidsUserStatus.RegisterdCompletly);
                currentuser.CurrentStatus = (int)KidsUserStatus.RegisterdCompletly;

                KidsUser_DataProvider.SaveKidsUser(currentuser, this, RefreshOnlineKidsUserInfo);

                ShowMessageBox("اطلاعات حساب شما با موفقیت ثبت شد", "ثبت اطلاعات حساب آرزو", MessageBoxType.Information);
                lblVerifyResult.Text = "اطلاعات حساب شما با موفقیت ثبت شد";
                pnlHaveNewWishAccount3.Visible = true;
                pnlHaveNewWishAccount2.Visible = false;
                pnlHaveNewWishAccount1.Visible = false;
            }
            else
            {
                ShowMessageBox("شماره حساب وارد شده متعلق به شما نمیباشد است", "ثبت اطلاعات حساب آرزو", MessageBoxType.Error);

            }
        }



        protected void btnRejectAgreement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RegisterWishAcc.aspx");
        }
    }
}