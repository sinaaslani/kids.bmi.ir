using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Classes.FileUploadManagement;
using Site.Kids.bmi.ir.Masters;

namespace Site.Kids.bmi.ir.AdminCP.PostalCardAdmin
{
    public partial class PostalCardAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsPostalCardAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            (Master as Admin).RegisterPostbackTrigger(btnSave);
            if (!Page.IsPostBack)
            {
                BindScoreTypes();
                string action = UtilityMethod.GetRequestParameter("act");
                if (action.ToLower() == "edit")
                {
                    lblHead.Text = " ویرایش اطلاعات کارت پستال ";
                    btnSave.Text = " ذخیره و بهنگام ";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    lblHead.Text = " حذف کارت پستال ";
                    btnSave.Text = "  حذف  ";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "new")
                {
                    lblHead.Text = " ایجاد بازی کارت پستال ";
                    btnSave.Text = " ایجاد کارت پستال ";
                    SetInitiateValues(action);
                }

            }

        }

        private void BindScoreTypes()
        {
            var scoretypes = Score_DataProvider.GetScoresTypes();

            drpScoreTypeId.Items.Add(new ListItem("---------", ""));
            foreach (ScoreType type in scoretypes)
            {
                drpScoreTypeId.Items.Add(new ListItem(type.ScoreFaName + "--->" + type.Description, type.CategoryId.ToString()));
            }
        }

        private void SetInitiateValues(string action)
        {
            if (action.ToLower() == "edit" || action.ToLower() == "del")
            {
                int gId = 0;
                if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                    gId = UtilityMethod.GetRequestParameter("gid").ToInt32();


                PostalCard PostalCard = PostalCard_DataProvider.GetPostalCard(gId).FirstOrDefault();
                if (PostalCard == null)
                {
                    ShowMessageBox("کارت پستال یافت نشد", "خطا", MessageBoxType.Information);
                    return;
                }

                txtCardScore.Text = PostalCard.CardScore.ToString();
                txtPostalCardTitle.Text = PostalCard.CardName;
                txtPostalCardDescription.Text = PostalCard.CardPostalDescription;
                drpScoreTypeId.SelectedValue = PostalCard.CardScoreTypeId.ToString();
                PostalCardPicDeleteLnk.Visible = !string.IsNullOrEmpty(PostalCard.CardPostalSmallPic);
                PostalCardFileDeleteLnk.Visible = !string.IsNullOrEmpty(PostalCard.CardPostalPic);
                
            }
            else if (action.ToLower() == "new")
            {
               
            }
            
        }

        private PostalCard GetPostalCardInfoFromSkin()
        {
            int? gId = null;
            if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                gId = Convert.ToInt32(UtilityMethod.GetRequestParameter("gid"));

            PostalCard PostalCard = gId.HasValue ? PostalCard_DataProvider.GetPostalCard(gId.Value).FirstOrDefault() : new PostalCard();

            PostalCard.CardName = txtPostalCardTitle.Text;
            PostalCard.CardPostalDescription = txtPostalCardDescription.Text;
            PostalCard.CardScore =txtCardScore.Text.ToInt32();
            PostalCard.CardScoreTypeId = drpScoreTypeId.SelectedValue.ToInt32();
            
            string smallPic = FileUploadUtil.SaveUploadeFile(fupPostalCardPicAddress, SystemConfigs.UrlPostalCardFilesPath, UploadFileType.Pictures, UploadFileSizeLimitation._1M);
            if (!string.IsNullOrWhiteSpace(smallPic))
                PostalCard.CardPostalSmallPic = smallPic;

            string largePic = FileUploadUtil.SaveUploadeFile(fupPostalCardFileAddress, SystemConfigs.UrlPostalCardFilesPath, UploadFileType.Pictures, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(largePic))
                PostalCard.CardPostalPic = largePic;

            return PostalCard;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            PostalCard PostalCard;

            if (action.ToLower() == "del")
            {
                int gId = 0;
                if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                    gId = UtilityMethod.GetRequestParameter("gid").ToInt32();

                PostalCard = PostalCard_DataProvider.GetPostalCard(gId).FirstOrDefault();
                if (PostalCard != null)
                {
                    PostalCard.MarkAsDeleted();
                    PostalCard_DataProvider.SavePostalCard(PostalCard);
                }
                Page.Response.Redirect("PostalCardList.aspx");
            }


            PostalCard = GetPostalCardInfoFromSkin();
           
            if (action.ToLower() == "new")
            {
                PostalCard_DataProvider.SavePostalCard(PostalCard);
                Page.Response.Redirect("PostalCardList.aspx");
            }
            else if (action.ToLower() == "edit")
            {
                PostalCard.MarkAsModified();
                PostalCard_DataProvider.SavePostalCard(PostalCard);
                Page.Response.Redirect("PostalCardList.aspx");
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("PostalCardList.aspx");
        }


        protected void SmallPicDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int gId = 0;
            if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                gId = UtilityMethod.GetRequestParameter("gid").ToInt32();
            PostalCard PostalCard = PostalCard_DataProvider.GetPostalCard(gId).FirstOrDefault();
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlPostalCardFilesPath + PostalCard.CardPostalSmallPic));
            PostalCard.CardPostalSmallPic = "";
            PostalCard_DataProvider.SavePostalCard(PostalCard);
            PostalCardPicDeleteLnk.Visible = false;
        }

        protected void LargePicDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int gId = 0;
            if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                gId = UtilityMethod.GetRequestParameter("gid").ToInt32();
            PostalCard PostalCard = PostalCard_DataProvider.GetPostalCard(gId).FirstOrDefault();
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlPostalCardFilesPath + PostalCard.CardPostalPic));
            PostalCard.CardPostalPic = "";
            PostalCard_DataProvider.SavePostalCard(PostalCard);
            PostalCardFileDeleteLnk.Visible = false;
        }
        
    }
}