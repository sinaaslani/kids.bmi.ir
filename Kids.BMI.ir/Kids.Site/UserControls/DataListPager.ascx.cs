using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kids.Utility;

namespace Site.Kids.bmi.ir.UserControls
{
    public partial class DataListPager : UserControl
    {
        private const int NumericPageLength = 10;

        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                    return 0;
                return ViewState["CurrentPage"].ToInt32();
            }
            set { ViewState["CurrentPage"] = value; }
        }

        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                    return 0;
                return ViewState["PageCount"].ToInt32();
            }
            set { ViewState["PageCount"] = value; }
        }


        public BindGrid BindGridAction { private get; set; }

        public delegate void BindGrid(int CurrentPage, out int PageCount);

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            CurrentPage = CalcCurrentPage(e.CommandName, e.CommandArgument);

            int _PageCount;
            BindGridAction(CurrentPage, out _PageCount);
            PageCount = _PageCount;


            var start = Math.Max(1, (CurrentPage / NumericPageLength) * NumericPageLength);
            if (start == PageCount)
                start = ((PageCount / NumericPageLength) - 1) * NumericPageLength;

            var end = Math.Min(PageCount, ((CurrentPage / NumericPageLength) * NumericPageLength) + NumericPageLength - 1);
            BindPager(start, end);

        }

        private void BindPager(int start, int end)
        {
            List<String> b = new List<string>();

            for (int i = start; i <= end; i++)
                b.Add(i.ToString());

            if (start > 1)
            {
                b.Insert(0, "...");
                b.Insert(0, "ابتدا");
            }

            if (end < PageCount)
            {
                b.Add("...");
                b.Add("انتها");
            }
            lstPager.DataSource = b;
            lstPager.DataBind();
        }

        private int CalcCurrentPage(string CommandName, object CommandArgument)
        {
            switch (CommandName)
            {
                case "NumericPage":
                    return CommandArgument.ToInt32();
                case "...Next":
                    return (((CurrentPage / NumericPageLength) + 1) * NumericPageLength);
                case "...Prev":
                    return (((CurrentPage / NumericPageLength)) * NumericPageLength) - 1;
                case ">>First":
                    return 1;
                case "<<Last":
                    return PageCount;
                default:
                    throw new ArgumentOutOfRangeException(CommandName);
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var btn = e.Item.FindControl("lnkPager") as LinkButton;
                var item = e.Item.DataItem.ToString();
                var itemindex = e.Item.ItemIndex;
                if (item.IsInt32())
                {
                    btn.CommandArgument = item;
                    btn.CommandName = "NumericPage";
                    btn.Text = item;
                    if (item == CurrentPage.ToString())
                        btn.Enabled = false;
                }
                else if (item == "..." & itemindex == (lstPager.DataSource as List<String>).Count - 2)
                {
                    btn.CommandArgument = item;
                    btn.CommandName = "...Next";
                    btn.Text = "...";
                }
                else if (item == "..." & itemindex == 1)
                {
                    btn.CommandArgument = item;
                    btn.CommandName = "...Prev";
                    btn.Text = "...";
                }
                else if (item == "ابتدا")
                {
                    btn.CommandArgument = item;
                    btn.CommandName = ">>First";
                    btn.Text = "ابتدا";
                }
                else if (item == "انتها")
                {
                    btn.CommandArgument = item;
                    btn.CommandName = "<<Last";
                    btn.Text = "انتها";
                }
            }
        }

        internal void InitialPager()
        {
            int _PageCount;
            BindGridAction(1, out _PageCount);
            PageCount = _PageCount;
            CurrentPage = 1;
            BindPager(1, Math.Min(PageCount, NumericPageLength - 1));
        }
    }
}