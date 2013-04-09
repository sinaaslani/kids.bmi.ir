using System;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir._Festival
{
    public partial class FestivalList : KidsSecureFormBaseClass
    {
        protected override void CheckKidsUser()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //lstgalleryDataListPager.BindGridAction = BindDataList;
            if (!IsPostBack)
            {
                var Curentfsv = Festival_DataProvider.GetActiveFestival(DateTime.Today);
                if (Curentfsv != null)
                    BindCurrentFestivalList(Curentfsv);

                BindOldFestivalList();

            }
        }

        private void BindOldFestivalList()
        {
            int ItemCount;
            var fsvPics = Festival_DataProvider.GetFestival(out ItemCount, FromDate: DateTime.Today.AddYears(-1));
            lstFestivalList.DataSource = fsvPics;
            lstFestivalList.DataBind();
        }

        private void BindCurrentFestivalList(Festival fsv)
        {
            int ItemCount;
            var fsvPics = Festival_DataProvider.GetFestivalPics(out ItemCount, fsv, true, PageSize: fsv.PictureShowCount);
            lstgalleryDataList.DataSource = fsvPics;
            lstgalleryDataList.DataBind();
        }


    }
}