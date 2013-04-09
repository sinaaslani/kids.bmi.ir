using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Classes.FileUploadManagement;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class Editable_UserProfileWidget : UserControlBaseClass
    {
        private void SetBasicInfoIsEditable(bool IsEditable)
        {

            txtChildName.ReadOnly = txtChildFamily.ReadOnly =
            txtChildFatherName.ReadOnly = txtChildIdentityNo.ReadOnly =
            txtChildMelliCode.ReadOnly = txtChildAccNo.ReadOnly =
            txtChildAccBranchNo.ReadOnly = txtParentAccNo.ReadOnly = !IsEditable;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucBirthDate.FromYear = PersianDateTime.Now.Year - 19;
            if (!IsPostBack)
            {
                BindKidsUserState();
                BindParentRelation();
            }
        }
        private void BindParentRelation()
        {
            IEnumerable<ParentRelation> rel = KidsUser_DataProvider.GetParentRelation();
            drpParentRelationId.Items.Add(new ListItem("----", "-1"));
            foreach (var relation in rel)
                drpParentRelationId.Items.Add(new ListItem(relation.ParentRelationName, relation.ParentRelationId.ToString()));

        }
        private void BindKidsUserState()
        {
            List<KidsUserState> AllStates = KidsUser_DataProvider.GetKidsUserStates();
            drpUserState.Items.Add(new ListItem("----", "-1"));
            foreach (var state in AllStates)
                drpUserState.Items.Add(new ListItem(state.StateName, state.Id.ToString()));

        }

        public void SetUserInfo(KidsUser user, bool BasicInfoIsEditable)
        {
            if (user != null)
            {
                SetBasicInfoIsEditable(BasicInfoIsEditable);

                SessionItems.ChildPic = null;
                SessionItems.ChildIdentityPic = null;
                SessionItems.ChildNationalCardFaceDownPic = null;
                SessionItems.ChildNationalCardFaceUpPic = null;

                lblSSOUserName.Text = user.SSOUserName;
                txtIntruducerId.Text = user.IntroducerUser == null ? null : string.Format("{0} {1}", user.IntroducerUser.ChildName, user.IntroducerUser.ChildFamily);

                drpUserState.SelectedValue = user.CurrentStatus.ToString();
                ucBirthDate.SelectedDateTime = user.ChildBirthDate;
                txtChildAddress.Text = user.ChildPostAddress;
                txtChildBirthLocation.Text = user.ChildBirthLocation;
                txtChildEmailAddress.Text = user.ChildEmailAddress;
                txtChildFamily.Text = user.ChildFamily;
                txtChildFatherName.Text = user.ChildFatherName;
                txtChildIdentityNo.Text = user.ChildIdentityNo;

                drpChildIdentitySerial1.SelectedValue = string.IsNullOrWhiteSpace(user.ChildIdentitySerial) ? "" : user.ChildIdentitySerial.Split('-')[0];
                drpChildIdentitySerial2.SelectedValue = string.IsNullOrWhiteSpace(user.ChildIdentitySerial) ? "" : user.ChildIdentitySerial.Split('-')[1];
                txtChildIdentitySerial3.Text = string.IsNullOrWhiteSpace(user.ChildIdentitySerial) ? "" : user.ChildIdentitySerial.Split('-')[2];

                txtChildMelliCode.Text = user.ChildMelliCode;
                txtChildMobileNumber.Text = user.ChildMobileNumber;
                txtChildName.Text = user.ChildName;


                txtChildPhoneNumber1.Text = string.IsNullOrWhiteSpace(user.ChildPhoneNumber) ? "" : user.ChildPhoneNumber.Split('-')[0];
                txtChildPhoneNumber2.Text = string.IsNullOrWhiteSpace(user.ChildPhoneNumber) ? "" : user.ChildPhoneNumber.Split('-')[1];



                txtChildPostCode.Text = user.ChildPostCode;
                drpSex.Text = user.ChildSex ? "True" : "False";


                txtChildAccNo.Text = user.ChildAccNo;
                txtChildAccBranchNo.Text = user.ChildAccBranchNo.HasValue ? user.ChildAccBranchNo.ToString() : "";

                txtParentAddress.Text = user.ParentPostAddress;
                txtParentEmailAddress.Text = user.ParentEmailAddress;
                txtParentFamily.Text = user.ParentFamily;
                txtParentIdentityNo.Text = user.ParentIdentityNo;
                txtParentMelliCode.Text = user.ParentMelliCode;
                txtParentMobileNumber.Text = user.ParentMobileNumber;
                txtParentName.Text = user.ParentName;
                txtParentAccNo.Text = user.ParentAccNo;

                txtParentPhoneNumber1.Text = string.IsNullOrWhiteSpace(user.ParentPhoneNumber) ? "" : user.ParentPhoneNumber.Split('-')[0];
                txtParentPhoneNumber2.Text = string.IsNullOrWhiteSpace(user.ParentPhoneNumber) ? "" : user.ParentPhoneNumber.Split('-')[1];

                txtParentPostCode.Text = user.ParentPostCode;
                drpParentRelationId.SelectedValue = user.ParentRelation.ParentRelationId.ToString();
                ucKidsUserSateWidget.SetUserInfo(user);
                lblWishAccountnumber.Text = user.ChildAccNo;

                try
                {
                string tx_date;
                    //lblWishAccountRemain.Text = string.IsNullOrWhiteSpace(user.ChildAccNo)
                    //                                ? ""
                    //                                : BMICustomer_DataProvider.GetAccRemain(user, out tx_date).ToString().Money3Dispaly() + " ریال ";
                }
                catch{}
                lblWishAccountBranch.Text = user.ChildAccBranchNo.HasValue ? user.ChildAccBranchNo.ToString() : "";


            }
        }


        internal KidsUser GetUserInfo(KidsUser user)
        {
            if (drpUserState.SelectedValue != "-1")
                user.CurrentStatus = drpUserState.SelectedValue.ToInt32();
            user.ChildBirthDate = ucBirthDate.SelectedDateTime.Value;
            user.ChildPostAddress = txtChildAddress.Text;
            user.ChildBirthLocation = txtChildBirthLocation.Text;
            user.ChildEmailAddress = txtChildEmailAddress.Text;
            user.ChildFamily = txtChildFamily.Text;
            user.ChildFatherName = txtChildFatherName.Text;
            user.ChildIdentityNo = txtChildIdentityNo.Text;

            user.ChildIdentitySerial = (drpChildIdentitySerial1.SelectedValue.IsInt32() && drpChildIdentitySerial2.SelectedValue.IsInt32() && !string.IsNullOrWhiteSpace(txtChildIdentitySerial3.Text))
                                       ? null :
                                       string.Format("{0}-{1}-{2}", drpChildIdentitySerial1.SelectedValue, drpChildIdentitySerial2.SelectedValue, txtChildIdentitySerial3.Text);


            user.ChildMelliCode = txtChildMelliCode.Text;
            user.ChildMobileNumber = txtChildMobileNumber.Text;
            user.ChildName = txtChildName.Text;


            user.ChildPhoneNumber = txtChildPhoneNumber1.Text + "-" + txtChildPhoneNumber2.Text;


            user.ChildPostCode = txtChildPostCode.Text;
            user.ChildSex = drpSex.SelectedValue.ToBool();

            user.ChildAccNo = txtChildAccNo.Text;
            user.ChildAccBranchNo = string.IsNullOrWhiteSpace(txtChildAccBranchNo.Text) ? (int?)null : txtChildAccBranchNo.Text.ToInt32();

            user.ParentPostAddress = txtParentAddress.Text;
            user.ParentEmailAddress = txtParentEmailAddress.Text;
            user.ParentFamily = txtParentFamily.Text;
            user.ParentIdentityNo = txtParentIdentityNo.Text;
            user.ParentMelliCode = txtParentMelliCode.Text;
            user.ParentMobileNumber = txtParentMobileNumber.Text;
            user.ParentName = txtParentName.Text;
            user.ParentAccNo = txtParentAccNo.Text;

            user.ParentPhoneNumber = txtParentPhoneNumber1.Text + "-" + txtParentPhoneNumber2.Text;

            user.ParentPostCode = txtParentPostCode.Text;
            user.ParentRelation.ParentRelationId = drpParentRelationId.SelectedValue.ToInt32();

            return user;
        }

        protected void AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            var fup = (sender as AsyncFileUpload);
            if (fup == null)
                return;

            if (KidsOnlineUser == null || KidsOnlineUser.Kids_UserInfo == null)
                return;

            var user = KidsOnlineUser.Kids_UserInfo;

            var FilePath = FileUploadUtil.SaveUploadeFile(fup, SystemConfigs.UrlKidsPicFilesPath(KidsOnlineUser.SSOUser.UserID),
                                                          UploadFileType.Pictures, UploadFileSizeLimitation._500K);

            if (string.IsNullOrWhiteSpace(FilePath))
                return;

            user.MarkAsModified();

            switch (fup.ID)
            {

                case "fupChildPic":
                    user.ChildPic = FilePath;
                    SessionItems.ChildPic = FilePath;
                    ImgChildPic.ImageUrl = "~/JpegImage.aspx?act=300&d=" + DateTime.Now;
                    break;

                case "fupChildIdentityPic":
                    user.ChildIdentityPic = FilePath;
                    SessionItems.ChildIdentityPic = FilePath;
                    ImgChildIdentityPic.ImageUrl = "~/JpegImage.aspx?act=301&d=" + DateTime.Now;
                    break;

                case "fupChildNationalCardFaceUp":
                    user.ChildNationalCardFaceUPPic = FilePath;
                    SessionItems.ChildNationalCardFaceUpPic = FilePath;
                    ImgChildNationalCardFaceUp.ImageUrl = "~/JpegImage.aspx?act=302&d=" + DateTime.Now;
                    break;

                case "fupChildNationalCardFaceDown":
                    user.ChildNationalCardFaceDownPic = FilePath;
                    SessionItems.ChildNationalCardFaceDownPic = FilePath;
                    ImgChildNationalCardFaceDown.ImageUrl = "~/JpegImage.aspx?act=303&d=" + DateTime.Now;
                    break;
            }
            KidsUser_DataProvider.SaveKidsUser(user, this, KidsSecureFormBaseClass.RefreshOnlineKidsUserInfo);
        }

        protected void AsyncFileUpload_UploadedFileError(object sender, AsyncFileUploadEventArgs e)
        {

        }
    }
}