using System;
using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.PollsAdmin
{
    public partial class AddEditPollItem : AdminSecureFormBaseClass
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
                    HeaderMsgLbl.Text = "اضافه کردن گزينه جديد";
                    HasNextResponseItem.Visible = true;
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "edit")
                {
                    AddBtn.Visible = false;
                    HasNextResponseItem.Visible = false;
                    editDelBtn.Text = " ذخيره و بهنگام ";
                    HeaderMsgLbl.Text = "ويرايش گزينه";
                    editDelBtn.Visible = true;
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    AddBtn.Visible = false;
                    HasNextResponseItem.Visible = false;
                    HeaderMsgLbl.Text = "حذف گزينه";
                    editDelBtn.Text = " حذف  ";
                    editDelBtn.Visible = true;
                    SetInitiateValues(action);
                }
            }
        }

        private void SetInitiateValues(string action)
        {
            long pollId = -1;
            if (action.ToLower() == "edit" || action.ToLower() == "del")
            {
                long itemId = -1;
                if (UtilityMethod.GetRequestParameter("itmId").IsInt64())
                    itemId = Convert.ToInt64(UtilityMethod.GetRequestParameter("itmId"));
                PollResponseItem item = Poll_DataProvider.GetPollResponseItem(itemId).FirstOrDefault();

                if (item == null)
                    return;

                ItemTextCtrl.Text = item.ItemText;
                pollTitleLbl.Text = item.PollQuestion.Title;



            }
            else if (action.ToLower() == "new")
            {

                //No.Checked = true ;
                ItemTextCtrl.Text = "";
                if (UtilityMethod.GetRequestParameter("pId").IsInt64())
                    pollId = Convert.ToInt64(UtilityMethod.GetRequestParameter("pId"));

                PollQuestion p = Poll_DataProvider.GetPoll(pollId).FirstOrDefault();
                if (p != null)
                    pollTitleLbl.Text = p.Title;
            }




        }

        private PollResponseItem GetItemInfoFromSkin()
        {
            PollResponseItem item = new PollResponseItem { ItemText = ItemTextCtrl.Text };
            return item;
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            long pollqId = -1;
            if (UtilityMethod.GetRequestParameter("pId").IsInt64())
                pollqId = Convert.ToInt64(UtilityMethod.GetRequestParameter("pId"));

            PollResponseItem pi = GetItemInfoFromSkin();
            PollQuestion p = Poll_DataProvider.GetPoll(pollqId).FirstOrDefault();
            p.PollResponseItems.Add(pi);

            p.MarkAsModified();

            Poll_DataProvider.SavePoll(p);
            if (!HasNextResponseItem.Checked)
            {
                Page.Response.Redirect("pollItemsList.aspx?pid=" + pollqId);
            }
            else // create newxt poll item
            {
                Page.Response.Redirect("AddEditPollItem.aspx?act=new&pid=" + pollqId);
            }

        }

        protected void editDelBtn_Click(object sender, EventArgs e)
        {
            long itemId = -1;
            if (UtilityMethod.GetRequestParameter("itmId").IsInt64())
                itemId = Convert.ToInt64(UtilityMethod.GetRequestParameter("itmId"));

            PollResponseItem oldItem = Poll_DataProvider.GetPollResponseItem(itemId).FirstOrDefault();
            if (oldItem == null)
                return;

            string action = UtilityMethod.GetRequestParameter("act");
            if (action.ToLower() == "del")
            {
                oldItem.MarkAsDeleted();
                Poll_DataProvider.SavePollResponseItem(oldItem);
                Page.Response.Redirect("PollItemsList.aspx?pid=" + oldItem.PollQuestionsId);
            }
            else if (action.ToLower() == "edit")
            {
                oldItem.ItemText = ItemTextCtrl.Text;
                oldItem.MarkAsModified();
                Poll_DataProvider.SavePollResponseItem(oldItem);
                Page.Response.Redirect("PollItemsList.aspx?pid=" + oldItem.PollQuestionsId);
            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            if (action.ToLower() == "new")
            {
                long pollId = -1;
                if (UtilityMethod.GetRequestParameter("pId").IsInt64())
                    pollId = Convert.ToInt64(UtilityMethod.GetRequestParameter("pId"));
                Page.Response.Redirect("PollItemsList.aspx?pid=" + pollId);
            }
            else
            {
                long itemId = -1;
                if (UtilityMethod.GetRequestParameter("itmId").IsInt64())
                    itemId = Convert.ToInt64(UtilityMethod.GetRequestParameter("itmId"));
                long pollId = Poll_DataProvider.GetPollResponseItem(itemId).FirstOrDefault().PollQuestionsId;
                Page.Response.Redirect("PollItemsList.aspx?pid=" + pollId);
            }
        }
    }
}