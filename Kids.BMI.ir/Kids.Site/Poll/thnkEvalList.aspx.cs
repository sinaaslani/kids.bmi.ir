using System;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Poll
{
    public partial class thnkEvalList : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ThbkEvalGrid.DataSource = QuestionnaireForm_DataProvider.GetQuestionnaireForm(null,QuestionnaireStatusType.Confirmed);
            ThbkEvalGrid.DataBind();
        }

        protected void ThbkEvalGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnk = (HyperLink)e.Row.FindControl("lnkForm");
                QuestionnaireForm qForm = (QuestionnaireForm)e.Row.DataItem;
                lnk.NavigateUrl = "ShowThnkEval.aspx?frmId=" + qForm.FormId;

            }
        }
    }
}