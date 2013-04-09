using System;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class AccBill : UserControlBaseClass
    {
        public string AccountNumber { private get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblAccNumber.Text = AccountNumber;
            if (AccountNumber == null)
            {
                Visible = false;
                return;
            }
            if (!IsPostBack)
            {
                ucFromDate.SelectedPersianDateTime = PersianDateTime.Now.AddDays(-PersianDateTime.Now.Day);
                ucToDate.SelectedPersianDateTime = PersianDateTime.Now;

                if (Visible)
                    ViewAccList();

            }
        }

        protected void btnGetAccList_Click(object sender, EventArgs e)
        {
            dgAccList.PageIndex = 0;
            ViewAccList();
        }

        private void ViewAccList()
        {
            string FromDate = ucFromDate.SelectedDateTime.HasValue
                                  ? ucFromDate.SelectedPersianDateTime.ToString()
                                  : PersianDateTime.Now.AddDays(-PersianDateTime.Now.Day).ToString();

            string ToDate = ucToDate.SelectedDateTime.HasValue
                                ? ucToDate.SelectedPersianDateTime.ToString()
                                : PersianDateTime.Now.ToString();

            var acclist = BMICustomer_DataProvider.GetAccBill(AccountNumber, FromDate, ToDate);

            dgAccList.DataSource = acclist;
            dgAccList.DataBind();

        }

        protected void dgAccList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRowId = e.Row.FindControl("lblRowId") as Label;

                var start = (dgAccList.PageIndex * dgAccList.PageSize);
                lblRowId.Text = (start + (e.Row.RowIndex + 1)).ToString().ToPersinDigit();
            }
        }

        protected void dgAccList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgAccList.PageIndex = e.NewPageIndex;
            ViewAccList();
        }


    }
}