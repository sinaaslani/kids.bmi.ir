using System;
using System.Linq;
using Kids.EntitiesModel;
using System.Collections.Generic;

namespace Kids.Common
{
    public class ConfigCache
    {
        public List<Config> ConfigList { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public static class Config_DataProvider
    {
        static ConfigCache configCache;

        private static ConfigCache LoadConfig
        {
            get
            {
                if (configCache == null || DateTime.Now.Subtract(configCache.LastUpdate).Minutes > 15)
                {
                    using (var ctx = new BMIKidsEntities(BaseDataProvider.ConnectionString))
                    {

                        var q = from m in ctx.Configs
                                select m;

                        configCache = new ConfigCache { ConfigList = q.ToList(), LastUpdate = DateTime.Now };
                    }
                }
                return configCache;
            }
        }

        public static List<Config> GetConfig(string ConfigName = null)
        {
            using (var ctx = new BMIKidsEntities(BaseDataProvider.ConnectionString))
            {

                var q = from m in ctx.Configs
                        where (string.IsNullOrEmpty(ConfigName) || m.ConfigName == ConfigName)
                        select m;

                return q.ToList();
            }
        }

        public static Config GetCacheConfig(string ConfigName)
        {
            var config = LoadConfig.ConfigList.FirstOrDefault(o => o.ConfigName == ConfigName);
            if (config != null)
                return config;
            throw new InvalidOperationException(string.Format("Config Name==>{0} not found!", ConfigName));
        }

        public static void ClearCacheConfig()
        {
            configCache = null;
        }


        public static void SaveConfig(Config c)
        {
            using (var ctx = new BMIKidsEntities(BaseDataProvider.ConnectionString))
            {
                try
                {
                    if (c.ChangeTracker.State == ObjectState.Unchanged)
                        c.MarkAsModified();

                    ctx.Configs.ApplyChanges(c);
                    ctx.SaveChanges();
                    c.AcceptChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }

        public static void DeleteConfig(Config c)
        {
            using (var ctx = new BMIKidsEntities(BaseDataProvider.ConnectionString))
            {
                try
                {
                    c.MarkAsDeleted();
                    ctx.Configs.ApplyChanges(c);
                    ctx.SaveChanges();
                    c.AcceptChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }
    }
}
