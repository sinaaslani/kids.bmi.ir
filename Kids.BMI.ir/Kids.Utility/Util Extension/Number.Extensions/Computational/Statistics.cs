using System;
using System.Collections.Generic;
using System.Linq;
using Kids.Utility.UtilExtension.NumberExtensions.Collections;


namespace NumberExtensions.Computation.Statistics
{
    public static class Statistics
    {
        public static double StandardDeviation(this IEnumerable<int> val)
        {
            double sumOfSqrs = 0;
            double avg = val.Average();
            foreach (var item in val)
            {
                sumOfSqrs += Math.Pow((item - avg), 2);
            }
            double n = val.Count();
            return Math.Sqrt(sumOfSqrs / (n - 1));
        }
        public static double StandardDeviation(this IEnumerable<decimal> val)
        {
            double sumOfSqrs = 0;
            decimal avg = val.Average();
            foreach (var item in val)
            {
                sumOfSqrs += Math.Pow((double)(item - avg), 2);
            }
            double n = (double)val.Count();
            return Math.Sqrt(sumOfSqrs / (n - 1));
        }
        public static double StandardDeviation(this IEnumerable<double> val)
        {
            double sumOfSqrs = 0;
            double avg = val.Average();
            foreach (var item in val)
            {
                sumOfSqrs += Math.Pow(((double)item - avg), 2);
            }
            double n = (double)val.Count();
            return Math.Sqrt(sumOfSqrs / (n - 1));
        }
        public static double StandardDeviation(this IEnumerable<float> val)
        {
            double sumOfSqrs = 0;
            double avg = val.Average();
            foreach (var item in val)
            {
                sumOfSqrs += Math.Pow(((double)item - avg), 2);
            }
            double n = (double)val.Count();
            return Math.Sqrt(sumOfSqrs / (n - 1));
        }

        public static double Median(this IEnumerable<int> val)
        {
            IList<int> sortedList = val.Sort(SortMethod.Bubble).ToList();

            double median = 0;
            int middleValue = 0;
            decimal half = 0;

            half = (sortedList.Count - 1) / 2;
            if (sortedList.Count % 2 == 1)
            {
                median = sortedList[Convert.ToInt32(Math.Ceiling(half))];
            }
            else
            {
                middleValue = Convert.ToInt32(Math.Floor(half));
                median = (double)(sortedList[middleValue] + sortedList[middleValue + 1]) / 2;
            }
            return median;
        }
        public static double Median(this IEnumerable<long> val)
        {
            IList<long> sortedList = val.Sort(SortMethod.Bubble).ToList();

            double median = 0;
            int middleValue = 0;
            decimal half = 0;

            half = (sortedList.Count - 1) / 2;
            if (sortedList.Count % 2 == 1)
            {
                median = sortedList[Convert.ToInt32(Math.Ceiling(half))];
            }
            else
            {
                middleValue = Convert.ToInt32(Math.Floor(half));
                median = (double)(sortedList[middleValue] + sortedList[middleValue + 1]) / 2;
            }
            return median;
        }
        public static decimal Median(this IEnumerable<decimal> val)
        {
            IList<decimal> sortedList = val.Sort(SortMethod.Bubble).ToList();

            decimal median = 0;
            int middleValue = 0;
            decimal half = 0;

            half = (sortedList.Count - 1) / 2;
            if (sortedList.Count % 2 == 1)
            {
                median = sortedList[Convert.ToInt32(Math.Ceiling(half))];
            }
            else
            {
                middleValue = Convert.ToInt32(Math.Floor(half));
                median = (sortedList[middleValue] + sortedList[middleValue + 1]) / 2;
            }
            return median;
        }
        public static float Median(this IEnumerable<float> val)
        {
            IList<float> sortedList = val.Sort(SortMethod.Bubble).ToList();

            float median = 0;
            int middleValue = 0;
            decimal half = 0;

            half = (sortedList.Count - 1) / 2;
            if (sortedList.Count % 2 == 1)
            {
                median = sortedList[Convert.ToInt32(Math.Ceiling(half))];
            }
            else
            {
                middleValue = Convert.ToInt32(Math.Floor(half));
                median = (sortedList[middleValue] + sortedList[middleValue + 1]) / 2;
            }
            return median;
        }

        public static float Mode(this IEnumerable<int> val)
        {
            throw new NotImplementedException();
        }
        public static float Mode(this IEnumerable<long> val)
        {
            throw new NotImplementedException();
        }
        public static float Mode(this IEnumerable<decimal> val)
        {
            throw new NotImplementedException();
        }
        public static float Mode(this IEnumerable<float> val)
        {
            throw new NotImplementedException();
        }
    }
}
