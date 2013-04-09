using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using EPS.MerchantHelper;
using EPS.MerchantHelper.MerchantUtilityWebRef;
using Kids.Common;
using Kids.EntitiesModel;
using Kids.LoggingHelper;

namespace Site.Kids.bmi.ir.Payment
{
    public static class CheckStatus_Auto
    {
        public static void Run()
        {
            CheckStatusWorker_DoWork();
        }

        static void CheckStatusWorker_DoWork()
        {
            while (true)
            {
                try
                {
                    IEnumerable<KidsUsers_Payments> TranList = KidsUser_DataProvider.GetPendingTransactionList();

                    foreach (KidsUsers_Payments payment in TranList)
                    {
                        var currentuser = payment.KidsUser;

                        CheckStatusResult PaymentResult = MerchantHelpers.CheckRequestStatus(SystemConfigs.ServiceUrl, true,
                                                                                               payment.OrderId,
                                                                                               SystemConfigs.MerchantId, SystemConfigs.TerminalId,
                                                                                               SystemConfigs.TransactionKey,
                                                                                               payment.RequestKey,
                                                                                               payment.Amount   
                                                                                             );
                        payment.MarkAsModified();
                        payment.RetrivalRefNo =PaymentResult.RefrenceNumber;
                        payment.SystemTraceNo = PaymentResult.TraceNo;
                        payment.AppStatusCode = PaymentResult.AppStatusCode;
                        payment.AppStatusDescription = PaymentResult.AppStatusDescription;
                        payment.CustomerCardNo = PaymentResult.CustomerCardNumber;

                        if (PaymentResult.AppStatusCode == 0 && PaymentResult.AppStatusDescription == "COMMIT")
                        {
                            List<CustomerAccInfo> childBMIAccounts = BMICustomer_DataProvider.GetAccByMellicode(currentuser.ChildMelliCode);
                            List<CustomerAccInfo> parentBMIAccounts = BMICustomer_DataProvider.GetAccByMellicode(currentuser.ParentMelliCode);

                            if (parentBMIAccounts.Any())
                            {
                                if (childBMIAccounts.Any())
                                {
                                    currentuser.CurrentStatus = (int)KidsUserStatus.WaiteForAccCreation;
                                    currentuser.StatusHistory = string.Format("{0},{1}", currentuser.CurrentStatus, (int)KidsUserStatus.WaiteForAccCreation);
                                }
                                else
                                {
                                    currentuser.CurrentStatus = (int)KidsUserStatus.WaiteForAccCreation_WithSabtConfirmation;
                                    currentuser.StatusHistory = string.Format("{0},{1}", currentuser.CurrentStatus, (int)KidsUserStatus.WaiteForAccCreation_WithSabtConfirmation);
                                }

                            }
                        }
                        KidsUser_DataProvider.SaveKidsUser(currentuser, null, null);
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("CheckStatusWorker_DoWork", ex, EventLogEntryType.Error);
                }
                finally
                {
                    Thread.Sleep(5 * 60000);
                }
            }
        }



    }
}
