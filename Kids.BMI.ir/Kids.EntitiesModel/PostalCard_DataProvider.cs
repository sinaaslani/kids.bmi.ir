using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{
    public class PostalCard_DataProvider : BaseDataProvider
    {
        public static List<PostalCard> GetPostalCard(int? PostalCardId = null, string PostalCardName = null)
        {
            long PageCount;
            return GetPostalCard(out PageCount, PostalCardId, PostalCardName);
        }

        public static List<PostalCard> GetPostalCard(out long PageCount, int? PostalCardId = null, string PostalCardName = null,
                                         int Currentpage = 1, int PageSize = DefaultPageSize)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.PostalCards.Include("ScoreType")
                        where (!PostalCardId.HasValue || m.CardId == PostalCardId.Value) &&
                        (string.IsNullOrEmpty(PostalCardName) || m.CardName.Contains(PostalCardName))
                        orderby m.CardId descending
                        select m;
                PageCount = q.LongCount();
                return q.Skip((Currentpage - 1) * PageSize).Take(PageSize).ToList();


            }
        }


        public static void SavePostalCard(PostalCard PostalCard)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.PostalCards.ApplyChanges(PostalCard);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("PostalCard_DataProvider_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }





    }
}
