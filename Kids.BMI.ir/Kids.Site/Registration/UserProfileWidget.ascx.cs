using System;
using AjaxControlToolkit;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Classes.FileUploadManagement;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class UserProfileWidget : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetUserInfo(KidsUser user)
        {
            if (user != null)
            {
                SessionItems.ChildPic = null;
                SessionItems.ChildIdentityPic = null;
                SessionItems.ChildNationalCardFaceDownPic = null;
                SessionItems.ChildNationalCardFaceUpPic = null;

                lblSSOUserName.Text = user.SSOUserName;
                lblIntroducer.Text = user.IntroducerUser == null ? null : string.Format("{0} {1}", user.IntroducerUser.ChildName, user.IntroducerUser.ChildFamily);
                lblBirthDate.Text = PersianDateTime.MiladiToPersian(user.ChildBirthDate).ToLongDateString();
                lblChildAddress.Text = user.ChildPostAddress;
                lblChildBirthLocation.Text = user.ChildBirthLocation;
                lblChildEmailAddress.Text = user.ChildEmailAddress;
                lblChildFamily.Text = user.ChildFamily;
                lblChildFatherName.Text = user.ChildFatherName;
                lblChildIdentityNo.Text = user.ChildIdentityNo;
                lblChildIdentitySerial.Text = user.ChildIdentitySerial;
                lblChildMelliCode.Text = user.ChildMelliCode;
                lblChildMobileNumber.Text = user.ChildMobileNumber;
                lblChildName.Text = user.ChildName;
                lblChildPhoneNumber.Text = user.ChildPhoneNumber;
                lblChildPostCode.Text = user.ChildPostCode;
                lblSex.Text = user.ChildSex ? "مرد" : "زن";

                lblParentAddress.Text = user.ParentPostAddress;
                lblParentEmailAddress.Text = user.ParentEmailAddress;
                lblParentFamily.Text = user.ParentFamily;
                lblParentIdentityNo.Text = user.ParentIdentityNo;
                lblParentMelliCode.Text = user.ParentMelliCode;
                lblParentMobileNumber.Text = user.ParentMobileNumber;
                lblParentName.Text = user.ParentName;
                lblParentPhoneNumber.Text = user.ParentPhoneNumber;
                lblParentPostCode.Text = user.ParentPostCode;
                lblParentRelationId.Text = user.ParentRelation.ParentRelationName;
                lblParentAccNo.Text = user.ParentAccNo;

                ucKidsUserSateWidget.SetUserInfo(user);
                lblWishAccountnumber.Text = user.ChildAccNo;
                string tx_date;
                lblWishAccountRemain.Text = string.IsNullOrWhiteSpace(user.ChildAccNo) ? "" : BMICustomer_DataProvider.GetAccRemain(user, out tx_date).ToString().Money3Dispaly();
                lblWishAccountBranch.Text = user.ChildAccBranchNo.HasValue ? user.ChildAccBranchNo.ToString() : "";

                SupportedFileSize = FileUploadUtil.GetFileSize(UploadFileSizeLimitation._500K).ToString();
                SupportedFileTypesScript = FileUploadUtil.GetFileTypes(UploadFileType.Pictures);
            }
        }



        protected void AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            var fup = sender as AsyncFileUpload;
            if (fup == null)
                return;

            if (KidsOnlineUser == null || KidsOnlineUser.Kids_UserInfo == null)
                return;
            var user = KidsOnlineUser.Kids_UserInfo;


            var FilePath = FileUploadUtil.SaveUploadeFile((AsyncFileUpload)sender, SystemConfigs.UrlKidsPicFilesPath(KidsOnlineUser.SSOUser.UserID),
                                                                    UploadFileType.Pictures, UploadFileSizeLimitation._500K);

            if (string.IsNullOrWhiteSpace(FilePath))
                return;

            user.MarkAsModified();

            switch (fup.ID)
            {

                case "fupChildPic":
                    user.ChildPic = FilePath;
                    SessionItems.ChildPic = FilePath;
                    ImgChildPic.ImageUrl = "~/JpegImage.aspx?act=30&d=" + DateTime.Now;
                    break;

                case "fupChildIdentityPic":
                    user.ChildIdentityPic = FilePath;
                    SessionItems.ChildIdentityPic = FilePath;
                    ImgChildIdentityPic.ImageUrl = "~/JpegImage.aspx?act=31&d=" + DateTime.Now;
                    break;

                case "fupChildNationalCardFaceUp":
                    user.ChildNationalCardFaceUPPic = FilePath;
                    SessionItems.ChildNationalCardFaceUpPic = FilePath;
                    ImgChildNationalCardFaceUp.ImageUrl = "~/JpegImage.aspx?act=32&d=" + DateTime.Now;
                    break;

                case "fupChildNationalCardFaceDown":
                    user.ChildNationalCardFaceDownPic = FilePath;
                    SessionItems.ChildNationalCardFaceDownPic = FilePath;
                    ImgChildNationalCardFaceDown.ImageUrl = "~/JpegImage.aspx?act=33&d=" + DateTime.Now;
                    break;
            }
            KidsUser_DataProvider.SaveKidsUser(user, this, KidsSecureFormBaseClass.RefreshOnlineKidsUserInfo);
        }

        protected void AsyncFileUpload_UploadedFileError(object sender, AsyncFileUploadEventArgs e)
        {

        }


        protected string SupportedFileSize { get; private set; }

        protected string SupportedFileTypesScript { get; private set; }
    }
}