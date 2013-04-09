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

namespace Site.Kids.bmi.ir.AdminCP.NewsAdmin
{
    public partial class NewsAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsNewsAdministrator || OnlineSystemUser.IsNewsOperator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master as Admin).RegisterPostbackTrigger(btnSave);
            if (!Page.IsPostBack)
            {
                string action = UtilityMethod.GetRequestParameter("act");
                if (action.ToLower() == "edit")
                {
                    lblHead.Text = " ویرایش اطلاعات خبر ";
                    btnSave.Text = " ذخیره و بهنگام ";
                    commitBtn.Visible = true;
                    DiscardBtn.Visible = true;
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    lblHead.Text = " حذف خبر ";
                    btnSave.Text = "  حذف  ";
                    commitBtn.Visible = false;
                    DiscardBtn.Visible = false;
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "new")
                {
                    lblHead.Text = " ایجاد خبر جدید ";
                    btnSave.Text = " ایجاد خبر ";
                    NewsStatus.Text = " در حال ایجاد ";
                    commitBtn.Visible = false;
                    DiscardBtn.Visible = false;
                    SetInitiateValues(action);
                }

            }

        }



        private void SetInitiateValues(string action)
        {
            if (action.ToLower() == "edit" || action.ToLower() == "del")
            {
                int newsId = 0;
                if (UtilityMethod.GetRequestParameter("nwsId").IsInt32())
                    newsId = UtilityMethod.GetRequestParameter("nwsId").ToInt32();


                News news = News_DataProvider.GetNews(newsId);
                if (news == null)
                {
                    ShowMessageBox("خبر یافت نشد", "خطا", MessageBoxType.Information);
                    return;
                }
                // check permission
                // check news  status ;
                // if news is commited the admin can edit or delete it just
                if (news.Status == (int)News_DataProvider.NewsStatusType.Confirmed)
                {
                }

                txtTitle.Text = news.Title;
                summaryCtrl.Text = news.Summary;
                txtHTMLNewsbody.Text = news.Body;
                ucNewsDateDime.SelectedDateTime = news.CreateDateTime;



                //isShowInMarqueCtrl.Checked = news.ShowInMarque;
                //chkIsShowInFirstPage.Checked = news.ShowInFirstPage;
                //chkShowInTab.Checked = news.ShowInTab;
                // set delete file links

                SmallPicDeleteLnk.Visible = !string.IsNullOrEmpty(news.SmallPicAddress);

                LargePicDeleteLnk.Visible = !string.IsNullOrEmpty(news.PicAddress);

                BodyFileDeleteLnk.Visible = !string.IsNullOrEmpty(news.BodyFileAddress);

                MediaFileDeleteLnk.Visible = !string.IsNullOrEmpty(news.MediaFileAddress);

                RealFileDeleteLnk.Visible = !string.IsNullOrEmpty(news.RealFileAddress);


                switch (news.Status)
                {
                    case (int)News_DataProvider.NewsStatusType.Confirmed:
                        NewsStatus.Text = " خبر تایید شده است ";
                        break;
                    case (int)News_DataProvider.NewsStatusType.NotConfirmed:
                        NewsStatus.Text = " خبر هنوز تایید نشده است ";
                        break;
                    case (int)News_DataProvider.NewsStatusType.Discareded:
                        NewsStatus.Text = " خبر مردود شده است ";
                        break;
                }

                NewCatsGrid.DataSource = News_DataProvider.GetNewsCategory();
                NewCatsGrid.DataBind();

                foreach (GridViewRow item in NewCatsGrid.Rows)
                {
                    int catId = (item.FindControl("hdnCategoryId") as HiddenField).Value.ToInt32();
                    if (news.NewsCategories.Any(nc => nc.NewsCategoryId == catId))
                        (item.FindControl("chkCategory") as CheckBox).Checked = true;
                }


            }
            else if (action.ToLower() == "new")
            {
                NewCatsGrid.DataSource = News_DataProvider.GetNewsCategory();
                NewCatsGrid.DataBind();
                ucNewsDateDime.SelectedDateTime = DateTime.Now;

            }

        }

        private News GetNewsInfoFromSkin()
        {
            int? newsId = null;
            if (UtilityMethod.GetRequestParameter("nwsId").IsInt32())
                newsId = Convert.ToInt32(UtilityMethod.GetRequestParameter("nwsId"));

            News news = newsId.HasValue ? News_DataProvider.GetNews(newsId.Value) : new News();
            news.Title = txtTitle.Text;
            news.Summary = summaryCtrl.Text;
            news.Body = txtHTMLNewsbody.Text;
            //news.ShowInMarque = isShowInMarqueCtrl.Checked;
            //news.ShowInFirstPage = chkIsShowInFirstPage.Checked;
            //news.ShowInTab = chkShowInTab.Checked;


            news.CreateDateTime = ucNewsDateDime.SelectedDateTime.Value;

            news.StartTracking();
            foreach (GridViewRow newsCat in NewCatsGrid.Rows)
            {

                int catId = (newsCat.FindControl("hdnCategoryId") as HiddenField).Value.ToInt32();
                var catCheck = (newsCat.FindControl("chkCategory") as CheckBox);

                if (news.NewsCategories.Any(o => o.NewsCategoryId == catId))
                {
                    if (!catCheck.Checked)
                        news.NewsCategories.Remove(news.NewsCategories.First(o => o.NewsCategoryId == catId));
                }
                else
                {
                    if (catCheck.Checked)
                    {
                        var cat = new NewsCategory { NewsCategoryId = catId };
                        cat.MarkAsUnchanged();
                        news.NewsCategories.Add(cat);
                    }
                }

            }



            string smallPic = FileUploadUtil.SaveUploadeFile(SmallPicAddress, SystemConfigs.UrlNewsFilesPath, UploadFileType.Pictures, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(smallPic))
                news.SmallPicAddress = smallPic;

            string largePic = FileUploadUtil.SaveUploadeFile(PicAddress, SystemConfigs.UrlNewsFilesPath, UploadFileType.Pictures, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(largePic))
                news.PicAddress = largePic;

            string bodyFileAddress = FileUploadUtil.SaveUploadeFile(bodyFile, SystemConfigs.UrlNewsFilesPath, UploadFileType.Document, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(bodyFileAddress))
                news.BodyFileAddress = bodyFileAddress;

            string mediaFileAddress = FileUploadUtil.SaveUploadeFile(mediaFile, SystemConfigs.UrlNewsFilesPath, UploadFileType.Video | UploadFileType.Sounde, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(mediaFileAddress))
                news.MediaFileAddress = mediaFileAddress;

            string realFileAddress = FileUploadUtil.SaveUploadeFile(realFile, SystemConfigs.UrlNewsFilesPath, UploadFileType.Video | UploadFileType.Sounde, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(realFileAddress))
                news.RealFileAddress = realFileAddress;


            return news;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            News news;

            if (action.ToLower() == "del")
            {
                int newsId = 0;
                if (UtilityMethod.GetRequestParameter("nwsId").IsInt32())
                    newsId = UtilityMethod.GetRequestParameter("nwsId").ToInt32();

                news = News_DataProvider.GetNews(newsId);
                if (news != null)
                {
                    news.MarkAsDeleted();
                    News_DataProvider.SaveNews(news);
                }
                Page.Response.Redirect("NewsList.aspx");
            }


            news = GetNewsInfoFromSkin();
            if (news.NewsCategories.Count == 0)
            {
                lblSubjectValidator.Visible = true;
                return;
            }
            lblSubjectValidator.Visible = false;


            if (action.ToLower() == "new")
            {
                news.Status = (int)News_DataProvider.NewsStatusType.NotConfirmed;

                News_DataProvider.SaveNews(news);
                // Page.Response.Write(" <br> nid : " + nid);
                //Page.Response.Write(" <br> title : " + news.Title);
                Page.Response.Redirect("NewsList.aspx");
            }
            else if (action.ToLower() == "edit")
            {
                News_DataProvider.SaveNews(news);
                Page.Response.Redirect("NewsList.aspx");
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("NewsList.aspx");
        }

        protected void commitBtn_Click(object sender, EventArgs e)
        {

            News news = GetNewsInfoFromSkin();
            if (news.NewsCategories.Count == 0)
            {
                lblSubjectValidator.Visible = true;
                return;
            }
            lblSubjectValidator.Visible = false;

            news.Status = (int)News_DataProvider.NewsStatusType.Confirmed;
            News_DataProvider.SaveNews(news);
            Page.Response.Redirect("NewsList.aspx");
        }

        protected void DiscardBtn_Click(object sender, EventArgs e)
        {
            News news = GetNewsInfoFromSkin();
            news.Status = (int)News_DataProvider.NewsStatusType.Discareded;
            News_DataProvider.SaveNews(news);
            Page.Response.Redirect("NewsList.aspx");
        }

        protected void SmallPicDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int newsId = 0;
            if (UtilityMethod.GetRequestParameter("nwsId").IsInt32())
                newsId = UtilityMethod.GetRequestParameter("nwsId").ToInt32();
            News news = News_DataProvider.GetNews(newsId);
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlNewsFilesPath + news.SmallPicAddress));
            news.SmallPicAddress = "";
            News_DataProvider.SaveNews(news);
            SmallPicDeleteLnk.Visible = false;
        }

        protected void LargePicDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int newsId = 0;
            if (UtilityMethod.GetRequestParameter("nwsId").IsInt32())
                newsId = UtilityMethod.GetRequestParameter("nwsId").ToInt32();
            News news = News_DataProvider.GetNews(newsId);
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlNewsFilesPath + news.PicAddress));
            news.PicAddress = "";
            News_DataProvider.SaveNews(news);
            LargePicDeleteLnk.Visible = false;
        }

        protected void MediaFileDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int newsId = 0;
            if (UtilityMethod.GetRequestParameter("nwsId").IsInt32())
                newsId = UtilityMethod.GetRequestParameter("nwsId").ToInt32();
            News news = News_DataProvider.GetNews(newsId);
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlNewsFilesPath + news.MediaFileAddress));
            news.MediaFileAddress = "";
            News_DataProvider.SaveNews(news);
            MediaFileDeleteLnk.Visible = false;
        }

        protected void RealFileDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int newsId = 0;
            if (UtilityMethod.GetRequestParameter("nwsId").IsInt32())
                newsId = Convert.ToInt32(UtilityMethod.GetRequestParameter("nwsId"));
            News news = News_DataProvider.GetNews(newsId);
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlNewsFilesPath + news.RealFileAddress));
            news.RealFileAddress = "";
            News_DataProvider.SaveNews(news);
            RealFileDeleteLnk.Visible = false;
        }

        protected void BodyFileDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int newsId = 0;
            if (UtilityMethod.GetRequestParameter("nwsId").IsInt32())
                newsId = UtilityMethod.GetRequestParameter("nwsId").ToInt32();
            News news = News_DataProvider.GetNews(newsId);
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlNewsFilesPath + news.BodyFileAddress));
            news.BodyFileAddress = "";
            News_DataProvider.SaveNews(news);
            BodyFileDeleteLnk.Visible = false;
        }


    }
}