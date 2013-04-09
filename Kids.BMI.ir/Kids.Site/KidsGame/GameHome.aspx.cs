using System;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.KidsGame
{
    public partial class GameHome : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageMaster.NewsMarque.NewsCategoryId = News_DataProvider.PreDefinedNewsCategory.Game;
                BindGameList();
            }
        }

        private void BindGameList()
        {
            var gameList = Game_DataProvider.GetGame();
            dgGames.DataSource = gameList;
            dgGames.DataBind();
        }

        protected void dgGames_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var game = e.Item.DataItem as Game;
                var lbl = e.Item.FindControl("lblGameName") as Label;
                lbl.Text = string.Format("{0}({1})", game.Name, GetGameState(game));


                HyperLink lnk = e.Item.FindControl("lnkGame") as HyperLink;
                lnk.NavigateUrl = string.Format("~/KidsGame/GameShow.aspx?id={0}", game.GameId);
            }
        }

        private string GetGameState(Game game)
        {
            if (game.UserStateRequired.HasValue)
            {
                switch ((KidsUserStatus)game.UserStateRequired)
                {
                    case KidsUserStatus.RegisterWithoutConfirmation:
                    case KidsUserStatus.RegisterdWithNoAcc:
                    case KidsUserStatus.WaiteForAccCreation:
                    case KidsUserStatus.WaiteForAccCreation_WithSabtConfirmation:
                    case KidsUserStatus.WaiteForAccCreation_Failed:
                    case KidsUserStatus.WaiteForAccCreation_FailedSabt:
                    case KidsUserStatus.WaiteForAccCreation_SentToSabt:
                        return "کاربران عضو";

                    case KidsUserStatus.AccCreated_WaiteForDBCR:
                    case KidsUserStatus.RegisterdCompletly:
                        return "کاربران عضو دارای حساب آرزو";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return "رایگان";

        }

    }


}