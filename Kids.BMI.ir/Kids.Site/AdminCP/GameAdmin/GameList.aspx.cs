using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.Common;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.GameAdmin
{
    public partial class GameList : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser == null || !(OnlineSystemUser.IsGameAdministrator || OnlineSystemUser.IsSiteAdministrator))
                Page.Response.Redirect("~/Error/NotAccess.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                long PageCount;
                IList<Game> newsList = Game_DataProvider.GetGame(out PageCount, GameName: searchKeyTxt.Text, Currentpage: dgGames.PageIndex + 1);
                InitializeNewsGrid(newsList);
            }

        }

        private void InitializeNewsGrid(IList<Game> GameList)
        {
            foreach (Game n in GameList)
            {
                n.Name = string.Format("<font color=blue>{0} <BR> شماره بازی:({1})</font>", n.Name, n.GameId);
                if (n.Description.Length < 256)
                    n.Name += "<br>" + n.Description;
                else
                    n.Name += "<br>" + n.Description.Substring(0, 256) + " ... ";

                n.Name += "<br><a href=" + "GameAdmin.aspx?act=edit&gid=" + n.GameId + ">ویرایش" + "</a>";
                n.Name += "&nbsp;&nbsp;&nbsp;" + "<a href=" + "GameAdmin.aspx?act=del&gId=" + n.GameId + ">حذف" + "</a>";
            }
            dgGames.DataSource = GameList;
            try
            {
                dgGames.DataBind();
            }
            catch
            {
                dgGames.PageIndex = 0;
                dgGames.DataBind();
            }

            ResultNumberLbl.Text = GameList.Count.ToString();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            long PageCount;
            List<Game> newsList = Game_DataProvider.GetGame(out PageCount, GameName: searchKeyTxt.Text, Currentpage: dgGames.PageIndex + 1);
            InitializeNewsGrid(newsList);
        }

        protected void dgGames_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            long PageCount;
            IList<Game> newsList = Game_DataProvider.GetGame(out PageCount, GameName: searchKeyTxt.Text, Currentpage: dgGames.PageIndex + 1);
            InitializeNewsGrid(newsList);

            dgGames.PageIndex = e.NewPageIndex;

            try
            {
                dgGames.DataBind();
            }
            catch
            {
                dgGames.PageIndex = 0;
                dgGames.DataBind();
            }

        }


    }
}