using System;
using System.Web.UI;
using Kids.Utility;
using Kids.Utility.UtilExtension.StringExtensions;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.WishAccount
{
    public partial class GanjTunelCalculator : UserControlBaseClass
    {
        public string ValidationGrp { private get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnCalculate.ValidationGroup = ValidationGrp;
            CompareValidator1.ValidationGroup = ValidationGrp;
            RequiredFieldValidator1.ValidationGroup = ValidationGrp;
        }

        protected void btnCalculate_Click(object sender, ImageClickEventArgs e)
        {
            if (txtAmount.Text.IsInt64() && drpDuration.SelectedValue != "0")
            {
                var res = (txtAmount.Text.ToDouble()/drpDuration.SelectedValue.ToInt32());
                lblResult.Text = string.Format("میزان پس انداز در طول سال : {0} ریال ",
                                               Math.Round(res*365).ToString().Money3Dispaly().ToPersinDigit());
            }
            else
            {
                ShowMessageBox("لطفا مقادیر مبلغ و بازه زمانی را وارد نمایید", "");
            }
        }
    }
}