using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{

    public enum QuestionnaireStatusType
    {
        NotConfirmed = 0,
        Confirmed = 1
    }

    public enum ResponseItemsType
    {
        CheckBox = 1,
        RadioButton = 0,
        InputText = 2,
    }


    public class QuestionnaireForm_DataProvider : BaseDataProvider
    {
        public static List<QuestionnaireForm> GetQuestionnaireForm(long? FormId = null, QuestionnaireStatusType? Status = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.QuestionnaireForms.Include("QuestionnaireForm_Questions").Include("QuestionnaireForm_Questions.QuestionnaireForms_ResponseItems")
                        where (!FormId.HasValue || m.FormId == FormId.Value)
                        && (!Status.HasValue || m.Status == (int)Status.Value)
                        select m;

                return q.ToList();
            }
        }

        public static List<QuestionnaireForm_Questions> GetQuestion(long? questionId = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.QuestionnaireForm_Questions.Include("QuestionnaireForm").Include("QuestionnaireForms_ResponseItems")
                        where (!questionId.HasValue || m.QuestionId == questionId.Value)
                        select m;

                return q.ToList();
            }
        }

        public static void SaveUserResponse(QuestionnaireForm_UserResponses resp)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.QuestionnaireForm_UserResponses.ApplyChanges(resp);
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

        public static List<QuestionnaireForms_ResponseItems> GetQuestionnaireForms_ResponseItem(long? ItemId = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.QuestionnaireForms_ResponseItems.Include("QuestionnaireForm_Questions")
                        where (!ItemId.HasValue || m.ItemId == ItemId.Value)
                        select m;

                return q.ToList();
            }
        }
    }

}
