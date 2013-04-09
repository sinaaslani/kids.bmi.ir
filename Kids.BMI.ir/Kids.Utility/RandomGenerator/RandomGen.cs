using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Kids.Utility.RandomGenerator
{
    [Flags]
    public enum CharacterType
    {
        Space = 1,
        Digit = 2,
        UpperCase = 4,
        LowerCase = 8,
        Symbol = 16
    }

    [Flags]
    public enum NameType
    {
        MaleName = 1,
        FemaleName = 2,
        Surname = 4,
        Word = 8
    }

    public abstract class RandomGeneratorBase<T>
    {
        protected readonly Random _random = new Random(int.Parse(
                                                           Guid.NewGuid().ToString().Substring(0, 8),
                                                           System.Globalization.NumberStyles.HexNumber));
        public abstract T GetRandom();
    }

    public class BooleanRandonGen : RandomGeneratorBase<bool>
    {

        public override bool GetRandom()
        {
            return (_random.NextDouble() >= 0.5);
        }

    }

    public class IntegerRandomGenerator : RandomGeneratorBase<int>
    {
        private readonly int _min;
        private readonly int _max;

        public IntegerRandomGenerator(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public override int GetRandom()
        {
            return _random.Next(_min, _max);
        }

    }

    public class DoubleRandomGenerator : RandomGeneratorBase<double>
    {
        private double _min;
        private double _max;

        public DoubleRandomGenerator(double min, double max)
        {
            _min = min;
            _max = max;
        }

        public override double GetRandom()
        {
            return ((_max - _min) * _random.NextDouble()) + _min;
        }


    }

    public class StringRandomGenerator : RandomGeneratorBase<string>
    {
        private char[] _chars;
        private int _min = 0;
        private int _max = 0;
        private bool _padRight = DEFAULT_PAD_RAIGHT;
        private const bool DEFAULT_PAD_RAIGHT = false;

        public StringRandomGenerator(int maxLength, CharacterType charType)
            : this(0, maxLength, charType, DEFAULT_PAD_RAIGHT) { }

        public StringRandomGenerator(int maxLength, CharacterType charType, bool padRight)
            : this(0, maxLength, charType, padRight) { }

        public StringRandomGenerator(int minLength, int maxLength, CharacterType charType)
            : this(minLength, maxLength, charType, DEFAULT_PAD_RAIGHT) { }

        public StringRandomGenerator(int minLength, int maxLength, CharacterType charType, bool padRight)
        {
            _min = minLength;
            _max = maxLength;
            _chars = RandomGeneratorHelper.GetChars(charType);
            _padRight = padRight;
        }

        public override string GetRandom()
        {
            int n = 0;
            if (_min == _max)
            {
                n = _max;
            }
            else
            {
                n = GetRandomLength();
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= n - 1; i++)
            {
                sb.Append(GetRandomChar());
            }
            if (_padRight)
            {
                return sb.ToString().PadRight(_max);
            }
            else
            {
                return sb.ToString();
            }
        }

        private int GetRandomLength()
        {
            return RandomGeneratorHelper.GetRandomInteger(_min, _max + 1);
        }

        private char GetRandomChar()
        {
            int rnd = RandomGeneratorHelper.GetRandomInteger(0, _chars.GetUpperBound(0) + 1);
            return _chars[rnd];
        }

    }

    public class RandomGeneratorHelper
    {
        private static Random _random = new Random(System.DateTime.Now.Millisecond * System.DateTime.Now.Second);

        public static int GetRandomInteger()
        {
            return GetRandomInteger(true);
        }

        public static int GetRandomInteger(bool onlyPositive)
        {
            if (onlyPositive)
            {
                return GetRandomInteger(0, int.MaxValue);
            }
            else
            {
                return GetRandomInteger(int.MinValue, int.MaxValue);
            }
        }

        public static int GetRandomInteger(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static char[] GetChars(CharacterType cht)
        {
            char[] ch = null;
            if ((cht & CharacterType.Digit) > 0)
            {
                ch = ReziseAndAppendCharArray(ch, GetDigits());
            }
            if ((cht & CharacterType.UpperCase) > 0)
            {
                ch = ReziseAndAppendCharArray(ch, GetUpperCase());
            }
            if ((cht & CharacterType.LowerCase) > 0)
            {
                ch = ReziseAndAppendCharArray(ch, GetLowerCase());
            }
            if ((cht & CharacterType.Space) > 0)
            {
                char[] spc = { ' ' };
                ch = ReziseAndAppendCharArray(ch, spc);
            }
            if ((cht & CharacterType.Symbol) > 0)
            {
                char[] spc = GetChars("!-/*");
                ch = ReziseAndAppendCharArray(ch, spc);
            }
            return ch;
        }

        private static char[] ReziseAndAppendCharArray(char[] mainArray, char[] toBeCopiedArray)
        {
            if (mainArray == null)
            {
                mainArray = toBeCopiedArray;
            }
            else
            {
                int oldLength = mainArray.Length;
                char[] newArray = new char[oldLength + toBeCopiedArray.Length];
                Array.Copy(mainArray, 0, newArray, 0, oldLength);
                Array.Copy(toBeCopiedArray, 0, newArray, oldLength, toBeCopiedArray.Length);
                mainArray = newArray;
            }
            return mainArray;
        }

        private static char[] GetChars(string charRange)
        {
            string[] ss = charRange.Split('-');
            char ch1 = ss[0][0];
            char ch2 = ss[1][0];
            char[] ch = new char[Convert.ToInt32(ch2) - Convert.ToInt32(ch1) + 1];
            for (int i = Convert.ToInt32(ch1); i <= Convert.ToInt32(ch2); i++)
            {
                ch[i - Convert.ToInt32(ch1)] = Convert.ToChar(i);
            }
            return ch;
        }

        private static char[] GetDigits()
        {
            char[] ch = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            return ch;
        }

        private static char[] GetUpperCase()
        {
            char[] ch = new char[26];
            int A_ASC_VALUE = Convert.ToInt32('A');
            for (int i = A_ASC_VALUE; i < A_ASC_VALUE + 26; i++)
            {
                ch[i - A_ASC_VALUE] = Convert.ToChar(i);
            }
            return ch;
        }

        private static char[] GetLowerCase()
        {
            char[] ch = new char[26];
            int A_ASC_VALUE = Convert.ToInt32('a');
            for (int i = A_ASC_VALUE; i < A_ASC_VALUE + 26; i++)
            {
                ch[i - A_ASC_VALUE] = Convert.ToChar(i);
            }
            return ch;
        }
    }

    public class ListRandomGenerator<T> : RandomGeneratorBase<T>
    {

        private IList<T> _list = new List<T>();
        private bool _unique = false;

        public ListRandomGenerator(IList<T> list) : this(list, false, false) { }

        public ListRandomGenerator(IList<T> list, bool unique, bool makeLocalCopyOfList)
        {

            _unique = unique;

            if (unique && makeLocalCopyOfList)
            {
                // make a copy 
                _list = new List<T>();
                foreach (T o in list)
                    _list.Add(o);
            }
            else
                _list = list;

        }

        public override T GetRandom()
        {
            if (_unique)
            {
                if (_list.Count == 0)
                    throw new InvalidOperationException("The list is exhausted. No more unique items could be returned.");
                int index = base._random.Next(0, _list.Count);
                T o = _list[index];
                _list.RemoveAt(index);
                return o;
            }
            else
                return _list[base._random.Next(0, _list.Count)];
        }
    }

    public class PrioritisedListRandomGenerator<T> : RandomGeneratorBase<T>
    {
        private List<T> _list = new List<T>();

        public PrioritisedListRandomGenerator(IList<T> items, int maxScore)
        {
            foreach (T item in items)
            {
                for (int i = 0; i < _random.Next(1, maxScore); i++)
                    _list.Add(item);
            }
        }

        public PrioritisedListRandomGenerator(Dictionary<T, int> itemAsKeyIntegerScoreAsValue)
        {

            foreach (T key in itemAsKeyIntegerScoreAsValue.Keys)
            {
                int count = itemAsKeyIntegerScoreAsValue[key];
                for (int i = 0; i < count; i++)
                    _list.Add(key);
            }
        }

        public override T GetRandom()
        {
            return _list[base._random.Next(0, _list.Count)];
        }
    }

    public class DateRandomGenerator : RandomGeneratorBase<DateTime>
    {

        private long _minDate = DateTime.MinValue.Ticks;
        private long _maxDate = DateTime.Now.Ticks;

        public DateRandomGenerator() : this(DateTime.MinValue, DateTime.Now) { }

        public DateRandomGenerator(DateTime minDate) : this(minDate, DateTime.Now) { }

        public DateRandomGenerator(DateTime minDate, DateTime maxDate)
        {
            _minDate = minDate.Ticks;
            _maxDate = maxDate.Ticks;
        }

        public override DateTime GetRandom()
        {
            return new DateTime((long)((_random.NextDouble() * (_maxDate - _minDate)) + _minDate));
        }

    }

    public class NameRandomGenerator : RandomGeneratorBase<string>
    {
        private IList<string> _list;

        public NameRandomGenerator(NameType type)
        {
            _list = NameCache.GetList(type);
        }

        public override string GetRandom()
        {
            return _list[base._random.Next(0, _list.Count)];
        }


        private class NameCache
        {
            private static Dictionary<NameType, List<string>> _nameLists = new Dictionary<NameType, List<string>>();
            private static List<string> _maleNames = new List<string>();
            private static List<string> _femaleNames = new List<string>();
            private static List<string> _surnames = new List<string>();
            private static List<string> _words = new List<string>();


            public static List<string> GetList(NameType type)
            {
                if (!_nameLists.ContainsKey(type))
                {
                    _nameLists.Add(type, GetMixedNameList(type));
                }
                return (List<string>)_nameLists[type];

            }

            private static List<string> GetMixedNameList(NameType type)
            {
                List<string> al = new List<string>();
                if ((type & NameType.MaleName) == NameType.MaleName)
                {
                    FillListFromList(al, MaleNames);
                }
                if ((type & NameType.FemaleName) == NameType.FemaleName)
                {
                    FillListFromList(al, FemaleNames);
                }
                if ((type & NameType.Surname) == NameType.Surname)
                {
                    FillListFromList(al, Surnames);
                }
                if ((type & NameType.Word) == NameType.Word)
                {
                    FillListFromList(al, Words);
                }

                return al;
            }

            public static List<string> MaleNames
            {
                get
                {
                    if (_maleNames.Count == 0)
                    {
                        FillList(_maleNames, NameType.MaleName);
                    }
                    return _maleNames;
                }
            }

            public static List<string> FemaleNames
            {
                get
                {
                    if (_femaleNames.Count == 0)
                    {
                        FillList(_femaleNames, NameType.FemaleName);
                    }
                    return _femaleNames;
                }
            }

            public static List<string> Surnames
            {
                get
                {
                    if (_surnames.Count == 0)
                    {
                        FillList(_surnames, NameType.Surname);
                    }
                    return _surnames;
                }
            }

            public static List<string> Words
            {
                get
                {
                    if (_words.Count == 0)
                    {
                        FillList(_words, NameType.Word);
                    }
                    return _words;
                }
            }

            public static void FillList(List<string> list, NameType type)
            {
                switch (type)
                {
                    case NameType.MaleName:
                        FillListFromResource(list, "RandomGenerationFramework.MaleName.txt", false);
                        break;
                    case NameType.FemaleName:
                        FillListFromResource(list, "RandomGenerationFramework.FemaleName.txt", false);
                        break;
                    case NameType.Surname:
                        FillListFromResource(list, "RandomGenerationFramework.Surname.txt", false);
                        break;
                    case NameType.Word:
                        FillListFromResource(list, "RandomGenerationFramework.Word.txt", false);
                        break;

                }
            }

            private static void FillListFromResource(ICollection<string> list, string resourceName, bool truncate)
            {
                Assembly ass = Assembly.GetExecutingAssembly();
                StreamReader sr = new StreamReader(ass.GetManifestResourceStream(resourceName));
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (truncate)
                        line = line.Split(' ')[0];
                    list.Add(line);
                }
                sr.Close();
            }

            private static void FillListFromList(ICollection<string> destination, IEnumerable<string> source)
            {
                foreach (string s in source)
                    destination.Add(s);
            }

        }


    }

    public class EnumRandomGenerator<T> : ListRandomGenerator<T>
    {

        public EnumRandomGenerator()
            : base(new List<T>((IList<T>)Enum.GetValues(typeof(T))))
        {

        }


    }
}