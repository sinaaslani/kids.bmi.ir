
namespace Kids.Utility.UtilExtension.NumberExtensions.FluentDate
{
    public static class NumericDateTime
    {
        /// <summary>
        /// Associate Week with the integer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpanSelector Weeks(this int i)
        {
            return new WeekSelector { ReferenceValue = i };
        }
        /// <summary>
        /// Associate Days with the integer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpanSelector Days(this int i)
        {
            return new DaysSelector { ReferenceValue = i };
        }
        /// <summary>
        /// Associate Years with the integer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpanSelector Years(this int i)
        {
            return new YearsSelector { ReferenceValue = i };
        }
        /// <summary>
        /// Associate Hours with the integer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpanSelector Hours(this int i)
        {
            return new HourSelector { ReferenceValue = i };
        }
        /// <summary>
        /// Associate Minutes with the integer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpanSelector Minutes(this int i)
        {
            return new MinuteSelector { ReferenceValue = i };
        }
        /// <summary>
        /// Associate Seconds with the integer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpanSelector Seconds(this int i)
        {
            return new SecondSelector { ReferenceValue = i };
        }
    }    
}
