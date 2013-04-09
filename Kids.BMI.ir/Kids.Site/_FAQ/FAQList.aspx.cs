using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Contact._FAQ
{
    public partial class FAQList : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? catId = null;
            if (!Page.IsPostBack)
            {

                TopCats.Items.Add(new ListItem("----------", ""));
                IList<FAQCategory> itmCatList = FAQ_DataProvider.GetFAQCategory();
                foreach (FAQCategory ncat in itmCatList)
                {
                    ListItem li = new ListItem(ncat.Title, ncat.CategoryId.ToString());
                    TopCats.Items.Add(li);
                }


                try
                {
                    if (Page.Request["cid"] != null)
                        catId = Convert.ToInt32(Page.Request["cid"]);
                }
                catch
                {
                    catId = null;
                }
                InitializeFAQGrid(catId);
            }

        }

        protected void TopCats_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? catId = TopCats.SelectedValue.IsInt32() ? TopCats.SelectedValue.ToInt32() : (int?)null;
            InitializeFAQGrid(catId);
        }

        private void InitializeFAQGrid(int? catId)
        {
            if (catId.HasValue)
            {
                FAQCategory cat = FAQ_DataProvider.GetFAQCategory(catId.Value).FirstOrDefault();
                if (cat != null) lblTitle.Text = cat.Title;
            }

            List<FAQ> faqList = FAQ_DataProvider.GetFAQ(FAQCatId: catId);
            itemsGrid.DataSource = faqList;
            try
            {
                itemsGrid.DataBind();
            }
            catch
            {
                itemsGrid.CurrentPageIndex = 0;
                itemsGrid.DataBind();
            }

            itemsGrid.Visible = faqList.Count != 0;

        }



    }
}