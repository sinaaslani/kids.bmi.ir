using System;
using System.ComponentModel;
using System.Data.SqlClient;

namespace BMISSOService
{
    public class UserProfile
    {
        public UserProfile()
        {
            Gender = Genders.Male;
            Address = "";
            Birthdate = "";
            Mobile = "";
            FaxNumber = "";
            PhoneNumber = "";
            Email = "";
            FatherName = "";
            Name = "";
            NationalCode = "";
            IdNumber = "";
            PersonalNo = "";
        }
        
        private int _TotalTryToLogin = 1;


        public string UserID { get; set; }

        public string PersonalNo { get; set; }

        public string IdNumber { get; set; }

        public string NationalCode { get; set; }

        public string Name { get; set; }

        public bool IsBMIEmployee { get; set; }

        public string FatherName { get; set; }

        [Category("Contact Info")]
        public string Email { get; set; }

        [CategoryAttribute("Contact Info")]
        public string PhoneNumber { get; set; }

        [Category("Contact Info")]
        public string FaxNumber { get; set; }

        [Category("Contact Info")]
        public string Mobile { get; set; }

        public string Birthdate { get; set; }


        [CategoryAttribute("Contact Info")]
        public string Address { get; set; }

        public DateTime LastLoginTime { get; set; }

        public DateTime CurrentLoginTime { get; set; }


        public Genders Gender { get; set; }

        public int TotalTryToLogin
        {
            get { return _TotalTryToLogin; }
            set { _TotalTryToLogin = value; }
        }

        public DateTime LastActivity { get; set; }

        public enum Genders
        {
            Male,
            Female
        } ;
        public static UserProfile PopulateUser(SqlDataReader reader, out string UserPassword)
        {
            UserPassword = null;
            try
            {
                UserPassword = Convert.ToString(reader["Password"]).Trim();
                UserProfile up = new UserProfile
                                     {
                                         UserID = Convert.ToString(reader["UserID"]).Trim(),
                                         Name = Convert.ToString(reader["Name"]).Trim(),
                                         Email = Convert.ToString(reader["Email"]).Trim(),
                                         Address = Convert.ToString(reader["Address"]).Trim(),
                                         Birthdate = Convert.ToString(reader["birthdate"]).Trim(),
                                         Mobile = Convert.ToString(reader["mobile"]).Trim(),
                                         FaxNumber = Convert.ToString(reader["fax"]).Trim(),
                                         PhoneNumber = Convert.ToString(reader["tel"]).Trim(),
                                         PersonalNo = Convert.ToString(reader["PerNo"]).Trim(),
                                         IdNumber = Convert.ToString(reader["id_num"]).Trim(),
                                         NationalCode = Convert.ToString(reader["NationalCode"]).Trim(),
                                         FatherName = Convert.ToString(reader["father"]).Trim(),
                                         Gender = Convert.ToInt32(reader["sex"]) == 1 ? Genders.Male : Genders.Female,
                                         IsBMIEmployee = !String.IsNullOrWhiteSpace(reader["PerNo"].ToString()),
                                     };
                if (!(reader["last_s_login_time"] is DBNull))
                    up.LastLoginTime = Convert.ToDateTime(reader["last_s_login_time"]);
                if (!(reader["cur_login_time"] is DBNull))
                    up.CurrentLoginTime = Convert.ToDateTime(reader["cur_login_time"]);

                return up;

            }
            catch
            {
                return null;
            }

        }
    }
}
