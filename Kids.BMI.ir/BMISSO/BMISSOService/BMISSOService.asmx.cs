using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Microsoft.Web.Services3;

namespace BMISSOService
{
    public enum AuthenticationMode
    {
        Authenticated,
        UserOrPasswordWrong,
        Suspended,
        ErrorInConnection
    } ;

    public enum PasswordMode
    {
        PlainText,
        Hashed
    } ;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class BMISSOService : WebService //WebServicesClientProtocol //WebService
    {
        private static void IsRequestAuthunticated()
        {
            if (RequestSoapContext.Current.Security.Tokens.Count == 0)
            {
                throw new SoapException(
                    "Missing security token",
                    SoapException.ClientFaultCode);
            }

            if (RequestSoapContext.Current.Security.Tokens.Count > 0)
                return;

            throw new Exception("UsernameToken not supplied");
        }

        [WebMethod]
        public UserProfile Authenticate(String UserID, String Password)
        {
            
            IsRequestAuthunticated();
            return UserManager.AuthenticateUser(UserID, Password);
            
        }

        [WebMethod]
        public UserProfile GetUserInfo(String UserID)
        {
            IsRequestAuthunticated();
            return UserManager.GetUserInfo(UserID);
        }
        
    }
}
