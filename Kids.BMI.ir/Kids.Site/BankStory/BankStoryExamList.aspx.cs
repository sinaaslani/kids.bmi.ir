using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.BankStory
{
    public partial class BankStoryExamList : KidsSecureFormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindExamListGrid();
        }

        private void BindExamListGrid()
        {
            List<BankStoryExam> Exams = BankStory_DataProvider.GetExams(CurrentdateTime: DateTime.Now);
            dgExamList.DataSource = Exams;
            dgExamList.DataBind();
        }

        protected override void CheckKidsUser()
        {

        }

        protected void dgExamList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblExamQuestionCount = e.Row.FindControl("lblExamQuestionCount") as Label;
                lblExamQuestionCount.Text = (e.Row.DataItem as BankStoryExam).BankStoryExam_Question.Count.ToString();
            }
        }
    }
}