using System;
using System.ComponentModel;
using System.Data.SqlClient;

public class UserProfile
{
    public UserProfile()
    {
    }
    public UserProfile(String PersonalNumber)
    {
        _PersonalNo = PersonalNumber;
    }

    private String _UserID;
    private String _Password = "";
    private String _PersonalNo = "";

    private String _Name = "";
    private String _Email = "";
    private Genders _Gender = Genders.Male;
    private String _PhoneNumber = "";
    private String _FaxNumber = "";
    private String _Mobile = "";
    private String _BirthDate = "";
    private String _Address = "";

    private String _Id_Num = "";
    private String _NationalCode = "";
    private String _FatherName = "";

    private DateTime _CurrentLoginTime;
    private DateTime _LastLoginTime;



    public string UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }
    public String Password
    {
        get { return _Password; }
        set { _Password = value; }
    }

    public String PersonalNo
    {
        get { return _PersonalNo; }
        set { _PersonalNo = value; }
    }
    public String IdNumber
    {
        get { return _Id_Num; }
        set { _Id_Num = value; }
    }
    public String NationalCode
    {
        get { return _NationalCode; }
        set { _NationalCode = value; }
    }
    public String Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public String FatherName
    {
        get { return _FatherName; }
        set { _FatherName = value; }
    }

    [Category("Contact Info")]
    public String Email
    {
        get { return _Email; }
        set { _Email = value; }
    }

    [CategoryAttribute("Contact Info")]
    public String PhoneNumber
    {
        get { return _PhoneNumber; }
        set { _PhoneNumber = value; }
    }

    [Category("Contact Info")]
    public String FaxNumber
    {
        get { return _FaxNumber; }
        set { _FaxNumber = value; }
    }
    [Category("Contact Info")]
    public String Mobile
    {
        get { return _Mobile; }
        set { _Mobile = value; }
    }
    public String Birthdate
    {
        get { return _BirthDate; }
        set { _BirthDate = value; }
    }


    [CategoryAttribute("Contact Info")]
    public String Address
    {
        get { return _Address; }
        set { _Address = value; }
    }

    public DateTime LastLoginTime
    {
        get { return _LastLoginTime; }
        set { _LastLoginTime = value; }
    }

    public DateTime CurrentLoginTime
    {
        get { return _CurrentLoginTime; }
        set { _CurrentLoginTime = value; }
    }


    public Genders Gender
    {
        get { return _Gender; }
        set { _Gender = value; }
    }

    public enum Genders
    {
        Male,
        Female
    } ;
    public static UserProfile PopulateUser(SqlDataReader reader)
    {
        UserProfile up = new UserProfile();
        up.UserID = Convert.ToString(reader["UserID"]).Trim();
        up.Password = Convert.ToString(reader["Password"]).Trim();
        up.Name = Convert.ToString(reader["Name"]).Trim();
        up.Gender = Convert.ToInt32(reader["sex"]) == 1 ? Genders.Male : Genders.Female;
        up.Email = Convert.ToString(reader["Email"]).Trim();
        up.Address = Convert.ToString(reader["Address"]).Trim();
        up.Birthdate = Convert.ToString(reader["birthdate"]).Trim();
        up.Mobile = Convert.ToString(reader["mobile"]).Trim();
        up.FaxNumber = Convert.ToString(reader["fax"]).Trim();
        up.PhoneNumber = Convert.ToString(reader["tel"]).Trim();
        up.LastLoginTime = Convert.ToDateTime(reader["last_s_login_time"]);
        up.CurrentLoginTime = Convert.ToDateTime(reader["cur_login_time"]);
        up.PersonalNo = Convert.ToString(reader["PerNo"]).Trim();

        up.IdNumber = Convert.ToString(reader["id_num"]).Trim();
        up.NationalCode = Convert.ToString(reader["NationalCode"]).Trim();
        up.FatherName = Convert.ToString(reader["father"]).Trim();

        return up;
    }
}
