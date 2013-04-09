using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kids.EntitiesModel;
using Kids.LoggingHelper;

namespace Site.Kids.bmi.ir.KidsGame
{
    public class TempUser
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public bool Sex { get; set; }
    }

    public class TempUserWrapper
    {
        public KidsUser User { get; set; }
        public DateTime CreateDateTime { get; set; }

    }

    public class TempUserMapperManager
    {
        private const int ExpirationMinute = 30;
        private static readonly Dictionary<String, TempUserWrapper> Mapptable = new Dictionary<String, TempUserWrapper>();
        private static TempUserMapperManager _Instance;
        private TempUserMapperManager()
        {
            Task.Factory.StartNew(RefreshUser);
        }

        private void RefreshUser()
        {
            try
            {
                var user = Mapptable.Where(o => o.Value.CreateDateTime <= DateTime.Now.AddMinutes(-ExpirationMinute))
                                    .Select(o => o.Key)
                                    .ToList();
                foreach (var pair in user)
                    Mapptable.Remove(pair);
            }
            catch (Exception ex)
            {
                LogUtility.WriteEntryEventLog("RefreshUser", ex, EventLogEntryType.Error);
            }
            finally
            {
                Thread.Sleep(60000 * 5);
            }
        }
        public static TempUserMapperManager Instance
        {
            get { return _Instance ?? (_Instance = new TempUserMapperManager()); }
        }

        public string AddTempUser(KidsUser user)
        {
            var userlist = Mapptable.Where(o => o.Value.User.KidsUserId == user.KidsUserId).Select(o => o.Key).ToList();
            foreach (var pair in userlist)
                Mapptable.Remove(pair);

            string Key = Guid.NewGuid().ToString();
            Mapptable.Add(Key, new TempUserWrapper { User = user, CreateDateTime = DateTime.Now });
            return Key;
        }

        public TempUserWrapper this[string Key]
        {
            get
            {
                if (Mapptable.ContainsKey(Key))
                    return Mapptable[Key];
                return null;
            }
        }
    }
}