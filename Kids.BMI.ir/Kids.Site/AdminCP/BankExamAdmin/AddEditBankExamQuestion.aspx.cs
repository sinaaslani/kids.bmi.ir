using System;
using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.BankExamAdmin
{
    public partial class AddEditBankExamQuestion : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsExamAdministrator || OnlineSystemUser.IsSiteAdministrator))
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
                    HeaderMsgLbl.Text = "اضافه کردن سوال جديد";
                    HasNextResponseItem.Visible = true;
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "edit")
                {
                    AddBtn.Visible = false;
                    HasNextResponseItem.Visible = false;
                    editDelBtn.Text = " ذخيره و بهنگام ";
                    HeaderMsgLbl.Text = "ويرايش سوال";
                    editDelBtn.Visible = true;
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    AddBtn.Visible = false;
                    HasNextResponseItem.Visible = false;
                    HeaderMsgLbl.Text = "حذف سوال";
                    editDelBtn.Text = " حذف  ";
                    editDelBtn.Visible = true;
                    SetInitiateValues(action);
                }
            }
        }

        private void SetInitiateValues(string action)
        {
            int? ExamId = null;
            if (action.ToLower() == "edit" || action.ToLower() == "del")
            {
                int? itemId = null;
                if (UtilityMethod.GetRequestParameter("itmId").IsInt32())
                    itemId = UtilityMethod.GetRequestParameter("itmId").ToInt32();

                BankStoryExam_Question item = BankStory_DataProvider.GetExamsQuestion(itemId).FirstOrDefault();

                if (item == null)
                    return;



                txtQuestionBody.Text = item.QuestionBody;
                txtQuestionAnswerA.Text = item.AnswerA;
                txtQuestionAnswerB.Text = item.AnswerB;
                txtQuestionAnswerC.Text = item.AnswerC;
                txtQuestionAnswerD.Text = item.AnswerD;
                drpCorrectAnswer.SelectedValue = item.Answer.ToString();

                pollTitleLbl.Text = item.BankStoryExam.ExamName;

            }
            else if (action.ToLower() == "new")
            {

                txtQuestionBody.Text ="";
                txtQuestionAnswerA.Text = "";
                txtQuestionAnswerB.Text = "";
                txtQuestionAnswerC.Text = "";
                txtQuestionAnswerD.Text = "";

                if (UtilityMethod.GetRequestParameter("pId").IsInt64())
                    ExamId = UtilityMethod.GetRequestParameter("pId").ToInt32();

                BankStoryExam p = BankStory_DataProvider.GetExams(ExamId).FirstOrDefault();
                if (p != null)
                    pollTitleLbl.Text = p.ExamName;
            }




        }

        private BankStoryExam_Question GetItemInfoFromSkin()
        {
            BankStoryExam_Question item = new BankStoryExam_Question
                {
                    QuestionBody = txtQuestionBody.Text,
                    AnswerA = txtQuestionAnswerA.Text,
                    AnswerB = txtQuestionAnswerA.Text,
                    AnswerC = txtQuestionAnswerA.Text,
                    AnswerD = txtQuestionAnswerA.Text,
                    Answer = drpCorrectAnswer.SelectedValue.ToInt32()
                };
            return item;
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            int? ExamId = null;
            if (UtilityMethod.GetRequestParameter("pId").IsInt32())
                ExamId = UtilityMethod.GetRequestParameter("pId").ToInt32();

            BankStoryExam_Question pi = GetItemInfoFromSkin();
            BankStoryExam p = BankStory_DataProvider.GetExams(ExamId).FirstOrDefault();
            p.BankStoryExam_Question.Add(pi);

            p.MarkAsModified();

            BankStory_DataProvider.SaveExam(p);
            if (!HasNextResponseItem.Checked)
            {
                Page.Response.Redirect("BankExamList.aspx?pid=" + ExamId);
            }
            else // create newxt poll item
            {
                Page.Response.Redirect("AddEditBankExamQuestion.aspx?act=new&pid=" + ExamId);
            }

        }

        protected void editDelBtn_Click(object sender, EventArgs e)
        {
            int? itemId = null;
            if (UtilityMethod.GetRequestParameter("itmId").IsInt32())
                itemId = UtilityMethod.GetRequestParameter("itmId").ToInt32();

            BankStoryExam_Question oldItem = BankStory_DataProvider.GetExamsQuestion(itemId).FirstOrDefault();
            if (oldItem == null)
                return;

            string action = UtilityMethod.GetRequestParameter("act");
            if (action.ToLower() == "del")
            {
                oldItem.MarkAsDeleted();
                BankStory_DataProvider.SaveExamQuestion(oldItem);
                Page.Response.Redirect("BankExamList.aspx?pid=" + oldItem.ExamId);
            }
            else if (action.ToLower() == "edit")
            {
                oldItem.QuestionBody = txtQuestionBody.Text;
                oldItem.AnswerA = txtQuestionAnswerA.Text;
                oldItem.AnswerB = txtQuestionAnswerA.Text;
                oldItem.AnswerC = txtQuestionAnswerA.Text;
                oldItem.AnswerD = txtQuestionAnswerA.Text;
                oldItem.Answer = drpCorrectAnswer.SelectedItem.ToInt32();

                oldItem.MarkAsModified();
                BankStory_DataProvider.SaveExamQuestion(oldItem);
                Page.Response.Redirect("BankExamList.aspx?pid=" + oldItem.ExamId);
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
                Page.Response.Redirect("BankExamList.aspx?pid=" + pollId);
            }
            else
            {
                int? itemId = null;
                if (UtilityMethod.GetRequestParameter("itmId").IsInt32())
                    itemId = UtilityMethod.GetRequestParameter("itmId").ToInt32();
                long pollId = BankStory_DataProvider.GetExamsQuestion(itemId).FirstOrDefault().ExamId;
                Page.Response.Redirect("BankExamList.aspx?pid=" + pollId);
            }
        }
    }
}