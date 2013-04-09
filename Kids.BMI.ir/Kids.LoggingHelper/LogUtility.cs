using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Kids.LoggingHelper
{
    public static class LogUtility
    {
        private static String ENCRYPTION()
        {
            StringBuilder a = new StringBuilder();
            a = new StringBuilder();
            a = new StringBuilder();
            a = new StringBuilder();
            return "@@[^&$جواد$&^]@@";
        }

        private static Byte[] ENCRYPTION_SALT()
        {
            StringBuilder a = new StringBuilder();
            a = new StringBuilder();
            a = new StringBuilder();
            a = new StringBuilder();
            return new Byte[] { 200, 10, 180, 45, 20, 254, 3, 144 };
        }
        public static string KidsConnectionString
        {
            get
            {
                return Decrypt(ConfigurationManager.ConnectionStrings["BMIKidsEntities"].ConnectionString);
            }
        }

        private static string Decrypt(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            Byte[] encryptedData = Convert.FromBase64String(inputText);

            //Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(ENCRYPTION_KEY, ENCRYPTION_SALT, 1000);
            PasswordDeriveBytes pwdGen = new PasswordDeriveBytes(ENCRYPTION(), ENCRYPTION_SALT());
            byte[] Key = pwdGen.GetBytes(32);
            byte[] IV = pwdGen.GetBytes(16);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(Key, IV))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (
                        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
                        )
                    {
                        byte[] plainText = new Byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }

        private static IEnumerable<string> eChargeCardAdminNumbers
        {
            get { return ConfigurationManager.AppSettings["eChargeCardAdminNumbers"].Split(','); }
        }
        private static string eChargeCardAdminEmails
        {
            get { return ConfigurationManager.AppSettings["eChargeCardAdminEmails"]; }
        }

        private static string SMTPServer
        {
            get { return ConfigurationManager.AppSettings["SMTPServer"]; }
        }

        private static int SMTPPort
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]); }
        }
        private static string SMTPUserName
        {
            get { return ConfigurationManager.AppSettings["SMTPUserName"]; }
        }
        private static string SMTPPassword
        {
            get { return ConfigurationManager.AppSettings["SMTPPassword"]; }
        }


        private static string CreateEntitiesConnectionString()
        {

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(KidsConnectionString)
            {
                MultipleActiveResultSets = false
            };

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlBuilder.ToString(),
                Metadata = @"res://*/ErrorLog_DataModel.csdl|res://*/ErrorLog_DataModel.ssdl|res://*/ErrorLog_DataModel.msl",
            };

            return entityBuilder.ToString();
        }

        private static void ConsolWrite(Exception exception, string msg, ConsoleColor? c)
        {
            Console.ForegroundColor = c.HasValue ? c.Value : ConsoleColor.White;
            Console.WriteLine(@"<------------------------------------>");
            if (exception != null)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
            }

            Console.WriteLine(msg);
            Console.WriteLine(@"<------------------------------------>");
        }

        private static void WriteErrorToDB(ErrorLog log)
        {
            string cnnString = CreateEntitiesConnectionString();
            using (BMIKidsEntities ctx = new BMIKidsEntities(cnnString))
            {
                ctx.ErrorLogs.AddObject(log);
                ctx.SaveChanges();
                ctx.AcceptAllChanges();
            }
        }

        public static List<ErrorLog> GetLogs(string LocationName = null, string MethodName = null,
                                             string ErroMessage = "", EventLogEntryType? type = null,
                                             DateTime? LogDateTime = null
                                            )
        {
            if (LogDateTime.HasValue)
                LogDateTime = LogDateTime.Value.Date;
            string cnnString = CreateEntitiesConnectionString();
            using (BMIKidsEntities ctx = new BMIKidsEntities(cnnString))
            {
                var q = from l in ctx.ErrorLogs
                        where (String.IsNullOrEmpty(LocationName) || l.Location == LocationName) &&
                         (String.IsNullOrEmpty(ErroMessage) || l.ErrorDescription.Contains(ErroMessage)) &&
                          (!LogDateTime.HasValue || l.LogDateTime >= LogDateTime)
                        orderby l.LogDateTime descending
                        select l;
                return q.ToList();
            }

        }
        public static void TruncateErrorLogs()
        {
            string cnnString = CreateEntitiesConnectionString();
            using (BMIKidsEntities e = new BMIKidsEntities(cnnString))
            {
                e.ExecuteStoreCommand("Truncate Table ErrorLogs");
            }
        }

        private static void WriteEntryEventLog(string LocationName, string MethodName, string ErroMessage, Exception ex, bool SendSMSAlert, bool SendEmailAlert, EventLogEntryType type = EventLogEntryType.Error)
        {
            try
            {
                String Message = "";
                Message += ex + "<BR>";
                while (ex != null && ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Message += ex + "<BR>";
                }


                ErrorLog Log = new ErrorLog
                                   {
                                       LogId = Guid.NewGuid(),
                                       Location = LocationName,
                                       MethodName = MethodName,
                                       ErrorDescription = ErroMessage + "<***\r\n***>" + Message,
                                       ErrorType = (int)type,
                                       LogDateTime = DateTime.Now
                                   };
                WriteErrorToDB(Log);
                ConsolWrite(ex, ErroMessage, ConsoleColor.Green);

                if (SendSMSAlert)
                {
                    //foreach (var number in eChargeCardAdminNumbers)
                     //   SMSMagfaHelper.SendSMS(number, "خطا در سيستم شارژ" + ErroMessage);
                }

                if (SendEmailAlert)
                {
                    SendEmail("kids@bmi.ir", eChargeCardAdminEmails, "پيام هشداري سيستم ",Log.ErrorDescription);
                }
            }
            catch
            {
            }
        }

        private static bool SendEmail(string FromEmail, string To, string Subject, string HtmBody)
        {
            try
            {
                MailMessage msgMail = new MailMessage();

                if (!String.IsNullOrEmpty(To))
                {
                    string[] toList = To.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in toList)
                    {
                        try
                        {
                            msgMail.To.Add(s);
                        }
                        catch (Exception ex)
                        {
                            WriteEntryEventLog("EMailing", "Email IS :" + s, ex, EventLogEntryType.Error);
                        }
                    }
                }
                if (msgMail.To.Count == 0)
                    return true;


                msgMail.From = new MailAddress(FromEmail, "سیستم وب سایت کودکان", Encoding.UTF8);
                msgMail.Subject = Subject;
                msgMail.Body = HtmBody;
                msgMail.IsBodyHtml = true;
                msgMail.Priority = MailPriority.High;
                msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.None;

                SmtpClient Client = new SmtpClient
                                        {
                                            Host = SMTPServer,
                                            Port = SMTPPort,
                                            EnableSsl = false,
                                            Credentials = !String.IsNullOrEmpty(SMTPUserName) ? new NetworkCredential(SMTPUserName, SMTPPassword)
                                                              : CredentialCache.DefaultNetworkCredentials,
                                        };

                Client.Send(msgMail);
                return true;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("5.1.3 Invalid address"))
                    return true;

                WriteEntryEventLog("EMailing", ex, EventLogEntryType.Error);
                return false;
            }

        }

        public static void WriteEntryEventLog(string Source, Exception ex, EventLogEntryType eventLogEntryType, bool SendSMSAlert = false, bool SendEmailAlert = false)
        {
            WriteEntryEventLog(Source, "", null, ex, SendSMSAlert, SendEmailAlert, eventLogEntryType);
        }

        public static void WriteEntryEventLog(string Source, string Message, EventLogEntryType eventLogEntryType, bool SendSMSAlert = false, bool SendEmailAlert = false)
        {
            WriteEntryEventLog(Source, "", Message, null, SendSMSAlert, SendEmailAlert, eventLogEntryType);
        }

        public static void WriteEntryEventLog(string Source, string Message, Exception ex, EventLogEntryType eventLogEntryType, bool SendSMSAlert = false, bool SendEmailAlert = false)
        {
            WriteEntryEventLog(Source, "", Message, ex, SendSMSAlert, SendEmailAlert, eventLogEntryType);
        }
    }
}