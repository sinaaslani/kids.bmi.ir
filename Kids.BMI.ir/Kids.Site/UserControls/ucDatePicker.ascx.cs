using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions;

namespace Site.Kids.bmi.ir.UserControls
{
    public partial class ucDatePicker : UserControl
    {

        public string ValidationGroup
        {
            get { return Reqval1.ValidationGroup; }
            set { Reqval1.ValidationGroup = Reqval2.ValidationGroup = Reqval3.ValidationGroup = value; }
        }
        public bool IsRequired
        {
            get { return Reqval1.Enabled; }
            set { Reqval1.Enabled = Reqval2.Enabled = Reqval3.Enabled = value; }
        }

        public int? FromYear { get; set; }
        public int? ToYear { get; set; }

        public bool Enabled
        {
            get { return drpYear.Enabled && drpMonth.Enabled && drpDay.Enabled; }
            set { drpYear.Enabled = drpMonth.Enabled = drpDay.Enabled = value; }
        }

        public bool ShowTime
        {
            get { return pnlTime.Visible; }
            set { pnlTime.Visible = value; }
        }

        public DateTime? SelectedDateTime
        {
            get
            {
                if (drpYear.SelectedValue != "-1" && drpMonth.SelectedValue != "-1" && drpDay.SelectedValue != "-1")
                {
                    PersianDateTime persiandate = new PersianDateTime(drpYear.SelectedValue.ToInt32(),
                                                             drpMonth.SelectedValue.ToInt32(),
                                                             drpDay.SelectedValue.ToInt32(),
                                                             drpHour.SelectedValue.ToInt32(),
                                                             drpMinute.SelectedValue.ToInt32());
                    return PersianDateTime.PersianToMiladi(persiandate);
                }
                return null;
            }
            set
            {
                SetDropDown();
                if (value.HasValue)
                {
                    PersianDateTime p = PersianDateTime.MiladiToPersian(value.Value);
                    drpDay.SelectedValue = p.Day.ToString().PadLeft(2, '0');
                    drpMonth.SelectedValue = p.Month.ToString().PadLeft(2, '0');
                    drpYear.SelectedValue = p.Year.ToString().PadLeft(4, '0');

                    drpHour.SelectedValue = GetTime(p.Hour).ToString();
                    drpMinute.SelectedValue = p.Minute.ToString();
                }
                else
                {
                    drpDay.SelectedValue = "-1";
                    drpMonth.SelectedValue = "-1";
                    drpYear.SelectedValue = "-1";

                    drpHour.SelectedValue = "0";
                    drpMinute.SelectedValue = "0";
                }
            }

        }

        private object GetTime(int p)
        {
            return (p / 5) * 5;
        }

        public PersianDateTime SelectedPersianDateTime
        {
            get
            {
                if (drpYear.SelectedValue != "-1" && drpMonth.SelectedValue != "-1" && drpDay.SelectedValue != "-1")
                {
                    return new PersianDateTime(Convert.ToInt32((string)drpYear.SelectedValue),
                                               Convert.ToInt32((string)drpMonth.SelectedValue),
                                               Convert.ToInt32((string)drpDay.SelectedValue)
                                               );
                }
                return null;
            }
            set
            {
                SetDropDown();
                if (value != null)
                {
                    drpDay.SelectedValue = value.Day.ToString().PadLeft(2, '0');
                    drpMonth.SelectedValue = value.Month.ToString().PadLeft(2, '0');
                    drpYear.SelectedValue = value.Year.ToString().PadLeft(4, '0');
                }
                else
                {
                    drpDay.SelectedValue = "-1";
                    drpMonth.SelectedValue = "-1";
                    drpYear.SelectedValue = "-1";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDropDown();
            }
        }

        private void SetDropDown()
        {
            if (drpYear.Items.Count > 0)
                return;
            int To;
            if (FromYear.HasValue)
            {
                To = ToYear.HasValue ? ToYear.Value : PersianDateTime.Now.Year;
            }
            else
            {
                FromYear = PersianDateTime.Now.Year - 5;
                To = PersianDateTime.Now.Year + 5;
            }
            drpYear.Items.Clear();
            drpYear.Items.Add(new ListItem("-----", "-1"));
            for (int i = FromYear.Value; i <= To; i++)
                drpYear.Items.Add(new ListItem(i.ToString().ToPersinDigit(), i.ToString().PadLeft(4, '0')));

            drpDay.Items.Add(new ListItem("-----", "-1"));
            for (int i = 1; i <= 31; i++)
            {
                drpDay.Items.Add(new ListItem(i.ToString().ToPersinDigit(), i.ToString().PadLeft(2,'0')));
            }


            for (int i = 0; i < 24; i++)
            {
                drpHour.Items.Add(new ListItem(i.ToString().ToPersinDigit(), i.ToString()));
            }


            for (int i = 0; i < 60; i = i + 5)
            {
                drpMinute.Items.Add(new ListItem(i.ToString().ToPersinDigit(), i.ToString()));
            }
        }
    }
}