using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using System.Configuration;
using BMISSOClientHelper;
using CRYPTOLib;
using System.Linq;
using System.Collections.Generic;

namespace BMISSOService
{
    public static class UserManager
    {

        private static readonly string CnnString = ConfigurationManager.ConnectionStrings["BmiMembershipConnection"].ConnectionString;


        public static UserProfile AuthenticateUser(string uname, string pass)
        {
            string UserPassword;
            UserProfile userinfo = GetUserInfo(uname, out UserPassword);
            return userinfo;
            bool userIsBlock = IsInBlockList(uname);
            if (userinfo != null && !userIsBlock)
            {
                CryptoEngine Cryp = new CryptoEngine();
                string DecryptPass = Cryp.decrypt(userinfo.UserID, UserPassword);
                if (DecryptPass == pass)
                    return userinfo;
            }
            else
            {
                AddRankInBlockList(uname);
                
                if (userIsBlock)
                {
                    string excep = " کاربر گرامي به علت رعايت نکات امنيتي و جلوگيري از حدس زدن کلمه عبور ، account شما بصورت موقت تا 2 ساعت ديگر مسدود شده است";
                    excep += "لطفا مجددا سعي نفرماييد.";
                    throw new Exception(excep);
                }
            }
            return null;
        }

        public static UserProfile GetUserInfo(string uname)
        {
            string UserPassword;
            return GetUserInfo(uname, out UserPassword);
        }

        private static UserProfile GetUserInfo(string uname, out string UserPassword)
        {
            
            UserPassword = null;
            return new UserProfile { UserID = uname, Birthdate = "13740125", };
            SqlConnection conn = new SqlConnection(CnnString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT    * FROM  V_UserInfo  WHERE     (userid = @UserID)", conn) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@UserID", uname);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return UserProfile.PopulateUser(reader, out UserPassword);
                else
                    return null;
            }
            catch (SqlException sqle)
            {
                throw new Exception("SQL Error from WebService!\n" + sqle.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private static bool IsInBlockList(string userId)
        {
            List<UserProfile> users;
            // Read from the cache if available
            if (HttpContext.Current.Cache["SSOUsersBlockList"] == null)
            {
                users = new List<UserProfile>();
                HttpContext.Current.Cache.Insert("SSOUsersBlockList", users, null, DateTime.Now.AddMinutes(120), TimeSpan.Zero);
                return false;
            }
            else
            {
                users = (List<UserProfile>)HttpContext.Current.Cache["SSOUsersBlockList"];
                UserProfile blockeduser = users.FirstOrDefault(o => o.UserID.Trim().ToLower() == userId.Trim().ToLower());

                if (blockeduser != null)
                {
                    if (blockeduser.TotalTryToLogin > 5)
                    {
                        if (blockeduser.LastActivity > DateTime.Now.AddHours(-2))
                            return true; // user is in block list

                        blockeduser.TotalTryToLogin = 2; // user try  ,max: let 3 test for login
                        blockeduser.LastActivity = DateTime.Now;
                        return false;
                    }
                }

                return false;

            }

        }

        private static void AddRankInBlockList(string userId)
        {
            List<UserProfile> users;
            // Read from the cache if available
            if (HttpContext.Current.Cache["SSOUsersBlockList"] == null)
            {
                users = new List<UserProfile>();
                HttpContext.Current.Cache.Insert("SSOUsersBlockList", users, null, DateTime.Now.AddMinutes(120), TimeSpan.Zero);
            }

            users = (List<UserProfile>)HttpContext.Current.Cache["SSOUsersBlockList"];


            UserProfile blockeduser = users.FirstOrDefault(o => o.UserID.Trim().ToLower() == userId.Trim().ToLower());
            if (blockeduser != null)
            {
                blockeduser.TotalTryToLogin += 1;
                blockeduser.LastActivity = DateTime.Now;
            }
            else
            {
                UserProfile newU = new UserProfile { UserID = userId, TotalTryToLogin = 1, LastActivity = DateTime.Now };
                users.Add(newU);
            }
            HttpContext.Current.Cache["SSOUsersBlockList"] = users;

        }

        #region otheruser Management method

        public static Boolean ChangePassword(string uname, string password, PasswordMode mode)
        {
            SqlConnection conn = new SqlConnection(CnnString);

            String Pass = "";
            switch (mode)
            {
                case PasswordMode.PlainText:
                    {
                        Pass = CryptographyHelper.CalculateHash(password, CryptographyHelper.HashMode.SHA1);
                        break;
                    }
                case PasswordMode.Hashed:
                    {
                        Pass = password;
                        break;
                    }
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("", conn) { CommandType = CommandType.StoredProcedure, CommandText = "ChangePassword" };
                cmd.Parameters.AddWithValue("PersonalNo", uname);
                cmd.Parameters.AddWithValue("Password", Pass);
                cmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                conn.Close();
            }
        }

        public static Boolean SetLastLogin(string uname, DateTime lastLogin)
        {
            SqlConnection conn = new SqlConnection(CnnString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("", conn) { CommandType = CommandType.StoredProcedure, CommandText = "UpdateLastLogin" };
                cmd.Parameters.AddWithValue("PersonalNo", uname);
                cmd.Parameters.AddWithValue("LastLogin", lastLogin);
                cmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                conn.Close();
            }

        }

        public static Boolean SetLastActivity(string uname, DateTime lastActivity)
        {
            SqlConnection conn = new SqlConnection(CnnString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("", conn) { CommandType = CommandType.StoredProcedure, CommandText = "UpdateLastActivity" };
                cmd.Parameters.AddWithValue("PersonalNo", uname);
                cmd.Parameters.AddWithValue("LastActivity", lastActivity);
                cmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                conn.Close();
            }


        }

        public static Boolean ChangeSuspensionStatus(String userName, Boolean Suspended, String SuspensionReason)
        {
            SqlConnection conn = new SqlConnection(CnnString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("", conn) { CommandType = CommandType.StoredProcedure, CommandText = "ChangeSuspensionStatus" };
                cmd.Parameters.AddWithValue("PersonalNo", userName);
                {
                    switch (Suspended)
                    {
                        case false:
                            {
                                cmd.Parameters.AddWithValue("Suspended", "0");
                                break;
                            }
                        case true:
                            {
                                cmd.Parameters.AddWithValue("Suspended", "1");
                                break;
                            }
                    }
                }
                cmd.Parameters.AddWithValue("SuspensionReason", SuspensionReason);
                cmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                conn.Close();
            }


        }
        #endregion



    }
}
