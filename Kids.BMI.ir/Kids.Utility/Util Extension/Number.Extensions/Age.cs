
namespace Kids.Utility.UtilExtension.NumberExtensions
{
    public struct Age
    {
        public Age(int years,int months,int days)
        {
            Years = years;
            Months = months;
            Days = days;
        }
        public int Years;
        public int Months;
        public int Days;

        public override string ToString()
        {
            if (Years == 0 && Months == 0 && Days != 0)
                return string.Format("{0} Days", Days);
            if (Years == 0 && Months != 0 && Days == 0)
                return string.Format("{0} Months", Months);
            if (Years == 0 && Months != 0 && Days != 0)
                return string.Format("{0} Months, {1} Days", Months, Days);

            if (Years != 0 && Months != 0 && Days != 0)
                return string.Format("{0} Years, {1} Months, {2} Days", Years, Months, Days);

            if (Years != 0 && Months == 0 && Days != 0)
                return string.Format("{0} Years, {1} Days",Years, Days);            
            if (Years != 0 && Months != 0 && Days == 0)
                return string.Format("{0} Years, {1} Months",Years, Months);
            return string.Format("{0} Years, {1} Months, {2} Days", Years, Months, Days);
        }
    }
}
