using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;
using System;

namespace Site.Kids.bmi.ir.AdminCP.ScoreAdmin
{

    public partial class ScoreTypeCeategoryAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsScoreTypeAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            switch (action.ToLower())
            {
                case "new":
                    lblHead.Text = " ایجاد یک گروه امتیاز جدید ";
                    btnSave.Text = " ایجاد گروه ";
                    break;
                case "edit":
                    btnSave.Text = " ویرایش و بهنگام ";
                    lblHead.Text = " ویرایش یک گروه  ";
                    if (!Page.IsPostBack)
                        setInitiateValue();
                    break;
                case "del":
                    btnSave.Text = " حذف ";
                    lblHead.Text = " حذف یک گروه  ";
                    if (!Page.IsPostBack)
                        setInitiateValue();
                    break;
                default:
                    ShowMessageBox("خطا در پارامتر", "خطا", MessageBoxType.Error);
                    break;
            }
        }

        private void setInitiateValue()
        {
            if (UtilityMethod.GetRequestParameter("pcid").IsInt64())
            {
                int? cpcatId = UtilityMethod.GetRequestParameter("pcid").ToInt32();
                ScoreTypeCategory cpCatObj = Score_DataProvider.GetScoreTypeCategory(cpcatId).FirstOrDefault();

                if (cpCatObj != null)
                {
                    faCatName.Text = cpCatObj.CategoryName;
                }
                else
                    ShowMessageBox("خطا در پارامتر ارسالی", "خطا", MessageBoxType.Error);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScoreTypeCategory cpCat = null;
            if (UtilityMethod.GetRequestParameter("pcid").IsInt32())
            {
                int? cpcatId = UtilityMethod.GetRequestParameter("pcid").ToInt32();
                cpCat = Score_DataProvider.GetScoreTypeCategory(cpcatId).FirstOrDefault();
            }

            if (cpCat == null)
                cpCat = new ScoreTypeCategory();

            cpCat.CategoryName = faCatName.Text;

            string action = UtilityMethod.GetRequestParameter("act");
            if (action.ToLower() == "edit")
            {
                cpCat.MarkAsModified();
                Score_DataProvider.Save(cpCat);
                Page.Response.Redirect("ScoreTypeCategoryList.aspx");

            }
            else if (action.ToLower() == "del")
            {
                cpCat.MarkAsDeleted();
                Score_DataProvider.Save(cpCat);
                Page.Response.Redirect("ScoreTypeCategoryList.aspx");

            }
            else if (action.ToLower() == "new")
            {
                Score_DataProvider.Save(cpCat);
                Page.Response.Redirect("ScoreTypeCategoryList.aspx");
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("ScoreTypeCategoryList.aspx");
        }





    }
}
