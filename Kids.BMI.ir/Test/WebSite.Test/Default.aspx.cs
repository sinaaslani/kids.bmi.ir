using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.UI;
using Kids.Utility;
using WebSite.Test.ScoreServiceRefrence;

namespace WebSite.Test
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string p_Param = Request["p"];
            //p_Param = Server.UrlDecode(p_Param);
            var clientCert = new X509Certificate2(@"D:\My Study\My Project\Kids.BMI.Ir\Other\Cert\kids.pfx", "@kids@");
            string param = ASymetricCryptoHelper.Decrypt(p_Param, clientCert, Encoding.ASCII);

            string Id = param.Split('&')[0].Split('=')[1];
            string t = param.Split('&')[1].Split('=')[1];

            using (ScoreServiceSoapClient c = new ScoreServiceSoapClient())
            {
                var user = c.IsValidUser(Id);
                if (user == null)
                    Response.Write("InvalidUser");

                if (!c.KeepAliveTempUser(Id))
                    Response.Write("Keep Alive Failed");

                try
                {
                    c.AddGameScore(1, 10, 5);
                    c.AddGameScore(1, 11, 6);
                }
                catch
                {
                }

                try
                {
                    c.AddGameScoreForTempUser(Id, 1, 10, 5);
                    c.AddGameScoreForTempUser(Id, 1, 11, 6);
                }
                catch
                {
                }
            }

        }
    }
}