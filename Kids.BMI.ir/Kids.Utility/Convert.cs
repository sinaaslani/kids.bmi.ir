using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Kids.Utility.UtilExtension.StringExtensions;

namespace Kids.Utility
{
    public static class ConvertUtil
    {

        public static bool IsGuid(object numString)
        {
            if (numString == null)
                return false;
            Guid Result;
            return Guid.TryParse(numString.ToString(), out Result);
        }

        public static bool IsGuid(this string numString)
        {
            if (numString == null)
                return false;
            Guid Result;
            return Guid.TryParse(numString, out Result);
        }

        public static bool IsInt32(object numString)
        {
            if (numString == null)
                return false;
            Int32 Result;
            return Int32.TryParse(numString.ToString(), out Result);
        }

        public static bool IsColor(this string colorString)
        {
            if (colorString == null)
                return false;


            if (colorString.IsHexDigit())
            {
                if (!colorString.StartsWith("#"))
                    colorString = string.Format("#{0}", colorString);
            try
            {
                    var c = ColorTranslator.FromHtml(colorString);
                return true;
            }
            catch
            {
                    var c = Color.FromName(colorString);
                    return c.IsKnownColor;
            }
        }
            else
            {
                var c = Color.FromName(colorString);
                return c.IsKnownColor;
            }


        }

        public static Color ToColor(this string colorString)
            {
            if (colorString.IsHexDigit())
                {
                    if (!colorString.StartsWith("#"))
                        colorString = string.Format("#{0}", colorString);
                    return ColorTranslator.FromHtml(colorString);
                }
            return Color.FromName(colorString);

        }

        public static bool IsInt32(this string numString)
        {
            if (numString == null)
                return false;
            Int32 Result;
            return Int32.TryParse(numString, out Result);
        }

        public static bool IsInt64(this string numString)
        {
            if (numString == null)
                return false;
            Int64 Result;
            return Int64.TryParse(numString, out Result);
        }

        public static bool IsInt64(object numString)
        {
            if (numString == null)
                return false;
            Int64 Result;
            return Int64.TryParse(numString.ToString(), out Result);
        }
        public static string GetEnumName<T>(this T _enum)
        {
            return Enum.GetName(typeof(T), _enum);
        }

        public static bool IsDecimal(this string numString)
        {
            if (numString == null)
                return false;
            Decimal Result;
            return Decimal.TryParse(numString, out Result);
        }

       


        public static Decimal ToDecimal(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Decimal");

            return Convert.ToDecimal(val);
        }
        public static Double ToDouble(this object val)
        {
            if (val == null)
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Double");

            return Convert.ToDouble(val);
        }
        public static Double ToDouble(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Double");

            return Convert.ToDouble(val);
        }

        public static Int64 ToLong(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Int64");

            return Convert.ToInt64(val);

        }
        public static Int64 ToLong(this object val)
        {
            if (val == null)
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Int64");

            return Convert.ToInt64(val);
        }

        public static Int32 ToInt32(this object val)
        {
            if (val == null)
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Int32");

            return Convert.ToInt32(val);
        }
        public static Int32 ToInt32(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Int32");

            return Convert.ToInt32(val);
        }


        public static Guid ToGuid(this object val)
        {
            if (val == null)
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Int32");

            return Guid.Parse(val.ToString());
        }
        public static Guid ToGuid(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Int32");

            return Guid.Parse(val);
        }

        public static bool ToBool(this string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentOutOfRangeException("Value Null Can not converted To Boolean");

            return Convert.ToBoolean(val);
        }


        public static T ToEnumByValue<T>(this string EnumValue)
        {
            if (string.IsNullOrWhiteSpace(EnumValue))
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Enum");

            Int32 val = Convert.ToInt32(EnumValue);

            if (!Enum.IsDefined(typeof(T), val))
                throw new ArgumentException("Invalid Enum Value=" + val);

            return (T)((object)val);
        }
        public static T? ToEnumByName<T>(this string EnumName) where T : struct
        {
            if (string.IsNullOrWhiteSpace(EnumName))
                throw new ArgumentOutOfRangeException("Value Null Can not converted to Enum");

            T Res;
            if (Enum.TryParse(EnumName, true, out Res))
            return Res;
            return null;

        }


        public static string ConvertToHexString(IEnumerable<char> Block)
        {
            string Res = "";
            foreach (char c in Block)
                Res += ((int)c).ToString("X").PadLeft(2, '0');

            return Res;
        }


        public static string ListToCommaSepStr(DataRowCollection dataRowCollection, string ColumnName)
        {
            string Temp = "";
            foreach (DataRow Row in dataRowCollection)
                Temp += String.Format("{0},", Row[ColumnName]);

            return Temp.TrimEnd(",".ToCharArray());
        }

