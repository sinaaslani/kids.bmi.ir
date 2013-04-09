using System;
using System.Web.UI.WebControls;
using Kids.EntitiesModel;
using Site.Kids.bmi.ir.Classes;
using Site.Kids.bmi.ir.Scores;

namespace Site.Kids.bmi.ir.Payment
{
    public partial class PaymentList : UserControlBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetUserInfo(KidsUser user)
        {
            dgPaymentList.DataSource = user.KidsUsers_Payments;
            dgPaymentList.DataBind();
        }


            }
        }
