using System.Linq;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;
using System;
using System.Collections.Generic;

namespace Site.Kids.bmi.ir.InfoBox
{
    public partial class ucNewsMarque : UserControlBaseClass
    {
        public News_DataProvider.PreDefinedNewsCategory? NewsCategoryId { get; set; }

        protected string Links = "";
        protected string Titles = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            long PageCount;
            IEnumerable<News> newsList = News_DataProvider.GetNews(out PageCount,
                                                                   newsCategoryId: (int?)NewsCategoryId,
                                                                   newsStatusType: News_DataProvider.NewsStatusType.Confirmed,
                                                                   PageSize: 10);
            if (!newsList.Any())
                newsList = News_DataProvider.GetNews(out PageCount,
                                                                   newsStatusType: News_DataProvider.NewsStatusType.Confirmed,
                                                                   PageSize: 10);

            foreach (News news in newsList)
            {
                if (news.Title.Length > 105)
                    Titles += string.Format("'{0}',", news.Title.Substring(0, 104).Replace("\n", " ").Replace("\r", ""));
                else
                    Titles += string.Format("'{0}',", news.Title.Replace("\n", " ").Replace("\r", ""));

                Links += string.Format("'/News.aspx?id={0}',", news.NewsId);
            }
            Titles = Titles.TrimEnd(',');
            Links = Links.TrimEnd(',');

        }

    }
}
