using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Site.Kids.bmi.ir.KidsGame
{
    class MappingUrlInfo
    {
        public Guid Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string RealFilePath { get; set; }
    }
    internal class TempLinkManager
    {
        private readonly Dictionary<Guid, MappingUrlInfo> urlMap = new Dictionary<Guid, MappingUrlInfo>();

        private TempLinkManager()
        {
            Task.Factory.StartNew(CleanUrls);
        }
        public static TempLinkManager Instanse
        {
            get
            {
                if (HttpContext.Current.Application["TempLinkManager"] == null)
                    HttpContext.Current.Application["TempLinkManager"] = new TempLinkManager();
                return HttpContext.Current.Application["TempLinkManager"] as TempLinkManager;
            }
        }

        private void CleanUrls()
        {
            while (true)
            {
                try
                {
                    var oldItems = urlMap.Where(o => o.Value.CreateDateTime < DateTime.Now.AddMinutes(-2)).Select(o => o.Key);
                    foreach (Guid id in oldItems)
                        urlMap.Remove(id);
                }
                catch (Exception) { }
                finally
                {
                    Thread.Sleep(1 * 60000);
                }
            }
        }

        internal Guid AddLink(string RealFilePath)
        {
            var id = Guid.NewGuid();
            urlMap.Add(id, new MappingUrlInfo { Id = id, CreateDateTime = DateTime.Now, RealFilePath = RealFilePath });
            return id;
        }

        public MappingUrlInfo this[Guid id]
        {
            get
            {
                return urlMap.ContainsKey(id) ? urlMap[id] : null;
            }
        }
    }

}