using System;
using System.Collections.Generic;
using System.Web;
using Kids.EntitiesModel;
using Kids.EntitiesModel.Scores;
using Kids.Utility;
using Site.Kids.bmi.ir.Scores;

namespace Site.Kids.bmi.ir.Classes
{
    public static class SessionItems
    {
        public static string CaptchaImageTextForRegister
        {
            get
            {
                if (HttpContext.Current.Session["CaptchaImageTextForRegister"] != null)
                    return (string)HttpContext.Current.Session["CaptchaImageTextForRegister"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["CaptchaImageTextForRegister"] = value;
            }
        }


        public static string ChildNationalCardFaceUpPic
        {
            get
            {
                if (HttpContext.Current.Session["ChildNationalCardFaceUpPic"] != null)
                    return (string)HttpContext.Current.Session["ChildNationalCardFaceUpPic"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["ChildNationalCardFaceUpPic"] = value;
            }
        }

        public static string ChildPic
        {
            get
            {
                if (HttpContext.Current.Session["ChildPic"] != null)
                    return (string)HttpContext.Current.Session["ChildPic"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["ChildPic"] = value;
            }
        }

        public static string ChildIdentityPic
        {
            get
            {
                if (HttpContext.Current.Session["ChildIdentityPic"] != null)
                    return (string)HttpContext.Current.Session["ChildIdentityPic"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["ChildIdentityPic"] = value;
            }
        }

        public static string ChildNationalCardFaceDownPic
        {
            get
            {
                if (HttpContext.Current.Session["ChildNationalCardFaceDownPic"] != null)
                    return (string)HttpContext.Current.Session["ChildNationalCardFaceDownPic"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["ChildNationalCardFaceDownPic"] = value;
            }
        }

        public static KidsUsers_Payments CurrentTransaction
        {
            get
            {
                if (HttpContext.Current.Session["CurrentTransaction"] != null)
                    return SerializeHelper.DataContract_ToObject<KidsUsers_Payments>(HttpContext.Current.Session["CurrentTransaction"].ToString());
                return null;
            }
            set
            {
                HttpContext.Current.Session["CurrentTransaction"] = SerializeHelper.DataContract_ToString(value);
            }
        }

        public static string FormBody
        {
            get
            {
                if (HttpContext.Current.Session["FormBody"] != null)
                    return (string)HttpContext.Current.Session["FormBody"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["FormBody"] = value;
            }
        }

        public static Double? CurrentScore
        {
            get
            {
                if (HttpContext.Current.Session["CurrentScore"] == null)
                    KidsSecureFormBaseClass.RefreshKidsUserScores();


                if (HttpContext.Current.Session["CurrentScore"] != null)
                    return (double)HttpContext.Current.Session["CurrentScore"];
                return null;
            }

        }
        

        public static List<scoreListItem> CurrentDailyScoreList
        {
            get
            {
                if (HttpContext.Current.Session["CurrentDailyScoreList"] == null)
                    KidsSecureFormBaseClass.RefreshKidsUserScores();

                if (HttpContext.Current.Session["CurrentDailyScoreList"] != null)
                    return SerializeHelper.DataContract_ToObject<List<scoreListItem>>(HttpContext.Current.Session["CurrentDailyScoreList"].ToString());
                return null;
            }

        }

        public static List<scoreListItem> CurrentMonthlyScoreList
        {
            get
            {
                if (HttpContext.Current.Session["CurrentMonthlyScoreList"] == null)
                    KidsSecureFormBaseClass.RefreshKidsUserScores();

                if (HttpContext.Current.Session["CurrentMonthlyScoreList"] != null)
                    return SerializeHelper.DataContract_ToObject<List<scoreListItem>>(HttpContext.Current.Session["CurrentMonthlyScoreList"].ToString());
                return null;
            }

        }

        public static long? IntroducerId
        {
            get
            {
                if (HttpContext.Current.Session["IntroducerId"] != null)
                    return (long)HttpContext.Current.Session["IntroducerId"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["IntroducerId"] = value;
            }
        }
    }
}