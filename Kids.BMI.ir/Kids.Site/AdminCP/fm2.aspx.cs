using System;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.AdminCP
{
    public partial class fm2 : AdminSecureFormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string FromDate = new PersianDateTime(TextBox2.Text).ToString();
            string ToDate = new PersianDateTime(TextBox3.Text).ToString();

            var acclist = BMICustomer_DataProvider.GetAccBill(TextBox1.Text, FromDate, ToDate);

            GridView1.DataSource = acclist;
            GridView1.DataBind();

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            var lst2 = BMICustomer_DataProvider.GetAccByMellicode(TextBox4.Text);
            GridView1.DataSource = lst2;
            GridView1.DataBind();

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            var lst3 = BMICustomer_DataProvider.GetCustInfoByMelliCode(TextBox5.Text);
            GridView1.DataSource = lst3;
            GridView1.DataBind();

        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            string FromDate = new PersianDateTime(TextBox8.Text).ToString();
            string ToDate = new PersianDateTime(TextBox9.Text).ToString();
            var lst5 = BMICustomer_DataProvider.GetLoanBill(TextBox6.Text, FromDate, ToDate, TextBox10.Text.ToInt32());
            GridView1.DataSource = lst5;
            GridView1.DataBind();
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            var lst4 = BMICustomer_DataProvider.GetLoanAccByCUID(MelliCode: TextBox7.Text);
            GridView1.DataSource = lst4;
            GridView1.DataBind();
        }

        protected override void CheckAdminUser()
        {

        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            var lst4 = BMICustomer_DataProvider.GetCustInfoByCuid(TextBox11.Text);
            GridView1.DataSource = lst4;
            GridView1.DataBind();
        }
    }
}