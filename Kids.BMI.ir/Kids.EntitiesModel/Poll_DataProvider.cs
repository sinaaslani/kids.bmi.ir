using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{
   
    public class Poll_DataProvider : BaseDataProvider
    {
        
        public static List<PollQuestion> GetPoll(long? QuestionId = null, bool? IsActive = null)
        {

            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.PollQuestions.Include("PollResponseItems")
                        where (!IsActive.HasValue || m.IsActive == IsActive.Value)
                        && (!QuestionId.HasValue || m.QuestionId == QuestionId.Value)
                        orderby m.CreateDateTime descending 
                        select m;

                return q.ToList();
            }



        }


        public static void SavePollResponse(PollUserResponse UserResp)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    UserResp.Serial = Guid.NewGuid();
                    ctx.PollUserResponses.ApplyChanges(UserResp);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("Poll_DataProvider_SavePollResponse", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }


        public class UserResponsResult
        {
            public long itemId { get; set; }
            public int count { get; set; }

        }

        public static IEnumerable<UserResponsResult> GetPollUserResponse(PollQuestion Question, out long? TotalUserResp)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                TotalUserResp = ctx.PollUserResponses.LongCount(o => (long?)o.QuestionId == Question.QuestionId);


                var q = from m in ctx.PollUserResponses.Where(o => o.QuestionId == Question.QuestionId).GroupBy(o => o.ResponseItemId)
                        select new UserResponsResult
                        {
                            itemId = m.Key,
                            count = m.Count(),

                        };

                return q.ToList();
            }
        }

        public static long SavePoll(PollQuestion p)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.PollQuestions.ApplyChanges(p);
                    ctx.SaveChanges();
                    return p.QuestionId;
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("Poll_DataProvider_SavePoll", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }

        public static IEnumerable<PollResponseItem> GetPollResponseItem(long? ItemId=null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.PollResponseItems.Include("PollQuestion") 
                        where (!ItemId.HasValue || m.ItemId == ItemId.Value)
                        select m;

                return q.ToList();
            }
        }

        public static void SavePollResponseItem(PollResponseItem pItem)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.PollResponseItems.ApplyChanges(pItem);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("Poll_DataProvider_SavePoll", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }
    }
}
