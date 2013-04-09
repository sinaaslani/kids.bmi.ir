using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;
using Kids.Utility;

namespace Kids.EntitiesModel
{
    public enum KidsUserStatus
    {
        RegisterWithoutConfirmation = 10,
        RegisterdWithNoAcc = 11,

        WaiteForAccCreation = 20,
        WaiteForAccCreation_WithSabtConfirmation = 21,
        WaiteForAccCreation_Failed = 22,
        WaiteForAccCreation_FailedSabt = 23,
        WaiteForAccCreation_SentToSabt = 31,


        AccCreated_WaiteForDBCR = 3,
        RegisterdCompletly = 4,
        Geust = -1,
    }

    public static partial class extension
    {

        public static bool CanUseWishAccBenefits(this KidsUser user)
        {
            return user.CurrentStatus == (int)KidsUserStatus.RegisterdCompletly ||
                   user.CurrentStatus == (int)KidsUserStatus.AccCreated_WaiteForDBCR;
        }
        public static bool HasSuccessPayment(this KidsUser user)
        {
            return user.KidsUsers_Payments.Any(o => o.AppStatusCode == 0 && (o.AppStatusDescription == "COMMIT" && o.AppStatusDescription == "PendingTransactionCommited"));
        }

        public static KidsUsers_Payments GetSuccessPayment(this KidsUser user)
        {
            return user.KidsUsers_Payments.FirstOrDefault(o => o.AppStatusCode == 0 && (o.AppStatusDescription == "COMMIT" && o.AppStatusDescription == "PendingTransactionCommited"));
        }
        public static KidsUsers_Payments GetPaymentByOrderId(this KidsUser user, long PostOrderId)
        {
            return user.KidsUsers_Payments.FirstOrDefault(o => o.OrderId == PostOrderId);
        }

        public static KidsUsers_Payments GetPaymentByPaymentId(this KidsUser user, long PaymentId)
        {
            return user.KidsUsers_Payments.FirstOrDefault(o => o.PaymentId == PaymentId);
        }

        public static bool IsBirthDay(this KidsUser user)
        {
            var userbc = PersianDateTime.MiladiToPersian(user.ChildBirthDate);
            if (userbc.Day == PersianDateTime.Now.Day && userbc.Month == PersianDateTime.Now.Month)
                return true;
            return false;
        }

        public static bool IsRegistrionInformationComplete(this KidsUser user)
        {
            return (
                       !string.IsNullOrWhiteSpace(user.ChildMelliCode) &&
                       !string.IsNullOrWhiteSpace(user.ChildName) &&
                       !string.IsNullOrWhiteSpace(user.ChildFamily) &&
                       !string.IsNullOrWhiteSpace(user.ChildFatherName) &&
                       user.ChildBirthDate != null
                   );
        }
    }

    public class KidsUser_DataProvider : BaseDataProvider
    {
        public static List<KidsUser> GetKidsUser(
                                                long? KidsUserId = null, long? IntroducerId = null, string SSOUserName = null,
                                                string ChildMelliCode = null, string ParentMelliCode = null,
                                                string ChildAccNo = null, string ParentAccNo = null,
                                                string ChildName = null, string ChildFamily = null,
                                                int? CurrentStatus = null,
                                                long? OrderId = null, long? PaymentId = null,
                                                decimal? Age = null, string BirthDay = null
                                                )
        {
            int PageCount;
            return GetKidsUser(out PageCount,
                               KidsUserId, IntroducerId, SSOUserName,
                               ChildMelliCode, ParentMelliCode,
                               ChildAccNo, ParentAccNo,
                               ChildName, ChildFamily,
                               CurrentStatus,
                               OrderId, PaymentId,
                               Age, BirthDay
                );
        }

