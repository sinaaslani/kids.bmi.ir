using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.WebMessageBox;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Classes.FileUploadManagement;
using Site.Kids.bmi.ir.Masters;

namespace Site.Kids.bmi.ir.AdminCP.GameAdmin
{
    public partial class GameAdmin : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsGameAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            (Master as Admin).RegisterPostbackTrigger(btnSave);
            if (!Page.IsPostBack)
            {
                BindUserState();

                string action = UtilityMethod.GetRequestParameter("act");
                if (action.ToLower() == "edit")
                {
                    lblHead.Text = " ویرایش اطلاعات بازی ";
                    btnSave.Text = " ذخیره و بهنگام ";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "del")
                {
                    lblHead.Text = " حذف بازی ";
                    btnSave.Text = "  حذف  ";
                    SetInitiateValues(action);
                }
                else if (action.ToLower() == "new")
                {
                    lblHead.Text = " ایجاد بازی جدید ";
                    btnSave.Text = " ایجاد بازی ";
                    SetInitiateValues(action);
                }

            }

        }

        void BindUserState()
        {
            var userState = KidsUser_DataProvider.GetKidsUserStates();
            drpRequiredUserState.Items.Add(new ListItem("------", ""));
            foreach (var state in userState)
            {
                drpRequiredUserState.Items.Add(new ListItem(state.StateName, state.Id.ToString()));
            }

        }

        private void SetInitiateValues(string action)
        {
            if (action.ToLower() == "edit" || action.ToLower() == "del")
            {
                int gId = 0;
                if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                    gId = UtilityMethod.GetRequestParameter("gid").ToInt32();


                Game Game = Game_DataProvider.GetGame(gId).FirstOrDefault();
                if (Game == null)
                {
                    ShowMessageBox("بازی یافت نشد", "خطا", MessageBoxType.Information);
                    return;
                }

                txtTitle.Text = Game.Name;
                summaryCtrl.Text = Game.Description;
                drpRequiredUserState.SelectedValue = Game.UserStateRequired.ToString();

                GamePicDeleteLnk.Visible = !string.IsNullOrEmpty(Game.ThumbnailAddress);
                GameFileDeleteLnk.Visible = !string.IsNullOrEmpty(Game.FileAddress);

                drpGameType.SelectedValue = Game.IsExternalGame.ToString();
                txtGameUrl.Text = Game.GameUrl;

                ScoreTypeGrid.DataSource = Score_DataProvider.GetScoresTypes();
                ScoreTypeGrid.DataBind();
                foreach (GridViewRow item in ScoreTypeGrid.Rows)
                {
                    var ctrlId = item.FindControl("hdnId") as HiddenField;
                    var chkSelect = item.FindControl("chkSelect") as CheckBox;
                    int scId = Convert.ToInt32(ctrlId.Value);
                    if (Game.ScoreTypes.Any(nc => nc.Id == scId))
                        (chkSelect).Checked = true;
                }

            }
            else if (action.ToLower() == "new")
            {
                ScoreTypeGrid.DataSource = Score_DataProvider.GetScoresTypes();
                ScoreTypeGrid.DataBind();

            }



        }

        private Game GetGameInfoFromSkin()
        {
            int? gId = null;
            if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                gId = Convert.ToInt32(UtilityMethod.GetRequestParameter("gid"));

            Game Game = gId.HasValue ? Game_DataProvider.GetGame(gId.Value).FirstOrDefault() : new Game();
            Game.Name = txtTitle.Text;
            Game.Description = summaryCtrl.Text;
            Game.UserStateRequired = drpRequiredUserState.SelectedValue.IsInt32() ? drpRequiredUserState.SelectedValue.ToInt32() : (int?)null;

            Game.IsExternalGame = drpGameType.SelectedValue.ToBool();
            Game.GameUrl = txtGameUrl.Text;

            Game.StartTracking();
            foreach (GridViewRow GameCat in ScoreTypeGrid.Rows)
            {
                var ctrlId = GameCat.FindControl("hdnId") as HiddenField;
                var chkSelect = GameCat.FindControl("chkSelect") as CheckBox;

                if (Game.ScoreTypes.Any(o => o.Id == ctrlId.Value.ToInt32()))
                {
                    if (!chkSelect.Checked)
                        Game.ScoreTypes.Remove(Game.ScoreTypes.First(o => o.Id == ctrlId.Value.ToInt32()));
                }
                else
                {
                    if (chkSelect.Checked)
                    {
                        var cat = new ScoreType { Id = ctrlId.Value.ToInt32() };
                        cat.MarkAsUnchanged();
                        Game.ScoreTypes.Add(cat);
                    }
                }

            }

            string smallPic = FileUploadUtil.SaveUploadeFile(fupGamePicAddress, SystemConfigs.UrlGameFilesPath, UploadFileType.Pictures, UploadFileSizeLimitation._1M);
            if (!string.IsNullOrWhiteSpace(smallPic))
                Game.ThumbnailAddress = smallPic;

            string largePic = FileUploadUtil.SaveUploadeFile(fupGameFileAddress, SystemConfigs.UrlGameFilesPath, UploadFileType.Flash, UploadFileSizeLimitation.Unlimited);
            if (!string.IsNullOrWhiteSpace(largePic))
                Game.FileAddress = largePic;

            return Game;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string action = UtilityMethod.GetRequestParameter("act");
            Game Game;

            if (action.ToLower() == "del")
            {
                int gId = 0;
                if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                    gId = UtilityMethod.GetRequestParameter("gid").ToInt32();

                Game = Game_DataProvider.GetGame(gId).FirstOrDefault();
                if (Game != null)
                {
                    Game.MarkAsDeleted();
                    Game_DataProvider.SaveGame(Game);
                }
                Page.Response.Redirect("GameList.aspx");
            }


            Game = GetGameInfoFromSkin();
            if (Game.ScoreTypes.Count == 0)
            {
                lblSubjectValidator.Visible = true;
                return;
            }
            lblSubjectValidator.Visible = false;


            if (action.ToLower() == "new")
            {
                Game_DataProvider.SaveGame(Game);
                Page.Response.Redirect("GameList.aspx");
            }
            else if (action.ToLower() == "edit")
            {
                Game.MarkAsModified();
                Game_DataProvider.SaveGame(Game);
                Page.Response.Redirect("GameList.aspx");
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("GameList.aspx");
        }


        protected void SmallPicDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int gId = 0;
            if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                gId = UtilityMethod.GetRequestParameter("gid").ToInt32();
            Game Game = Game_DataProvider.GetGame(gId).FirstOrDefault();
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlGameFilesPath + Game.ThumbnailAddress));
            Game.ThumbnailAddress = null;
            Game_DataProvider.SaveGame(Game);
            GamePicDeleteLnk.Visible = false;
        }

        protected void LargePicDeleteLnk_Command(object sender, CommandEventArgs e)
        {
            int gId = 0;
            if (UtilityMethod.GetRequestParameter("gid").IsInt32())
                gId = UtilityMethod.GetRequestParameter("gid").ToInt32();
            Game Game = Game_DataProvider.GetGame(gId).FirstOrDefault();
            File.Delete(Page.Server.MapPath(SystemConfigs.UrlGameFilesPath + Game.FileAddress));
            Game.FileAddress = null;
            Game_DataProvider.SaveGame(Game);
            GameFileDeleteLnk.Visible = false;
        }






    }
}