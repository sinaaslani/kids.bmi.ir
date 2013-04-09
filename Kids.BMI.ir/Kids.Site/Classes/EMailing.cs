using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using Kids.Common;

namespace Site.Kids.bmi.ir.Classes
{
    public static class MailingHelper
    {
        public static void SendEmail(string FromEmail, string FromDispalyName, string ToList, string Subject,
                                     string HtmBody, IEnumerable<string> EmbedImagePathList, String BCCList = "")
        {
            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(HtmBody, null, MediaTypeNames.Text.Html);

            foreach (var em in EmbedImagePathList)
            {
                LinkedResource pic1 = new LinkedResource(HttpContext.Current.Server.MapPath(em), MediaTypeNames.Image.Jpeg) { ContentId = Path.GetFileNameWithoutExtension(HttpContext.Current.Server.MapPath(em)) };
                avHtml.LinkedResources.Add(pic1);
            }
            SendEmail(FromEmail, FromDispalyName, ToList, Subject, HtmBody, BCCList, avHtml);
        }

        public static void SendEmail(string FromEmail, string FromDispalyName, string ToList, string Subject, string HtmBody, string BCCList = "", AlternateView view = null)
        {
            MailMessage msgMail = new MailMessage();

            if (view != null)
                msgMail.AlternateViews.Add(view);

            if (!string.IsNullOrEmpty(ToList))
            {
                string[] toList = ToList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in toList)
                {
                    msgMail.To.Add(s);
                }
            }

            if (!string.IsNullOrEmpty(BCCList))
            {
                string[] bccList = BCCList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in bccList)
                {
                    msgMail.Bcc.Add(s);
                }
            }

            msgMail.From = new MailAddress(FromEmail, FromDispalyName, Encoding.UTF8);
            msgMail.Subject = Subject;
            msgMail.Body = HtmBody;
            msgMail.IsBodyHtml = true;
            msgMail.Priority = MailPriority.High;
            msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;

            SmtpClient Client = new SmtpClient(SystemConfigs.SMTPServer)
                                    {
                                        Port = Convert.ToInt32(SystemConfigs.SMTPPort),
                                        EnableSsl = Convert.ToBoolean(SystemConfigs.SMTPIsSSL)
                                    };

            string UserName = SystemConfigs.SMTPUserName;
            string Password = SystemConfigs.SMTPPassword;
            if (!string.IsNullOrEmpty(UserName))
            {
                NetworkCredential Ceredentials = new NetworkCredential(UserName, Password);
                Client.Credentials = Ceredentials;
            }
            else
            {
                Client.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            Client.Send(msgMail);
        }

    }
}