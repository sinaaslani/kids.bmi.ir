using System;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Masters
{
    public partial class AdminucHomeTopLogo : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string GlobalSWfurl = ResolveUrl("~/App_Themes/Default/images/Web/bmi.swf");
            lblHeaderLogo.Text = SWF_tag(GlobalSWfurl, 970, 86);
        }

    }

}