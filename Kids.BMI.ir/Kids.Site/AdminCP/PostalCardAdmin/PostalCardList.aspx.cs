using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.PostalCardAdmin
{
    public partial class PostalCardList : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsPostalCardAdministrator|| OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                long PageCount;
                List<PostalCard> newsList = PostalCard_DataProvider.GetPostalCard(out PageCount, PostalCardName: searchKeyTxt.Text, Currentpage: dgPostalCards.PageIndex + 1);
                InitializeNewsGrid(newsList);
            }

        }

        private void InitializeNewsGrid(List<PostalCard> PostalCardList)
        {
            foreach (PostalCard n in PostalCardList)
            {
                n.CardName = "<font color=blue>" + n.CardName + "</font>";
                if (n.CardPostalDescription.Length < 256)
                    n.CardName += "<br>" + n.CardPostalDescription;
                else
                    n.CardName += "<br>" + n.CardPostalDescription.Substring(0, 256) + " ... ";

                n.CardName += "<br><a href=" + "PostalCardAdmin.aspx?act=edit&gid=" + n.CardId + ">ویرایش" + "</a>";
                n.CardName += "&nbsp;&nbsp;&nbsp;" + "<a href=" + "PostalCardAdmin.aspx?act=del&gId=" + n.CardId + ">حذف" + "</a>";
            }
            dgPostalCards.DataSource = PostalCardList;
            try
            {
                dgPostalCards.DataBind();
            }
            catch
            {
                dgPostalCards.PageIndex = 0;
                dgPostalCards.DataBind();
            }

            ResultNumberLbl.Text = PostalCardList.Count.ToString();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            long PageCount;
            List<PostalCard> newsList = PostalCard_DataProvider.GetPostalCard(out PageCount, PostalCardName: searchKeyTxt.Text, Currentpage: dgPostalCards.PageIndex + 1);
            InitializeNewsGrid(newsList);
        }

        protected void dgPostalCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            long PageCount;
            List<PostalCard> postcardList = PostalCard_DataProvider.GetPostalCard(out PageCount, PostalCardName: searchKeyTxt.Text, Currentpage: dgPostalCards.PageIndex + 1);
            InitializeNewsGrid(postcardList);

            dgPostalCards.PageIndex = e.NewPageIndex;

            try
            {
                dgPostalCards.DataBind();
            }
            catch
            {
                dgPostalCards.PageIndex = 0;
                dgPostalCards.DataBind();
            }

        }


    }
}