        public static DataTable ConvertToDataTable<T>(IEnumerable<T> lst)
        {
            DataTable tbl = CreateTable<T>();
            Type entType = typeof(T);

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);

            foreach (T item in lst)
            {
                DataRow row = tbl.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (tbl.Columns.Contains(prop.Name))
                        row[prop.Name] = prop.GetValue(item);
                }
                tbl.Rows.Add(row);
            }

            return tbl;
        }

        private static DataTable CreateTable<T>()
        {
            Type entType = typeof(T);
            DataTable tbl = new DataTable(entType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            foreach (PropertyDescriptor prop in properties)
            {
                try
                {
                    tbl.Columns.Add(prop.Name, prop.PropertyType);
                }
                catch (NotSupportedException) { }
            }
            return tbl;
        }

        public static char ConvertIranSysToWin(Char c)
        {
            char result;

            switch ((int)c)
            {
                case 128:
                    result = '0';
                    break;
                case 129:
                    result = '1';
                    break;
                case 130:
                    result = '2';
                    break;
                case 131:
                    result = '3';
                    break;
                case 132:
                    result = '4';
                    break;
                case 133:
                    result = '5';
                    break;
                case 134:
                    result = '6';
                    break;
                case 135:
                    result = '7';
                    break;
                case 136:
                    result = '8';
                    break;
                case 137:
                    result = '9';
                    break;
                case 141:
                    result = 'آ';
                    break;
                case 142:
                    result = 'ئ';
                    break;
                case 143:
                    result = 'ء';
                    break;
                case 144:
                case 145:
                    result = 'ا';
                    break;
                case 146:
                case 147:
                    result = 'ب';
                    break;
                case 148:
                case 149:
                    result = 'پ';
                    break;
                case 150:
                case 151:
                    result = 'ت';
                    break;
                case 152:
                case 153:
                    result = 'ث';
                    break;
                case 154:
                case 155:
                    result = 'ج';
                    break;
                case 156:
                case 157:
                    result = 'چ';
                    break;
                case 158:
                case 159:
                    result = 'ح';
                    break;
                case 160:
                case 161:
                    result = 'خ';
                    break;
                case 162:
                    result = 'د';
                    break;
                case 163:
                    result = 'ذ';
                    break;
                case 164:
                    result = 'ر';
                    break;
                case 165:
                    result = 'ز';
                    break;
                case 166:
                    result = 'ژ';
                    break;
                case 167:
                case 168:
                    result = 'س';
                    break;
                case 169:
                case 170:
                    result = 'ش';
                    break;
                case 171:
                case 172:
                    result = 'ص';
                    break;
                case 173:
                case 174:
                    result = 'ض';
                    break;
                case 175:
                    result = 'ط';
                    break;
                case 224:
                    result = 'ظ';
                    break;
                case 225:
                case 226:
                case 227:
                case 228:
                    result = 'ع';
                    break;
                case 229:
                case 230:
                case 231:
                case 232:
                    result = 'غ';
                    break;
                case 233:
                case 234:
                    result = 'ف';
                    break;
                case 235:
                case 236:
                    result = 'ق';
                    break;
                case 237:
                case 238:
                    result = 'ک';
                    break;
                case 239:
                case 240:
                    result = 'گ';
                    break;
                case 241:
                case 243:
                    result = 'ل';
                    break;

                case 244:
                case 245:
                    result = 'م';
                    break;
                case 246:
                case 247:
                    result = 'ن';
                    break;
                case 248:
                    result = 'و';
                    break;
                case 249:
                case 250:
                case 251:
                    result = 'ه';
                    break;
                case 252:
                case 253:
                case 254:
                    result = 'ي';
                    break;
                case 255:
                    result = ' ';
                    break;
                case 176:
                    result = ' ';
                    break;
                default:
                    result = c;
                    break;

            }
            return result;
        }

        public static long DateTimeToTimeStamp(DateTime dt)
        {
            TimeSpan Now = dt.ToUniversalTime().Subtract(new DateTime(1970, 1, 1));
            long TimeStamp = Convert.ToInt64(Math.Floor(Now.TotalMilliseconds));
            return TimeStamp;
        }

        public static DateTime TimestampToDateTime(string TimeStamp)
        {
            if (IsInt64(TimeStamp))
            {
                DateTime UtcNow = new DateTime(1970, 1, 1).AddMilliseconds(Convert.ToInt64(TimeStamp));
                return UtcNow;
            }
            return DateTime.MinValue;
        }
    }
}
