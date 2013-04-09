using System;
using System.Collections.Generic;
using System.Linq;

namespace Kids.EntitiesModel
{

    public class News_DataProvider : BaseDataProvider
    {
        public enum PreDefinedNewsCategory
        {
            Game = 1,
            WishAccount = 2,
            BankStory = 3,
            Poll = 4
        }

        public enum NewsStatusType
        {
            NotConfirmed = 1,
            Confirmed = 2,
            Discareded = 3,
        }

        public static News GetNews(long? NewsId = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.News.Include("NewsCategories")
                        where (!NewsId.HasValue || m.NewsId == NewsId)
                        select m;

                return q.FirstOrDefault();
            }
        }

        public static List<News> GetNews(out long PageCount,
                                  long? NewsId = null, string NewsText = null,
                                  DateTime? FromDate = null, DateTime? ToDate = null,
                                  NewsStatusType? newsStatusType = NewsStatusType.Confirmed,
                                  int? newsCategoryId = null,
                                  int PageIndex = 0, int PageSize = DefaultPageSize)
        {
            int? newsStatusTypeId = null;
            if (newsStatusType.HasValue)
                newsStatusTypeId = (int)newsStatusType;

            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.News.Include("NewsCategories")
                        where (!NewsId.HasValue || m.NewsId == NewsId) &&
                        (!FromDate.HasValue || m.CreateDateTime >= FromDate) &&
                        (!ToDate.HasValue || m.CreateDateTime <= ToDate) &&
                        (!newsCategoryId.HasValue || m.NewsCategories.Any(o => o.NewsCategoryId == newsCategoryId)) &&
                        (!newsStatusTypeId.HasValue || m.Status == newsStatusTypeId) &&
                        (string.IsNullOrEmpty(NewsText) || m.Summary.Contains(NewsText) || m.Body.Contains(NewsText))

                        orderby m.CreateDateTime descending
                        select m;

                PageCount = q.LongCount();
                return q.Skip(PageIndex * PageSize).Take(PageSize).ToList();
            }
        }

        public static List<NewsCategory> GetNewsCategory(int? NewsCategoryId = null, String NewsCategoryName = null,bool? IsVisible=true, int PageIndex = 1, int PageSize = DefaultPageSize)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.NewsCategories
                        where (!NewsCategoryId.HasValue || m.NewsCategoryId == NewsCategoryId) &&
                        (!IsVisible.HasValue || m.IsVisibleCategory == IsVisible) &&
                        (string.IsNullOrEmpty(NewsCategoryName) || m.NewsCategoryName == NewsCategoryName)
                        orderby m.NewsCategoryName
                        select m;

                return q.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
        }


        public static void SaveNews(News news)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                if (news.ChangeTracker.State == ObjectState.Unchanged)
                    news.MarkAsModified();

                ctx.News.ApplyChanges(news);
                ctx.SaveChanges();
                news.AcceptChanges();
            }
        }

        public static void SaveNewsCategory(NewsCategory nwsCat)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                if (nwsCat.ChangeTracker.State == ObjectState.Unchanged)
                    nwsCat.MarkAsModified();

                ctx.NewsCategories.ApplyChanges(nwsCat);
                ctx.SaveChanges();
                nwsCat.AcceptChanges();
            }
        }
    }


}
