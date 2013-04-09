using System;
using System.Text.RegularExpressions;

namespace Kids.Utility.UtilExtension.StringExtensions.Validators
{
	public static class Validator
	{
        /// <summary>
        /// Checks if the string is a valid Email.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns true if it is a valid email address</returns>
        public static bool IsValidEmailAddress(this string val)
        {
            if (string.IsNullOrEmpty(val)) return false;

            const string expresion = @"^(?:[a-zA-Z0-9_'^&/+-])+(?:\.(?:[a-zA-Z0-9_'^&/+-])+)*@(?:(?:\[?(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))\.){3}(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\]?)|(?:[a-zA-Z0-9-]+\.)+(?:[a-zA-Z]){2,}\.?)$";
            Regex regex = new Regex(expresion, RegexOptions.IgnoreCase);
            return regex.IsMatch(val);           
        }
        /// <summary>
        /// Checks if the string is a valid URI
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns true if it is a valid URI</returns>
        public static bool IsValidURI(this string val)
        {
            if (string.IsNullOrEmpty(val)) return false;

            return Uri.IsWellFormedUriString(val, UriKind.Absolute);
        }
        /// <summary>
        /// Checks if the string is valid IP address.
        /// Validates IPv4 as well as IPv6 addresses
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns true if it is valid IP</returns>
        public static bool IsValidIP(this string val)
        {
            if (string.IsNullOrEmpty(val)) return false;

            const string strIPv4Pattern = @"\A(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}\z";
            const string strIPv6Pattern = @"\A(?:[0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}\z";
            const string strIPv6Pattern_HEXCompressed = @"\A((?:[0-9A-Fa-f]{1,4}(?::[0-9A-Fa-f]{1,4})*)?)::((?:[0-9A-Fa-f]{1,4}(?::[0-9A-Fa-f]{1,4})*)?)\z";
            const string StrIPv6Pattern_6Hex4Dec = @"\A((?:[0-9A-Fa-f]{1,4}:){6,6})(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}\z";
            const string StrIPv6Pattern_Hex4DecCompressed = @"\A((?:[0-9A-Fa-f]{1,4}(?::[0-9A-Fa-f]{1,4})*)?) ::((?:[0-9A-Fa-f]{1,4}:)*)(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}\z";
            Regex checkstrIPv4Pattern = new Regex(strIPv4Pattern);
            Regex checkstrIPv6Pattern = new Regex(strIPv6Pattern);
            Regex checkstrIPv6Pattern_HEXCompressed = new Regex(strIPv6Pattern_HEXCompressed);
            Regex checkStrIPv6Pattern_6Hex4Dec = new Regex(StrIPv6Pattern_6Hex4Dec);
            Regex checkStrIPv6Pattern_Hex4DecCompressed = new Regex(StrIPv6Pattern_Hex4DecCompressed);
            return checkstrIPv4Pattern.IsMatch(val, 0) ||
                checkstrIPv6Pattern.IsMatch(val, 0) ||
                checkstrIPv6Pattern_HEXCompressed.IsMatch(val, 0) ||
                checkStrIPv6Pattern_6Hex4Dec.IsMatch(val, 0) ||
                checkStrIPv6Pattern_Hex4DecCompressed.IsMatch(val, 0);
        }
        /// <summary>
        /// Checks if the string is Palindrome
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns true if it is a palindrome</returns>
        public static bool IsPalindrome(this string val)
        {
            if (val.Length == 0) return false;
            return val.ToLower() == val.Reverse().ToLower();
        }
	}
}
