using System;
using System.Globalization;
using System.Web.UI.WebControls;

namespace CommonUtility
{
    public static class Common
    {
        public static DropDownList SetYearDropDownList(DropDownList yearDropList, int yearShowNum)
        {
            PersianCalendar pcal = new PersianCalendar();
            DateTime today = DateTime.Today;
            int maxYear = pcal.GetYear(today.AddDays(2));
            for (int i = maxYear; i > maxYear - yearShowNum; i--)
                yearDropList.Items.Add(new ListItem(i.ToString(), i.ToString()));
            return yearDropList;

        }
        public static DropDownList SetYearDropDownList(DropDownList yearDropList, int yearShowNum, string lang)
        {
            if (lang.ToLower() == "fa")
                return SetYearDropDownList(yearDropList, yearShowNum);
            else
            {
                int todayYear = DateTime.Today.Year;
                for (int i = todayYear; i > todayYear - yearShowNum; i--)
                    yearDropList.Items.Add(new ListItem(i.ToString(), i.ToString()));
                return yearDropList;
            }
        }
        public static DropDownList SetYearDropDownList(DropDownList yearDropList, int startYear, int yearShowNum)
        {
            PersianCalendar pcal = new PersianCalendar();
            for (int i = startYear; i < startYear + yearShowNum; i++)
                yearDropList.Items.Add(new ListItem(i.ToString(), i.ToString()));
            return yearDropList;

        }


        public static String CreateTemporaryPassword(int length)
        {
            string strTempPassword = Guid.NewGuid().ToString("N");
            for (int i = 0; i < (length / 32); i++)
            {
                strTempPassword += Guid.NewGuid().ToString("N");
            }
            return strTempPassword.Substring(0, length);
        }
    }
}
