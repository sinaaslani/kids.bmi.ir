using System;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Masters
{
    public partial class ucHomeTopLogo : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string GlobalSWfurl = ResolveUrl("~/App_Themes/Default/Master/menu-kids.swf");
            lblHeaderLogo.Text =SWF_tag( GlobalSWfurl,748,170);
        }
        
    }

}