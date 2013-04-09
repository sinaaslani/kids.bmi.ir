using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Classes.FileUploadManagement;
using Site.Kids.bmi.ir.Masters;

namespace Site.Kids.bmi.ir.AdminCP.WishesAdmin
{
    public partial class WishesAdmin_Admin : AdminSecureFormBaseClass
    {
        private Wish _CurrentWish;
        private Wish CurrentWish
        {
            get
            {
                return _CurrentWish ??
                       (_CurrentWish = SerializeHelper.DataContract_ToObject<Wish>(ViewState["CurrentWish"].ToString()));
            }
            set
            {
                _CurrentWish = value;
                ViewState["CurrentWish"] = SerializeHelper.DataContract_ToString(_CurrentWish);
            }
        }

        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsWishesAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            (Master as Admin).RegisterPostbackTrigger(btnSave);
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPlanGrid();

            pnlResult.Visible = true;
            pnlDetails.Visible = false;
        }

        private void BindPlanGrid(int CurrentPage = 1)
        {
            int RecordCount;

            List<Wish> Plans = Wish_DataProvider.GetWish(out RecordCount, null, txtSearchWishName.Text,
                                                                  PageSize: dgPlans.PageSize,
                                                                  Currentpage: CurrentPage);
            GridViewFiller ObjGridFilter = new GridViewFiller();
            ObjGridFilter.PagingGridView(dgPlans, Plans, RecordCount);

        }

        protected void btnNewPlan_Click(object sender, EventArgs e)
        {
            ClearControl(pnlDetails);
            txtWishDescription.Text = "";
            PageState = Action.Add;
            CurrentWish = new Wish();

            pnlResult.Visible = false;
            pnlDetails.Visible = true;

        }

        protected void dgPlans_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            PageState = Action.Update;
            CurrentWish = Wish_DataProvider.GetWish(dgPlans.DataKeys[e.NewSelectedIndex].Value.ToInt32()).FirstOrDefault();
            ClearControl(pnlDetails);

            txtWishAmount.Text = CurrentWish.WishAmount.ToString();
            txtWishDescription.Text = CurrentWish.WishDescription;
            txtWishName.Text = CurrentWish.WishName;
            SmallPicDeleteLnk.Visible = !string.IsNullOrEmpty(CurrentWish.WiShPicSmall);
            LargePicDeleteLnk.Visible = !string.IsNullOrEmpty(CurrentWish.WishPic);

            pnlDetails.Visible = true;
        }

        protected void dgMagezinText_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Wish p = Wish_DataProvider.GetWish(Convert.ToInt32(dgPlans.DataKeys[e.RowIndex].Value)).FirstOrDefault();
            p.MarkAsDeleted();
            Wish_DataProvider.SaveWish(p);
            pnlDetails.Visible = false;
            BindPlanGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string smallPic = FileUploadUtil.SaveUploadeFile(fupSmallPicAddress, SystemConfigs.UrlWishFilesPath,
                                                             UploadFileType.Pictures, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(smallPic))
                CurrentWish.WiShPicSmall = smallPic;

            string largePic = FileUploadUtil.SaveUploadeFile(fupPicAddress, SystemConfigs.UrlWishFilesPath,
                                                             UploadFileType.Pictures, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(largePic))
                CurrentWish.WishPic = largePic;

            CurrentWish.WishAmount = txtWishAmount.Text.ToLong();
            CurrentWish.WishName = txtWishName.Text;
            CurrentWish.WishDescription = txtWishDescription.Text;


            if (PageState == Action.Add)
            {
                CurrentWish.MarkAsAdded();
                Wish_DataProvider.SaveWish(CurrentWish);

            }
            else if (PageState == Action.Update)
            {
                CurrentWish.MarkAsModified();
                Wish_DataProvider.SaveWish(CurrentWish);
            }
            pnlDetails.Visible = false;
            BindPlanGrid();
        }





        protected void dgPlans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgPlans.PageIndex = e.NewPageIndex;
            BindPlanGrid(e.NewPageIndex + 1);
        }

        protected void dgPlans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lnkDelete") as LinkButton).Attributes.Add("onclick", "return Confirm();");
            }
        }

        protected void btnCancelPlan_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
        }

        protected void SmallPicDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            //File.Delete(Page.Server.MapPath(SystemConfigs.UrlWishFilesPath + CurrentWish.WiShPicSmall));
            CurrentWish.WiShPicSmall = "";

            CurrentWish.MarkAsModified();
            Wish_DataProvider.SaveWish(CurrentWish);
            SmallPicDeleteLnk.Visible = false;
        }

        protected void LargePicDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            //File.Delete(Page.Server.MapPath(SystemConfigs.UrlWishFilesPath + CurrentWish.WishPic));
            CurrentWish.WishPic = "";

            CurrentWish.MarkAsModified();
            Wish_DataProvider.SaveWish(CurrentWish);
            LargePicDeleteLnk.Visible = false;
        }




    }
}