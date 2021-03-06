using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using DSD.Site.UtilityClasses.RSS.NET.RssChannel;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.RSS.NET.RSS;
using Rss;
using Site.Kids.bmi.ir.Classes.RSS.NET.RssChannel;
using Utility.RSS.NET.RssItem;

namespace Site.Kids.bmi.ir.Classes
{
    public static class RSSUtility
    {
        public static string CreateRSSLink()
        {
            const string CacheKey = "RSSLinksFA";

            List<HtmlLink> HTMLLinkList;
            if (HttpContext.Current.Cache[CacheKey] == null)
            {
                HTMLLinkList = new List<HtmlLink>();


                //Add AllNews Link
                HtmlLink lnkAllNewsLink = new HtmlLink();

                lnkAllNewsLink.Attributes.Add("title", "اخبار سایت");
                lnkAllNewsLink.Attributes.Add("href", "RSSFeed.aspx");

                lnkAllNewsLink.Attributes.Add("type", "application/rss+xml");
                lnkAllNewsLink.Attributes.Add("rel", "alternate");
                lnkAllNewsLink.Attributes.Add("target", "_blank");
                HTMLLinkList.Add(lnkAllNewsLink);

                HttpContext.Current.Cache.Insert(CacheKey, HTMLLinkList);
            }
            else
            {
                HTMLLinkList = HttpContext.Current.Cache[CacheKey] as List<HtmlLink>;
            }

            StringBuilder sb = new StringBuilder();
            foreach (HtmlLink link in HTMLLinkList)
            {

                sb.AppendFormat(
                    "<LINK title='{0}' target='_blank' href='{1}' type='application/rss+xml' rel='alternate' />",
                    link.Attributes["title"], link.Href);
            }
            return sb.ToString();
        }


        public static RssFeed GenerateNewsRSS(DateTime StartTime, DateTime EndTime)
        {
            RssFeed ObjRss = SetRssAndChannel("اخبار سایت", "آخرین اخبار سایت", "http://www.hossein-shirazi.com/DSD/NewsList.aspx");

            RssChannel ObjRSSChannel = ObjRss.Channels[0];

            /////////////////////////////////////////////////////////////////////////////////////////////

            ObjRSSChannel.Image = SetRssImage();
            ////////////////////////////////////////////////////////////////////////////////////////////////
            long RecordCount;
            List<News> newsList = News_DataProvider.GetNews(out RecordCount, null, null, StartTime, EndTime, News_DataProvider.NewsStatusType.Confirmed);

            if (newsList.Count > 0)
            {
                foreach (News Item in newsList)
                {
                    RssItem ObjRSSItem = new RssItem
                                             {
                                                 Author = PersianDateTime.MiladiToPersian(Item.CreateDateTime).ToShortDateString(),
                                                 Title = Item.Summary,
                                                 Description = Item.Body,
                                                 Comments = new Uri("http://Kids.bmi.ir/InfoBox/ShowNews.aspx?newsId=" + Item.NewsId).ToString(),
                                                 Link = new Uri("http://Kids.bmi.ir/InfoBox/ShowNews.aspx?newsId=" + Item.NewsId),
                                                 PubDate = Item.CreateDateTime
                                             };

                    ObjRSSChannel.Items.Add(ObjRSSItem);
                }
            }
            else
            {
                RssItem ObjRSSItem = new RssItem { Title = "No Data Available!", Description = "No Data Available!" };
                ObjRSSChannel.Items.Add(ObjRSSItem);
            }
            return ObjRss;
        }


        private static RssFeed SetRssAndChannel(string RssTitle, string RssDescription,
                                               string RssLink)
        {
            RssFeed ObjRss = new RssFeed { Version = RssVersion.RSS20, Encoding = Encoding.UTF8 };

            RssChannel ObjRSSChannel = new RssChannel
                                           {
                                               Title = RssTitle,
                                               Description = RssDescription,
                                               LastBuildDate = PersianDateTime.MiladiToPersian(DateTime.Now).ToShortDateString(),
                                               Link = new Uri(RssLink),
                                               PubDate = DateTime.Now,
                                               WebMaster = "BMI WebSite",
                                               Copyright = "(c) 2010, www.hossein-shirazi.ir. All rights reserved.",
                                               Generator = "www.hossein-shirazi.ir"
                                           };
            ObjRss.Channels.Add(ObjRSSChannel);

            return ObjRss;
        }


        private static RssImage SetRssImage()
        {
            RssImage objRssImage = new RssImage
                                       {
                                           Description = "www.hossein-shirazi.ir",
                                           Title = "www.hossein-shirazi.ir",
                                           Url = new Uri("http://www.hossein-shirazi.com/DSD/App_Themes/Default/accept.ico"),
                                           Link = new Uri("http://www.hossein-shirazi.com/DSD/"),
                                           Height = 30,
                                           Width = 100
                                       };

            return objRssImage;
        }


    }
}