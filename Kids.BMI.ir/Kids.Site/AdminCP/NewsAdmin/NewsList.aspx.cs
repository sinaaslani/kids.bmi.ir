using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.Utility;
using Kids.Common;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.NewsAdmin
{
    public partial class NewsList : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsNewsAdministrator || OnlineSystemUser.IsNewsOperator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucFromDate.SelectedDateTime = DateTime.Now.AddDays(-30);
                ucToDate.SelectedDateTime = DateTime.Now.AddDays(2);

                DateTime? startTime = ucFromDate.SelectedDateTime;
                DateTime? endTime = ucToDate.SelectedDateTime;


                BindNewsCategory();

                long PageCount;
                IList<News> newsList = News_DataProvider.GetNews(out PageCount, FromDate: startTime, ToDate: endTime, newsStatusType: null, PageIndex: newsGrid.PageIndex);
                InitializeNewsGrid(newsList);
            }

        }

        private void BindNewsCategory()
        {
            List<NewsCategory> newsCatList = News_DataProvider.GetNewsCategory();
            foreach (NewsCategory ncat in newsCatList)
                NewsCatCtrl.Items.Add(new ListItem(ncat.NewsCategoryName, ncat.NewsCategoryId.ToString()));
            NewsCatCtrl.Items.Insert(0, new ListItem("همه موضوعات", "-1"));
        }

        private string GetNewsStatusImg(News news)
        {
            string imgUrl = "";
            string imgStatus = "";
            switch (news.Status)
            {
                case (int)News_DataProvider.NewsStatusType.Confirmed:
                    imgUrl = ResolveUrl("~/App_Themes/Default/Images/ledgreen.gif");
                    imgStatus = string.Format("وضعیت خبر : {0}", "تایید شده");
                    break;
                case (int)News_DataProvider.NewsStatusType.NotConfirmed:
                    imgUrl = ResolveUrl("~/App_Themes/Default/Images/ledyellow.gif");
                    imgStatus = string.Format("وضعیت خبر : {0}", "جدید");
                    break;
                case (int)News_DataProvider.NewsStatusType.Discareded:
                    imgUrl = ResolveUrl("~/App_Themes/Default/images/ledRed.gif");
                    imgStatus = string.Format("وضعیت خبر : {0}", "رد شده");
                    break;
            }
            string img = "<img src='" + imgUrl + "' alt='" + imgStatus + "' />";
            return img;
        }
        private void InitializeNewsGrid(IList<News> newsList)
        {
            foreach (News n in newsList)
            {
                if (!string.IsNullOrWhiteSpace(n.SmallPicAddress))
                    n.SmallPicAddress = "<img border=0 width='90' src='" + ResolveUrl(SystemConfigs.UrlNewsFilesPath + n.SmallPicAddress) + "' >";

                n.PicAddress = GetNewsStatusImg(n);

                n.Title = string.Format("<font color=blue>{0}</font>", n.Title);
                if (n.Summary.Length < 256)
                    n.Title += string.Format("<br>{0}", n.Summary);
                else
                    n.Title += string.Format("<br>{0} ... ", n.Summary.Substring(0, 256));

                n.Title += string.Format("<br><font size=1>{0}</font>", PersianDateTime.MiladiToPersian(n.CreateDateTime).ToLongDateString());

                n.Title += string.Format("<br><a href=" + "newsAdmin.aspx?act=edit&nwsid={0}>ویرایش" + "</a>", n.NewsId);
                n.Title += string.Format("&nbsp;&nbsp;&nbsp;" + "<a href=" + "NewsAdmin.aspx?act=del&nwsId={0}>حذف" + "</a>", n.NewsId);
            }
            newsGrid.DataSource = newsList;
            try
            {
                newsGrid.DataBind();
            }
            catch
            {
                newsGrid.PageIndex = 0;
                newsGrid.DataBind();
            }

            ResultNumberLbl.Text = newsList.Count.ToString();

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime? startTime = ucFromDate.SelectedDateTime;
            DateTime? endTime = ucToDate.SelectedDateTime;


            if (endTime < startTime)
            {
                InvalisSearchMsg.Visible = true;
                return;
            }

            int? newsCat = null;
            if (NewsCatCtrl.SelectedValue != "-1")
                newsCat = Convert.ToInt32(NewsCatCtrl.SelectedValue);

            InvalisSearchMsg.Visible = false;

            long PageCount;
            IList<News> newsList = News_DataProvider.GetNews(out PageCount, FromDate: startTime, ToDate: endTime, newsCategoryId: newsCat, newsStatusType: null, PageIndex: newsGrid.PageIndex);
            InitializeNewsGrid(newsList);

        }

        protected void newsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            DateTime? startTime = ucFromDate.SelectedDateTime;
            DateTime? endTime = ucToDate.SelectedDateTime;

            if (endTime < startTime)
            {
                InvalisSearchMsg.Visible = true;
                return;
            }

            int? newsCat = null;
            if (NewsCatCtrl.SelectedValue != "-1")
                newsCat = Convert.ToInt32(NewsCatCtrl.SelectedValue);

            InvalisSearchMsg.Visible = false;

            long PageCount;
            IList<News> newsList = News_DataProvider.GetNews(out PageCount, null, searchKeyTxt.Text, startTime, endTime, null, newsCat, newsGrid.PageIndex);
            InitializeNewsGrid(newsList);

            newsGrid.PageIndex = e.NewPageIndex;

            try
            {
                newsGrid.DataBind();
            }
            catch
            {
                newsGrid.PageIndex = 0;
                newsGrid.DataBind();
            }

        }
        
    }
}