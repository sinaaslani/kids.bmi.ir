using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{
    public class BankStory_DataProvider : BaseDataProvider
    {
        public static List<BankStoryExam> GetExams(int? ExamId = null, DateTime? CurrentdateTime = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.BankStoryExams.Include("BankStoryExam_Question")
                        where (!ExamId.HasValue || m.ExamId == ExamId.Value) &&
                        (
                                !CurrentdateTime.HasValue ||
                                (m.IsActiveFromDate <= CurrentdateTime.Value && m.IsActiveToDate >= CurrentdateTime.Value)
                        )
                        select m;

                return q.ToList();
            }
        }

        public static List<BankStoryExam_Question> GetExamsQuestion(int? QuestionId)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.BankStoryExam_Question.Include("BankStoryExam") 
                        where (!QuestionId.HasValue || m.QuestionId == QuestionId.Value)
                        select m;

                return q.ToList();
            }
        }

        public static int SaveExam(BankStoryExam Exam)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.BankStoryExams.ApplyChanges(Exam);
                    ctx.SaveChanges();
                    return Exam.ExamId;
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("BankStory_DataProvider_SaveExam", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }



        public static void SaveExamQuestion(BankStoryExam_Question Question)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.BankStoryExam_Question.ApplyChanges(Question);
                    ctx.SaveChanges();

                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("BankStory_DataProvider_SaveExamQuestion", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }
    }
}
