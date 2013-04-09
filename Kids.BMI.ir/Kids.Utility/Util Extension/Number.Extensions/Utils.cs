using System;

namespace Kids.Utility.UtilExtension.NumberExtensions
{
    public static class Utils
    {
        /// <summary>
        /// Converts long to words
        /// Test Coverage: Included
        /// </summary>
        /// <param name="var"></param>
        /// <returns>The representation of the number in words</returns>
        public static string ToWords(this long var)
        {
            return NumberArticulator.ConvertNumberToWord(var);
        }
        /// <summary>
        /// Converts integer to words
        /// Test Coverage: Included
        /// </summary>
        /// <param name="var"></param>
        /// <returns>The representation of the number in words</returns>
        public static string ToWords(this int var)
        {
            return NumberArticulator.ConvertNumberToWord((long)var);
        }
        /// <summary>
        /// Returns Age given the Date Of Birth.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns>The Age</returns>
        public static Age Age(this DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            if (today < birthDate)
                throw new InvalidOperationException("birth date cannot be greater than the current date");

            int ageInYears = 0;
            int ageInMonths = 0;
            int ageInDays = 0;

            CalculateAge(birthDate, today, out ageInYears, out ageInMonths, out ageInDays);
            return CreateAge(ageInYears,ageInMonths,ageInDays);
        }
        /// <summary>
        /// Calculates Age.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="birthDate">Date Of Birth.</param>
        /// <param name="futureDate">The Date at which the age has to be calculated.</param>
        /// <returns>Age at a particular date.</returns>
        public static Age AgeAt(this DateTime birthDate, DateTime futureDate)
        {
            if (futureDate < birthDate)
                throw new InvalidOperationException("Future date cannot be less than the date of birth");
            int ageInYears = 0;
            int ageInMonths = 0;
            int ageInDays = 0;

            CalculateAge(birthDate, futureDate, out ageInYears, out ageInMonths, out ageInDays);
            return CreateAge(ageInYears, ageInMonths, ageInDays);
        }
        /// <summary>
        /// Convert to Ordinal number
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>String representation of the Ordinal number</returns>
        public static string ToOrdinal(this int val)
        {
            if (val <= 0) throw new ArgumentException("Cardinal must be positive.");

            int lastTwoDigits = val % 100;
            int lastDigit = lastTwoDigits % 10;
            string suffix;
            switch (lastDigit)
            {
                case 1:
                    suffix = "st";
                    break;

                case 2:
                    suffix = "nd";
                    break;

                case 3:
                    suffix = "rd";
                    break;

                default:
                    suffix = "th";
                    break;
            }
            if (11 <= lastTwoDigits && lastTwoDigits <= 13)
            {
                suffix = "th";
            }
            return string.Format("{0}{1}", val, suffix);
        }        

        #region -- Private Methods --
		private static void CalculateAge(DateTime adtDateOfBirth,DateTime referenceDate, out int noOfYears, out int noOfMonths, out int noOfDays)
        {
            DateTime adtCurrentDate = referenceDate;
 
            noOfDays = adtCurrentDate.Day - adtDateOfBirth.Day;
            noOfMonths = adtCurrentDate.Month - adtDateOfBirth.Month;
            noOfYears = adtCurrentDate.Year - adtDateOfBirth.Year;
 
            if (noOfDays < 0)
            {
                noOfDays += DateTime.DaysInMonth(adtCurrentDate.Year, adtCurrentDate.Month);
                noOfMonths--;
            }
 
            if (noOfMonths < 0)
            {
                noOfMonths += 12;
                noOfYears--;
            }
        }
        private static Age CreateAge(int years,int Months,int days)
        {
            Age age = new Age();
            age.Years = years;
            age.Months = Months;
            age.Days = days;
            return age;
        }
	    #endregion    
    }
}
