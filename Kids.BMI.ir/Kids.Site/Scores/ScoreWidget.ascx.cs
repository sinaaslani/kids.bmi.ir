using System;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.Scores
{
    public partial class ScoreWidget : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionItems.CurrentScore.HasValue)
            {
                lblCurrentScore.Text = string.Format("امتیاز:{0:0.00}", SessionItems.CurrentScore);
                lblCurrentScore.Visible = true;
            }
            
        }




    }
}