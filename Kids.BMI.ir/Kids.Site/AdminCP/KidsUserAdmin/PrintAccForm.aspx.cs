using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Kids.EntitiesModel;
using Kids.Utility;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.KidsUserAdmin
{
    public partial class PrintAccForm : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsKidsUserManager || OnlineSystemUser.IsSiteAdministrator || OnlineSystemUser.IsBranchAdmin || OnlineSystemUser.IsBranchUser))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DeleteOldFiles(Server.MapPath("~/AdminCP/Files/KidsAccForm/Temp"));
                PrintForm();
            }
        }

       

        private void PrintForm()
        {
            if (Request["uid"].IsInt64())
            {
                var user = KidsUser_DataProvider.GetKidsUser(Request["uid"].ToLong()).FirstOrDefault();
                if (user == null)
                    return;

                string templatefilePath = Server.MapPath("~/AdminCP/Files/KidsAccForm/528_2.xlsx");
                FileInfo templateFileInfo = new FileInfo(templatefilePath);

                string NewfilePath = string.Format("~/AdminCP/Files/KidsAccForm/Temp/528_2_{0}.xlsx", PersianDateTime.Now.ToLongDateTimeString().Replace("/", "-").Replace(":", "-"));
                FileInfo NewfileInfo = new FileInfo(Server.MapPath(NewfilePath));

                ExcelPackage xlPackage = new ExcelPackage(templateFileInfo, true);

                ExcelWorksheet workSheetReal_1 = xlPackage.Workbook.Worksheets["حقيقي-1"];
                FillExcellRow(user, workSheetReal_1);

                ExcelWorksheet workSheetReal_2 = xlPackage.Workbook.Worksheets["حقيقي-2"];
                FillExcellRow2(user, workSheetReal_2);


                ExcelWorksheet w_MelliCard = xlPackage.Workbook.Worksheets["کارت ملی"];
                ExcelWorksheet w_Identity = xlPackage.Workbook.Worksheets["شناسنامه"];

                var w = JpegImage.CmToPx(20).ToInt32();
                var h = JpegImage.CmToPx(30).ToInt32();
                CreateImage(w_Identity, JpegImage.GetPicFromDB(user, JpegImage.ImageActType.ChildIdentityPic, false, w, h), 0, 0);


                w = JpegImage.CmToPx(10).ToInt32();
                h = JpegImage.CmToPx(20).ToInt32();
                CreateImage(w_MelliCard, JpegImage.GetPicFromDB(user, JpegImage.ImageActType.ChildNationalCardFaceUPPic, false, w, h), 0, 0);
                CreateImage(w_MelliCard, JpegImage.GetPicFromDB(user, JpegImage.ImageActType.ChildNationalCardFaceDownPic, false, w, h), 17, 0);




                xlPackage.SaveAs(NewfileInfo);
                xlPackage.Dispose();
                ClientRedirect(NewfilePath, 2000);
            }
        }

        private void CreateImage(ExcelWorksheet workSheetCommit, Image img, int Row, int Column)
        {          
            ExcelPicture pic = workSheetCommit.Drawings.AddPicture(string.Format("pic_{0}_{1}", Row, Column), img);
            pic.SetPosition(Row, 0, Column, 0);

        }

        private void FillExcellRow(KidsUser user, ExcelWorksheet sheet)
        {
            string ChildFullName = user.ChildName + " " + user.ChildFamily;
            var ArrChildFullName = ChildFullName.ToCharArray();

            sheet.Cells["G5"].Value = user.ChildAccBranchNo.HasValue ? user.ChildAccBranchNo.ToString() : "";
            sheet.Cells["G6"].Value = PersianDateTime.Now.ToString("/");
            sheet.Cells["N8"].Value = "سپرده کوتاه مدت - آرزو";
            sheet.Cells["G10"].Value = ChildFullName;
            sheet.Cells["J12"].Value = user.ChildFamily;
            sheet.Cells["J14"].Value = user.ChildName;
            sheet.Cells["I17"].Value = user.ChildCustomerNo;
            sheet.Cells["I18"].Value = user.ChildAccNo;
            sheet.Cells["I19"].Value = "";

            for (int i = 0; i < ArrChildFullName.Length; i++)
                sheet.Cells[22, i + 5].Value = ArrChildFullName[i].ToString();

            sheet.Cells["H25"].Value = user.ChildFatherName;
            sheet.Cells["T25"].Value = user.ChildIdentityNo;

            var perbdate = PersianDateTime.MiladiToPersian(user.ChildBirthDate);
            sheet.Cells["AF25"].Value = perbdate.Day;
            sheet.Cells["AG25"].Value = perbdate.Month;
            sheet.Cells["AH25"].Value = perbdate.Year;

            sheet.Cells["H27"].Value = user.ChildBirthLocation;
            sheet.Cells["T27"].Value = "";
            sheet.Cells["AF27"].Value = "";

            sheet.Cells["H29"].Value = "";
            sheet.Cells["J29"].Value = "";
            sheet.Cells["L29"].Value = "";
            sheet.Cells["T29"].Value = "ایرانی";
            sheet.Cells["AF29"].Value = user.ChildMelliCode;


            string sex = user.ChildSex ? "K32" : "K31";
            sheet.Cells[sex].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[sex].Style.Fill.BackgroundColor.SetColor(Color.Black);

            sheet.Cells["W31"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["W31"].Style.Fill.BackgroundColor.SetColor(Color.Black);

            sheet.Cells["AH32"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AH32"].Style.Fill.BackgroundColor.SetColor(Color.Black);



            sheet.Cells["H34"].Value = "";
            sheet.Cells["T34"].Value = "";
            sheet.Cells["AF34"].Value = user.ChildPostCode;

            sheet.Cells["H36"].Value = "";
            sheet.Cells["T36"].Value = user.ChildPhoneNumber;
            sheet.Cells["AF36"].Value = "";

            sheet.Cells["H38"].Value = user.ChildMobileNumber;
            sheet.Cells["T38"].Value = "";
            sheet.Cells["AF38"].Value = user.ChildIdentitySerial;


            sheet.Cells["M40"].Value = user.ChildEmailAddress;

            sheet.Cells["I41"].Value = "";
            sheet.Cells["H42"].Value = user.ChildPostAddress;

        }

        private void FillExcellRow2(KidsUser user, ExcelWorksheet sheet)
        {
            sheet.Cells["F5"].Value = user.ParentName + " " + user.ParentFamily;
            var acc = BMICustomer_DataProvider.GetAccByMellicode(user.ParentMelliCode).FirstOrDefault();
            if (acc != null) sheet.Cells["I5"].Value = acc.cu_id;

            sheet.Cells["P5"].Value = user.ChildAccBranchNo.HasValue ? user.ChildAccBranchNo.ToString() : "";
            sheet.Cells["P4"].Value = PersianDateTime.Now.ToString("/");
        }

    }
}