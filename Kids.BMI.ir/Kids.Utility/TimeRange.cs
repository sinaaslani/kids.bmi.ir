using System;

namespace Kids.Utility
{
    public static class DateTimeExtend
    {
        public static Time ToTime(this DateTime Inpute)
        {
            return new Time(Inpute.Hour, Inpute.Minute, Inpute.Second);
        }
    }

    [Serializable]
    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }


        public Time(int Hour, int Minute, int Second)
        {
            this.Hour = Hour;
            this.Minute = Minute;
            this.Second = Second;
        }

        public Time(string time)
        {
            string[] Arr_time = time.Split(':');
            Hour = Convert.ToInt16(Arr_time[0]);
            Minute = Convert.ToInt16(Arr_time[1]);
            if (Arr_time.Length == 3)
                Second = Convert.ToInt16(Arr_time[2]);
        }

        public static bool operator ==(Time d1, Time d2)
        {
            return d1.Hour == d2.Hour && d1.Minute == d2.Minute && d1.Second == d2.Second;
        }

        public static bool operator !=(Time d1, Time d2)
        {
            return !(d1 == d2);
        }

        public static bool operator >(Time d1, Time d2)
        {
            if (d1.Hour > d2.Hour)
                return true;

            if (d1.Hour == d2.Hour && d1.Minute > d2.Minute)
                return true;

            if (d1.Hour == d2.Hour && d1.Minute > d2.Minute && d1.Second > d2.Second)
                return true;

            return false;
        }

        public static bool operator <(Time d1, Time d2)
        {
            if (d1 == d2)
                return false;
            return !(d1 > d2);
        }

        public static bool operator >=(Time d1, Time d2)
        {
            return d1 > d2 || d1 == d2;
        }

        public static bool operator <=(Time d1, Time d2)
        {
            return d1 < d2 || d1 == d2;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Hour.ToString().PadLeft(2, '0'), Minute.ToString().PadLeft(2, '0'));
        }

        private bool Equals(Time other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Hour == Hour && other.Minute == Minute && other.Second == Second;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Time)) return false;
            return Equals((Time)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Hour;
                result = (result * 397) ^ Minute;
                result = (result * 397) ^ Second;
                return result;
            }
        }
    }

    [Serializable]
    public class TimeRange
    {
        public Time Begin { get; private set; }
        public Time End { get; private set; }

        public TimeRange(string TimeRange)
        {
            try
            {
                string b = TimeRange.Split('-')[0];
                string e = TimeRange.Split('-')[1];

                Begin = new Time(b);
                End = new Time(e);
            }
            catch
            {
                throw new FormatException("Invalid Time Format.Valid Format Is HH1:mm1:ss1-HH2:mm2:ss2");
            }
        }

        public TimeRange(Time begin, Time end)
        {
            Begin = begin;
            End = end;
        }

        private bool IsIn(Time Input)
        {
            if (Begin == End) return false;
            return Input >= Begin && Input <= End;
        }
        public bool IsIn(DateTime Input)
        {
            if (Begin == End) return false;
            return IsIn(Input.ToTime());
        }

        public override string ToString()
        {
            return Begin + " تا " + End;
        }
    }
}