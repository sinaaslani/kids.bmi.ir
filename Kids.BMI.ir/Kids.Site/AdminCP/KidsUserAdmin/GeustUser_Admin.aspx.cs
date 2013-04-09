using System;
using System.IO;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using OfficeOpenXml;
using Site.Kids.bmi.ir.Classes;


namespace Site.Kids.bmi.ir.AdminCP.KidsUserAdmin
{
    public partial class GeustUser_Admin : AdminSecureFormBaseClass
    {

        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsKidsUserManager || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DeleteOldFiles(Server.MapPath("~/AdminCP/Files/GeustUser/Temp"));
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();

            pnlResult.Visible = true;

        }

        private void BindGrid(int CurrentPage = 1)
        {
            long RecordCount;
            var kids = KidsUser_DataProvider.GetGeustUser(out RecordCount,
                                                        Name: txtSearchName.Text,
                                                        Family: txtSearchFamily.Text,
                                                        MelliCode: txtSearchMelliCode.Text,
                                                        EmailAddress: txtSearchEmailAddress.Text,
                                                        MobileNumber: txtSearchMobileNumber.Text,
                                                        SelectDistinct:false,
                                                        PageSize: dgGeustUser.PageSize,
                                                        Currentpage: CurrentPage);
            GridViewFiller ObjGridFilter = new GridViewFiller();
            ObjGridFilter.PagingGridView(dgGeustUser, kids, RecordCount.ToInt32());


        }
        protected void ImgExportToExcell_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            long RecordCount;
            var kids = KidsUser_DataProvider.GetGeustUser(out RecordCount,
                                                        Name: txtSearchName.Text,
                                                        Family: txtSearchFamily.Text,
                                                        MelliCode: txtSearchMelliCode.Text,
                                                        EmailAddress: txtSearchEmailAddress.Text,
                                                        MobileNumber: txtSearchMobileNumber.Text,
                                                        SelectDistinct: true,
                                                        PageSize: 100000
                                                        );

            string templatefilePath = Server.MapPath("~/AdminCP/Files/GeustUser/GeustUserReport.xlsx");
            FileInfo templateFileInfo = new FileInfo(templatefilePath);

            string NewfilePath = string.Format("~/AdminCP/Files/GeustUser/Temp/GeustUserReport_{0}.xlsx", PersianDateTime.Now.ToLongDateTimeString().Replace("/", "-").Replace(":", "-"));
            FileInfo NewfileInfo = new FileInfo(Server.MapPath(NewfilePath));

            ExcelPackage xlPackage = new ExcelPackage(templateFileInfo, true);

            ExcelWorksheet workSheetGeustUser = xlPackage.Workbook.Worksheets["GeustUser"];
            int i = 2;
            foreach (var user in kids)
            {
                FillExcellRow(user, workSheetGeustUser, i);
                i++;
            }
            xlPackage.SaveAs(NewfileInfo);
            xlPackage.Dispose();
            ClientRedirect(NewfilePath, 2000);
        }

        private void FillExcellRow(GeustKidsUser user, ExcelWorksheet sheet, int RowId)
        {
            sheet.Cells[RowId, 1].Value = user.Name;
            sheet.Cells[RowId, 2].Value = user.Family;
            sheet.Cells[RowId, 3].Value = user.MelliCode;
            sheet.Cells[RowId, 4].Value = user.EmailAddress;
            sheet.Cells[RowId, 5].Value = user.MobileNumber;
            sheet.Cells[RowId, 6].Value = PersianDateTime.MiladiToPersian(user.CreateDateTime).ToShortDateTimeString();
        }
        protected void dgGeustUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgGeustUser.PageIndex = e.NewPageIndex;
            BindGrid(e.NewPageIndex + 1);
        }
        

    }
}