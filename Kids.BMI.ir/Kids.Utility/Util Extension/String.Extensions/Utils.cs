using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using ISC.FTS.Upload.Security;
using SabteAhvalLibrary;

namespace Kids.Utility.UtilExtension.StringExtensions
{
    public static class Utils
    {
        /// <summary>
        /// Replaces a given character with another character in a string. 
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charToReplace">The character to replace</param>
        /// <param name="replacement">The character by which to be replaced</param>
        /// <returns>Copy of string with the characters replaced</returns>
        public static string CaseInsenstiveReplace(this string val, char charToReplace, char replacement)
        {
            Regex regEx = new Regex(charToReplace.ToString(), RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(val, replacement.ToString());
        }
        /// <summary>
        /// Replaces a given string with another string in a given string. 
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="stringToReplace">The string to replace</param>
        /// <param name="replacement">The string by which to be replaced</param>
        /// <returns>Copy of string with the string replaced</returns>
        public static string CaseInsenstiveReplace(this string val, string stringToReplace, string replacement)
        {
            Regex regEx = new Regex(stringToReplace, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(val, replacement);
        }
        /// <summary>
        /// Replaces the first occurrence of a string with another string in a given string
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="stringToReplace">The string to replace</param>
        /// <param name="replacement">The string by which to be replaced</param>
        /// <returns>Copy of string with the string replaced</returns>
        public static string ReplaceFirst(this string val, string stringToReplace, string replacement)
        {
            Regex regEx = new Regex(stringToReplace, RegexOptions.Multiline);
            return regEx.Replace(val, replacement, 1);
        }
        /// <summary>
        /// Replaces the first occurrence of a character with another character in a given string
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charToReplace">The character to replace</param>
        /// <param name="replacement">The character by which to replace</param>
        /// <returns>Copy of string with the character replaced</returns>
        public static string ReplaceFirst(this string val, char charToReplace, char replacement)
        {
            Regex regEx = new Regex(charToReplace.ToString(), RegexOptions.Multiline);
            return regEx.Replace(val, replacement.ToString(), 1);
        }
        /// <summary>
        /// Replaces the last occurrence of a character with another character in a given string
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charToReplace">The character to replace</param>
        /// <param name="replacement">The character by which to replace</param>
        /// <returns>Copy of string with the character replaced</returns>
        public static string ReplaceLast(this string val, char charToReplace, char replacement)
        {
            int index = val.LastIndexOf(charToReplace);
            if (index < 0)
            {
                return val;
            }
            else
            {
                StringBuilder sb = new StringBuilder(val.Length - 2);
                sb.Append(val.Substring(0, index));
                sb.Append(replacement);
                sb.Append(val.Substring(index + 1,
                   val.Length - index - 1));
                return sb.ToString();
            }
        }
        /// <summary>
        /// Replaces the last occurrence of a string with another string in a given string
        /// The replacement is case insensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="stringToReplace">The string to replace</param>
        /// <param name="replacement">The string by which to be replaced</param>
        /// <returns>Copy of string with the string replaced</returns>
        public static string ReplaceLast(this string val, string stringToReplace, string replacement)
        {
            int index = val.LastIndexOf(stringToReplace);
            if (index < 0)
            {
                return val;
            }
            else
            {
                StringBuilder sb = new StringBuilder(val.Length - stringToReplace.Length + replacement.Length);
                sb.Append(val.Substring(0, index));
                sb.Append(replacement);
                sb.Append(val.Substring(index + stringToReplace.Length,
                   val.Length - index - stringToReplace.Length));

                return sb.ToString();
            }
        }
        /// <summary>
        /// Removes occurrences of words in a string
        /// The match is case sensitive
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="filterWords">Array of words to be removed from the string</param>
        /// <returns>Copy of the string with the words removed</returns>
        public static string RemoveWords(this string val, params string[] filterWords)
        {
            return MaskWords(val, char.MinValue, filterWords);
        }
        /// <summary>
        /// Masks the occurence of words in a string with a given character
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="mask">The character mask to apply</param>
        /// <param name="filterWords">The words to be replaced</param>
        /// <returns>The copy of string with the mask applied</returns>
        public static string MaskWords(this string val, char mask, params string[] filterWords)
        {
            string stringMask = mask == char.MinValue ?
               string.Empty : mask.ToString();
            string totalMask = stringMask;

            foreach (string s in filterWords)
            {
                Regex regEx = new Regex(s, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (stringMask.Length > 0)
                {
                    for (int i = 1; i < s.Length; i++)
                        totalMask += stringMask;
                }

                val = regEx.Replace(val, totalMask);
                totalMask = stringMask;
            }
            return val;
        }
        /// <summary>
        /// Left pads the passed string using the passed pad string for the total number of spaces. 
        /// It will not cut-off the pad even if it causes the string to exceed the total width.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="pad">The pad string</param>
        /// <param name="totalWidth">The total width of the resulting string</param>
        /// <returns>Copy of string with the padding applied</returns>
        public static string PadLeft(this string val, string pad, int totalWidth)
        {
            return PadLeft(val, pad, totalWidth, false);
        }
        /// <summary>
        /// Left pads the passed string using the passed pad string for the total number of spaces. 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="pad">The pad string</param>
        /// <param name="totalWidth">The total width of the resulting string</param>
        /// <param name="cutOff">True to cut off the characters if exceeds the specified width</param>
        /// <returns>Copy of string with the padding applied</returns>
        public static string PadLeft(this string val, string pad, int totalWidth, bool cutOff)
        {
            if (val.Length >= totalWidth)
                return val;

            int padCount = pad.Length;
            string paddedString = val;

            while (paddedString.Length < totalWidth)
            {
                paddedString += pad;
            }

            if (cutOff)
                paddedString = paddedString.Substring(0, totalWidth);
            return paddedString;
        }
        /// <summary>
        /// Right pads the passed string using the passed pad string for the total number of spaces. 
        /// It will not cut-off the pad even if it causes the string to exceed the total width.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="pad">The pad string</param>
        /// <param name="totalWidth">The total width of the resulting string</param>
        /// <returns>Copy of string with the padding applied</returns>
        public static string PadRight(this string val, string pad, int totalWidth)
        {
            return PadRight(val, pad, totalWidth, false);
        }
        /// <summary>
        /// Right pads the passed string using the passed pad string for the total number of spaces. 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="pad">The pad string</param>
        /// <param name="totalWidth">The total width of the resulting string</param>
        /// <param name="cutOff">True to cut off the characters if exceeds the specified width</param>
        /// <returns>Copy of string with the padding applied</returns>
        public static string PadRight(this string val, string pad, int totalWidth, bool cutOff)
        {
            if (val.Length >= totalWidth)
                return val;

            string paddedString = string.Empty;

            while (paddedString.Length < totalWidth - val.Length)
            {
                paddedString += pad;
            }

            if (cutOff)
                paddedString = paddedString.Substring(0, totalWidth - val.Length);
            paddedString += val;
            return paddedString;
        }
        /// <summary>
        /// Removes new line characters from a string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns copy of string with the new line characters removed</returns>
        public static string RemoveNewLines(this string val)
        {
            return RemoveNewLines(val, false);
        }
        /// <summary>
        /// Removes new line characters from a string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="input"></param>
        /// <param name="addSpace">True to add a space after removing a new line character</param>
        /// <returns>Returns a copy of the string after removing the new line character</returns>
        public static string RemoveNewLines(this string input, bool addSpace)
        {
            string replace = addSpace ? " " : string.Empty;
            const string pattern = @"[\r|\n]";
            Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(input, replace);
        }
        /// <summary>
        /// Removes a non numeric character from a string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Copy of the string after removing non numeric characters</returns>
        public static string RemoveNonNumeric(this string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
                if (Char.IsNumber(s[i]))
                    sb.Append(s[i]);
            return sb.ToString();
        }
        /// <summary>
        /// Removes numeric characters from a given string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Copy of the string after removing the numeric characters</returns>
        public static string RemoveNumeric(this string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
                if (!Char.IsNumber(s[i]))
                    sb.Append(s[i]);
            return sb.ToString();
        }
        /// <summary>
        /// Reverses a string
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Copy of the reversed string</returns>
        public static string Reverse(this string val)
        {
            char[] reverse = new char[val.Length];
            for (int i = 0, k = val.Length - 1; i < val.Length; i++, k--)
            {
                if (char.IsSurrogate(val[k]))
                {
                    reverse[i + 1] = val[k--];
                    reverse[i++] = val[k];
                }
                else
                {
                    reverse[i] = val[k];
                }
            }
            return new string(reverse);
        }
        /// <summary>
        /// Changes the string as sentence case.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Copy of string with the sentence case applied</returns>
        public static string SentenceCase(this string val)
        {
            if (val.Length < 1)
                return val;

            string sentence = val.ToLower();
            return sentence[0].ToString().ToUpper() +
               sentence.Substring(1);
        }
        /// <summary>
        /// Changes the string as title case.
        /// Ignores short words in the string.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Copy of string with the title case applied</returns>
        public static string TitleCase(this string val)
        {
            if (val.Length == 0) return string.Empty;
            return TitleCase(val, true);
        }
        /// <summary>
        /// Changes the string as title case.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="ignoreShortWords">true to ignore short words</param>
        /// <returns>Copy of string with the title case applied</returns>
        public static string TitleCase(this string val, bool ignoreShortWords)
        {
            if (val.Length == 0) return string.Empty;

            IList<string> ignoreWords = null;
            if (ignoreShortWords)
            {
                //TODO: Add more ignore words?
                ignoreWords = new List<string>();
                ignoreWords.Add("a");
                ignoreWords.Add("is");
                ignoreWords.Add("was");
                ignoreWords.Add("the");
            }

            string[] tokens = val.Split(' ');
            StringBuilder sb = new StringBuilder(val.Length);
            foreach (string s in tokens)
            {
                if (ignoreShortWords == true
                    && s != tokens[0]
                    && ignoreWords.Contains(s.ToLower()))
                {
                    sb.Append(s + " ");
                }
                else
                {
                    sb.Append(s[0].ToString().ToUpper());
                    sb.Append(s.Substring(1).ToLower());
                    sb.Append(" ");
                }
            }

            return sb.ToString().Trim();
        }
        /// <summary>
        /// Removes multiple spaces between words
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns a copy of the string after removing the extra spaces</returns>
        public static string TrimIntraWords(this string val)
        {
            Regex regEx = new Regex(@"[\s]+");
            return regEx.Replace(val, " ");
        }
        /// <summary>
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charCount">The number of characters after which it should wrap the text</param>
        /// <returns>The copy of the string after applying the Wrap</returns>
        public static string WordWrap(this string val, int charCount)
        {
            return WordWrap(val, charCount, false, Environment.NewLine);
        }
        /// <summary>
        /// Wraps the passed string at the passed total number of characters (if cuttOff is true)
        /// or at the next whitespace (if cutOff is false). 
        /// Uses the environment new line symbol for the break text
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charCount">The number of characters after which to break</param>
        /// <param name="cutOff">true to break at specific</param>
        /// <returns></returns>
        public static string WordWrap(this string val, int charCount, bool cutOff)
        {
            return WordWrap(val, charCount, cutOff, Environment.NewLine);
        }
        private static string WordWrap(this string val, int charCount, bool cutOff, string breakText)
        {
            StringBuilder sb = new StringBuilder(val.Length + 100);
            int counter = 0;

            if (cutOff)
            {
                while (counter < val.Length)
                {
                    if (val.Length > counter + charCount)
                    {
                        sb.Append(val.Substring(counter, charCount));
                        sb.Append(breakText);
                    }
                    else
                    {
                        sb.Append(val.Substring(counter));
                    }
                    counter += charCount;
                }
            }
            else
            {
                string[] strings = val.Split(' ');
                for (int i = 0; i < strings.Length; i++)
                {
                    // added one to represent the space.
                    counter += strings[i].Length + 1;
                    if (i != 0 && counter > charCount)
                    {
                        sb.Append(breakText);
                        counter = 0;
                    }

                    sb.Append(strings[i] + ' ');
                }
            }
            // to get rid of the extra space at the end.
            return sb.ToString().TrimEnd();
        }
        /// <summary>
        /// Converts an list of string to CSV string representation.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="insertSpaces">True to add spaces after each comma</param>
        /// <returns>CSV representation of the data</returns>
        public static string ToCSV(this IEnumerable<string> val, bool insertSpaces)
        {
            if (insertSpaces)
                return String.Join(", ", val.ToArray());
            else
                return String.Join(",", val.ToArray());
        }
        /// <summary>
        /// Converts an list of characters to CSV string representation.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <param name="insertSpaces">True to add spaces after each comma</param>
        /// <returns>CSV representation of the data</returns>
        public static string ToCSV(this IEnumerable<char> val, bool insertSpaces)
        {
            List<string> casted = new List<string>();
            foreach (var item in val)
                casted.Add(item.ToString());

            if (insertSpaces)
                return String.Join(", ", casted.ToArray());
            else
                return String.Join(",", casted.ToArray());
        }
        /// <summary>
        /// Converts CSV to list of string.
        /// Test Coverage: Included
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable collection of string</returns>
        public static IEnumerable<string> ListFromCSV(this string val)
        {
            string[] split = val.Split(',');
            foreach (string item in split)
            {
                item.Trim();
            }
            return new List<string>(split);
        }
        /// <summary>
        /// Binary Serialization to a file
        /// </summary>
        /// <param name="val"></param>
        /// <param name="filePath">The file where serialized data has to be stored</param>
        public static void Serialize(this string val, string filePath)
        {
            try
            {
                Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, val);
                stream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Right(this string val, int length)
        {
            return val.Substring(val.Length - length);
        }
        public static int RightDigits(this string val, int length)
        {
            return Convert.ToInt32(Right(val, length));
        }

        public static string AddSplitter(this string InputString, string Splitter = "-", int Seed = 4)
        {
            int i = Seed;
            while (i < InputString.Length)
            {
                InputString = InputString.Insert(i, Splitter);
                i += (Seed + 1);
            }
            return InputString;
        }
        public static string AddSplitter(this decimal Inputnumber, string Splitter = "-", int Seed = 4)
        {
            return AddSplitter(Inputnumber.ToString(), Splitter, Seed);
        }



        public static string Money3Dispaly(this long InputValue)
        {
            if (InputValue == 0)
                return "0";
            return String.Format("{0:#,###}", InputValue);
        }

        public static bool IsHexDigit(this string InputValue)
        {
            var hexdigits = new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
            return InputValue.All(hexdigits.Contains);
        }

        public static string ToPersinDigit(this string InputValue)
        {
            string UniCodeStr = "";
            foreach (char c in InputValue)
            {
                if (char.IsDigit(c))
                    UniCodeStr += ((char)(c + 1728));
                else
                    UniCodeStr += c;
            }
            return UniCodeStr;
        }
        public static string Money3Dispaly(this string InputValue)
        {
            try
            {
            long _InputValue = Convert.ToInt64(InputValue);
            return Money3Dispaly(_InputValue);
        }
            catch
            {
                return InputValue;
            }
        }

        public static bool IsValidCardNo(this string card_num)
        {
            if (card_num.Length != 16)
                return false;

            return (Crypt.GetCheckMode10(card_num.Substring(0, 15)) == card_num.Substring(15, 1));
        }

        public static bool IsBMIValidAccountNo(this string ac_num)
        {
            if (ac_num.Length != 13)
                return false;

            string ac_s = ac_num.Substring(0, 12);
            string check_digit = ac_num.Substring(12, 1);

            int check_dig = 5 * Convert.ToInt32(ac_s.Substring(11, 1)) + 7 * Convert.ToInt32(ac_s.Substring(10, 1));
            // & _
            check_dig = check_dig + 13 * Convert.ToInt32(ac_s.Substring(9, 1)) + 17 * Convert.ToInt32(ac_s.Substring(8, 1));
            // & _
            check_dig = check_dig + 19 * Convert.ToInt32(ac_s.Substring(7, 1)) + 23 * Convert.ToInt32(ac_s.Substring(6, 1));
            //& _
            check_dig = check_dig + 29 * Convert.ToInt32(ac_s.Substring(5, 1)) + 31 * Convert.ToInt32(ac_s.Substring(4, 1));
            //& _
            check_dig = check_dig + 37 * Convert.ToInt32(ac_s.Substring(3, 1)) + 41 * Convert.ToInt32(ac_s.Substring(2, 1));
            //& _
            check_dig = check_dig + 43 * Convert.ToInt32(ac_s.Substring(1, 1)) + 47 * Convert.ToInt32(ac_s.Substring(0, 1));

            check_dig = check_dig % 11;
            if (check_dig != 1)
            {
                check_dig = 11 - check_dig;
                if (check_dig == 11)
                {
                    check_dig = 0;
                }
                if (check_dig.ToString() == check_digit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public static bool IsValidMelliCode(this string mellicode)
        {
            return SabteAhval.ValidateNIN(mellicode);
        }
        public static bool IsValidMobileNumber(this string mobileNo)
        {
            return (mobileNo.IsInt64() && mobileNo.Length == 11 );
        }
        public static bool IsValidMCINumber(this string mobileNo)
        {
            return (mobileNo.IsInt64() && mobileNo.Length == 11 && mobileNo.StartsWith("091"));
        }

        public static bool IsValidIranCellPhonenumber(this string mobileNo)
        {
            return (mobileNo.IsInt64() && mobileNo.Length == 11 && mobileNo.StartsWith("093"));
        }
        public static bool IsValidIrancellWimaxNumber(this string mobileNo)
        {
            return (mobileNo.IsInt64() && mobileNo.Length == 11 && mobileNo.StartsWith("0941"));
        }

        public static string ToNonInternationalPhoneNumber(this string mobileNo)
        {
            if (mobileNo.StartsWith("+98"))
            {
                return mobileNo.ReplaceFirst("+98", "0");
            }
            if (mobileNo.StartsWith("98"))
            {
                return mobileNo.ReplaceFirst("98", "0");
            }
            if (mobileNo.StartsWith("0098"))
            {
                return mobileNo.ReplaceFirst("0098", "0");
            }
            return mobileNo;
        }
        public static PersianDateTime ToPersianDateTime(this string strDateTime)
        {
            return new PersianDateTime(strDateTime);
        }

        public static string ToInternationalPhoneNumber(this string mobileNo)
        {
            if (mobileNo.StartsWith("093"))
            {
                return mobileNo.ReplaceFirst("093", "9893");
            }
            if (mobileNo.StartsWith("091"))
            {
                return mobileNo.ReplaceFirst("091", "9891");
            }
            return mobileNo;
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static string JoinPart(this string[] input, int Start, int? End = null, char Seperator = ' ')
        {
            int _End = End.HasValue ? End.Value : (input.Length - 1);
            string Temp = "";
            for (int i = Start; i <= _End; i++)
                Temp += input[i] + Seperator;

            Temp = Temp.TrimEnd(Seperator);
            return Temp;
        }


    }

    /// <summary>
    /// This class implements string comparison algorithm
    /// based on character pair similarity
    /// Source: http://www.catalysoft.com/articles/StrikeAMatch.html
    /// </summary>
    public static class SimilarityTool
    {
        /// <summary>
        /// Compares the two strings based on letter pair matches
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns>The percentage match from 0.0 to 1.0 where 1.0 is 100%</returns>
        public static double Similarity(this string str1, string str2)
        {
            List<string> pairs1 = WordLetterPairs(str1.ToUpper());
            List<string> pairs2 = WordLetterPairs(str2.ToUpper());

            int intersection = 0;
            int union = pairs1.Count + pairs2.Count;

            for (int i = 0; i < pairs1.Count; i++)
            {
                for (int j = 0; j < pairs2.Count; j++)
                {
                    if (pairs1[i] == pairs2[j])
                    {
                        intersection++;
                        pairs2.RemoveAt(j);//Must remove the match to prevent "GGGG" from appearing to match "GG" with 100% success

                        break;
                    }
                }
            }

            return (2.0 * intersection) / union;
        }


        private static List<string> WordLetterPairs(string str)
        {
            List<string> AllPairs = new List<string>();

            // Tokenize the string and put the tokens/words into an array
            string[] Words = Regex.Split(str, @"\s");

            // For each word
            for (int w = 0; w < Words.Length; w++)
            {
                if (!string.IsNullOrEmpty(Words[w]))
                {
                    // Find the pairs of characters
                    String[] PairsInWord = LetterPairs(Words[w]);

                    for (int p = 0; p < PairsInWord.Length; p++)
                    {
                        AllPairs.Add(PairsInWord[p]);
                    }
                }
            }

            return AllPairs;
        }


        private static string[] LetterPairs(string str)
        {
            int numPairs = str.Length - 1;

            string[] pairs = new string[numPairs];

            for (int i = 0; i < numPairs; i++)
            {
                pairs[i] = str.Substring(i, 2);
            }

            return pairs;
        }
    }
}