        public static List<KidsUser> GetKidsUser(out int PageCount,
                                           long? KidsUserId = null, long? IntroducerId = null, string SSOUserName = null,
                                           string ChildMelliCode = null, string ParentMelliCode = null,
                                           string ChildAccNo = null, string ParentAccNo = null,
                                           string ChildName = null, string ChildFamily = null,
                                           int? CurrentStatus = null,
                                           long? OrderId = null, long? PaymentId = null,
                                           decimal? Age = null, string BirthDay = null,
                                           int Currentpage = 1, int PageSize = DefaultPageSize,
                                           string[] SortOrder = null)
        {


            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.KidsUsers.Include("ParentRelation")
                                               .Include("KidsUserState")
                                               .Include("KidsUsers_Payments")
                                               .Include("Kids_Wishes").Include("Kids_Wishes.Wish")
                                               .Include("Kids_Scores")
                                               .Include("PollUserResponses")
                                               .Include("PollUserResponses.PollQuestion")
                                               .Include("IntroducerUser")
                                               .Include("KidsUser_BankStoryExam")
                        where

                          (string.IsNullOrEmpty(SSOUserName) || m.SSOUserName == SSOUserName) &&
                          (string.IsNullOrEmpty(ChildMelliCode) || m.ChildMelliCode == ChildMelliCode) &&
                          (string.IsNullOrEmpty(ParentMelliCode) || m.ParentMelliCode == ParentMelliCode) &&
                          (string.IsNullOrEmpty(ChildAccNo) || m.ChildAccNo == ChildAccNo) &&
                          (string.IsNullOrEmpty(ParentAccNo) || m.ParentAccNo == ParentAccNo) &&
                          (string.IsNullOrEmpty(ChildName) || m.ChildName.Contains(ChildName)) &&
                          (string.IsNullOrEmpty(ChildFamily) || m.ChildFamily.Contains(ChildFamily)) &&

                          (!OrderId.HasValue || m.KidsUsers_Payments.Any(o => o.OrderId == OrderId.Value)) &&
                          (!PaymentId.HasValue || m.KidsUsers_Payments.Any(o => o.PaymentId == PaymentId.Value)) &&
                          (!KidsUserId.HasValue || m.KidsUserId == KidsUserId.Value) &&
                          (!IntroducerId.HasValue || m.IntruducerId == IntroducerId.Value) &&
                          (!CurrentStatus.HasValue || m.CurrentStatus == CurrentStatus.Value) &&
                          (!Age.HasValue || m.ChildAge == Age.Value) &&

                          (string.IsNullOrEmpty(BirthDay) || m.ChildPersianBirthDay.Substring(4) == BirthDay.Substring(4))

                        select m;
                if (SortOrder == null)
                    SortOrder = new[] { "CreateDateTime" };

                q = q.OrderBy(SortOrder);

                PageCount = q.Count();
                return q.Skip((Currentpage - 1) * PageSize).Take(PageSize).ToList();
            }
        }


        public static void SaveKidsUser(KidsUser user, object p, Action<Object> a)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    if (user.ChangeTracker.State == ObjectState.Unchanged)
                        user.MarkAsModified();

                    if (user.ChangeTracker.State == ObjectState.Added)
                    {
                        user.CreateDateTime = DateTime.Now;
                        user.LastUpdateDateTime = null;
                    }
                    else
                        user.LastUpdateDateTime = DateTime.Now;

                    ctx.KidsUsers.ApplyChanges(user);
                    ctx.SaveChanges();
                    user.AcceptChanges();

                    if (a != null)
                        a(p);
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("SaveSystemUser_DataProvider_UpdateTransaction", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }


        public static IEnumerable<ParentRelation> GetParentRelation()
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.ParentRelations
                        select m;

