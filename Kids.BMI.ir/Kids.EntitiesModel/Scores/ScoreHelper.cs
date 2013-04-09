using System;
using System.Collections.Generic;
using System.Linq;
using Kids.Utility;

namespace Kids.EntitiesModel.Scores
{
    public class scoreListItem
    {
        public ScoreType scoreType { get; set; }
        public string Date { get; set; }
        public double Sum_Filtered { get; set; }

        public double Sum_NotFiltered { get; set; }

    }

    public static class ScoreHelper
    {
        private enum SpecialScoreType
        {
            ACCREMAIN,
            REGISTRATION,
            ADDACCOUNT,
            POLL,
        }

        public static double CalculateScore(KidsUser user, bool CalcRemain, out List<scoreListItem> DailyscoreList, out List<scoreListItem> MonthlyscoreList)
        {
            DailyscoreList = new List<scoreListItem>();
            MonthlyscoreList = new List<scoreListItem>();
            double CalculatedScore = 0.0;

            if (user != null)
            {
                var scoreTypes = Score_DataProvider.GetScoresTypes();
                var userScore = user.Kids_Scores.ToList();

                foreach (ScoreType scoreType in scoreTypes)
                {
                    var specialtype = scoreType.ScoreEnName.ToEnumByName<SpecialScoreType>();
                    if (specialtype.HasValue && IsSpecialScoreType(scoreType))
                    {
                        if (!CalcRemain && specialtype.Value == SpecialScoreType.ACCREMAIN)
                            continue;

                        CalculatedScore += CalculateSpecialScore(user, scoreType, ref DailyscoreList, ref MonthlyscoreList);
                    }
                    else
                    {
                        CalculatedScore = !scoreType.IsRangeBaseScore ?
                            CalculateScalerScore(ref DailyscoreList, ref MonthlyscoreList, userScore, scoreType, CalculatedScore) :
                            CalculateVectorScore(ref DailyscoreList, ref MonthlyscoreList, scoreType, userScore, CalculatedScore);
                    }
                }

            }
            return CalculatedScore;
        }

        private static double CalculateVectorScore(ref List<scoreListItem> DailyscoreList, ref List<scoreListItem> MonthlyscoreList,
                                                   ScoreType scoreType, List<Kids_Scores> userScore, double CalculatedScore)
        {
            int scoreTypeId = scoreType.Id;
            //////////////////////////////Daily///////////////////////////////////////////////////////////////////////////////////
            var Ranges = scoreType.GetRangeScore();

            var userscore_PerDayList = userScore.Where(o => o.ScoreTypeId == scoreTypeId)
                                                .GroupBy(o => o.PersianCreateDateTime);

            double total_Today = 0;
            foreach (var userscore_Day in userscore_PerDayList)
            {
                double TodayScore = 0;
                foreach (Kids_Scores score in userscore_Day)
                {
                    var r = Ranges.FirstOrDefault(o => score.Value >= o.From && score.Value <= o.To);
                    if (r == null)
                        throw new ArgumentOutOfRangeException(string.Format("No Range Found for this Value :{0} --- Value:{1}", scoreType.ScoreEnName, score.Value));
                    TodayScore += score.Value * r.CoefficentValue;
                }
                total_Today += Math.Min(TodayScore, scoreType.MaxPerDay);
                DailyscoreList.Add(new scoreListItem
                {
                    scoreType = scoreType,
                    Date = userscore_Day.Key,
                    Sum_NotFiltered = TodayScore,
                    Sum_Filtered = Math.Min(TodayScore, scoreType.MaxPerDay),
                });

            }


            ///////////////////////////////Monthly//////////////////////////////////////////////////////////////////////////////////


            var userscore_PerMonthList = userScore.Where(o => o.ScoreTypeId == scoreTypeId)
                                                  .GroupBy(o => o.PersianCreateDateTime.Substring(0, 6));

            double total_Month = 0;
            foreach (var userscore_Month in userscore_PerMonthList)
            {
                double ThisMonthScore = 0;
                foreach (Kids_Scores score in userscore_Month)
                {
                    var r = Ranges.FirstOrDefault(o => score.Value >= o.From && score.Value <= o.To);
                    if (r == null)
                        throw new ArgumentOutOfRangeException(string.Format("No Range Found for this Value :{0} --- Value:{1}", scoreType.ScoreEnName, score.Value));
                    ThisMonthScore += score.Value * r.CoefficentValue;
                }
                total_Month += Math.Min(ThisMonthScore, scoreType.MaxPerMonth);
                MonthlyscoreList.Add(new scoreListItem
                {
                    scoreType = scoreType,
                    Date = userscore_Month.Key,
                    Sum_NotFiltered = ThisMonthScore,
                    Sum_Filtered = Math.Min(ThisMonthScore, scoreType.MaxPerDay),
                });

            }
            CalculatedScore += Math.Min(total_Month, total_Today);

            return CalculatedScore;
        }

