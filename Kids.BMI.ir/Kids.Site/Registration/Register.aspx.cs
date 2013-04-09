using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using BMISSOClientHelper.BMISSOService;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.EntitiesModel.Scores;
using Kids.Utility;
using Kids.Utility.UtilExtension.DateTimeExtensions;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Classes.FileUploadManagement;
using Kids.Utility.UtilExtension.StringExtensions;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class Register : KidsSecureFormBaseClass
    {
        protected string SupportedFileSize { get; private set; }
        protected string SupportedFileTypesScript { get; private set; }

        protected override void CheckKidsUser()
        {
            if (OnlineKidsUser == null || OnlineKidsUser.Kids_UserInfo == null)
            {
                pnlAccCreation.Visible = true;
                return;
            }
            switch ((KidsUserStatus)OnlineKidsUser.Kids_UserInfo.CurrentStatus)
            {
                case KidsUserStatus.RegisterWithoutConfirmation:
                    pnlAccCreation.Visible = false;
                    ucUserProfile.Visible = true;
                    pnlResult1.Visible = true;
                    ucUserProfile.SetUserInfo(OnlineKidsUser.Kids_UserInfo);
                    break;

                case KidsUserStatus.WaiteForAccCreation_Failed:
                case KidsUserStatus.WaiteForAccCreation_FailedSabt:
                    Response.Redirect("~/Profile.aspx");
                    break;
                
                case KidsUserStatus.RegisterdWithNoAcc:
                    Response.Redirect("~/RegisterAcc.aspx");
                    break;

                case KidsUserStatus.RegisterdCompletly:
                    Response.Redirect("~");
                    break;

                case KidsUserStatus.WaiteForAccCreation:
                case KidsUserStatus.WaiteForAccCreation_WithSabtConfirmation:
                case KidsUserStatus.WaiteForAccCreation_SentToSabt:
                    pnlWaiteForAccCreation.Visible = true;
                    break;

                case KidsUserStatus.AccCreated_WaiteForDBCR:
                    Response.Redirect("~");
                    break;

                default:
                    pnlAccCreation.Visible = true;
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["inid"].IsInt64())
            {
                if (IsValidIntroducer(Request["inid"].ToLong()))
                    SessionItems.IntroducerId = Request["inid"].ToLong();
            }
            if (OnlineKidsUser == null || OnlineKidsUser.SSOUser == null)
            {
                Response.Redirect("~/ورود.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (SessionItems.IntroducerId.HasValue)
                    txtIntruducerId.Text = SessionItems.IntroducerId.ToString();

                if (!SystemConfigs.IsInTestMode && !IsValidSSOUser(OnlineKidsUser.SSOUser))
                    return;

                BindParentRelation();
                ucBirthDate.FromYear = PersianDateTime.Now.Year - 19;
                SetPrimitiveSSOInformation(OnlineKidsUser.SSOUser);

                SupportedFileSize = FileUploadUtil.GetFileSize(UploadFileSizeLimitation._500K).ToString();
                SupportedFileTypesScript = FileUploadUtil.GetFileTypes(UploadFileType.Pictures);

                Session.Clear();
            }
        }

        private bool IsValidIntroducer(long IntroducerId)
        {
            return KidsUser_DataProvider.GetKidsUser(KidsUserId: IntroducerId).Any();
        }

        private bool IsValidSSOUser(UserProfile userProfile)
        {
            if (PersianDateTime.PersianToMiladi(userProfile.Birthdate).CalculateAge() > 18)
            {
                ShowMessageBox(@"کاربرگرامی ضمن تشکر از حسن توجه شما <BR>
                               سن شما بیش از 18 سال میباشد و شما نمیتوانید از مزایای حساب آرزو بهره مند شوید"
                              , "خطا");
                pnlAccCreation.Visible = false;
                BMIUserInteraction.LogOutCurrentUser("", GoToSSO: false, forceRedirect: false);
                return false;
            }
            return true;
        }

        private void SetPrimitiveSSOInformation(UserProfile userProfile)
        {
            try
            {
                lblSSOUserName.Text = OnlineKidsUser.SSOUser.UserID;


                txtChildAddress.Text = userProfile.Address;

                var datetime = PersianDateTime.TryPersianToMiladi(userProfile.Birthdate.Replace("/", ""));
                ucBirthDate.SelectedDateTime = datetime;

                txtChildEmailAddress.Text = userProfile.Email;
                txtChildFatherName.Text = userProfile.FatherName;
                drpSex.SelectedValue = userProfile.Gender == Genders.Female ? "True" : "False";
                txtChildIdentityNo.Text = userProfile.IdNumber;
                txtChildMobileNumber.Text = userProfile.Mobile;
                txtChildName.Text = userProfile.Name.Split(' ')[0];
                txtChildFamily.Text = userProfile.Name.Split(' ').JoinPart(1);
                txtChildMelliCode.Text = userProfile.NationalCode;
                txtChildPhoneNumber2.Text = userProfile.PhoneNumber;
            }
            catch { }

        }

        private void BindParentRelation()
        {
            IEnumerable<ParentRelation> rel = KidsUser_DataProvider.GetParentRelation();
            drpParentRelationId.Items.Add(new ListItem("----", "-1"));
            foreach (var relation in rel)
                drpParentRelationId.Items.Add(new ListItem(relation.ParentRelationName, relation.ParentRelationId.ToString()));

        }



        protected void txtRegisterUserInfo_Click(object sender, EventArgs e)
        {
            if (SessionItems.CaptchaImageTextForRegister.ToLower() != txtCaptchaImage.Text.ToLower())
            {
                ShowMessageBox("متن تصویر وارد شده صحیح نمیباشد", "خطا");
                txtCaptchaImage.Text = "";
                return;
            }

            if (!txtChildMelliCode.Text.IsValidMelliCode())
            {
                ShowMessageBox("کد ملی کودک نا معتبر است", "خطا");
                return;
            }

            if (!txtParentMelliCode.Text.IsValidMelliCode())
            {
                ShowMessageBox("کد ملی ولی نا معتبر است", "خطا");
                return;
            }

            if (!ucBirthDate.SelectedDateTime.HasValue)
            {
                ShowMessageBox("تاریخ تولد نا معتبر است", "خطا");
                return;
            }

            if (ucBirthDate.SelectedDateTime.CalculateAge() >= 18)
            {
                ShowMessageBox("کاربرگرامی سن شما بیش از 18 سال میباشدو شما نمیتوانید از مزایای حساب آرزو بهره مند شوید", "خطا");
                return;
            }



            if (!IsValidBMICustomer(txtChildMelliCode.Text,
                                    txtChildIdentityNo.Text,
                                    txtChildFatherName.Text,
                                    txtChildName.Text,
                                    txtChildFamily.Text, ucBirthDate.SelectedPersianDateTime, true)
                )
            {
                return;
            }


            if (!IsValidBMICustomer(txtParentMelliCode.Text,
                                   txtParentIdentityNo.Text,
                                   null,
                                   txtParentName.Text,
                                   txtParentFamily.Text, null, false)
               )
            {
                return;
            }

            KidsUser intruducerUser = null;
            var IntruducerId = string.IsNullOrWhiteSpace(txtIntruducerId.Text) ? (long?)null : txtIntruducerId.Text.ToLong();
            if (IntruducerId.HasValue)
                intruducerUser = KidsUser_DataProvider.GetKidsUser(KidsUserId: IntruducerId).FirstOrDefault();

            var newuser = new KidsUser
                {
                    SSOUserName = OnlineKidsUser.SSOUser.UserID,
                    IntruducerId = intruducerUser == null ? (long?)null : intruducerUser.KidsUserId,

                    ChildSex = drpSex.SelectedValue.ToBool(),
                    ChildBirthDate = ucBirthDate.SelectedDateTime.Value,
                    ChildBirthLocation = txtChildBirthLocation.Text,
                    ChildFatherName = txtChildFatherName.Text,
                    ChildEmailAddress = txtChildEmailAddress.Text,
                    ChildFamily = txtChildFamily.Text,
                    ChildMelliCode = txtChildMelliCode.Text,
                    ChildMobileNumber = txtChildMobileNumber.Text,
                    ChildName = txtChildName.Text,
                    ChildPhoneNumber = txtChildPhoneNumber1.Text + "-" + txtChildPhoneNumber2.Text,
                    ChildPostCode = txtChildPostCode.Text,
                    ChildPostAddress = txtChildAddress.Text,
                    ChildIdentityNo = txtChildIdentityNo.Text,

                    ChildIdentitySerial = (drpChildIdentitySerial1.SelectedValue.IsInt32() && drpChildIdentitySerial2.SelectedValue.IsInt32() && !string.IsNullOrWhiteSpace(txtChildIdentitySerial3.Text)) 
                                          ? null : 
                                          string.Format("{0}-{1}-{2}", drpChildIdentitySerial1.SelectedValue, drpChildIdentitySerial2.SelectedValue, txtChildIdentitySerial3.Text),


                    ParentEmailAddress = txtParentEmailAddress.Text,
                    ParentFamily = txtParentFamily.Text,
                    ParentMelliCode = txtParentMelliCode.Text,
                    ParentMobileNumber = txtParentMobileNumber.Text,
                    ParentName = txtParentName.Text,
                    ParentPhoneNumber = txtParentPhoneNumber1.Text + "-" + txtParentPhoneNumber2.Text,
                    ParentPostCode = txtParentPostCode.Text,
                    ParentPostAddress = txtParentAddress.Text,
                    ParentRelationId = drpParentRelationId.SelectedValue.ToInt32(),

                    ChildAccNo = null,
                    ParentAccNo = null,
                    StatusHistory = ((int)KidsUserStatus.RegisterWithoutConfirmation).ToString(),
                    CurrentStatus = (int)KidsUserStatus.RegisterWithoutConfirmation,


                    ChildPic = SessionItems.ChildPic,
                    ChildIdentityPic = SessionItems.ChildIdentityPic,
                    ChildNationalCardFaceUPPic = SessionItems.ChildNationalCardFaceUpPic,
                    ChildNationalCardFaceDownPic = SessionItems.ChildNationalCardFaceDownPic

                };

            try
            {
                KidsUser_DataProvider.SaveKidsUser(newuser, this, RefreshOnlineKidsUserInfo);

                if (intruducerUser != null)
                {
                    var scoreType = Score_DataProvider.GetScoresTypes(ScoreEnName: "INTRODUCER").First();
                    ScoreHelper.AddScore(intruducerUser, scoreType, 1);
                }

                ucUserProfile.SetUserInfo(OnlineKidsUser.Kids_UserInfo);
                pnlResult1.Visible = true;
                pnlAccCreation.Visible = false;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("IX_KidsUsers_UniqueMelliCode"))
                    ShowMessageBox("خطا در ثبت اطلاعات :<BR> کد ملی در سیستم استفاده شده است", "خطا");
                else
                    ShowMessageBox(ex.Message + "خطا در ثبت اطلاعات", "خطا");
            }

        }


        private bool IsValidBMICustomer(string MelliCode, string IdentitySerial, string FatherName, string Fname, string Lname, PersianDateTime birthDate, bool IsChild)
        {
            //if (SystemConfigs.IsInTestMode)
                return true;

            //string msg = IsChild ? "کودک" : "ولی کودک";
            //var cInfo = BMICustomer_DataProvider.GetCustInfoByMelliCode(MelliCode);
            //if (cInfo != null && cInfo.Any())
            //{
            //    if (cInfo.All(o=>o.SabtId != IdentitySerial))
            //    {
            //        ShowMessageBox(string.Format("شماره شناسنامه {0} اشتباه است", msg), "خطا");
            //        return false;
            //    }

            //    if (IsChild && cInfo.All(o=>o.BirthDayDate != birthDate.ToString().Substring(2)))
            //    {
            //        ShowMessageBox("تاریخ تولد اشتباه است", "خطا");
            //        return false;
            //    }

            //    if (IsChild && cInfo.All(o=>o.cu_FatherName.Similarity(FatherName) < SystemConfigs.DefaultStringSimilarity))
            //    {
            //        ShowMessageBox("نام پدر اشتباه است", "خطا");
            //        return false;
            //    }

            //    if (cInfo.All(o=>o.cu_fname.Similarity(Fname) < SystemConfigs.DefaultStringSimilarity))
            //    {
            //        ShowMessageBox(string.Format("نام و نام خانوادگی {0} اشتباه است", msg), "خطا");
            //        return false;
            //    }
            //    if (cInfo.All(o=>o.cu_lname.Similarity(Lname) < SystemConfigs.DefaultStringSimilarity))
            //    {
            //        ShowMessageBox(string.Format("نام و نام خانوادگی {0} اشتباه است", msg), "خطا");
            //        return false;
            //    }

            //}

            //return true;
        }

        protected void AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            var FilePath = FileUploadUtil.SaveUploadeFile((AsyncFileUpload)sender, SystemConfigs.UrlKidsPicFilesPath(OnlineKidsUser.SSOUser.UserID),
                                                          UploadFileType.Pictures, UploadFileSizeLimitation._500K);


            if (string.IsNullOrWhiteSpace(FilePath))
                return;

            switch ((sender as AsyncFileUpload).ID)
            {

                case "fupChildPic":
                    SessionItems.ChildPic = FilePath;
                    ImgChildPic.ImageUrl = "~/JpegImage.aspx?act=30&d=" + DateTime.Now;
                    break;

                case "fupChildIdentityPic":
                    SessionItems.ChildIdentityPic = FilePath;
                    ImgChildIdentityPic.ImageUrl = "~/JpegImage.aspx?act=31&d=" + DateTime.Now;
                    break;

                case "fupChildNationalCardFaceUp":
                    SessionItems.ChildNationalCardFaceUpPic = FilePath;
                    ImgChildNationalCardFaceUp.ImageUrl = "~/JpegImage.aspx?act=32&d=" + DateTime.Now;
                    break;

                case "fupChildNationalCardFaceDown":
                    SessionItems.ChildNationalCardFaceDownPic = FilePath;
                    ImgChildNationalCardFaceDown.ImageUrl = "~/JpegImage.aspx?act=33&d=" + DateTime.Now;
                    break;
            }

        }

        protected void AsyncFileUpload_UploadedFileError(object sender, AsyncFileUploadEventArgs e)
        {

        }

        protected void btnVerifyInformation_Click(object sender, EventArgs e)
        {
            try
            {
                var kid = OnlineKidsUser.Kids_UserInfo;
                kid.MarkAsModified();

                kid.StatusHistory = string.Format("{0},{1}", kid.CurrentStatus, (int)KidsUserStatus.RegisterdWithNoAcc);
                kid.CurrentStatus = (int)KidsUserStatus.RegisterdWithNoAcc;

                KidsUser_DataProvider.SaveKidsUser(kid, this, RefreshOnlineKidsUserInfo);
                ShowMessageBox("اطلاعات با موفقیت ثبت شد", "ثبت کاربر جدید", MessageBoxType.Information);
                pnlResult2.Visible = true;
                pnlResult1.Visible = false;
                pnlAccCreation.Visible = false;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message + "خطا در ثبت اطلاعات", "خطا");
            }
        }

        protected void btnReEditInformation_Click(object sender, EventArgs e)
        {
            pnlResult1.Visible = false;
            pnlAccCreation.Visible = true;
        }

        protected void btnAddWishAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RegisterAcc.aspx");
        }

        protected void btnAcceptAgreement_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = true;
            pnlAgreement.Visible = false;
        }

        protected void btnRejectAgreement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~");
        }
    }
}