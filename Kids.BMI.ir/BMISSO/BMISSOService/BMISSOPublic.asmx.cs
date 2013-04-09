using System;
using System.Web.Services;
using System.Configuration;

namespace BMISSOService
{
   
    [WebService(Namespace = "http://BMI.ir/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class BMISSOPublic : WebService
    {
        static readonly string[] WebServiceUsers = ConfigurationManager.AppSettings["WebServiceUsers"].Split(",".ToCharArray());
        static readonly string[] WebServicePasswords = ConfigurationManager.AppSettings["WebServicePasswords"].Split(",".ToCharArray());




        private static bool IsRequestAuthunticated(SSOUserToken token)
        {
            //throw new Exception("UsernameToken not supplied");
            int useridx = Array.IndexOf(WebServiceUsers, token.SSOUserName);
            string pass = "";
            if (useridx >= 0)
                pass = WebServicePasswords[useridx];
            if (pass != "" && (pass == token.SSOPassword))
                return true;

            throw new Exception("UsernameToken is not valid");
        }

        [WebMethod]
        public UserProfile Authenticate(SSOUserToken token, String UserID, String Password)
        {
            if (IsRequestAuthunticated(token))
                return UserManager.AuthenticateUser(UserID, Password);
            return null;
        }

        [WebMethod]
        public UserProfile GetUserInfo(SSOUserToken token, String UserID)
        {
            if (IsRequestAuthunticated(token))
                return UserManager.GetUserInfo(UserID);
            return null;
        }
        
    }
}