                return q.ToList();
            }
        }

        public static List<KidsUserState> GetKidsUserStates()
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                var q = from m in ctx.KidsUserStates
                        select m;

                return q.ToList();
            }
        }

        public static long? GetLatestOrderId()
        {
            try
            {
                using (BMIKidsEntities e = new BMIKidsEntities(ConnectionString))
                {
                    DateTime NextDay = DateTime.Today.AddDays(1);
                    var q = from m in e.KidsUsers_Payments
                            where m.CreateDateTime >= DateTime.Today && m.CreateDateTime < NextDay
                            select (long?)m.OrderId;

                    return q.Max();
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteEntryEventLog("GetLatestOrderId", ex, EventLogEntryType.Information);
                throw;
            }

        }

        public static IEnumerable<KidsUsers_Payments> GetPendingTransactionList()
        {

            using (var e = new BMIKidsEntities(ConnectionString))
            {
                DateTime PastTime = DateTime.Now.AddMinutes(-20);
                DateTime YesterDay = DateTime.Today.AddDays(-1).AddHours(22);


                var q = from m in e.KidsUsers_Payments.Include("KidsUser")
                        where
                            m.CreateDateTime <= PastTime &&
                            (
                                (m.AppStatusCode == null) ||
                                (m.AppStatusCode >= 9000 && m.AppStatusCode != 9006 && m.AppStatusCode != 9012 &&
                                 m.AppStatusCode != 9013 && m.AppStatusCode != 9011) ||
                                (m.AppStatusCode == 9011 && m.CreateDateTime <= YesterDay)
                            )

                        select m;

                q = q.OrderByDescending(o => o.CreateDateTime);
                var RetList = q.ToList();
                return RetList;
            }

        }

        public static void SaveKidsUserPayment(KidsUsers_Payments payment)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {

                    if (payment.ChangeTracker.State == ObjectState.Added)
                    {
                        payment.CreateDateTime = DateTime.Now;
                        payment.LastUpdateDateTime = null;
                    }
                    else
                        payment.LastUpdateDateTime = DateTime.Now;

                    ctx.KidsUsers_Payments.ApplyChanges(payment);
                    ctx.SaveChanges();
                    payment.AcceptChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("SaveSystemUser_DataProvider_UpdateTransaction", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }

        public static void SaveGeustKidsUser(GeustKidsUser gUser)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.GeustKidsUsers.ApplyChanges(gUser);
                    ctx.SaveChanges();
                    gUser.AcceptChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("SaveGeustKidsUser", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }


        public static List<GeustKidsUser> GetGeustUser(out long recordCount, string Name = null, string Family = null,
                                                        string MelliCode = null, string EmailAddress = null, string MobileNumber = null,
                                                        DateTime? FromDateTime = null, DateTime? ToDateTime = null,
                                                        bool SelectDistinct = false, int Currentpage = 1, int PageSize = DefaultPageSize)
        {

            using (var e = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    var q = from m in e.GeustKidsUsers
                            where
                               (string.IsNullOrEmpty(m.Name) || m.Name.Contains(Name)) &&
                               (string.IsNullOrEmpty(m.Family) || m.Family.Contains(Family)) &&
                               (string.IsNullOrEmpty(m.MelliCode) || m.MelliCode.Contains(MelliCode)) &&
                               (string.IsNullOrEmpty(m.EmailAddress) || m.EmailAddress.Contains(EmailAddress)) &&
                               (string.IsNullOrEmpty(m.MobileNumber) || m.MobileNumber.Contains(MobileNumber)) &&
                               (!FromDateTime.HasValue || m.CreateDateTime >= FromDateTime) &&
                               (!ToDateTime.HasValue || m.CreateDateTime >= ToDateTime)
                            select m;

                    

                    if (!SelectDistinct)
                    {
                    recordCount = q.LongCount();
                        q = q.OrderByDescending(o => o.CreateDateTime);
                    var RetList = q.Skip((Currentpage - 1) * PageSize).Take(PageSize).ToList();
                    return RetList;
                }
                    else
                    {
                        recordCount = q.Distinct().LongCount();
                        q = q.Distinct().OrderByDescending(o => o.CreateDateTime);
                        var RetList = q.Skip((Currentpage - 1) * PageSize).Take(PageSize).ToList();
                        return RetList;
                    }

                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("SaveGeustKidsUser", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }

            }

        }
    }
}
