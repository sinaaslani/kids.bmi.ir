using Kids.Common;
using Site.Kids.bmi.ir.Classes;
using System;

namespace Site.Kids.bmi.ir.Contact
{

    public partial class ContactUs : FormBaseClass
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void sendBtn_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string bodyStr = "";

            bodyStr += "نام و نام خانوادگي فرستنده:" + txtFnameLname.Text + "<br>";
            bodyStr += "شماره تلفن تماس:" + txtPhone.Text + "<br>";
            bodyStr += "آدرس :" + txtAddress.Text + "<br>";
            bodyStr += "<br>" + bodyTxt.Text;
            bodyStr += "<br><br>this suggestion send by  " + fromAddressTxt.Text;
            string subjectStr = "BMI Kids Suggestion :: " + subjectTxt.Text;
            string receiverEmail = SystemConfigs.ContacUsAdmin;

            try
            {
                MailingHelper.SendEmail("Kids@BMI.IR", "تماس با ما : سایت جوانه ها", receiverEmail, subjectStr, bodyStr);
                statusMsgLbl.Text = "پيغام شما با موفقيت ارسال شد";
               
            }
            catch
            {
                statusMsgLbl.Text = "خطا : متاسفانه پيام شما ارسال نشد ";
            }
            statusMsgLbl.Visible = true;
            suggestTbl.Visible = false;
        }
    }
}
