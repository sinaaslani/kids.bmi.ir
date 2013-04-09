using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Site.Kids.bmi.ir.AdminCP.ScoreAdmin
{
    public partial class ScoreTypeAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsScoreTypeAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<ScoreTypeCategory> catList = Score_DataProvider.GetScoreTypeCategory();
                foreach (ScoreTypeCategory cpc in catList)
                    drpScoreTypeCategoryId.Items.Add(new ListItem(cpc.CategoryName, cpc.CategoryId.ToString()));

                string action = UtilityMethod.GetRequestParameter("act");
                if (action.ToLower() == "edit")
                {
                    lblHead.Text = " ویرایش اطلاعات نوع امتیاز ";
                    btnSave.Text = " ذخیره و بهنگام ";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    lblHead.Text = " حذف نوع امتیاز ";
                    btnSave.Text = "  حذف  ";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "new")
                {
                    lblHead.Text = " ایجاد نوع امتیاز جدید ";
                    btnSave.Text = " ایجاد نوع امتیاز ";
                    SetInitiateValues(action);
                }

            }
        }


        private void SetInitiateValues(string action)
        {
            if (action.ToLower() == "edit" || action.ToLower() == "del")
            {
                if (UtilityMethod.GetRequestParameter("pid").IsInt64())
                {
                    long? ScoreTypeId = UtilityMethod.GetRequestParameter("pid").ToLong();

                    ScoreType ScoreType = Score_DataProvider.GetScoresTypes(ScoreTypeId).FirstOrDefault();
                    if (ScoreType == null)
                    {
                        ShowMessageBox("مقدار نامعتبر است", "", MessageBoxType.Error);
                        return;
                    }

                    txtCoefficentValue.Text = ScoreType.CoefficentValue.ToString();
                    txtMaxPerDay.Text = ScoreType.MaxPerDay.ToString();
                    txtMaxPerMonth.Text = ScoreType.MaxPerMonth.ToString();
                    txtScoreEnName.Text = ScoreType.ScoreEnName;
                    txtScoreFAName.Text = ScoreType.ScoreFaName;
                    drpScoreTypeCategoryId.Items.FindByValue(ScoreType.CategoryId.ToString()).Selected = true;

                }

            }
            else if (action.ToLower() == "new")
            {
            }



        }


        private ScoreType GetScoreTypeInfoFromSkin()
        {
            long? ScoreTypeId = null;
            if (UtilityMethod.GetRequestParameter("pid").IsInt64())
                ScoreTypeId = UtilityMethod.GetRequestParameter("pid").ToLong();

            ScoreType ScoreType = new ScoreType();
            if (ScoreTypeId.HasValue)
                ScoreType = Score_DataProvider.GetScoresTypes(ScoreTypeId).FirstOrDefault() ?? new ScoreType();

            ScoreType.CategoryId = Convert.ToInt32(drpScoreTypeCategoryId.SelectedValue);
            ScoreType.CoefficentValue = txtCoefficentValue.Text.ToDouble();
            ScoreType.MaxPerDay = txtMaxPerDay.Text.ToInt32();
            ScoreType.MaxPerMonth = txtMaxPerMonth.Text.ToInt32();
            ScoreType.ScoreEnName = txtScoreEnName.Text;
            ScoreType.ScoreFaName = txtScoreFAName.Text;

            return ScoreType;

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");

            if (action.ToLower() == "del")
            {
                long? ScoreTypeId = UtilityMethod.GetRequestParameter("pid").ToLong();

                ScoreType thisScoreType = Score_DataProvider.GetScoresTypes(ScoreTypeId).FirstOrDefault();
                if (thisScoreType != null)
                {
                    thisScoreType.MarkAsDeleted();
                    Score_DataProvider.SaveScoreType(thisScoreType);
                    Page.Response.Redirect("ScoreTypeList.aspx");
                }
            }
            ScoreType ScoreType = GetScoreTypeInfoFromSkin();
            if (action.ToLower() == "new")
            {
                Score_DataProvider.SaveScoreType(ScoreType);
                Page.Response.Redirect("ScoreTypeList.aspx");
            }
            else if (action.ToLower() == "edit")
            {
                ScoreType.MarkAsModified();
                Score_DataProvider.SaveScoreType(ScoreType);
                Page.Response.Redirect("ScoreTypeList.aspx");
            }

        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("ScoreTypeList.aspx");
        }




    }
}
