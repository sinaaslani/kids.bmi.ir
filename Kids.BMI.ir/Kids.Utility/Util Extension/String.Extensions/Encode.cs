using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Kids.Utility.UtilExtension.StringExtensions.Encode
{
    public static class Encode
    {
        /// <summary>
        /// Encodes to Base64
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Base 64 Encoded string</returns>
        public static string Base64StringEncode(this string val)
        {
            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(val);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        /// <summary>
        /// Decodes a Base64 encoded string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Base 64 decoded string</returns>
        public static string Base64StringDecode(this string val)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(val);
            string returnValue = ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
        /// <summary>
        /// Left pads the passed string using the HTML non-breaking space ( ) for the total number of spaces. 
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="totalSpaces">Total number of spaces to add</param>
        /// <returns>string after adding the Html Spaces(&nbsp)</returns>
        public static string PadLeftHtmlSpaces(this string val,int totalSpaces)
        {
            string space = "&nbsp;";
            return Utils.PadLeft(val, space, val.Length + (totalSpaces * space.Length));
        }
        /// <summary>
        /// Right pads the passed string using the HTML non-breaking space ( ) for the total number of spaces. 
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="totalSpaces">total number of spaces to add</param>
        /// <returns>string after adding the Html Spaces(&nbsp)</returns>
        public static string PadRightHtmlSpaces(this string val,int totalSpaces)
        {
            string space = "&nbsp;";
            return Utils.PadRight(val, space, val.Length + (totalSpaces * space.Length));
        }
        /// <summary>
        /// A wrapper around HttpUtility.HtmlEncode
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string HtmlSpecialEntitiesEncode(this string val)
        {
            return HttpUtility.HtmlEncode(val);
        }
        /// <summary>
        /// A wrapper around HttpUtility.HtmlDecode
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string HtmlSpecialEntitiesDecode(this string val)
        {
            return HttpUtility.HtmlDecode(val);
        }
        /// <summary>
        /// Encrypts a string to using MD5 algorithm
        /// </summary>
        /// <param name="val"></param>
        /// <returns>string representation of the MD5 encryption</returns>
        public static string MD5String(this string val)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(val));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        /// <summary>
        /// Verifies the string against the encrypted value for equality
        /// </summary>
        /// <param name="val"></param>
        /// <param name="hash">The encrypted value of the string</param>
        /// <returns>true is the given string is equal to the string encrypted</returns>
        public static bool VerifyMD5String(this string val, string hash)
        {
            string hashOfInput = MD5String(val);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(hashOfInput, hash) ? true : false;
        }
        /// <summary>
        /// Converts all spaces to HTML non-breaking spaces
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string SpaceToNbsp(this string val)
        {
            string space = "&nbsp;";
            return val.Replace(" ", space);
        }
        /// <summary>
        /// Removes all HTML tags from the passed string.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string StripTags(this string val)
        {
            Regex stripTags = new Regex("<(.|\n)+?>");
            return stripTags.Replace(val, "");
        }
        /// <summary>
        /// Converts each new line (\n) and carriage return (\r) symbols to the HTML <br /> tag.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string NewLineToBreak(this string val)
        {
            Regex regEx = new Regex(@"[\n|\r]+");
            return regEx.Replace(val, "<br/>");
        }
    }
}
