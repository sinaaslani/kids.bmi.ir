
public static class num2char
{
    private static readonly string[] yekan = new [] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
    private static readonly string[] dahgan = new [] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
    private static readonly string[] dahyek = new [] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
    //array[10..19]
    private static readonly string[] sadgan = new [] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
    private static readonly string[] basex = new [] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };


    private static string getnum3(int num3)
    {
        string s = "";
        int d12 = num3 % 100;
        int d3 = num3 / 100;
        if (d3 != 0)
            s = sadgan[d3] + " و ";
        if ((d12 >= 10) && (d12 <= 19))
        {
            s = s + dahyek[d12 - 10];
        }
        else
        {
            int d2 = d12 / 10;
            if (d2 != 0)
                s = s + dahgan[d2] + " و ";
            int d1 = d12 % 10;
            if (d1 != 0)
                s = s + yekan[d1] + " و ";
            s = s.Substring(0, s.Length - 3);
        }
        return s;
    }

    public static string num2str(this string snum)
    {
        string stotal = "";
        if (snum == "0")
        {
            return yekan[0];
        }
        else
        {
            snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
            int L = snum.Length / 3 - 1;
            for (int i = 0; i <= L; i++)
            {
                int b = int.Parse(snum.Substring(i * 3, 3));
                if (b != 0)
                    stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
            }
            stotal = stotal.Substring(0, stotal.Length - 3);
        }
        return stotal;
    }
}
