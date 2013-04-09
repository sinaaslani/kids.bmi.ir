using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{

    public class Festival_DataProvider : BaseDataProvider
    {

        public static List<Festival> GetFestival(int? FestivalId = null, string FestivalName = null)
        {
            int PageCount;
            return GetFestival(out PageCount, FestivalId, FestivalName);
        }

        public static List<Festival> GetFestival(out int PageCount, int? FestivalId = null, string FestivalName = null,
                                                 DateTime? FromDate = null, DateTime? ToDate = null,
                                         int Currentpage = 1, int PageSize = DefaultPageSize)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.Festivals
                        where

                        (!FestivalId.HasValue || m.FestivalId == FestivalId.Value) &&
                        (!FromDate.HasValue || m.FromDate >= FromDate.Value) &&
                        (!ToDate.HasValue || m.ToDate >= ToDate.Value) &&
                        (!FestivalId.HasValue || m.FestivalId == FestivalId.Value) &&
                        (string.IsNullOrEmpty(FestivalName) || m.Name.Contains(FestivalName))
                        orderby m.FestivalId descending
                        select m;
                PageCount = q.Count();
                return q.Skip((Currentpage - 1) * PageSize).Take(PageSize).ToList();


            }
        }


        public static void SaveFestival(Festival festival)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    if (festival.ChangeTracker.State == ObjectState.Unchanged)
                        festival.MarkAsModified();

                    ctx.Festivals.ApplyChanges(festival);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("Festival_DataProvider_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }


        public static Festival GetActiveFestival(DateTime dt)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.Festivals
                        where (m.FromDate <= dt) && (m.ToDate >= dt)
                        select m;
                return q.FirstOrDefault();
            }
        }

        public static List<Festival_Pictures> GetFestivalPics(out int PageCount, Festival Curentfsv, bool? Approved = null, int Currentpage = 1, int PageSize = DefaultPageSize)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.Festival_Pictures
                        where
                        (m.FestivalId == Curentfsv.FestivalId) &&
                        (!Approved.HasValue || m.IsApproved == Approved.Value)
                        select m;
                PageCount = q.Count();
                return q.OrderBy(x => Guid.NewGuid()).Skip((Currentpage - 1) * PageSize).Take(PageSize).ToList();
            }
        }
    }
}
