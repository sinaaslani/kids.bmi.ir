using System;
using System.Configuration;
using BMISSOClientHelper;
using Microsoft.Web.Services3.Security.Tokens;

namespace BMISSOService
{
    public class AuthenticationCustomized : UsernameTokenManager
    {
        protected override string AuthenticateToken(UsernameToken token)
        {
            string[] WebServiceUsers = ConfigurationManager.AppSettings["WebServiceUsers"].Split(",".ToCharArray());
            string[] WebServicePasswords = ConfigurationManager.AppSettings["WebServicePasswords"].Split(",".ToCharArray());
        

            switch (token.PasswordOption)
            {
                case PasswordOption.SendPlainText:
                    {
                        int useridx = Array.IndexOf(WebServiceUsers, token.Username);
                        if (useridx >= 0)
                            return WebServicePasswords[useridx];
                        break;
                    }
                case PasswordOption.SendHashed:
                    {
                        int useridx = Array.IndexOf(WebServiceUsers, token.Username);
                        if (useridx >= 0)
                            return CryptographyHelper.CalculateHash(WebServicePasswords[useridx], CryptographyHelper.HashMode.SHA1);

                        break;
                    }
            }
            return "";
        }
    }
}


