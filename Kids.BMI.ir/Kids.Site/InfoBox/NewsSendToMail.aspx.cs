using System;
using System.Net.Mail;
using Kids.Common;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir.InfoBox
{
    public partial class NewsSendToMail : FormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            String HTMLBody = newLinkAddress.Text;
            HTMLBody += @"
            <br>
            <p dir='rtl'>
            <font face='tahoma' size='2'>ازطرف دوستان دعوت به مشاهده خبري در سايت کودکان و نوجوانان بانک ملی ایران
            شده ايد. براي مشاهده خبر لينک زير را کليک نماييد</font></p>
            <br>
            <a href='{0}'>{0}</a><br>
            <hr>
            <p align='right' dir='rtl'>
            <font size='2' face='tahoma' > آدرس فرستنده: {1}<br>
            پيغام دوست شما :
            <br>
            
            </font> </p>
            <hr>
            <font face='tahoma' size='2'>بخش خبر<a href='{3}'></a>:: وب سايت
            <a href='http://www.bmi.ir'>بانک ملی ایران</a> </font>
            ";
            HTMLBody = String.Format(HTMLBody, "http://kids.bmi.ir/News.aspx?id=1", txtFromAddress.Text, txtBody.Text);

            string resMsgstr;
            MailMessage mailMsg = new MailMessage();
            try
            {
                MailingHelper.SendEmail(SystemConfigs.FromEmailAddress, "سایت کودکان و نوجوانان بانک ملی ایران", txtToAddress.Text, "اخبار سایت کودکان بانک ملی ایران | " + txtSubject.Text,
                                         HTMLBody);

                resMsgstr = "<br><br><br>";
                resMsgstr += "<font face=tahoma size=3>" + "خبر مربوطه ارسال شد " + "</font>";
                resMsgstr += "<br>";
                resMsgstr += "<font face=tahoma size=2><a href='javascript:window.close();'>بستن پنجره</a></font>";
            }

            catch (Exception ex)
            {
                resMsgstr = "<br><br><br> ";
                resMsgstr += "<font face=tahoma size=3>" + " خطايي در ارسال ايميل رخ داد " + "</font>";
                resMsgstr += "<br>";
                resMsgstr += "<p dir=ltr align=left>" + ex.Message;
                resMsgstr += "<br>from : " + mailMsg.From;
                resMsgstr += "<br> to : " + mailMsg.To;
                //resMsgstr += "<br> cc : " + mailMsg.CC;
                resMsgstr += "<br> bcc : " + mailMsg.Bcc;
                resMsgstr += "<br> subject : " + mailMsg.Subject;
                resMsgstr += "<br> message : " + HTMLBody;
                resMsgstr += "</p>";


                resMsgstr += "<font face=tahoma size=2><a href='javascript:window.close();'>بستن پنجره</a></font>";
            }
            newLinkAddress.Text = resMsgstr;
            ExternalDiv.Visible = false;
            newLinkAddress.Visible = true;
        }


    }
}