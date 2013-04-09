using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel.Scores;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Scores
{
    public partial class ScoreList : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetUserInfo(IEnumerable<scoreListItem> Dailyscorelist, IEnumerable<scoreListItem> Monthlyscorelist)
        {
            dgDailyScoreList.DataSource = Dailyscorelist.OrderByDescending(o=>o.Date).ToList();
            dgDailyScoreList.DataBind();

            dgMonthlyScoreList.DataSource = Monthlyscorelist.OrderByDescending(o => o.Date).ToList();
            dgMonthlyScoreList.DataBind();
        }

        protected void dgDailyScoreList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ScoreItem = e.Row.DataItem as scoreListItem;

                var lblScoreName = e.Row.FindControl("lblScoreName") as Label;
                lblScoreName.Text = ScoreItem.scoreType.ScoreFaName;

                var lblMaxPerDay = e.Row.FindControl("lblMaxPerDay") as Label;
                lblMaxPerDay.Text = ScoreItem.scoreType.MaxPerDay.ToString();

                //var lblMaxPerMonth = e.Row.FindControl("lblMaxPerMonth") as Label;
                //lblMaxPerMonth.Text = ScoreItem.scoreType.MaxPerMonth.ToString();


                var lblCoefficentValue = e.Row.FindControl("lblCoefficentValue") as Label;
                lblCoefficentValue.Text = ScoreItem.scoreType.CoefficentValue.ToString();


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                var lblSum_NotFiltered = e.Row.FindControl("lblSum_NotFiltered") as Label;
                lblSum_NotFiltered.Text = (dgDailyScoreList.DataSource as List<scoreListItem>).Sum(o=>o.Sum_NotFiltered).ToString();

                var lblSum_Filtered = e.Row.FindControl("lblSum_Filtered") as Label;
                lblSum_Filtered.Text = (dgDailyScoreList.DataSource as List<scoreListItem>).Sum(o => o.Sum_Filtered).ToString();
            }
        }

        protected void dgMonthlyScoreList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ScoreItem = e.Row.DataItem as scoreListItem;

                var lblScoreName = e.Row.FindControl("lblScoreName") as Label;
                lblScoreName.Text = ScoreItem.scoreType.ScoreFaName;

                //var lblMaxPerDay = e.Row.FindControl("lblMaxPerDay") as Label;
                //lblMaxPerDay.Text = ScoreItem.scoreType.MaxPerDay.ToString();

                var lblMaxPerMonth = e.Row.FindControl("lblMaxPerMonth") as Label;
                lblMaxPerMonth.Text = ScoreItem.scoreType.MaxPerMonth.ToString();


                var lblCoefficentValue = e.Row.FindControl("lblCoefficentValue") as Label;
                lblCoefficentValue.Text = ScoreItem.scoreType.CoefficentValue.ToString();
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                var lblSum_NotFiltered = e.Row.FindControl("lblSum_NotFiltered") as Label;
                lblSum_NotFiltered.Text = (dgMonthlyScoreList.DataSource as List<scoreListItem>).Sum(o => o.Sum_NotFiltered).ToString();

                var lblSum_Filtered = e.Row.FindControl("lblSum_Filtered") as Label;
                lblSum_Filtered.Text = (dgMonthlyScoreList.DataSource as List<scoreListItem>).Sum(o => o.Sum_Filtered).ToString();
            }
        }
    }
}