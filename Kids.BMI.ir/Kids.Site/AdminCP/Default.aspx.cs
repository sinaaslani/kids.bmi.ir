using System.Web.UI;
using System.Web.UI.WebControls;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP
{
    public partial class _Default : AdminSecureFormBaseClass
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblHeaderAdminPanel.Text = "";

            if (OnlineSystemUser.IsSiteAdministrator
                || OnlineSystemUser.IsNewsAdministrator || OnlineSystemUser.IsNewsOperator)
                AddPanelToTable("~/AdminCP/HomeWidget/ucnewsAdminPanel.ascx");

            if (OnlineSystemUser.IsSiteAdministrator
             || OnlineSystemUser.IsPollAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/pollsAdminPanel.ascx");

            if (OnlineSystemUser.IsSiteAdministrator
               || OnlineSystemUser.IsDynamicPageAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/ucDynamicPageAdminPanel.ascx");
            

            if (OnlineSystemUser.IsKidsUserManager ||
                OnlineSystemUser.IsKidsUserAdmin ||
                  OnlineSystemUser.IsSiteAdministrator ||
                  OnlineSystemUser.IsBranchAdmin ||
                  OnlineSystemUser.IsBranchUser
                  )
                AddPanelToTable("~/AdminCP/HomeWidget/KidsUserAdminPanel.ascx");

            if (OnlineSystemUser.IsSiteAdministrator || OnlineSystemUser.IsScoreTypeAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/ucScoreTypeAdminPanel.ascx");

            
            if (OnlineSystemUser.IsSiteAdministrator
             || OnlineSystemUser.IsFAQAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/ucFAQAdminPanel.ascx");

            if (OnlineSystemUser.IsSiteAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/ConfigAdminPanel.ascx");

            if (OnlineSystemUser.IsSiteAdministrator || OnlineSystemUser.IsExamAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/BankExamAdminPanel.ascx");

            if (OnlineSystemUser.IsSiteAdministrator
                 || OnlineSystemUser.IsGameAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/ucGameAdminPanel.ascx");


            if (OnlineSystemUser.IsSiteAdministrator || OnlineSystemUser.IsWishesAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/WishesAdminPanel.ascx");

            if (OnlineSystemUser.IsSiteAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/SystemUserAdminPanel.ascx");

            if (OnlineSystemUser.IsSiteAdministrator || OnlineSystemUser.IsPostalCardAdministrator)
                AddPanelToTable("~/AdminCP/HomeWidget/PostalCardAdminPanel.ascx");

        }

        private void AddPanelToTable(string controlVPath)
        {

            TableRow tr = new TableRow();
            TableCell td = new TableCell
                {
                    VerticalAlign = VerticalAlign.Top,
                    HorizontalAlign = HorizontalAlign.Right,
                    Width = Unit.Percentage(50)
                };


            Control control = Page.LoadControl(controlVPath);
            int lastTableRow = adminPanelTable.Rows.Count - 1;
            if (lastTableRow == -1)
            {
                td.Controls.Add(control);
                tr.Cells.Add(td);
                adminPanelTable.Rows.Add(tr);
            }
            else
            {
                if (adminPanelTable.Rows[lastTableRow].Cells.Count == 2)
                {
                    td.Controls.Add(control);
                    tr.Cells.Add(td);
                    adminPanelTable.Rows.Add(tr);

                }
                else
                {
                    td.Controls.Add(control);
                    adminPanelTable.Rows[lastTableRow].Cells.Add(td);
                }

            }

        }

        protected override void CheckAdminUser()
        {

        }
    }


}
