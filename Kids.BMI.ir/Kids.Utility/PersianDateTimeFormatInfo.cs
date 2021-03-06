using System;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace Kids.Utility
{
    [Serializable]
    public class PersianDateTime
    {

        private static readonly PersianCalendar pcal = new PersianCalendar();

        #region Constructors

        private PersianDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public PersianDateTime(string PersianDate)
        {
            year = Convert.ToInt32(PersianDate.Substring(0, 4));
            month = Convert.ToInt32(PersianDate.Substring(4, 2));
            day = Convert.ToInt32(PersianDate.Substring(6, 2));
        }

        public PersianDateTime(int Year, int Month, int Day)
        {
            year = Year;
            month = Month;
            day = Day;
        }
        public PersianDateTime(int Year, int Month, int Day, int Hour, int Minute)
        {
            year = Year;
            month = Month;
            day = Day;
            hour = Hour;
            minute = Minute;
        }

        #endregion

        #region Public Property

        public int Year
        {
            get { return year; }
            //set { year = value; }
        }

        public int Month
        {
            get { return month; }
            //set { month = value; }
        }

        public int Day
        {
            get { return day; }
            //set { day = value; }
        }

        public int Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        public int Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        public int Second
        {
            get { return second; }
            set { second = value; }
        }

        #endregion
        #region Properties

        private int day;
        private int hour;
        private int minute;

        private int year;
        private int month;
        private int second;

        private String p_Year
        {
            get { return year.ToString(); }
        }

        private String p_Month
        {
            get
            {
                if (month.ToString().Length > 1)
                    return month.ToString();
                return "0" + month;
            }
        }

        private String p_Day
        {
            get
            {
                if (day.ToString().Length > 1)
                    return day.ToString();
                return "0" + day;
            }
        }

        private String p_Hour
        {
            get
            {
                if (hour.ToString().Length > 1)
                    return hour.ToString();
                return "0" + hour;
            }
        }

        private String p_Minute
        {
            get
            {
                if (minute.ToString().Length > 1)
                    return minute.ToString();
                return "0" + minute;
            }
        }

        private String p_Second
        {
            get
            {
                if (minute.ToString().Length > 1)
                    return second.ToString();
                return "0" + second;
            }
        }

        #endregion

        public static DayOfWeek FirstDayOfWeek
        {
            get { return DayOfWeek.Saturday; }
        }
        public static void SetPersianCalendar()
        {
            CultureInfo info = new CultureInfo("fa-IR")
                                   {
                                       NumberFormat = { DigitSubstitution = DigitShapes.NativeNational }
                                   };


            DateTimeFormatInfo dateTimeFormat = info.DateTimeFormat;
            dateTimeFormat.AMDesignator = "ق.ظ";
            dateTimeFormat.PMDesignator = "ب.ظ";
            dateTimeFormat.DateSeparator = "/";
            dateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            dateTimeFormat.FirstDayOfWeek = DayOfWeek.Saturday;
            dateTimeFormat.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            dateTimeFormat.DayNames = new[] { "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            dateTimeFormat.MonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            dateTimeFormat.ShortestDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            dateTimeFormat.AbbreviatedMonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            dateTimeFormat.MonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            PersianCalendar calendar = new PersianCalendar();
            typeof(DateTimeFormatInfo).GetField("calendar", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(dateTimeFormat, calendar);
            object obj2 = typeof(DateTimeFormatInfo).GetField("m_cultureTableRecord", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(dateTimeFormat);
            obj2.GetType().GetMethod("UseCurrentCalendar", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj2, new[] { calendar.GetType().GetProperty("ID", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(calendar, null) });
            typeof(CultureInfo).GetField("calendar", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(info, calendar);
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
            CultureInfo.CurrentCulture.DateTimeFormat = dateTimeFormat;
            CultureInfo.CurrentUICulture.DateTimeFormat = dateTimeFormat;
        }






        private static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "امرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string GetAbbreviatedMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "فر";
                case 2:
                    return "ار";
                case 3:
                    return "خر";
                case 4:
                    return "تی";
                case 5:
                    return "مر";
                case 6:
                    return "شه";
                case 7:
                    return "مه";
                case 8:
                    return "آب";
                case 9:
                    return "آذ";
                case 10:
                    return "دی";
                case 11:
                    return "به";
                case 12:
                    return "اس";
                default:
                    throw new ArgumentOutOfRangeException("month");
            }
        }

        private static string GetDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یکشنبه";
                case DayOfWeek.Monday:
                    return "دوشنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
                case DayOfWeek.Thursday:
                    return "پنجشنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string GetAbbreviatedDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Saturday:
                    return "ش";
                case DayOfWeek.Sunday:
                    return "ی";
                case DayOfWeek.Monday:
                    return "د";
                case DayOfWeek.Tuesday:
                    return "س";
                case DayOfWeek.Wednesday:
                    return "چ";
                case DayOfWeek.Thursday:
                    return "پ";
                case DayOfWeek.Friday:
                    return "ج";
                default:
                    throw new ArgumentOutOfRangeException("day");
            }
        }

        public String ToLongDateTimeString()
        {
            string PersianDT = "{0} {1} {2} {3}  {4}:{5}:{6}";
            PersianDT = string.Format(PersianDT, GetDayName(GetDayOfWeek()),
                                      p_Day, GetMonthName(Month), p_Year, p_Hour.PadLeft(2, '0'), p_Minute.PadLeft(2, '0'), p_Second.PadLeft(2, '0'));
            return PersianDT;
        }

        public String ToLongDateString()
        {
            string PersianDT = "{0} {1} {2} {3}";
            PersianDT = string.Format(PersianDT, GetDayName(GetDayOfWeek()), p_Day, GetMonthName(Month), p_Year);
            return PersianDT;
        }

        public String ToShortDateTimeString()
        {
            string PersianDT = "{0}/{1}/{2}  {3}:{4}";

            PersianDT = string.Format(PersianDT, p_Year, p_Month, p_Day, p_Hour, p_Minute);
            return PersianDT;
        }

        public String ToLongDateString(char Spliter)
        {
            string PersianDT = "{0}{4}{1}{4}{2}{4}{3}";
            PersianDT = string.Format(PersianDT, GetDayName(GetDayOfWeek()), p_Day, GetMonthName(Month), p_Year, Spliter);
            return PersianDT;
        }

        public String ToLongDateTimeString(char Spliter)
        {
            string PersianDT = "{0}{4}{1}{4}{2}{4}{3}{4}{5}{4}{6}";
            PersianDT = string.Format(PersianDT, GetDayName(GetDayOfWeek()), p_Day, GetMonthName(Month), p_Year, Spliter, p_Hour, p_Minute);
            return PersianDT;
        }
        public String ToShortDateString()
        {
            string PersianDT = "{0}/{1}/{2}";

            PersianDT = string.Format(PersianDT, p_Year, p_Month, p_Day);
            return PersianDT;
        }
        public String ToString(string Spliter)
        {
            string PersianDT = "{0}{1}{2}{3}{4}";

            PersianDT = string.Format(PersianDT, p_Year, Spliter, p_Month, Spliter, p_Day);
            return PersianDT;
        }

        public override String ToString()
        {
            string PersianDT = "{0}{1}{2}";

            PersianDT = string.Format(PersianDT, p_Year, p_Month, p_Day);
            return PersianDT;
        }
        public String ToPaddedString()
        {
            string PersianDT = "{0}{1}{2}";

            PersianDT = string.Format(PersianDT, p_Year.PadLeft(4, '0'), p_Month.PadLeft(2, '0'), p_Day.PadLeft(2, '0'));
            return PersianDT;
        }

        public String ToLongTimeString()
        {
            string PersianDT = "{0}:{1}:{2}";

            PersianDT = string.Format(PersianDT, p_Hour, p_Minute, second);
            return PersianDT;
        }
        public String ToLongTimeString(string Splitter)
        {
            string PersianDT = "{0}{3}{1}{3}{2}";

            PersianDT = string.Format(PersianDT, p_Hour, p_Minute, second, Splitter);
            return PersianDT;
        }
        public String ToShortTimeString()
        {
            string PersianDT = " {0}:{1}";

            PersianDT = string.Format(PersianDT, p_Hour, p_Minute);
            return PersianDT;
        }

        private DayOfWeek GetDayOfWeek()
        {
            return PersianToMiladi(this).DayOfWeek;
        }


        public static string GetShortPersianDateString(DateTime christDateTime)
        {
            PersianCalendar calendar = new PersianCalendar();

            string Month = calendar.GetMonth(christDateTime).ToString();
            Month = Month.Length == 1 ? ("0" + Month) : Month;

            string Day = calendar.GetDayOfMonth(christDateTime).ToString();
            Day = Day.Length == 1 ? ("0" + Day) : Day;

            return string.Format(@"{0}/{1}/{2}", calendar.GetYear(christDateTime),
                                 Month, Day);
        }

        public static string GetLongPersianDateString(DateTime christDateTime)
        {
            PersianCalendar calendar = new PersianCalendar();
            return string.Format(@"{0} {1} {2}", calendar.GetYear(christDateTime),
                                 GetMonthName(calendar.GetMonth(christDateTime)),
                                 calendar.GetDayOfMonth(christDateTime));
        }

        public static string GetShortPersianDateStringReverse(DateTime christDateTime)
        {
            PersianCalendar calendar = new PersianCalendar();
            string Month = calendar.GetMonth(christDateTime).ToString();
            Month = Month.Length == 1 ? ("0" + Month) : Month;

            string Day = calendar.GetDayOfMonth(christDateTime).ToString();
            Day = Day.Length == 1 ? ("0" + Month) : Day;


            return string.Format(@"{2}/{1}/{0}", calendar.GetYear(christDateTime),
                                 Month, Day);
        }

        public static string GetLongPersianDateStringReverse(DateTime christDateTime)
        {
            PersianCalendar calendar = new PersianCalendar();
            return string.Format(@"{2} {1} {0}", calendar.GetYear(christDateTime),
                                 GetMonthName(calendar.GetMonth(christDateTime)),
                                 calendar.GetDayOfMonth(christDateTime));
        }

        private static void CheckDateRange(int month, int day)
        {
            if (month > 12 || month < 1 || day > 31 || day < 1 || (month > 6 && day > 30))
            {
                throw new ApplicationException("Not a valid Hijri Shamsi Sate string");
            }
        }

        

        public static DateTime PersianToMiladi(string persianDate)
        {
            if (!persianDate.Contains("/"))
            {
                string Year = persianDate.Substring(0, 4);
                string Month = persianDate.Substring(4, 2);
                string Day = persianDate.Substring(6, 2);
                return p_PersianToMiladi(string.Format("{0}/{1}/{2}", Year, Month, Day));
            }
            return p_PersianToMiladi(persianDate);
        }


        public static DateTime? TryPersianToMiladi(string persianDate)
        {
            try
            {
                return PersianToMiladi(persianDate);
            }
            catch
            {
                return null;
            }
        }


        private static DateTime p_PersianToMiladi(string persianDate, string PersianTime = null)
        {
            Regex re = new Regex(@"(\d{2,4})/(\d{1,2})/(\d{1,2})");
            Regex revRegex = new Regex(@"(\d{1,2})/(\d{1,2})/(\d{2,4})");
            Match m;
            int Year = 0;
            int Month = 0;
            int Day = 0;
            if (re.IsMatch(persianDate))
            {
                m = re.Match(persianDate);
                Year = int.Parse(m.Groups[1].Value);
                Month = int.Parse(m.Groups[2].Value);
                Day = int.Parse(m.Groups[3].Value);
            }
            else if (revRegex.IsMatch(persianDate))
            {
                m = revRegex.Match(persianDate);
                Year = int.Parse(m.Groups[3].Value);
                Month = int.Parse(m.Groups[2].Value);
                Day = int.Parse(m.Groups[1].Value);
            }
            CheckDateRange(Month, Day);

            string[] arrPersianTime = new[] { "0", "0" };
            if (!string.IsNullOrWhiteSpace(PersianTime))
                arrPersianTime = PersianTime.Split(':');

            PersianCalendar calendar = new PersianCalendar();
            return calendar.ToDateTime(Year, Month, Day, arrPersianTime[0].ToInt32(), arrPersianTime[1].ToInt32(), 0, 0);
        }

        public static DateTime PersianToMiladi(PersianDateTime persiandate)
        {
            return pcal.ToDateTime(persiandate.Year, persiandate.Month, persiandate.Day, persiandate.Hour, persiandate.Minute, persiandate.Second, 0);
        }

        public static PersianDateTime MiladiToPersian(string MiladiDT)
        {
            int Year = Convert.ToInt32(MiladiDT.Substring(0, 4));
            int Month = Convert.ToInt32(MiladiDT.Substring(4, 2));
            int Day = Convert.ToInt32(MiladiDT.Substring(6, 2));
            DateTime pdate = new DateTime(Year, Month, Day, 0, 0, 0);
            return MiladiToPersian(pdate);
        }

        public static PersianDateTime MiladiToPersian(DateTime MiladiDT)
        {
            int year = pcal.GetYear(MiladiDT);
            int month = pcal.GetMonth(MiladiDT);
            int day = pcal.GetDayOfMonth(MiladiDT);
            int hour = pcal.GetHour(MiladiDT);
            int minute = pcal.GetMinute(MiladiDT);
            int second = pcal.GetSecond(MiladiDT);

            PersianDateTime pdate = new PersianDateTime(year, month, day, hour, minute, second);
            return pdate;
        }

        public static String GetPersainDateTimeToLargeString(DateTime dt, string dateTimeFormat)
        {
            string PersianDT = "";
            try
            {
                PersianDT = GetDayName(dt.DayOfWeek);
                PersianDT += " " + pcal.GetDayOfMonth(dt);
                PersianDT += " " + GetMonthName(pcal.GetMonth(dt))
                             + " " + pcal.GetYear(dt);
            }
            catch
            {
            }
            return PersianDT;
        }


        public static String GetPersainDateToString(DateTime dt, string dateTimeFormat)
        {
            string PersianDT = "";
            try
            {
                PersianDT += " " + pcal.GetDayOfMonth(dt);
                PersianDT += " " + GetMonthName(pcal.GetMonth(dt))
                             + " " + pcal.GetYear(dt);
                PersianDT += "  " + dt.Hour + ":" + dt.Minute;
            }
            catch
            {
            }
            return PersianDT;
        }

        public static String GetPersainDateTimeToShortDateString(DateTime dt, string dateTimeFormat)
        {
            string PersianDT = "";
            try
            {
                PersianDT = pcal.GetYear(dt) + "/" + pcal.GetMonth(dt) + "/" + pcal.GetDayOfMonth(dt);
            }
            catch
            {
            }
            return PersianDT;
        }

        public static string MiladiToGhamari(DateTime dt)
        {
            return string.Format("{0} , {1} {2} , {3}",
                                 dt.Year, dt.Day,
                                 GetMonthNameGhamari(dt.Month),
                                 GetDayNameGhamari(dt.DayOfWeek));
        }

        private static string GetMonthNameGhamari(int month)
        {
            switch (month)
            {
                case 1:
                    return "ینایر";
                case 2:
                    return "فبرایر";
                case 3:
                    return "مارس";
                case 4:
                    return "أبریل";
                case 5:
                    return "مایو";
                case 6:
                    return "یونيو";
                case 7:
                    return "یوليو";
                case 8:
                    return "أغسطس";
                case 9:
                    return "سبتمبر";
                case 10:
                    return "أآتوبر";
                case 11:
                    return "نوفمبر";
                case 12:
                    return "دیسمبر";
                default:
                    throw new ArgumentOutOfRangeException("month");
            }
        }

        private static string GetDayNameGhamari(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Saturday:
                    return "السبت";
                case DayOfWeek.Sunday:
                    return "الأحد";
                case DayOfWeek.Monday:
                    return "الاثنين";
                case DayOfWeek.Tuesday:
                    return "الثلاثاء";
                case DayOfWeek.Wednesday:
                    return "الأربعاء";
                case DayOfWeek.Thursday:
                    return "الخميس";
                case DayOfWeek.Friday:
                    return "الجمعة";
                default:
                    throw new ArgumentOutOfRangeException("day");
            }
        }

        public static string AddDays(string PerDate, int Days)
        {
            int Year = Convert.ToInt32(PerDate.Substring(0, 4));
            int Month = Convert.ToInt32(PerDate.Substring(4, 2));
            int Day = Convert.ToInt32(PerDate.Substring(6, 2));
            PersianDateTime pdate = new PersianDateTime(Year, Month, Day, 0, 0, 0);
            DateTime dt = PersianToMiladi(pdate);
            dt = dt.AddDays(Days);
            return MiladiToPersian(dt).ToString();
        }



        #region Operators

        public PersianDateTime SetTime(string p)
        {
            string[] Times = p.Split(':');
            if (Times.Length > 0)
                this.Hour = Convert.ToInt32(Times[0]);

            if (Times.Length > 1)
                this.Minute = Convert.ToInt32(Times[1]);

            if (Times.Length > 2)
                this.Second = Convert.ToInt32(Times[2]);

            return this;
        }

        public static PersianDateTime operator +(PersianDateTime d1, int DayCount)
        {
            return d1.AddDays(DayCount);
        }
        public static PersianDateTime operator +(PersianDateTime d1, TimeSpan TimeSpace)
        {
            DateTime md1 = PersianToMiladi(d1);
            md1 = md1.Add(TimeSpace);
            return MiladiToPersian(md1);
        }

        public static PersianDateTime operator -(PersianDateTime d1, int DayCount)
        {
            return d1.AddDays(-DayCount);
        }
        public static PersianDateTime operator -(PersianDateTime d1, TimeSpan TimeSpace)
        {
            DateTime md1 = PersianToMiladi(d1);
            md1 = md1.Subtract(TimeSpace);
            return MiladiToPersian(md1);
        }


        //public static bool operator >>(PersianDateTime d1, PersianDateTime d2)
        //{
        //    DateTime md1 = PersianToMiladi(d1);
        //    DateTime md2 = PersianToMiladi(d2);
        //    return md1.Subtract(md2).TotalDays > 365;
        //}
        //public static bool operator <<(PersianDateTime d1, PersianDateTime d2)
        //{
        //    DateTime md1 = PersianToMiladi(d1);
        //    DateTime md2 = PersianToMiladi(d2);
        //    return md1.Subtract(md2).TotalSeconds < 1;
        //}


        public static bool operator >(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = PersianToMiladi(d1);
            DateTime md2 = PersianToMiladi(d2);
            return md1.Date > md2.Date;
        }
        public static bool operator <(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = PersianToMiladi(d1);
            DateTime md2 = PersianToMiladi(d2);
            return md1.Date < md2.Date;
        }

        public static bool operator >=(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = PersianToMiladi(d1);
            DateTime md2 = PersianToMiladi(d2);
            return md1.Date >= md2.Date;
        }
        public static bool operator <=(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = PersianToMiladi(d1);
            DateTime md2 = PersianToMiladi(d2);
            return md1.Date <= md2.Date;
        }

        public static bool operator ==(PersianDateTime d1, PersianDateTime d2)
        {
            DateTime md1 = PersianToMiladi(d1);
            DateTime md2 = PersianToMiladi(d2);
            return md1 == md2;
        }
        public static bool operator !=(PersianDateTime d1, PersianDateTime d2)
        {
            if (ReferenceEquals(d1, null) && ReferenceEquals(d2, null))
                return false;

            if (ReferenceEquals(d1, null) || ReferenceEquals(d2, null))
                return true;

            DateTime md1 = PersianToMiladi(d1);
            DateTime md2 = PersianToMiladi(d2);
            return md1 != md2;
        }

        public static PersianDateTime operator ++(PersianDateTime d1)
        {
            DateTime md1 = PersianToMiladi(d1);
            return MiladiToPersian(md1.AddDays(1));
        }
        public static PersianDateTime operator --(PersianDateTime d1)
        {
            DateTime md1 = PersianToMiladi(d1);
            return MiladiToPersian(md1.AddDays(-1));
        }

        #endregion

        public TimeSpan Subtract(PersianDateTime InputDate)
        {
            DateTime dt = PersianToMiladi(this);
            DateTime Inputdt = PersianToMiladi(InputDate);

            return dt.Subtract(Inputdt);
        }



        public enum DateTimeFormat
        {
            MMDDhhmmss,
            hhmmss,
            MMDD,
        }
        public static string GetDateTime(DateTime dt, DateTimeFormat format)
        {
            switch (format)
            {
                case DateTimeFormat.MMDDhhmmss:
                    return dt.ToString("MMddHHmmss");

                case DateTimeFormat.hhmmss:
                    return dt.ToString("HHmmss");

                case DateTimeFormat.MMDD:
                    return dt.ToString("MMdd");

                default:
                    throw new ArgumentOutOfRangeException("format");
            }

        }

        public static PersianDateTime Now
        {
            get { return MiladiToPersian(DateTime.Now); }

        }

        public static PersianDateTime Today
        {
            get { return MiladiToPersian(DateTime.Today); }
        }


        public PersianDateTime AddDays(int i)
        {
            return MiladiToPersian(PersianToMiladi(this).AddDays(i));
        }

        public DateTime ToDateTime()
        {
            return PersianToMiladi(this);
        }

        public bool Equals(PersianDateTime other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.day == day && other.hour == hour && other.minute == minute && other.year == year && other.month == month && other.second == second;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PersianDateTime)) return false;
            return Equals((PersianDateTime)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = day;
                result = (result * 397) ^ hour;
                result = (result * 397) ^ minute;
                result = (result * 397) ^ year;
                result = (result * 397) ^ month;
                result = (result * 397) ^ second;
                return result;
            }
        }


        public PersianDateTime Date
        {
            get { return new PersianDateTime(Hour, Month, Day); }
        }
    }
}