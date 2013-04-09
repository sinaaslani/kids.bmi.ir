using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.KidsGame;

namespace Site.Kids.bmi.ir.Registration
{
    public partial class LastSiteUserWidget : UserControlBaseClass
    {
        //public bool EnablePaging { private get; set; }
        public bool ShowPicture { private get; set; }
        public int PageSize { get; set; }
        //public bool ShowContinue { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //lnkOtherNewUser.Visible = ShowContinue;
            if (!IsPostBack)
                BindLastUserGrid();
        }

        private void BindLastUserGrid()
        {
            dgLastKidsUser.Columns[1].Visible = ShowPicture;
            int count;
            List<KidsUser> LastUserList = KidsUser_DataProvider.GetKidsUser(out count, PageSize: PageSize, SortOrder: new[] { "CreateDateTime desc" });

            dgLastKidsUser.DataSource = LastUserList;
            dgLastKidsUser.DataBind();
           
        }
        private Guid GetTemproryLink(KidsUser user)
        {
            return TempLinkManager.Instanse.AddLink(MapPath(string.Format("/AdminCP/Files/KidsPic/{0}/{1}", user.SSOUserName, user.ChildPic)));
        }
        protected void dgLastKidsUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var img = e.Row.FindControl("ImgNewUser") as Image;
                var user = e.Row.DataItem as KidsUser;
                img.ImageUrl = string.Format("/KidsGame/GameFileManager.aspx?lid={0}", GetTemproryLink(user));
            }
        }
    }
}