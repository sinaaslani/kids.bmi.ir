using System;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.BankStory
{
    public partial class BankStoryHome : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string GlobalSWfurl = ResolveUrl("~/App_Themes/Default/Movie/BankStory.swf");
            lblSWF.Text = UserControlBaseClass.SWF_tag(GlobalSWfurl, 980, 630);
        }

       
    }
}