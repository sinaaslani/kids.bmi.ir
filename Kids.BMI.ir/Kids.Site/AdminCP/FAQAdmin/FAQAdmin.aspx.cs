using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Site.Kids.bmi.ir.AdminCP.FAQAdmin
{
    public partial class FAQAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsFAQAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<FAQCategory> catList = FAQ_DataProvider.GetFAQCategory();
                foreach (FAQCategory cpc in catList)
                    ddlCatList.Items.Add(new ListItem(cpc.Title, cpc.CategoryId.ToString()));

                string action = UtilityMethod.GetRequestParameter("act");
                if (action.ToLower() == "edit")
                {
                    lblHead.Text = " ویرایش اطلاعات سوال ";
                    btnSave.Text = " ذخیره و بهنگام ";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    lblHead.Text = " حذف سوال ";
                    btnSave.Text = "  حذف  ";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "new")
                {
                    lblHead.Text = " ایجاد سوال جدید ";
                    btnSave.Text = " ایجاد سوال ";
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
                    long? FAQId = UtilityMethod.GetRequestParameter("pid").ToLong();

                    FAQ faq = FAQ_DataProvider.GetFAQ(FAQId).FirstOrDefault();
                    if (faq == null)
                    {
                        ShowMessageBox("مقدار نامعتبر است", "", MessageBoxType.Error);
                        return;
                    }

                    txtTitle.Text = faq.Title;
                    summaryctrl.Text = faq.Summary;
                    txtBody.Text = faq.Body;
                    SortOrder.Items.FindByValue(faq.SortOrderId.ToString()).Selected = true;
                    ddlCatList.Items.FindByValue(faq.CategoryId.ToString()).Selected = true;

                }

            }
            else if (action.ToLower() == "new")
            {
            }



        }


        private FAQ GetFAQInfoFromSkin()
        {
            long? FAQId = null;
            if (UtilityMethod.GetRequestParameter("pid").IsInt64())
                FAQId = UtilityMethod.GetRequestParameter("pid").ToLong();

            FAQ faq = new FAQ();
            if (FAQId.HasValue)
                faq = FAQ_DataProvider.GetFAQ(FAQId).FirstOrDefault() ?? new FAQ();

            faq.CategoryId = Convert.ToInt32(ddlCatList.SelectedValue);
            faq.Title = txtTitle.Text;
            faq.Summary = summaryctrl.Text;
            faq.Body = txtBody.Text;
            faq.SortOrderId = Convert.ToInt32(SortOrder.SelectedValue);

            return faq;

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");

            if (action.ToLower() == "del")
            {
                long? FAQId = UtilityMethod.GetRequestParameter("pid").ToLong();

                FAQ thisFAQ = FAQ_DataProvider.GetFAQ(FAQId).FirstOrDefault();
                if (thisFAQ != null)
                {
                    thisFAQ.MarkAsDeleted();
                    FAQ_DataProvider.Save(thisFAQ);
                    Page.Response.Redirect("FAQList.aspx");
                }
            }
            FAQ faq = GetFAQInfoFromSkin();
            if (action.ToLower() == "new")
            {
                FAQ_DataProvider.Save(faq);
                Page.Response.Redirect("FAQList.aspx");
            }
            else if (action.ToLower() == "edit")
            {
                faq.MarkAsModified();
                FAQ_DataProvider.Save(faq);
                Page.Response.Redirect("FAQList.aspx");
            }

        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("FAQList.aspx");
        }




    }
}
