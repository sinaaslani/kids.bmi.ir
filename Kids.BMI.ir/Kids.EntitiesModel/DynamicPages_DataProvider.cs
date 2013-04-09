using System.Collections.Generic;
using System.Linq;

namespace Kids.EntitiesModel
{
    public class DynamicPages_DataProvider : BaseDataProvider
    {
        public static DynamicPage GetDynamicPageById(long? PageId)
        {
            using (var e = new BMIKidsEntities(ConnectionString))
            {
                var q = from a in e.DynamicPages
                        where (!PageId.HasValue || a.PageId == PageId.Value)
                        select a;
                return q.FirstOrDefault();
            }
        }

        public static List<DynamicPage> GetDynamicPage(out int RecordCount, long? PageId = null,
                                                       int[] PageTypeIds = null, string PageName = null,
                                                       long? ParentPageId = null,
                                                       int _PageSize = DefaultPageSize, int Currentpage = 1)
        {

            using (var e = new BMIKidsEntities(ConnectionString))
            {

                if (PageTypeIds != null && PageTypeIds.Any())
                {
                    var q = from a in e.DynamicPages.WhereIn(o => o.PageTypeId, PageTypeIds)
                            where
                                (!PageId.HasValue || a.PageId == PageId.Value) &&
                                (!ParentPageId.HasValue || a.ParentPageId == ParentPageId.Value) &&
                                (string.IsNullOrEmpty(PageName) || a.PageName == PageName)
                            orderby a.PageId ascending
                            select a;
                    RecordCount = q.Count();
                    return q.Skip((Currentpage - 1) * _PageSize).Take(_PageSize).ToList();
                }
                else
                {
                    var q = from a in e.DynamicPages
                            where
                                (!PageId.HasValue || a.PageId == PageId.Value) &&
                                (!ParentPageId.HasValue || a.ParentPageId == ParentPageId.Value) &&
                                (string.IsNullOrEmpty(PageName) || a.PageName == PageName)
                            orderby a.PageId ascending
                            select a;
                    RecordCount = q.Count();
                    return q.Skip((Currentpage - 1) * _PageSize).Take(_PageSize).ToList();
                }

                
            }
        }

        public static void SaveDynamicPage(DynamicPage p)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                ctx.DynamicPages.ApplyChanges(p);
                ctx.SaveChanges();
                p.AcceptChanges();
            }
        }

        public static IEnumerable<DynamicPageType> GetDynamicPageType()
        {
            using (var e = new BMIKidsEntities(ConnectionString))
            {
                var q = from a in e.DynamicPageTypes
                        select a;
                return q.ToList();
            }
        }
    }
}
