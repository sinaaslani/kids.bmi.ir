using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{
    public class FAQ_DataProvider : BaseDataProvider
    {
        public static List<FAQCategory> GetFAQCategory(int? FAQCatId = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.FAQCategories
                        where (!FAQCatId.HasValue || m.CategoryId == FAQCatId.Value)
                        orderby m.SortOrderId descending
                        select m;

                return q.ToList();

            }
        }

        public static List<FAQ> GetFAQ(long? FAQId = null, int? FAQCatId = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.FAQs
                        where (!FAQId.HasValue || m.FAQId == FAQId.Value) &&
                        (!FAQCatId.HasValue || m.CategoryId == FAQCatId.Value)
                        orderby m.SortOrderId descending
                        select m;

                return q.ToList();

            }
        }

        public static void Save(FAQ faq)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.FAQs.ApplyChanges(faq);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("FAQ_DataProvider_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }

        public static void Save(FAQCategory faqCat)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.FAQCategories.ApplyChanges(faqCat);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("faqCat_DataProvider_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }
    }
}
