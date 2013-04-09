using System;

namespace Kids.Utility.UtilExtension.NumberExtensions
{
    public abstract class TimeSpanSelector
    {
        protected TimeSpan myTimeSpan;

        internal int ReferenceValue
        {
            set { myTimeSpan = MyTimeSpan(value); }
        }

        public DateTime Ago { get { return DateTime.Now - myTimeSpan; } }
        public DateTime FromNow { get { return DateTime.Now + myTimeSpan; } }
        public DateTime AgoSince(DateTime dt) { return dt - myTimeSpan; }
        public DateTime From(DateTime dt) { return dt + myTimeSpan; }
        protected abstract TimeSpan MyTimeSpan(int refValue);

    }

    internal class WeekSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(7 * refValue, 0, 0, 0);
        }
    }

    internal class DaysSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(refValue, 0, 0, 0);
        }
    }

    internal class YearsSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(365 * refValue, 0, 0, 0);
        }
    }
    internal class HourSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(0, refValue, 0, 0);
        }
    }
    internal class MinuteSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(0, 0, refValue, 0);
        }
    }
    internal class SecondSelector : TimeSpanSelector
    {
        protected override TimeSpan MyTimeSpan(int refValue)
        {
            return new TimeSpan(0, 0, 0,refValue);
        }
    }
}