        private static double CalculateScalerScore(ref List<scoreListItem> DailyscoreList, ref List<scoreListItem> MonthlyscoreList,
                                                   List<Kids_Scores> userScore, ScoreType scoreType, double CalculatedScore)
        {
            int scoreTypeId = scoreType.Id;
            int MaxPerDay = scoreType.MaxPerDay;
            int MaxPerMonth = scoreType.MaxPerMonth;
            //////////////////////////////Daily///////////////////////////////////////////////////////////////////////////////////
            var scoresPerDay = userScore.Where(o => o.ScoreTypeId == scoreTypeId)
                                        .GroupBy(o => o.PersianCreateDateTime)
                                        .Select(lst => new
                                            {
                                                Date = lst.Key,
                                                Sum = lst.Sum(o => o.Value) * scoreType.CoefficentValue
                                            }).ToList();

            var scoresPerDay_Filtered = userScore.Where(o => o.ScoreTypeId == scoreTypeId)
                                       .GroupBy(o => o.PersianCreateDateTime)
                                       .Select(lst => new
                                       {
                                           Date = lst.Key,
                                           Sum = Math.Min(lst.Sum(o => o.Value) * scoreType.CoefficentValue, MaxPerDay)
                                       }).ToList();

            var totalByDay = scoresPerDay_Filtered.Sum(o => o.Sum);

            foreach (var day in scoresPerDay_Filtered)
            {
                var Date = day.Date;
                var Sum_NotFiltered = scoresPerDay.First(o => o.Date == Date).Sum;
                var a = new scoreListItem { scoreType = scoreType, Date = day.Date, Sum_Filtered = day.Sum, Sum_NotFiltered = Sum_NotFiltered };
                DailyscoreList.Add(a);
            }

            ///////////////////////////////Monthly//////////////////////////////////////////////////////////////////////////////////
            var scoresPerMonth = userScore.Where(o => o.ScoreTypeId == scoreTypeId)
                                          .GroupBy(o => o.PersianCreateDateTime.Substring(0, 6))
                                          .Select(lst => new
                                              {
                                                  Date = lst.Key,
                                                  Sum = lst.Sum(o => o.Value) * scoreType.CoefficentValue,
                                              }).ToList();

            var scoresPerMonth_Filtred = userScore.Where(o => o.ScoreTypeId == scoreTypeId)
                                          .GroupBy(o => o.PersianCreateDateTime.Substring(0, 6))
                                          .Select(lst => new
                                          {
                                              Date = lst.Key,
                                              Sum = Math.Min(lst.Sum(o => o.Value) * scoreType.CoefficentValue, MaxPerMonth)
                                          }).ToList();

            var totalByMonth = scoresPerMonth.Sum(o => o.Sum);

            foreach (var month in scoresPerMonth_Filtred)
            {
                var Month = month.Date;
                var Sum_NotFiltered = scoresPerMonth.First(o => o.Date == Month).Sum;
                var a = new scoreListItem { scoreType = scoreType, Date = month.Date, Sum_Filtered = month.Sum, Sum_NotFiltered = Sum_NotFiltered };
                MonthlyscoreList.Add(a);
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var total = Math.Min(totalByDay, totalByMonth);
            CalculatedScore += total;
            return CalculatedScore;
        }

        private static double CalculateSpecialScore(KidsUser user, ScoreType scoreType, ref List<scoreListItem> DailyscoreList,
                                                    ref List<scoreListItem> MonthlyscoreList)
        {
            switch (scoreType.ScoreEnName.ToEnumByName<SpecialScoreType>())
            {
                case SpecialScoreType.REGISTRATION:
                    #region
                    {
                        if (user.CurrentStatus == (int)KidsUserStatus.RegisterWithoutConfirmation)
                            return 0;

                        DailyscoreList.Add(new scoreListItem
                            {
                                Date = PersianDateTime.MiladiToPersian(user.CreateDateTime).ToString(),
                                scoreType = scoreType,
                                Sum_Filtered = scoreType.CoefficentValue,
                                Sum_NotFiltered = scoreType.CoefficentValue
                            });

                        MonthlyscoreList.Add(new scoreListItem
                        {
                            Date = PersianDateTime.MiladiToPersian(user.CreateDateTime).ToString(),
                            scoreType = scoreType,
                            Sum_Filtered = scoreType.CoefficentValue,
                            Sum_NotFiltered = scoreType.CoefficentValue
                        });
                        return scoreType.CoefficentValue;
                    }
                    #endregion

                case SpecialScoreType.ADDACCOUNT:
                    #region

                    {
                        if (user != null && !string.IsNullOrWhiteSpace(user.ChildAccNo))
                        {
                            DailyscoreList.Add(new scoreListItem
                                {
                                    Date = PersianDateTime.MiladiToPersian(user.LastUpdateDateTime.Value).ToString(),
                                    scoreType = scoreType,
                                    Sum_Filtered = scoreType.CoefficentValue,
                                    Sum_NotFiltered = scoreType.CoefficentValue
                                });

                            MonthlyscoreList.Add(new scoreListItem
                            {
                                Date = PersianDateTime.MiladiToPersian(user.LastUpdateDateTime.Value).ToString(),
                                scoreType = scoreType,
                                Sum_Filtered = scoreType.CoefficentValue,
                                Sum_NotFiltered = scoreType.CoefficentValue
                            });

                            return scoreType.CoefficentValue;
                        }
                        return 0;
                    }
                    #endregion

                case SpecialScoreType.ACCREMAIN:
                    #region
                    {
                        try
                        {
                            if (user != null && !string.IsNullOrWhiteSpace(user.ChildAccNo))
                            {
                                string tx_date;
                                long Remain = BMICustomer_DataProvider.GetAccRemain(user, out tx_date);
                                var RemainScore = Remain.ToString().ToDouble() * scoreType.CoefficentValue;
                                DailyscoreList.Add(new scoreListItem
                                                  {
                                                      Date = tx_date,
                                                      scoreType = scoreType,
                                                      Sum_NotFiltered = RemainScore,
                                                      Sum_Filtered = Math.Min(RemainScore, scoreType.MaxPerMonth)
                                                  });

                                MonthlyscoreList.Add(new scoreListItem
                                {
                                    Date = tx_date,
                                    scoreType = scoreType,
                                    Sum_NotFiltered = RemainScore,
                                    Sum_Filtered = Math.Min(RemainScore, scoreType.MaxPerMonth)
                                });
                                return Math.Min(RemainScore, scoreType.MaxPerMonth);

                            }
                            return 0;
                        }
                        catch
                        {
                            return 0;
                        }
                    }
                    #endregion

                case SpecialScoreType.POLL:
                    #region
                    {
                        int cnt = user.PollUserResponses.Count(poll => poll.PollQuestion.HasScore);
                        var pollscore = cnt * scoreType.CoefficentValue;


                        var Monthly_PollGroup = user.PollUserResponses.Where(poll => poll.PollQuestion.HasScore)
                                                    .GroupBy(o => PersianDateTime.MiladiToPersian(o.CreateDateTime).ToString().Substring(0, 6)).ToList();

                        var Daily_PollGroup = user.PollUserResponses.Where(poll => poll.PollQuestion.HasScore)
                                                    .GroupBy(o => PersianDateTime.MiladiToPersian(o.CreateDateTime).ToString()).ToList();

                        foreach (var d in Daily_PollGroup)
                        {
                            DailyscoreList.Add(new scoreListItem
                            {
                                Date = d.Key,
                                scoreType = scoreType,
                                Sum_NotFiltered = d.Count() * scoreType.CoefficentValue,
                                Sum_Filtered = d.Count() * scoreType.CoefficentValue
                            });
                        }

                        foreach (var m in Monthly_PollGroup)
                        {
                            MonthlyscoreList.Add(new scoreListItem
                            {
                                Date = m.Key,
                                scoreType = scoreType,
                                Sum_NotFiltered = m.Count() * scoreType.CoefficentValue,
                                Sum_Filtered = Math.Min(m.Count() * scoreType.CoefficentValue, scoreType.MaxPerMonth)
                            });
                        }

                        return Math.Min(pollscore, scoreType.MaxPerMonth);
                    }
                    #endregion

                default:
                    throw new NotImplementedException(scoreType.ScoreEnName);
            }
        }

        private static bool IsSpecialScoreType(ScoreType scoreType)
        {
            switch (scoreType.ScoreEnName.ToEnumByName<SpecialScoreType>())
            {
                case SpecialScoreType.ACCREMAIN:
                case SpecialScoreType.REGISTRATION:
                case SpecialScoreType.ADDACCOUNT:
                case SpecialScoreType.POLL:

                    return true;
                default:
                    return false;
            }
        }

        public static void AddScore(KidsUser user, ScoreType scoreType, Double Value)
        {
            Kids_Scores kscore = new Kids_Scores
            {
                KidsUser = user,
                ScoreTypeId = scoreType.Id,
                Value = Value,
                CreateDateTime = DateTime.Now
            };
            Score_DataProvider.SaveScore(kscore);
        }
    }



}