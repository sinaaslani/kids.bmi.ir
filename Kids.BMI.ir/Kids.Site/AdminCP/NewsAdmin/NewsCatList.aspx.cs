using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.NewsAdmin
{

    public partial class NewsCatList : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsNewsAdministrator || OnlineSystemUser.IsNewsOperator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            newsCatGrid.DataSource = News_DataProvider.GetNewsCategory();
            newsCatGrid.DataBind();
        }


    }
}
