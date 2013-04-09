using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{
    public class SystemUser_DataProvider : BaseDataProvider
    {

        public static SystemUser GetValidSystemUser(string SSOUserName)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.SystemUsers.Include("SystemRoles")
                        where (m.SSOUserName == SSOUserName) && (m.Active)
                        select m;

                return q.FirstOrDefault();
            }
        }

        public static List<SystemUser> GetSystemUser(string SSOUserName = null, long? UserId = null, string FirstName = null, string LastName = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.SystemUsers.Include("SystemRoles")
                        where
                        (string.IsNullOrEmpty(SSOUserName) || m.SSOUserName.Contains(SSOUserName)) &&
                              (!UserId.HasValue || m.UserId == UserId.Value) &&
                              (string.IsNullOrEmpty(FirstName) || m.Name == FirstName) &&
                              (string.IsNullOrEmpty(LastName) || m.Family == LastName)
                        select m;

                return q.ToList();
            }
        }


        public static List<SystemRole> GetRoles(int? roleId = null)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.SystemRoles
                        where (!roleId.HasValue || m.RoleId == roleId.Value)
                        select m;

                return q.ToList();
            }
        }
        public static IEnumerable<SystemRole> GetRolesWithUsers(int? roleId)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.SystemRoles.Include("SystemUsers")
                        where (!roleId.HasValue || m.RoleId == roleId.Value)
                        select m;

                return q.ToList();
            }
        }

        public static void SaveSystemUser(SystemUser user)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.SystemUsers.ApplyChanges(user);
                    ctx.SaveChanges();
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


        public static void SaveRoles(SystemRole role)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    ctx.SystemRoles.ApplyChanges(role);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("SaveRoles_DataProvider_UpdateTransaction", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }

    }
}
