using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.EntitiesModel.Scores;
using Kids.Utility;
using OfficeOpenXml;
using Site.Kids.bmi.ir.Classes;


namespace Site.Kids.bmi.ir.AdminCP.KidsUserAdmin
{
    public partial class KidsUser_Admin : AdminSecureFormBaseClass
    {
        private KidsUser _CurrentKidsUser;
        private KidsUser CurrentKidsUser
        {
            get
            {
                return _CurrentKidsUser ??
                       (_CurrentKidsUser = SerializeHelper.DataContract_ToObject<KidsUser>(ViewState["KidsUser"].ToString()));
            }
            set
            {
                _CurrentKidsUser = value;
                ViewState["KidsUser"] = SerializeHelper.DataContract_ToString(_CurrentKidsUser);
            }
        }

        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsKidsUserManager || OnlineSystemUser.IsKidsUserAdmin || OnlineSystemUser.IsSiteAdministrator))
                Response.Redirect("~/Error/NotAccess.aspx");

            if (OnlineSystemUser.IsKidsUserManager &&
                !(OnlineSystemUser.IsKidsUserAdmin || OnlineSystemUser.IsSiteAdministrator))
            {
                btnNewKidsUser.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindUserStates();
                DeleteOldFiles(Server.MapPath("~/AdminCP/Files/KidsUser/Temp"));
            }
        }

        private void BindUserStates()
        {
            var lst = KidsUser_DataProvider.GetKidsUserStates();
            drpUserState.Items.Add(new ListItem("----", ""));
            foreach (var state in lst)
                drpUserState.Items.Add(new ListItem(state.Id.ToString() + "==>" + state.StateName, state.Id.ToString()));

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();

            pnlResult.Visible = true;
            pnlDetails.Visible = false;
        }

        private void BindGrid(int CurrentPage = 1)
        {
            int RecordCount;
            int? UserState = null;
            if (drpUserState.SelectedValue.IsInt32())
                UserState = drpUserState.SelectedValue.ToInt32();

            var kids = KidsUser_DataProvider.GetKidsUser(out RecordCount,
                                                                        SSOUserName: txtSearchSSOUserName.Text,
                                                                        ChildMelliCode: txtSearchChildMelliCode.Text,
                                                                        ParentMelliCode: txtSearchParentMelliCode.Text,
                                                                        ChildName: txtSearchChildName.Text,
                                                                        ChildFamily: txtSearchChildFamily.Text,
                                                                        CurrentStatus: UserState,
                                                                        PageSize: dgKidsUser.PageSize,
                                                                        Currentpage: CurrentPage);
            GridViewFiller ObjGridFilter = new GridViewFiller();
            ObjGridFilter.PagingGridView(dgKidsUser, kids, RecordCount.ToInt32());

        }

        protected void btnNewKidsUser_Click(object sender, EventArgs e)
        {
            ClearControl(pnlDetails);

            PageState = Action.Add;
            CurrentKidsUser = new KidsUser();

            pnlResult.Visible = false;
            pnlDetails.Visible = true;

        }

        protected void dgKidsUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            PageState = Action.Update;
            CurrentKidsUser = KidsUser_DataProvider.GetKidsUser(KidsUserId: Convert.ToInt64(dgKidsUser.DataKeys[e.NewSelectedIndex].Value)).FirstOrDefault();
            ClearControl(pnlDetails);

            if (OnlineSystemUser.IsKidsUserManager &&
                !(OnlineSystemUser.IsKidsUserAdmin || OnlineSystemUser.IsSiteAdministrator))
            {
                ucUserProfile.SetUserInfo(CurrentKidsUser);
                ucUserProfile.Visible = true;
                btnSave.Visible = false;
            }
            else
            {
                ucEditableUserProfile.SetUserInfo(CurrentKidsUser, true);
                ucEditableUserProfile.Visible = true;
                btnSave.Visible = true;
            }
            ucPaymentList.SetUserInfo(CurrentKidsUser);

            List<scoreListItem> DailyscoreList, MonthlyscoreList;
            ScoreHelper.CalculateScore(CurrentKidsUser, false, out DailyscoreList, out MonthlyscoreList);
            ucScoreList.SetUserInfo(DailyscoreList, MonthlyscoreList);

            pnlDetails.Visible = true;
        }

        protected void dgKidsUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var u = KidsUser_DataProvider.GetKidsUser(KidsUserId: Convert.ToInt64(dgKidsUser.DataKeys[e.RowIndex].Value)).FirstOrDefault();
            u.MarkAsDeleted();
            KidsUser_DataProvider.SaveKidsUser(u, this, KidsSecureFormBaseClass.RefreshOnlineKidsUserInfo);
            pnlDetails.Visible = false;
            BindGrid();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CurrentKidsUser = ucEditableUserProfile.GetUserInfo(CurrentKidsUser);

            if (PageState == Action.Add)
                CurrentKidsUser.MarkAsAdded();
            else if (PageState == Action.Update)
                CurrentKidsUser.MarkAsModified();

            KidsUser_DataProvider.SaveKidsUser(CurrentKidsUser, this, KidsSecureFormBaseClass.RefreshOnlineKidsUserInfo);
            pnlDetails.Visible = false;

            BindGrid();
        }





        protected void dgKidsUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgKidsUser.PageIndex = e.NewPageIndex;
            BindGrid(e.NewPageIndex + 1);
        }

        protected void dgKidsUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lnkDelete") as LinkButton).Attributes.Add("onclick", "return Confirm();");

                var KidsUser = e.Row.DataItem as KidsUser;
                //List<scoreListItem> scoreList;

                (e.Row.FindControl("lblCurrentState") as Label).Text = KidsUser.CurrentStatus.ToString();
                (e.Row.FindControl("lblCurrentState") as Label).ToolTip = KidsUser.KidsUserState.StateName;
                (e.Row.FindControl("lblCurrentScore") as Label).Text = KidsUser.LastCalculatedScore.ToString();
                //string.Format("{0:0,00}", ScoreHelper.CalculateScore(KidsUser, out scoreList));

            }
        }

        protected void btnCancelPlan_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
        }

        protected void ImgExportToExcell_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            int RecordCount;
            int? UserState = null;
            if (drpUserState.SelectedValue.IsInt32())
                UserState = drpUserState.SelectedValue.ToInt32();
            var kids = KidsUser_DataProvider.GetKidsUser(out RecordCount,
                                                                        SSOUserName: txtSearchSSOUserName.Text,
                                                                        ChildMelliCode: txtSearchChildMelliCode.Text,
                                                                        ParentMelliCode: txtSearchParentMelliCode.Text,
                                                                        ChildName: txtSearchChildName.Text,
                                                                        ChildFamily: txtSearchChildFamily.Text,
                                                                        CurrentStatus: UserState,
                                                                        PageSize: 10000,
                                                                        Currentpage: 1);

            string templatefilePath = Server.MapPath("~/AdminCP/Files/KidsUser/KidsUserReport.xlsx");
            FileInfo templateFileInfo = new FileInfo(templatefilePath);

            string NewfilePath = string.Format("~/AdminCP/Files/KidsUser/Temp/KidsUserReport_{0}.xlsx", PersianDateTime.Now.ToLongDateTimeString().Replace("/", "-").Replace(":", "-"));
            FileInfo NewfileInfo = new FileInfo(Server.MapPath(NewfilePath));

            ExcelPackage xlPackage = new ExcelPackage(templateFileInfo, true);

            ExcelWorksheet workSheetGeustUser = xlPackage.Workbook.Worksheets["KidsUser"];
            int i = 1;
            foreach (var user in kids)
            {
                FillExcellRow(user, workSheetGeustUser, i);
                i++;
            }
            xlPackage.SaveAs(NewfileInfo);
            xlPackage.Dispose();
            ClientRedirect(NewfilePath, 2000);
        }
        private void FillExcellRow(KidsUser user, ExcelWorksheet sheet, int RowId)
        {
            sheet.Cells[RowId, 1].Value = user.KidsUserId;
            sheet.Cells[RowId, 2].Value = user.IntruducerId;
            sheet.Cells[RowId, 3].Value = user.SSOUserName;
            sheet.Cells[RowId, 4].Value = user.ChildName;
            sheet.Cells[RowId, 5].Value = user.ChildFamily;
            sheet.Cells[RowId, 6].Value = user.ChildFatherName;
            sheet.Cells[RowId, 7].Value = user.ChildSex;
            sheet.Cells[RowId, 8].Value = user.ChildMelliCode;
            sheet.Cells[RowId, 9].Value = user.ChildIdentityNo;
            sheet.Cells[RowId, 10].Value = user.ChildIdentitySerial;
            sheet.Cells[RowId, 11].Value = user.ChildBirthLocation;
            sheet.Cells[RowId, 12].Value = user.ChildBirthDate;
            sheet.Cells[RowId, 13].Value = user.ChildAge;
            sheet.Cells[RowId, 14].Value = user.ChildPersianBirthDay;
            sheet.Cells[RowId, 15].Value = user.ChildMobileNumber;
            sheet.Cells[RowId, 16].Value = user.ChildPhoneNumber;
            sheet.Cells[RowId, 17].Value = user.ChildEmailAddress;

            sheet.Cells[RowId, 18].Value = user.ChildPostCode;
            sheet.Cells[RowId, 19].Value = user.ChildPostAddress;
            sheet.Cells[RowId, 20].Value = user.ParentRelationId;
            sheet.Cells[RowId, 21].Value = user.ParentName;
            sheet.Cells[RowId, 22].Value = user.ParentFamily;
            sheet.Cells[RowId, 23].Value = user.ParentIdentityNo;
            sheet.Cells[RowId, 24].Value = user.ParentMelliCode;
            sheet.Cells[RowId, 25].Value = user.ParentMobileNumber;
            sheet.Cells[RowId, 26].Value = user.ParentPhoneNumber;
            sheet.Cells[RowId, 27].Value = user.ParentEmailAddress;
            sheet.Cells[RowId, 28].Value = user.ParentPostCode;
            sheet.Cells[RowId, 29].Value = user.ParentPostAddress;
            sheet.Cells[RowId, 30].Value = user.ChildAccNo;
            sheet.Cells[RowId, 31].Value = user.ChildAccBranchNo;
            sheet.Cells[RowId, 32].Value = user.ChildCustomerNo;
            sheet.Cells[RowId, 33].Value = user.ParentAccNo;
            sheet.Cells[RowId, 34].Value = user.ParentCustomerNo;
            sheet.Cells[RowId, 35].Value = user.LastCalculatedScore;
            sheet.Cells[RowId, 36].Value = user.CurrentStatus;
            sheet.Cells[RowId, 37].Value = user.StatusHistory;
            sheet.Cells[RowId, 38].Value = PersianDateTime.MiladiToPersian(user.CreateDateTime).ToShortDateTimeString();
            sheet.Cells[RowId, 39].Value = user.LastUpdateDateTime.HasValue ? PersianDateTime.MiladiToPersian(user.LastUpdateDateTime.Value).ToShortDateTimeString() : "";

        }

    }
}