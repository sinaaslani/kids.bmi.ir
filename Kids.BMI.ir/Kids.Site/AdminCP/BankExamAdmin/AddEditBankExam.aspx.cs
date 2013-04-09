using System;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.BankExamAdmin
{
    public partial class AddEditBankExam : AdminSecureFormBaseClass
    {

        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsExamAdministrator|| OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                BindScoreTypes();
                string action = UtilityMethod.GetRequestParameter("act");

                if (action.ToLower() == "new")
                {
                    editDelBtn.Visible = false;
                    AddBtn.Visible = true;
                    EditresponseItems.Visible = false;
                    HeaderMsgLbl.Text = "اضافه کردن آزمون جدید";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "edit")
                {
                    AddBtn.Visible = false;
                    editDelBtn.Text = " ذخيره و بهنگام ";
                    editDelBtn.Visible = true;
                    EditresponseItems.Visible = true;
                    HeaderMsgLbl.Text = "ويرايش آزمون";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    AddBtn.Visible = false;
                    EditresponseItems.Visible = false;
                    editDelBtn.Text = " حذف  ";
                    HeaderMsgLbl.Text = "حذف آزمون";
                    editDelBtn.Visible = true;
                    SetInitiateValues(action);
                }
            }
        }

        private void BindScoreTypes()
        {
            var scoretypes= Score_DataProvider.GetScoresTypes();

            drpScoreTypeId.Items.Add(new ListItem("---------",""));
            foreach (ScoreType type in scoretypes)
            {
                drpScoreTypeId.Items.Add(new ListItem(type.ScoreFaName + "--->" + type.Description, type.CategoryId.ToString()));
            }
        }

        private void SetInitiateValues(string action)
        {
            if (action.ToLower() == "edit" || action.ToLower() == "del")
            {
                int? ExamId = null;
                if (UtilityMethod.GetRequestParameter("pId").IsInt32())
                    ExamId = UtilityMethod.GetRequestParameter("pId").ToInt32();
                BankStoryExam Exam = BankStory_DataProvider.GetExams(ExamId: ExamId).FirstOrDefault();

                if (Exam == null)
                    return;

                txtExamName.Text = Exam.ExamName;
                txtExamDescription.Text = Exam.Description;
                txtExamDuration.Text = Exam.DurationTime.ToString();
                ucActiveFromDate.SelectedDateTime = Exam.IsActiveFromDate;
                ucActiveToDate.SelectedDateTime = Exam.IsActiveToDate;
                drpScoreTypeId.SelectedValue = Exam.ScoreTypeId.ToString();


                EditresponseItems.NavigateUrl = "BankExamQuestionList.aspx?pid=" + ExamId;

            }
            else if (action.ToLower() == "new")
            {
            }
        }

        private BankStoryExam GetPollInfoFromSkin()
        {
            BankStoryExam p = new BankStoryExam
                 {
                     ExamName = txtExamName.Text,
                     Description = txtExamDescription.Text,
                     DurationTime = txtExamDuration.Text.ToInt32(),
                     IsActiveFromDate = ucActiveFromDate.SelectedDateTime.Value,
                     IsActiveToDate = ucActiveToDate.SelectedDateTime.Value,
                     ScoreTypeId = drpScoreTypeId.SelectedValue.ToInt32(),
                     CreateDateTime = DateTime.Now,

                 };

            return p;

        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            BankStoryExam p = GetPollInfoFromSkin();

            int ExamId = BankStory_DataProvider.SaveExam(p);
            Page.Response.Redirect("AddEditBankExamQuestion.aspx?act=new&pid=" + ExamId);
        }

        protected void editDelBtn_Click(object sender, EventArgs e)
        {
            int? ExamId = null;
            if (UtilityMethod.GetRequestParameter("pId").IsInt32())
                ExamId = UtilityMethod.GetRequestParameter("pId").ToInt32();

            BankStoryExam Exam = BankStory_DataProvider.GetExams(ExamId).FirstOrDefault();
            if (Exam == null)
                return;

            string action = UtilityMethod.GetRequestParameter("act");
            if (action.ToLower() == "del")
            {
                Exam.MarkAsDeleted();
                BankStory_DataProvider.SaveExam(Exam);
                Page.Response.Redirect("BankExamList.aspx");
            }
            BankStoryExam p = GetPollInfoFromSkin();
            p.ExamId = ExamId.Value;
            p.MarkAsModified();

            BankStory_DataProvider.SaveExam(p);
            Page.Response.Redirect("BankExamList..aspx");

        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("BankExamList..aspx");

        }



    }


}
