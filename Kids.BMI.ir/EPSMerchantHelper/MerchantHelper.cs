using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EPS.MerchantHelper.MerchantUtilityWebRef;


namespace EPS.MerchantHelper
{
    public static class MerchantHelpers
    {
        private static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static string DESEncrypt(string originalString, string Key)
        {
            if (String.IsNullOrEmpty(originalString))
                throw new ArgumentNullException(@"The string which needs to be encrypted can not be null.");


            byte[] bytes = Encoding.ASCII.GetBytes(Key);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);

            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();

            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }


        private static string CalcTimeStamp(string ServiceUrl, bool IgnoreInvalidServerCertificate)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);

            MerchantUtility s = new MerchantUtility { Url = ServiceUrl };
            return s.CalcTimeStamp();
        }

        private static string CalcRequestKey(string CardAcqID, string TransacionKey, long OrderId, string RequestFP,
                                               string Timestamp)
        {
            string textInput = string.Concat(CardAcqID, OrderId.ToString(), RequestFP, TransacionKey);
            MD5 hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Input = encoding.GetBytes(textInput);
            byte[] result = hash.ComputeHash(Input);
            string RequestKey = Timestamp + BitConverter.ToString(result);
            RequestKey = RequestKey.Replace("-", "").ToLower();
            return RequestKey;
        }


        private static string CalcFpOrder(string CardAcqID, long AmountTrans, string TransacionKey, long OrderId,
                                            string Timestamp)
        {
            string textInput = string.Concat(CardAcqID, OrderId.ToString(), AmountTrans.ToString(), TransacionKey, Timestamp);
            MD5 hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Input = encoding.GetBytes(textInput);
            byte[] result = hash.ComputeHash(Input);
            string Fp = BitConverter.ToString(result);
            return Fp;
        }




        public static CheckStatusResult GetRequestStatus(string ServiceURL, bool IgnoreInvalidServerCertificate, long OrderID, string MerchantId,
                                                         string TerminalId, string TranactionKey, string RequestKey, long AmountTrans
                                                        )
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            MerchantUtility cm = new MerchantUtility { Url = ServiceURL };
            return cm.GetRequestStatusResult(OrderID, MerchantId, TerminalId, TranactionKey, RequestKey, AmountTrans);
        }

        public static int CheckRequestStatus(string ServiceURL, bool IgnoreInvalidServerCertificate, long OrderID, string CardAcqID, string TerminalID, string transactionKey,
                                            string RequestKey, long AmountTrans, out string RetrivalRefNo,
                                            out string AppStatus)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

            string timestampOrder = CalcTimeStamp(ServiceURL, IgnoreInvalidServerCertificate);
            string fpOrder = CalcFpOrder(CardAcqID, AmountTrans, transactionKey, OrderID, timestampOrder);

            MerchantUtility cm = new MerchantUtility { Url = ServiceURL };


            int AppStatusCode = cm.CheckRequestStatusEx(OrderID, AmountTrans, CardAcqID, TerminalID, RequestKey,
                                                        timestampOrder, fpOrder, out RetrivalRefNo, out AppStatus
                                                       );


            return AppStatusCode;
        }

        public static CheckStatusResult CheckBillStatus(string ServiceURL, bool IgnoreInvalidServerCertificate, long MerchantID, long TerminalID, long orderId, string RequestKey, BillInfo bInfo)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);

            MerchantUtility cm = new MerchantUtility { Url = ServiceURL };
            CheckStatusResult Res = cm.CheckBillStatus(MerchantID.ToString(), TerminalID.ToString(), orderId, RequestKey, bInfo.BillID, bInfo.BillPaymentID);


            return Res;
        }


        public static CheckStatusResult CheckAuthStatus(string ServiceURL, bool IgnoreInvalidServerCertificate, long MerchantID, long TerminalID, long orderId, string RequestKey, string CardNumber)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);

            MerchantUtility cm = new MerchantUtility { Url = ServiceURL };
            CheckStatusResult Res = cm.GetAuthRequestStatus(MerchantID.ToString(), TerminalID.ToString(), orderId, RequestKey, CardNumber);


            return Res;
        }



        public static int CheckRequestStatus(string ServiceURL, bool IgnoreInvalidServerCertificate, long OrderID, string MerchantID, string TerminalID, string transactionKey,
                                             string RequestKey, long AmountTrans, out string RetrivalRefNo,
                                             out string AppStatus, out string RealTransactionDateTime)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);


            string timestampOrder = CalcTimeStamp(ServiceURL, IgnoreInvalidServerCertificate);
            string fpOrder = CalcFpOrder(MerchantID, AmountTrans, transactionKey, OrderID, timestampOrder);

            MerchantUtility cm = new MerchantUtility { Url = ServiceURL };


            int AppStatusCode = cm.CheckRequestStatusExWithRealTransactionDateTime(OrderID, AmountTrans, MerchantID, TerminalID, RequestKey,
                                                        timestampOrder, fpOrder, out RetrivalRefNo, out AppStatus,
                                                        out  RealTransactionDateTime);


            return AppStatusCode;
        }


        public static CheckStatusResult CheckRequestStatus(string ServiceURL, bool IgnoreInvalidServerCertificate, long OrderID, string CardAcqID, string TerminalID, string transactionKey,
                                          string RequestKey, long AmountTrans)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);

            MerchantUtility cm = new MerchantUtility { Url = ServiceURL };
            CheckStatusResult Result = cm.CheckRequestStatusResult(OrderID, CardAcqID, TerminalID, transactionKey, RequestKey, AmountTrans);

            return Result;
        }


        public static string PaymentUtility(string ServiceUrl, bool IgnoreInvalidServerCertificate, string PaymentURL, string CardAcqID, long AmountTrans, long OrderId, string TransactionKey,
                                            string TerminalId, string RedirectURL, string MerchantAdditionalData, string CustomerEmailAddress,
                                            string Encrypted_OptionalPaymentParameter, out string RequestKey)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);


            StringBuilder output = new StringBuilder();
            string timestampOrder = CalcTimeStamp(ServiceUrl, IgnoreInvalidServerCertificate);

            string fpOrder = CalcFpOrder(CardAcqID, AmountTrans, TransactionKey, OrderId, timestampOrder);
            RequestKey = CalcRequestKey(CardAcqID, TransactionKey, OrderId, fpOrder, timestampOrder);


            output.AppendFormat("<form id='paymentUTLfrm' action='{0}' method='post'>", PaymentURL);

            output.AppendFormat("<input type='hidden' name='CardAcqID' value='{0}' />", CardAcqID);

            output.AppendFormat("<input type='hidden' name='AmountTrans' value='{0}' />  ", AmountTrans);

            output.AppendFormat("<input type='hidden' name='ORDERID' value='{0}' />", OrderId);

            output.AppendFormat("<input type='hidden' name='TerminalID' value='{0}' />", TerminalId);

            output.AppendFormat("<input type='hidden' name='TimeStamp' value='{0}' />", timestampOrder);

            output.AppendFormat("<input type='hidden' name='FP' value='{0}' />", fpOrder);

            output.AppendFormat("<input type='hidden' name='RedirectURL' value='{0}' />", RedirectURL);

            output.AppendFormat("<input type='hidden' name='MerchantAdditionalData' value='{0}' />", MerchantAdditionalData);

            output.AppendFormat("<input type='hidden' name='CustomerEmailAddress' value='{0}' />", CustomerEmailAddress);

            if (!string.IsNullOrEmpty(Encrypted_OptionalPaymentParameter))
                output.AppendFormat("<input type='hidden' name='OptionalPaymentParameter' value='{0}' />", Encrypted_OptionalPaymentParameter);


            return output.ToString();
        }


        public class BillInfo
        {
            public long BillID { set; get; }
            public long BillPaymentID { set; get; }

            public override string ToString()
            {
                return BillID + "," + BillPaymentID;
            }

            public static string SerializeBillList(IEnumerable<BillInfo> BillList)
            {
                string Result = "";
                foreach (BillInfo billInfo in BillList)
                {
                    Result += billInfo + ",";
                }
                return Result.TrimEnd(',');
            }
        }

        public static string BillPaymentUtility(string ServiceUrl, bool IgnoreInvalidServerCertificate, string BillPaymentURL, string CardAcqID, long OrderId, string TransactionKey,
                                                string TerminalId, string RedirectURL, string MerchantAdditionalData, string CustomerEmailAddress, IEnumerable<BillInfo> BillList,
                                                out string RequestKey)
        {

            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);


            StringBuilder output = new StringBuilder();
            string timestampOrder = CalcTimeStamp(ServiceUrl, IgnoreInvalidServerCertificate);

            string fpOrder = CalcFpBillPayment(CardAcqID, BillList, TransactionKey, OrderId, timestampOrder);
            RequestKey = CalcRequestKeyBillPayment(CardAcqID, BillList, TransactionKey, OrderId, fpOrder, timestampOrder);


            output.AppendFormat("<form id='paymentUTLfrm' action='{0}' method='post'>", BillPaymentURL);

            output.AppendFormat("<input type='hidden' name='CardAcqID' value='{0}' />", CardAcqID);

            output.AppendFormat("<input type='hidden' name='ORDERID' value='{0}' />", OrderId);

            output.AppendFormat("<input type='hidden' name='TerminalID' value='{0}' />", TerminalId);

            output.AppendFormat("<input type='hidden' name='TimeStamp' value='{0}' />", timestampOrder);

            output.AppendFormat("<input type='hidden' name='FP' value='{0}' />", fpOrder);

            output.AppendFormat("<input type='hidden' name='RedirectURL' value='{0}' />", RedirectURL);

            output.AppendFormat("<input type='hidden' name='MerchantAdditionalData' value='{0}' />", MerchantAdditionalData);

            output.AppendFormat("<input type='hidden' name='CustomerEmailAddress' value='{0}' />", CustomerEmailAddress);

            output.AppendFormat("<input type='hidden' name='BillList' value='{0}' />", BillInfo.SerializeBillList(BillList));


            return output.ToString();
        }
        public static string AuthenticationRequerstUtility(string ServiceUrl, bool IgnoreInvalidServerCertificate, string AuthenticationRequerstURL,
                                                           string CardAcqID, string TerminalId, string TransactionKey,
                                                           long OrderId, string CardNumber, string RedirectURL,
                                                           out string RequestKey)
        {


            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);


            StringBuilder output = new StringBuilder();
            string TimeStamp = CalcTimeStamp(ServiceUrl, IgnoreInvalidServerCertificate);

            string fpOrder = CalcFpAuthenticationRequerst(CardAcqID, CardNumber, TransactionKey, OrderId, TimeStamp);
            RequestKey = CalcRequestKeyAuthenticationRequerst(CardAcqID, CardNumber, TransactionKey, OrderId, fpOrder, TimeStamp);


            output.AppendFormat("<form id='paymentUTLfrm' action='{0}' method='post'>", AuthenticationRequerstURL);

            output.AppendFormat("<input type='hidden' name='CardAcqID' value='{0}' />", CardAcqID);

            output.AppendFormat("<input type='hidden' name='ORDERID' value='{0}' />", OrderId);

            output.AppendFormat("<input type='hidden' name='TerminalID' value='{0}' />", TerminalId);

            output.AppendFormat("<input type='hidden' name='TimeStamp' value='{0}' />", TimeStamp);

            output.AppendFormat("<input type='hidden' name='FP' value='{0}' />", fpOrder);

            output.AppendFormat("<input type='hidden' name='RedirectURL' value='{0}' />", RedirectURL);

            output.AppendFormat("<input type='hidden' name='CardNumber' value='{0}' />", CardNumber);


            return output.ToString();

        }

        private static string CalcRequestKeyAuthenticationRequerst(string CardAcqID, string cardNumber, string TransacionKey, long OrderId, string RequestFP, string RequestId)
        {
            string textInput = string.Concat(CardAcqID, OrderId.ToString(), cardNumber, RequestFP, TransacionKey);
            MD5 hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Input = encoding.GetBytes(textInput);
            byte[] result = hash.ComputeHash(Input);
            string RequestKey = RequestId + BitConverter.ToString(result);
            RequestKey = RequestKey.Replace("-", "").ToLower();
            return RequestKey;
        }

        private static string CalcFpAuthenticationRequerst(string CardAcqID, string CardNumber, string TransactionKey, long OrderId, string RequestId)
        {
            string textInput = string.Concat(CardAcqID, OrderId.ToString(), CardNumber, TransactionKey, RequestId);
            MD5 hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Input = encoding.GetBytes(textInput);
            byte[] result = hash.ComputeHash(Input);
            string Fp = BitConverter.ToString(result);
            return Fp;
        }

        private static string CalcRequestKeyBillPayment(string CardAcqID, IEnumerable<BillInfo> BillList, string TransacionKey, long OrderId, string RequestFP, string Timestamp)
        {

            string textInput = string.Concat(CardAcqID, OrderId.ToString(), BillInfo.SerializeBillList(BillList), RequestFP, TransacionKey);
            MD5 hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Input = encoding.GetBytes(textInput);
            byte[] result = hash.ComputeHash(Input);
            string RequestKey = Timestamp + BitConverter.ToString(result);
            RequestKey = RequestKey.Replace("-", "").ToLower();
            return RequestKey;

        }

        private static string CalcFpBillPayment(string CardAcqID, IEnumerable<BillInfo> BillList, string TransactionKey, long OrderId, string Timestamp)
        {
            string textInput = string.Concat(CardAcqID, OrderId.ToString(), BillInfo.SerializeBillList(BillList), TransactionKey, Timestamp);
            MD5 hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Input = encoding.GetBytes(textInput);
            byte[] result = hash.ComputeHash(Input);
            string Fp = BitConverter.ToString(result);
            return Fp;


        }


        public static void GetTransactionReportByPageNumber(string ServiceUrl, bool IgnoreInvalidServerCertificate, string MerchantId,
                                                            string Terminal, string TransactionKey, string ShamsiDate, int PageNumber,
                                                            out string XMLData, out string XMLSchema, out int TotalPage)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);


            MerchantUtility rptService = new MerchantUtility { Url = ServiceUrl };

            string TimeStamp = rptService.CalcTimeStamp();

            string textInput = string.Concat(TimeStamp, MerchantId, Terminal, TransactionKey);
            MD5 hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Input = encoding.GetBytes(textInput);
            byte[] result = hash.ComputeHash(Input);
            string Fp = BitConverter.ToString(result);

            XMLData = rptService.GetTransactionReportByPageNumber(MerchantId, Terminal, TimeStamp, Fp, ShamsiDate, PageNumber, out TotalPage, out XMLSchema);

        }

        public static void GetTransactionReport(string ServiceUrl, bool IgnoreInvalidServerCertificate, string MerchantId,
                                                string Terminal, string TransactionKey, string ShamsiDate,
                                                out string XMLData, out string XMLSchema)
        {
            int TotalPage;
            GetTransactionReportByPageNumber(ServiceUrl, IgnoreInvalidServerCertificate, MerchantId,
                                            Terminal, TransactionKey, ShamsiDate, 1, out XMLData, out XMLSchema, out TotalPage);
        }

        public static void GetCommitReportByPageNumber(string ServiceUrl, bool IgnoreInvalidServerCertificate, string MerchantId,
                                           string Terminal, string TransactionKey,
                                           string FromShamsiDate, string ToShamsiDate, string FromHour, string ToHour,
                                           string TraceNo, string Amount, string OrderId,
                                           int PageNumber, out int TotelPage,
                                           out string XMLData, out string XMLSchema)
        {
            ServicePointManager.ServerCertificateValidationCallback = null;
            if (IgnoreInvalidServerCertificate)
                ServicePointManager.ServerCertificateValidationCallback =
                       new RemoteCertificateValidationCallback(ValidateServerCertificate);


            MerchantUtility rptService = new MerchantUtility { Url = ServiceUrl };

            string TimeStamp = rptService.CalcTimeStamp();

            string textInput = string.Concat(TimeStamp, MerchantId, Terminal, TransactionKey);
            MD5 hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Input = encoding.GetBytes(textInput);
            byte[] result = hash.ComputeHash(Input);
            string Fp = BitConverter.ToString(result);

            XMLData = rptService.GetCommitReportByPageNumber(MerchantId, Terminal, TimeStamp, Fp, FromShamsiDate, ToShamsiDate, FromHour, ToHour, TraceNo,
                                                             Amount, OrderId, PageNumber, out TotelPage, out XMLSchema);

        }


        public static void GetCommitReport(string ServiceUrl, bool IgnoreInvalidServerCertificate, string MerchantId,
                                           string Terminal, string TransactionKey,
                                           string FromShamsiDate, string ToShamsiDate, string FromHour, string ToHour,
                                           string TraceNo, string Amount, string OrderId,
                                           out string XMLData, out string XMLSchema)
        {
            int TotalPage;
            GetCommitReportByPageNumber(ServiceUrl, IgnoreInvalidServerCertificate, MerchantId,
                                        Terminal, TransactionKey,
                                        FromShamsiDate, ToShamsiDate, FromHour, ToHour,
                                        TraceNo, Amount, OrderId, 1, out TotalPage,
                                        out  XMLData, out  XMLSchema);
        }



        public static DataSet XMLToDataSet(string SchemaXML, string DataXML)
        {
            TextReader TextReaderSchima = new StringReader(SchemaXML);
            TextReader TextReaderData = new StringReader(DataXML);
            DataSet ds = new DataSet();

            ds.ReadXmlSchema(TextReaderSchima);
            ds.ReadXml(TextReaderData);
            return ds;
        }



    }
}