using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;
using Kids.Utility;

namespace Kids.EntitiesModel
{
    public class ScoreTypeRange
    {
        public double From { get; set; }

        public double To { get; set; }

        public double CoefficentValue { get; set; }
    }

    public static partial class extension
    {
        public static List<ScoreTypeRange> GetRangeScore(this ScoreType score)
        {
            if (score.IsRangeBaseScore)
            {
                var rangeDef = score.RangeDefinition;
                if (string.IsNullOrWhiteSpace(rangeDef))
                    throw new ArgumentOutOfRangeException("Invalid ScoreRange Definitaion");

                var ArrrangeDef = rangeDef.Split(new[] { "}{" }, StringSplitOptions.RemoveEmptyEntries);

                return ArrrangeDef.Select(s => s.Split(new[] { ":" }, StringSplitOptions.None)).Select(ArrValues => new ScoreTypeRange
                    {
                        From = string.IsNullOrWhiteSpace(ArrValues[0]) ? double.MinValue : ArrValues[0].ToDouble(),
                        To = string.IsNullOrWhiteSpace(ArrValues[1]) ? double.MaxValue : ArrValues[1].ToDouble(),
                        CoefficentValue = ArrValues[2].ToDouble()
                    }).ToList();
            }
            return null;
        }
    }

    public class Score_DataProvider : BaseDataProvider
    {
        public static List<ScoreTypeCategory> GetScoreTypeCategory(int? ScoreTypeCatId = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.ScoreTypeCategories
                        where (!ScoreTypeCatId.HasValue || m.CategoryId == ScoreTypeCatId.Value)
                        select m;

                return q.ToList();
            }
        }

        public static IEnumerable<ScoreType> GetScoresTypes(long? ScoreTypeId = null, long? ScoreTypeCategoryId = null, string ScoreEnName = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.ScoreTypes
                        where (!ScoreTypeId.HasValue || m.Id == ScoreTypeId.Value) &&
                              (!ScoreTypeCategoryId.HasValue || m.CategoryId == ScoreTypeCategoryId.Value) &&
                              (string.IsNullOrEmpty(ScoreEnName) || m.ScoreEnName == ScoreEnName)
                        select m;

                return q.ToList();
            }
        }

        public static IEnumerable<Kids_Scores> GetKids_Scores(long? KidsUserId = null, int? ScoreTypeId = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.Kids_Scores
                        where (!KidsUserId.HasValue || m.KidsUserId == KidsUserId.Value) &&
                        (!ScoreTypeId.HasValue || m.ScoreTypeId == ScoreTypeId.Value) &&
                         (!FromDate.HasValue || m.CreateDateTime >= FromDate.Value) &&
                         (!ToDate.HasValue || m.CreateDateTime <= ToDate.Value)
                        select m;

                return q.ToList();
            }
        }


        internal static void SaveScore(Kids_Scores game)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.Kids_Scores.ApplyChanges(game);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("Kids_Scores_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }






        public static void SaveScoreType(ScoreType ScoreType)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.ScoreTypes.ApplyChanges(ScoreType);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("SaveScoreType_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }

        public static void Save(ScoreTypeCategory cpCat)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.ScoreTypeCategories.ApplyChanges(cpCat);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("ScoreTypeCategories_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }
    }
}
