using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Kids.Utility
{
    public static class ImpersonateHelper
    {
        private enum SECURITY_IMPERSONATION_LEVEL
        {
            //SecurityAnonymous = 0,
            //SecurityIdentification = 1,
            SecurityImpersonation = 2,
            //SecurityDelegation = 3
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private extern static bool CloseHandle(IntPtr handle);

        // creates duplicate token handle
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
            int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

        public static WindowsImpersonationContext ImpersonateUser()
        {
            //const string ImpersonateUserName = "Test", ImpersonateUserPassword = "@Password@", ImpersonateDomain = "";
            const string ImpersonateUserName = "epaysvr", ImpersonateUserPassword = "Mdb#hjP48A", ImpersonateDomain = "";

            return ImpersonateUser(ImpersonateUserName, ImpersonateDomain, ImpersonateUserPassword);
        }

        private static WindowsImpersonationContext ImpersonateUser(string sUsername, string sDomain, string sPassword)
        {
            // initialize tokens
            IntPtr pExistingTokenHandle = IntPtr.Zero;
            IntPtr pDuplicateTokenHandle = IntPtr.Zero;

            // if domain name was blank, assume local machine
            if (sDomain == "")
                sDomain = Environment.MachineName;

            try
            {
                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_INTERACTIVE = 2;
                bool bImpersonated = LogonUser(sUsername, sDomain, sPassword,
                    LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref pExistingTokenHandle);

                // did impersonation fail?
                if (false == bImpersonated)
                {
                    int nErrorCode = Marshal.GetLastWin32Error();
                    throw new ApplicationException("LogonUser() failed with error code: " + nErrorCode + "\r\n");
                }

                bool bRetVal = DuplicateToken(pExistingTokenHandle, (int)SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, ref pDuplicateTokenHandle);

                // did DuplicateToken fail?
                if (false == bRetVal)
                {
                    int nErrorCode = Marshal.GetLastWin32Error();
                    CloseHandle(pExistingTokenHandle); // close existing handle
                    throw new ApplicationException("DuplicateToken() failed with error code: " + nErrorCode + "\r\n");


                }
                else
                {
                    // create new identity using new primary token
                    WindowsIdentity newId = new WindowsIdentity(pDuplicateTokenHandle);
                    WindowsImpersonationContext impersonatedUser = newId.Impersonate();

                    return impersonatedUser;
                }

            }
            finally
            {
                // close handle(s)
                if (pExistingTokenHandle != IntPtr.Zero)
                    CloseHandle(pExistingTokenHandle);
                if (pDuplicateTokenHandle != IntPtr.Zero)
                    CloseHandle(pDuplicateTokenHandle);
            }
        }
    }
}
