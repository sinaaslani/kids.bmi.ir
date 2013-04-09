using System;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class PostalCardList : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageMaster.NewsMarque.NewsCategoryId = News_DataProvider.PreDefinedNewsCategory.WishAccount;
                BindPostalCardList();
            }
        }

        private void BindPostalCardList()
        {
            var PostalCardList = PostalCard_DataProvider.GetPostalCard();
            dgPostalCards.DataSource = PostalCardList;
            dgPostalCards.DataBind();
        }

        protected void dgPostalCards_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var postalCard = e.Item.DataItem as PostalCard;
                var lbl = e.Item.FindControl("lblPostalCardName") as Label;
                lbl.Text = string.Format("{0}( مبلغ : {1} امتیاز)", postalCard.CardName, postalCard.CardScore);


                HyperLink lnk = e.Item.FindControl("lnkPostalCard") as HyperLink;
                lnk.NavigateUrl = string.Format("~/WishAccount/PostalCardShow.aspx?id={0}", postalCard.CardId);
            }
        }

    

    }


}