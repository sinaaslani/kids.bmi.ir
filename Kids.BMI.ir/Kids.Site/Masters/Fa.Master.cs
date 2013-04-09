using System;
using System.IO;
using System.Web.UI;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.InfoBox;

namespace Site.Kids.bmi.ir.Masters
{
    public partial class Fa : MasterPage
    {
        protected string RSSLinks;
        protected bool _DEBUG;
        protected string Position;
        protected int BlackAreaHight;
        protected string MasterBackGroundImageUrl { get; set; }

        public bool ShowKidsRightMenu
        {
            get { return RightPanel.Visible; }
            set { RightPanel.Visible = value; }
        }

        public Control TopPlaceHolder
        {
            get { return TopRightPanelPlaceHolder; }

        }
      
        public ucNewsMarque NewsMarque
        {
            get { return ucNewsMarque; }

        }
        public ScriptManager ScriptManager
        {
            get { return this.ScriptManager1; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MasterBackGroundImageUrl = GetPageBackGroundImage(Page);
#if DEBUG
            _DEBUG = true;
            BlackAreaHight = 100;
#else
            _DEBUG=false;
            BlackAreaHight=5500;
            
#endif
            Position = Request.Browser.IsBrowser("IE") ? "relative" : "fixed";

            if (!IsPostBack)
                RSSLinks = RSSUtility.CreateRSSLink();
        }

        private string GetPageBackGroundImage(Page p)
        {
            string Vpath = string.Format("/App_Themes/Default/Master/MasterBG/{0}.jpg", p.GetType().BaseType.Name);
            if (File.Exists(Server.MapPath(Vpath)))
                return Vpath;
            return "/App_Themes/Default/Master/MasterBG/General.jpg";
        }
    }
}
