using System;
using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.PollsAdmin
{
    public partial class PollItemsList : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsPollAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UtilityMethod.GetRequestParameter("pId").IsInt64())
            {
                long? pollId = UtilityMethod.GetRequestParameter("pId").ToLong();

                PollQuestion poll = Poll_DataProvider.GetPoll(pollId).FirstOrDefault();
                if (poll != null)
                {
                    pollTitleLbl.Text = poll.Title;
                    PollitemsGrid.DataSource = poll.PollResponseItems;
                    PollitemsGrid.DataBind();
                    AddNewItemLnk.NavigateUrl = "AddEditPollItem.aspx?act=new&pid=" + pollId;

                    showPostResult(poll);
                }
               
            }
        }


        private void showPostResult(PollQuestion q)
        {
            long? TotalUserResp;
            var userResp = Poll_DataProvider.GetPollUserResponse(q, out TotalUserResp);

            tblrseult.Text = "<table>";
            tblrseult.Text += "<tr><td colspan=2 dir=rtl></td></tr>";

            foreach (var ri in userResp)
            {
                tblrseult.Text += "<tr>";
                tblrseult.Text += "<td><font class='normalTextSmall' >" + q.PollResponseItems.FirstOrDefault(o => o.ItemId == ri.itemId).ItemText + "</font></td>";

                tblrseult.Text += "<td><font class='normalTextSmall'>" + Math.Round((ri.count / (double)TotalUserResp.Value) * 100, 2) + "%</font></td>";
                tblrseult.Text += "</tr>";

            }
            tblrseult.Text += "<tr><td align=center class=normaltextSmall> تعداد آراء " + TotalUserResp.ToString() + " راي</td></tr>";
            tblrseult.Text += "</table>";


        }

        
    }
}