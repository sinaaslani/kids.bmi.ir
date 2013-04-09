using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.KidsGame
{
    public partial class GameShow : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["id"].IsInt32())
                {
                    var GameId = Request["id"].ToInt32();
                    var game = Game_DataProvider.GetGame(GameId).FirstOrDefault();
                    if (game != null)
                    {
                        if (CanKidPlayThisGame(game))
                        {
                            if (!game.IsExternalGame)
                            {
                                Title = game.Name;
                                string GlobalSWfurl = string.Format("/KidsGame/GameFileManager.aspx?lid={0}", GetTemproryLink(game));
                                lblSWF.Text = UserControlBaseClass.SWF_tag(GlobalSWfurl, 850, 590);
                                pnlGame.Visible = true;
                            }
                            else
                            {
                                KidsUser user;
                                if (KidsSecureFormBaseClass.OnlineKidsUser != null)
                                    user = KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo;
                                else
                                {
                                    ShowMessageBox(@"کاربر گرامی :<BR>
                                           در حال حاضر شما امکان استفاده از این بازی را نداری.شما میتوانید با عضویت در سایت و تکمیل مراحل ثبت نام از کلیه بازیهای سایت بهره مند گردید.", "بازی");
                                    ClientRedirect("~/بازی.aspx", 3000);
                                    return;
                                }


                                string Guid = TempUserMapperManager.Instance.AddTempUser(user);
                                string Param = string.Format("Id={0}&t={1}", Guid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                                var clientCert = new X509Certificate2(SystemConfigs.PublicKeyPath);
                                string Enc_Param = ASymetricCryptoHelper.Encrypt(Param, clientCert, Encoding.ASCII);

                                string Url = string.Format("{0}?p={1}", game.GameUrl, Server.UrlEncode(Enc_Param));
                                Response.Redirect(Url);
                            }
                        }
                        else
                        {
                            ShowMessageBox(@"کاربر گرامی :<BR>
                                           در حال حاضر شما امکان استفاده از این بازی را نداری.شما میتوانید با عضویت در سایت و تکمیل مراحل ثبت نام از کلیه بازیهای سایت بهره مند گردید.", "بازی");
                            ClientRedirect("~/بازی.aspx", 3000);
                        }
                    }
                }
            }
        }
        private Guid GetTemproryLink(Game game)
        {
            return TempLinkManager.Instanse.AddLink(MapPath(string.Format("/AdminCP/Files/Game/{0}", game.FileAddress)));
        }
        private bool CanKidPlayThisGame(Game game)
        {
            KidsUser user = null;
            if (KidsSecureFormBaseClass.OnlineKidsUser != null)
                user = KidsSecureFormBaseClass.OnlineKidsUser.Kids_UserInfo;

            if (game.UserStateRequired.HasValue)
            {
                if (user != null)
                    return user.CurrentStatus.ToString().PadRight(2, '0').ToInt32() >= game.UserStateRequired.ToString().PadRight(2, '0').ToInt32();
                return false;
            }
            return true;

        }

    }
}