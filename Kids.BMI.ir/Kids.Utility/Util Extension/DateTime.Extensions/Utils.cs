using System;


namespace Kids.Utility.UtilExtension.DateTimeExtensions
{
    public static class Utils
    {
        /// <summary>
        /// Represents TimeSpan in words
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>String representation of the timespan</returns>
        public static string ToWords(this TimeSpan val)
        {
            return TimeSpanArticulator.Articulate(val, TemporalGroupType.day
                |TemporalGroupType.hour
                |TemporalGroupType.minute
                |TemporalGroupType.month 
                |TemporalGroupType.second 
                |TemporalGroupType.week 
                |TemporalGroupType.year);
        }
        /// <summary>
        /// Converts Datetime value at midnight
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DateTime value with time set to Midnight</returns>
        public static DateTime MidNight(this DateTime val)
        {
            return new DateTime(val.Year, val.Month, val.Day, 0, 0, 0);
        }
        /// <summary>
        /// Converts Datetime value at noon
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>DateTime value with time set to Noon</returns>
        public static DateTime Noon(this DateTime val)
        {
            return new DateTime(val.Year, val.Month, val.Day, 12, 0, 0);
        }
        /// <summary>
        /// Checks if the Datetime lies within a given range
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="floor">The floor value of the range</param>
        /// <param name="ceiling">The ceiling value of the range</param>
        /// <param name="includeBase">True to include the floor and ceiling values for comparison</param>
        /// <returns>Returns true if the value lies within the range</returns>
        public static bool IsWithinRange(this DateTime val, DateTime floor, DateTime ceiling, bool includeBase)
        {
            if (floor > ceiling)
                throw new InvalidOperationException("floor value cannot be greater than ceiling value");
            if (floor == ceiling)
                throw new InvalidOperationException("floor value cannot be equal to ceiling value");

            if (includeBase)
                return (val >= floor && val <= ceiling);
            else
                return (val > floor && val < ceiling);
        }
        /// <summary>
        /// Calculates the TimeSpan between the current Datetime and the provided Datetime
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>TimeSpan between the current DateTime & the provided DateTime</returns>
        public static TimeSpan GetTimeSpan(this DateTime val)
        {
            TimeSpan dateDiff;
            if (val < DateTime.Now)
                dateDiff = DateTime.Now.Subtract(val);
            else if (val > DateTime.Now)
                dateDiff = val.Subtract(DateTime.Now);
            else
                throw new InvalidOperationException("value cannot be equal to DateTime.Now");
            return dateDiff;
        }

        public static int CalculateAge(this DateTime? birthDate)
        {
            if (birthDate.HasValue)
            {
                int age = DateTime.Now.Year - birthDate.Value.Year;
                if (DateTime.Now.Month < birthDate.Value.Month ||
                    (DateTime.Now.Month == birthDate.Value.Month && DateTime.Now.Day < birthDate.Value.Day)) age--;
                return age;
            }
            throw new InvalidCastException();
        }

        public static int CalculateAge(this DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.Month < birthDate.Month ||
                (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day)) age--;
            return age;
        }

        public static double CalculateAgeExact(this DateTime? birthDate)
        {
            if (birthDate.HasValue)
                return DateTime.Now.Subtract(birthDate.Value).TotalDays/(365.0);
            throw new InvalidCastException();
        }
    }    
}
