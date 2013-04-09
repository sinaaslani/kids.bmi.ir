using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.Utility;
using Kids.Common;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.InfoBox
{
    public partial class ShowNews : FormBaseClass
    {
        public int NewsId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindNewsCategory();
            }

            if (UtilityMethod.GetRequestParameter("id").IsInt32())
            {
                NewsId = Convert.ToInt32(UtilityMethod.GetRequestParameter("id"));
                News news = News_DataProvider.GetNews(NewsId);
                if (news != null)
                {
                    TitleLbl.Text = news.Title;
                    summaryLbl.Text = "<font size=1 color=gray>" + PersianDateTime.MiladiToPersian(news.CreateDateTime).ToLongDateTimeString() + "</font>";
                    summaryLbl.Text += "<br>" + news.Summary.Replace("\n", "<br>");
                    BodyLbl.Text = news.Body;
                    lnkPrintNews.NavigateUrl = "/InfoBox/Printnews.aspx?id=" + NewsId.ToString();

                    if (!string.IsNullOrWhiteSpace(news.PicAddress))
                    {
                        newsImage.ImageUrl = SystemConfigs.UrlNewsFilesPath + news.PicAddress;
                        newsImage.Visible = true;

                    }
                    else
                        newsImage.Visible = false;

                    if (!string.IsNullOrWhiteSpace(news.BodyFileAddress))
                    {
                        bodyFileLnk.NavigateUrl = SystemConfigs.UrlNewsFilesPath + news.BodyFileAddress;
                        bodyFileLnk.Visible = true;
                        bodyFileDivLbl.Visible = true;

                    }
                    else
                    {
                        bodyFileLnk.Visible = false;
                        bodyFileDivLbl.Visible = false;
                    }



                    if (!string.IsNullOrWhiteSpace(news.RealFileAddress))
                    {
                        realFileLnk.NavigateUrl = SystemConfigs.UrlNewsFilesPath + news.RealFileAddress;
                        realFileLnk.Visible = true;
                        realFileDivLbl.Visible = true;
                    }
                    else
                    {
                        realFileLnk.Visible = false;
                        realFileDivLbl.Visible = false;
                    }


                    if (!string.IsNullOrWhiteSpace(news.MediaFileAddress))
                    {
                        MediaFileLnk.NavigateUrl = SystemConfigs.UrlNewsFilesPath + news.MediaFileAddress;
                        MediaFileLnk.Visible = true;
                        mediaFileDivLbl.Visible = true;
                    }
                    else
                    {
                        MediaFileLnk.Visible = false;
                        mediaFileDivLbl.Visible = false;
                    }

                    Page.Title = news.Title;
                    Page.MetaDescription = news.Title + " , " + " بانک ملی ایران ";
                    Page.MetaKeywords = news.Title + " اخبار کودک و مطالب و مقالات بانکداری " + "بانک ملی ایران";
                }


            }

        }
        private void BindNewsCategory()
        {
            List<NewsCategory> newsCatList = News_DataProvider.GetNewsCategory();
            foreach (NewsCategory ncat in newsCatList)
                TopCatNews.Items.Add(new ListItem(ncat.NewsCategoryName, ncat.NewsCategoryId.ToString()));
            TopCatNews.Items.Insert(0, new ListItem("همه موضوعات", "-1"));
        }

        protected void TopCatNews_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.Response.Redirect("InfoBox.aspx?nwscid=" + TopCatNews.SelectedValue);
        }
    }
}