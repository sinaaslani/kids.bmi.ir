using System.Linq;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;
using System;

namespace Site.Kids.bmi.ir.Poll
{

    public partial class showResults : UserControlBaseClass
    {
        private PollQuestion _ActiveQuestion;

        public PollQuestion ActiveQuestion
        {
            get
            {
                if (_ActiveQuestion == null)
                    return _ActiveQuestion = Poll_DataProvider.GetPoll().FirstOrDefault();

                return _ActiveQuestion;
            }
            set
            {
                _ActiveQuestion = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ActiveQuestion == null)
                return;

            long? TotalUserResp;
            var userResp = Poll_DataProvider.GetPollUserResponse(ActiveQuestion, out TotalUserResp);


            tblrseult.Text = "<table cellspacing=5>";
            tblrseult.Text += "<tr><td colspan=2 dir=rtl><span class=RedTitleSmaller>نمايش کل آراء : " + TotalUserResp + "</span></td></tr>";

            foreach (var ri in userResp)
            {
                tblrseult.Text += "<tr>";
                tblrseult.Text += "<td><font class='normalTextSmaller' >" + ActiveQuestion.PollResponseItems.FirstOrDefault(o => o.ItemId == ri.itemId).ItemText + "</font></td>";

                tblrseult.Text += "<td><font class='normalTextSmaller'>" +
                    Math.Round((ri.count / (double)TotalUserResp.Value) * 100, 2) +"%</font></td>";
                tblrseult.Text += "</tr>";

            }
            tblrseult.Text += "</table>";
        }



    }
}
