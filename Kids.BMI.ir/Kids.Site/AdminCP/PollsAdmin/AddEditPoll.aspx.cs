using System;
using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.PollsAdmin
{
    public partial class AddEditPoll : AdminSecureFormBaseClass
    {

        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsPollAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string action = UtilityMethod.GetRequestParameter("act");

                if (action.ToLower() == "new")
                {
                    editDelBtn.Visible = false;
                    AddBtn.Visible = true;
                    EditresponseItems.Visible = false;
                    HeaderMsgLbl.Text = "اضافه کردن نظر سنجی جديد";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "edit")
                {
                    AddBtn.Visible = false;
                    editDelBtn.Text = " ذخيره و بهنگام ";
                    editDelBtn.Visible = true;
                    EditresponseItems.Visible = true;
                    HeaderMsgLbl.Text = "ويرايش نظر سنجی";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    AddBtn.Visible = false;
                    EditresponseItems.Visible = false;
                    editDelBtn.Text = " حذف  ";
                    HeaderMsgLbl.Text = "حذف نظر سنجی";
                    editDelBtn.Visible = true;
                    SetInitiateValues(action);
                }
            }
        }


        private void SetInitiateValues(string action)
        {
            if (action.ToLower() == "edit" || action.ToLower() == "del")
            {
                long pollId = -1;
                if (UtilityMethod.GetRequestParameter("pId").IsInt64())
                    pollId = Convert.ToInt64(UtilityMethod.GetRequestParameter("pId"));
                PollQuestion poll = Poll_DataProvider.GetPoll(pollId).FirstOrDefault();

                if (poll == null)
                    return;

                TitleCtrl.Text = poll.Title;

                chkBoxResultViewStatus.Checked = poll.UsersCanViewResult;

                EditresponseItems.NavigateUrl = "pollItemsList.aspx?pid=" + pollId;

            }
            else if (action.ToLower() == "new")
            {
            }
        }

        private PollQuestion GetPollInfoFromSkin()
        {
            PollQuestion p = new PollQuestion
                {
                    Title = TitleCtrl.Text,
                    UsersCanViewResult = chkBoxResultViewStatus.Checked,
                    CreateDateTime = DateTime.Now,
                    HasScore = chkHasScore.Checked,
                    IsActive = chkIsActive.Checked
                };

            return p;

        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            PollQuestion p = GetPollInfoFromSkin();

            long pollId = Poll_DataProvider.SavePoll(p);
            Page.Response.Redirect("AddEditPollItem.aspx?act=new&pid=" + pollId);
        }

        protected void editDelBtn_Click(object sender, EventArgs e)
        {
            long pollId = -1;
            if (UtilityMethod.GetRequestParameter("pId").IsInt64())
                pollId = Convert.ToInt64(UtilityMethod.GetRequestParameter("pId"));

            PollQuestion poll = Poll_DataProvider.GetPoll(pollId).FirstOrDefault();
            if (poll == null)
                return;

            string action = UtilityMethod.GetRequestParameter("act");
            if (action.ToLower() == "del")
            {
                poll.MarkAsDeleted();
                Poll_DataProvider.SavePoll(poll);
                Page.Response.Redirect("pollList.aspx");
            }
            PollQuestion p = GetPollInfoFromSkin();
            p.QuestionId = pollId;
            p.MarkAsModified();

            Poll_DataProvider.SavePoll(p);
            Page.Response.Redirect("pollList.aspx");
            
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("pollList.aspx");

        }



    }


}
