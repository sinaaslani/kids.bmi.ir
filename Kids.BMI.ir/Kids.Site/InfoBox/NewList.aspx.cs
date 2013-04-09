using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.Common;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.InfoBox
{
    public partial class NewList : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindNewsCategory();
                int? nwscid = null;

                try
                {
                    if (Page.Request["nwscid"] != null)
                        nwscid = Convert.ToInt32(Page.Request["nwscid"]);

                }
                catch{}
                
                long PageCount;
                IList<News> newsList = News_DataProvider.GetNews(out PageCount, newsStatusType: News_DataProvider.NewsStatusType.Confirmed, newsCategoryId: nwscid, PageIndex: newsGrid.PageIndex, PageSize: SystemConfigs.NewsResultNumber);
                InitializeNewsGrid(newsList);
            }
        }

        private void BindNewsCategory()
        {
            List<NewsCategory> newsCatList = News_DataProvider.GetNewsCategory();
            foreach (NewsCategory ncat in newsCatList)
                TopCatNews.Items.Add(new ListItem(ncat.NewsCategoryName, ncat.NewsCategoryId.ToString()));
            TopCatNews.Items.Insert(0, new ListItem("همه موضوعات", "-1"));
        }

        private void InitializeNewsGrid(IEnumerable<News> newsList)
        {
            newsGrid.DataSource = newsList;
            newsGrid.DataBind();

        }

        protected void TopCatNews_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? newsCatId = null;
            if (TopCatNews.SelectedValue != "-1")
                newsCatId = Convert.ToInt32(TopCatNews.SelectedValue);
            long PageCount;
            IList<News> newsList = News_DataProvider.GetNews(out PageCount, null, null, null, null, News_DataProvider.NewsStatusType.Confirmed, newsCatId, newsGrid.PageIndex, PageSize: SystemConfigs.NewsResultNumber);
            InitializeNewsGrid(newsList);

        }

    }
}