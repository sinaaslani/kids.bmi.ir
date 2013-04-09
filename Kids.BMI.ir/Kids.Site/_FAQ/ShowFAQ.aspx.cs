using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir._FAQ
{
    public partial class ShowFAQ : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IList<FAQCategory> catList = FAQ_DataProvider.GetFAQCategory();
                foreach (FAQCategory cat in catList)
                {
                    ListItem li = new ListItem(cat.Title, cat.CategoryId.ToString());
                    TopCats.Items.Add(li);
                }


                if (UtilityMethod.GetRequestParameter("itmId").IsInt64())
                {
                    long? itmId = UtilityMethod.GetRequestParameter("itmId").ToLong();
                    FAQ faq = FAQ_DataProvider.GetFAQ(FAQId: itmId).FirstOrDefault();
                    if (faq != null)
                    {
                        TitleLbl.Text += faq.Title;
                        BodyLbl.Text = faq.Body.Replace("\n", "<br>");
                        Page.Title = faq.Title;
                    }
                }
            }
        }


        protected void TopCats_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.Response.Redirect("faqList.aspx" + "?cid=" + TopCats.SelectedValue);
        }
		
    }
}