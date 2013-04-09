using System;

namespace NumberExtensions.Computation.Mathematical
{
    public static class Mathematical
    {
        /// <summary>
        /// Checks if the number is Prime
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>true if the number is prime</returns>
        public static bool IsPrime(this int val)
        {
            if ((val & 1) == 0)
            {
                return val == 2;
            }
            int num = (int)Math.Sqrt(val);
            for (int i = 3; i <= num; i += 2)
            {
                if ((val % i) == 0)                
                    return false;                
            }
            return true;
        }
        /// <summary>
        /// Calculates the factorial of a number
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Factorial of the number</returns>
        public static double Factorial(this int val)
        {
            if (val <= 1)
                return 1;
            else
                return val * Factorial(val - 1);
        }
        /// <summary>
        /// Calculates the percentage of the number
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="value">The value against percentage to be calculated</param>
        /// <param name="roundOffTo">Precision of the result</param>
        /// <returns></returns>
        public static double PercentOf(this double val,double value,int roundOffTo)
        {
            return Math.Round((val / 100d) * value, roundOffTo);
        }
        /// <summary>
        /// Calculates the percentage of the number
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="value">The value against percentage to be calculated</param>
        /// <param name="roundOffTo">Precision of the result</param>
        /// <returns></returns>
        public static double PercentOf(this int val, double value, int roundOffTo)
        {
            return Math.Round((val / 100d) * value, roundOffTo);
        }
        /// <summary>
        /// Calculates the power of a number
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="off">The number raised to</param>
        /// <returns>The number raised to the power</returns>
        public static double PowerOf(this int val,int off)
        {
            return Math.Pow(val, off);
        }
    }
}
