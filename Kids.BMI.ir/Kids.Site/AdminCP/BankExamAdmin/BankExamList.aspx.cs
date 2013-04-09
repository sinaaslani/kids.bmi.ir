using System;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.BankExamAdmin
{
    public partial class BankExamList : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsExamAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dgExamList.DataSource = BankStory_DataProvider.GetExams();
            dgExamList.DataBind();

        }
    }
}