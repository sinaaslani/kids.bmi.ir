using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.EntitiesModel.Scores;
using Kids.LoggingHelper;

namespace Site.Kids.bmi.ir.Scores
{
    public class ScoreCalculator
    {
        private readonly HttpContext context;

        private ScoreCalculator(HttpContext c)
        {
            context = c;
        }
        private static ScoreCalculator _Instance;
        public static ScoreCalculator Instance(HttpContext c)
        {
            return _Instance ?? (_Instance = new ScoreCalculator(c));
        }

        DateTime? LastCalculationDateTime
        {
            get
            {
                if (context.Application["LastCalculationDateTime"] != null)
                    return context.Application["LastCalculationDateTime"] as DateTime?;
                return null;
            }
            set { context.Application["LastCalculationDateTime"] = value; }
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    if (LastCalculationDateTime == null ||
                        DateTime.Now.Subtract(LastCalculationDateTime.Value).TotalHours > SystemConfigs.ScoreCalculationInterval
                        )
                    {
                        int currentPage = 0;
                        while (true)
                        {
                            int PageCount;
                            currentPage++;

                            var UserList = KidsUser_DataProvider.GetKidsUser(out PageCount, Currentpage: currentPage);

                            if (!UserList.Any())
                                break;

                            foreach (KidsUser user in UserList)
                            {
                                List<scoreListItem> DailyscoreList, MonthlyscoreList;
                                user.LastCalculatedScore = ScoreHelper.CalculateScore(user, true, out DailyscoreList, out MonthlyscoreList);
                                KidsUser_DataProvider.SaveKidsUser(user, this, null);
                            }

                        }
                        LastCalculationDateTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("ScoreCalculator", ex, EventLogEntryType.Error);
                }
                finally
                {
                    Thread.Sleep(60000 * 60);
                }
            }
        }
    }
}