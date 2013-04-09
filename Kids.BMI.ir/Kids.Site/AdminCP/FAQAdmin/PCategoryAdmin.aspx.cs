using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;
using System;

namespace Site.Kids.bmi.ir.AdminCP.FAQAdmin
{

    public partial class PCategoryAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsFAQAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            switch (action.ToLower())
            {
                case "new":
                    lblHead.Text = " ایجاد یک گروه جدید ";
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
                FAQCategory cpCatObj = FAQ_DataProvider.GetFAQCategory(cpcatId).FirstOrDefault();

                if (cpCatObj != null)
                {
                    faCatName.Text = cpCatObj.Title;
                    SortOrder.Items.FindByValue(cpCatObj.SortOrderId.ToString()).Selected = true;

                }
                else
                    ShowMessageBox("خطا در پارامتر ارسالی", "خطا", MessageBoxType.Error);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FAQCategory cpCat = null;
            if (UtilityMethod.GetRequestParameter("pcid").IsInt32())
            {
                int? cpcatId = UtilityMethod.GetRequestParameter("pcid").ToInt32();
                cpCat = FAQ_DataProvider.GetFAQCategory(cpcatId).FirstOrDefault();
            }

            if (cpCat == null)
                cpCat = new FAQCategory();

            cpCat.Title = faCatName.Text;
            cpCat.SortOrderId = Convert.ToInt32(SortOrder.SelectedValue);

            string action = UtilityMethod.GetRequestParameter("act");
            if (action.ToLower() == "edit")
            {
                cpCat.MarkAsModified();
                FAQ_DataProvider.Save(cpCat);
                Page.Response.Redirect("PCategoryList.aspx");

            }
            else if (action.ToLower() == "del")
            {
                cpCat.MarkAsDeleted();
                FAQ_DataProvider.Save(cpCat);
                Page.Response.Redirect("PCategoryList.aspx");

            }
            else if (action.ToLower() == "new")
            {
                FAQ_DataProvider.Save(cpCat);
                Page.Response.Redirect("PCategoryList.aspx");
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("PCategoryList.aspx");
        }





    }
}
