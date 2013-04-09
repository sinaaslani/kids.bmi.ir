using System;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Scores
{
    public partial class PowerKidsUserWidget : UserControlBaseClass
    {
        //public bool EnablePaging { get; set; }
        //public bool ShowContinue { get; set; }
        public int PageSize { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            //lnkOtherNewUser.Visible = ShowContinue;
            if (!IsPostBack)
                BindPowerUserGrid();
        }

        private void BindPowerUserGrid()
        {
            int count;
            var PowerUserList = KidsUser_DataProvider.GetKidsUser(out count, PageSize: PageSize, SortOrder: new[] { "LastCalculatedScore desc" });

            dgPowerKidsUser.DataSource = PowerUserList;
            dgPowerKidsUser.DataBind();

        }


    }
}