using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.LoggingHelper;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP.Logging
{
    public partial class ViewLogs : AdminSecureFormBaseClass
    {
        protected override void CheckAdminUser()
        {
            if (OnlineSystemUser != null)
            {
                if (!(OnlineSystemUser.IsSiteAdministrator))
                {
                    Response.Redirect("~/Error/NotAccess.aspx");
                }
            }
            else
                SendToLoginPage();
        }


        private void BindLogs()
        {
            DateTime? FromDate = ucDatePicker1.SelectedDateTime;
            List<ErrorLog> TransactionList = LogUtility.GetLogs(ErroMessage: txtErrorDescription.Text, LogDateTime: FromDate);


            dgErrorList.DataSource = TransactionList;
            dgErrorList.DataBind();

        }


        protected void Search_Click(object sender, EventArgs e)
        {
            try
            {
                BindLogs();
            }
            catch (Exception exp)
            {
                ShowMessageBox(exp);
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            LogUtility.TruncateErrorLogs();
            BindLogs();
        }

        protected void dgErrorList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgErrorList.PageIndex = e.NewPageIndex;
            BindLogs();
        }

    }
}
