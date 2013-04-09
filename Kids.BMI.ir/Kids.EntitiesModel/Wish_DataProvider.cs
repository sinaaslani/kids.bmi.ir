using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{
    public class Wish_DataProvider : BaseDataProvider
    {

        public static IEnumerable<Wish> GetWish(int? WishId = null, string WishName = null)
        {
            int PageCount;
            return GetWish(out PageCount, WishId, WishName);
        }

        public static List<Wish> GetWish(out int PageCount, int? WishId = null, string WishName = null,
                                         int Currentpage = 1, int PageSize = DefaultPageSize)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.Wishes
                        where (!WishId.HasValue || m.WishId == WishId.Value) &&
                         (string.IsNullOrEmpty(WishName) || m.WishName == WishName)
                        orderby m.WishId descending
                        select m;
                PageCount = q.Count();
                return q.Skip((Currentpage - 1) * PageSize).Take(PageSize).ToList();


            }
        }

        public static void SaveWish(Wish Wish)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.Wishes.ApplyChanges(Wish);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("Wish_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }
    }
}
