using System;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class Profile : KidsSecureFormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshOnlineKidsUserInfo();
               
                var user = OnlineKidsUser.Kids_UserInfo;
                if (user.CurrentStatus == (int)KidsUserStatus.WaiteForAccCreation_Failed ||
                    user.CurrentStatus == (int)KidsUserStatus.WaiteForAccCreation_FailedSabt
                    )
                {
                    ShowMessageBox("اطلاعات هویتی شما در حین فرآیند افتتاح حساب توسط سازمان ثبت احوال رد شده است.<BR>لطفا صحت اطلاعات اولیه خود شامل (نام- نام خانوادگی - نام پدر - شماره شناسنامه و کدملی) را در کمال دقت چک کرده و دکمه ثبت را فشار دهید.");
                    pnlEdit.Visible = true;
                    ucEditable_UserProfileWidget.SetUserInfo(OnlineKidsUser.Kids_UserInfo, true);
                    return;
                }

                pnlUserInfo.Visible = true;
                ucUserProfile.SetUserInfo(OnlineKidsUser.Kids_UserInfo);
               
            }
        }

        protected override void CheckKidsUser()
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var user = OnlineKidsUser.Kids_UserInfo;
            bool BasicInfoIsEditable = user.CurrentStatus == (int)KidsUserStatus.WaiteForAccCreation_Failed ||
                                       user.CurrentStatus == (int)KidsUserStatus.WaiteForAccCreation_FailedSabt;

            ucEditable_UserProfileWidget.SetUserInfo(OnlineKidsUser.Kids_UserInfo, BasicInfoIsEditable);
            pnlEdit.Visible = true;
            pnlWish.Visible = false;
            pnlUserInfo.Visible = false;
            pnlScore.Visible = false;
            pnlPayment.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var CurrentKidsUser = ucEditable_UserProfileWidget.GetUserInfo(OnlineKidsUser.Kids_UserInfo);

            CurrentKidsUser.MarkAsModified();

            if (CurrentKidsUser.CurrentStatus == (int)KidsUserStatus.WaiteForAccCreation_Failed ||
                CurrentKidsUser.CurrentStatus == (int)KidsUserStatus.WaiteForAccCreation_FailedSabt
                )
            {
                CurrentKidsUser.CurrentStatus = (int)KidsUserStatus.WaiteForAccCreation_WithSabtConfirmation;
                CurrentKidsUser.StatusHistory += "," + (int)KidsUserStatus.WaiteForAccCreation_WithSabtConfirmation;
            }

            KidsUser_DataProvider.SaveKidsUser(CurrentKidsUser, this, RefreshOnlineKidsUserInfo);
            pnlEdit.Visible = false;

            ucUserProfile.SetUserInfo(OnlineKidsUser.Kids_UserInfo);
        }

        protected void lnkScores_Click(object sender, EventArgs e)
        {
            RefreshKidsUserScores();
            ucScoreList.SetUserInfo(SessionItems.CurrentDailyScoreList, SessionItems.CurrentMonthlyScoreList);
            
            pnlEdit.Visible = false;
            pnlWish.Visible = false;
            pnlUserInfo.Visible = false;
            pnlScore.Visible = true;
            pnlPayment.Visible = false;
        }

        protected void lnkPayments_Click(object sender, EventArgs e)
        {
            ucPaymentList.SetUserInfo(OnlineKidsUser.Kids_UserInfo);

            pnlEdit.Visible = false;
            pnlWish.Visible = false;
            pnlUserInfo.Visible = false;
            pnlScore.Visible = false;
            pnlPayment.Visible = true;
        }

        protected void lnkWish_Click(object sender, EventArgs e)
        {
            pnlEdit.Visible = false;
            pnlWish.Visible = true;
            pnlUserInfo.Visible = false;
            pnlScore.Visible = false;
            pnlPayment.Visible = false;
        }


    }
}