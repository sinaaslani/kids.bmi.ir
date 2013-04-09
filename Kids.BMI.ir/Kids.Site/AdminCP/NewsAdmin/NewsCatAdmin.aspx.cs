using System;
using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.NewsAdmin
{
    public partial class NewsCatAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsNewsAdministrator || OnlineSystemUser.IsNewsOperator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            switch (action.ToLower())
            {
                case "new":
                    lblHead.Text = " ایجاد یک موضوع جدید ";
                    btnSave.Text = " ایجاد موضوع ";
                    break;
                case "edit":
                    btnSave.Text = " ویرایش و بهنگام ";
                    lblHead.Text = " ویرایش یک موضوع خبری ";
                    if (!Page.IsPostBack)
                        setInitiateValue();
                    break;
                case "del":
                    btnSave.Text = " حذف ";
                    lblHead.Text = " حذف یک موضوع خبری ";
                    if (!Page.IsPostBack)
                        setInitiateValue();
                    break;
                default:
                    throw new InvalidOperationException();

            }
        }


        private void setInitiateValue()
        {
            int? newsCatid = null;
            if (UtilityMethod.GetRequestParameter("nwscid").IsInt32())
                newsCatid = UtilityMethod.GetRequestParameter("nwscid").ToInt32();
            NewsCategory newsCatObj = News_DataProvider.GetNewsCategory(newsCatid).FirstOrDefault();
            if (newsCatObj != null)
            {
                CatName.Text = newsCatObj.NewsCategoryName;
                comment.Text = newsCatObj.NewsCategoryDescription;
                SortOrder.Items.FindByValue(newsCatObj.SortOrderId.ToString()).Selected = true;

            }
            else
                ShowMessageBox("مقادیر ارسالی نا معتبراست");

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int? newsCatid = null;
            if (UtilityMethod.GetRequestParameter("nwscid").IsInt32())
                newsCatid = UtilityMethod.GetRequestParameter("nwscid").ToInt32();
            NewsCategory nwsCat = News_DataProvider.GetNewsCategory(newsCatid).FirstOrDefault() ?? new NewsCategory();

            nwsCat.NewsCategoryName = CatName.Text;
            nwsCat.NewsCategoryDescription = comment.Text;
            nwsCat.SortOrderId = Convert.ToInt32(SortOrder.SelectedValue);
            nwsCat.IsVisibleCategory = chkIsVisible.Visible;

            string action = UtilityMethod.GetRequestParameter("act");
            if (action.ToLower() == "edit")
            {
                News_DataProvider.SaveNewsCategory(nwsCat);
                Page.Response.Redirect("NewsCatList.aspx");
            }
            else if (action.ToLower() == "del")
            {
                nwsCat.MarkAsDeleted();
                News_DataProvider.SaveNewsCategory(nwsCat);
                Page.Response.Redirect("NewsCatList.aspx");

            }
            else if (action.ToLower() == "new")
            {
                News_DataProvider.SaveNewsCategory(nwsCat);
                Page.Response.Redirect("NewsCatList.aspx");
            }


        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("NewsCatList.aspx");
        }



    }
}