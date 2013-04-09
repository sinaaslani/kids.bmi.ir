using System;
using Kids.EntitiesModel;
using Kids.Utility;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class TodayKidsBirthDay : KidsSecureFormBaseClass
    { 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindBirthDayUserGrid();
        }

        private void BindBirthDayUserGrid()
        {
            var KidsUserList = KidsUser_DataProvider.GetKidsUser(BirthDay: PersianDateTime.Today.ToString().Substring(2));

            dgTodayKidsBirthDay.DataSource = KidsUserList;
            dgTodayKidsBirthDay.DataBind();

        }
        protected override void CheckKidsUser()
        {

        }
    }
}