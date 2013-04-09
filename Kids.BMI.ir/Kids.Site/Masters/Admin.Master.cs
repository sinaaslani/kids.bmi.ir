using System;
using System.Web.UI;

namespace Site.Kids.bmi.ir.Masters
{
    public partial class Admin : MasterPage
    {
        protected bool _DEBUG;
        protected string Position;
        protected int BlackAreaHight;

        public void RegisterPostbackTrigger(Control trigger)
        {
            ScriptManager1.RegisterPostBackControl(trigger);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
#if DEBUG
            _DEBUG = true;
            BlackAreaHight = 100;
#else
           
            _DEBUG=false;
            BlackAreaHight=5500;
#endif
            Position = Request.Browser.IsBrowser("IE") ? "relative" : "fixed";
        }


    }
}