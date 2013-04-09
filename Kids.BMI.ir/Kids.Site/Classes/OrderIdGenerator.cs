using System;
using System.Web;
using Kids.EntitiesModel;
using Kids.Utility;

namespace Site.Kids.bmi.ir.Classes
{
    public class OrderIdGenerator
    {
        private static readonly Object _Lock = new object();
        private static readonly Object _OrderIdLock = new object();

        public static OrderIdGenerator Instance
        {
            get
            {
                if (HttpContext.Current.Application["OrderIdGenerator"] == null)
                {
                    lock (_Lock)
                    {
                        if (HttpContext.Current.Application["OrderIdGenerator"] == null)
                            HttpContext.Current.Application["OrderIdGenerator"] = new OrderIdGenerator();
                    }
                }
                return HttpContext.Current.Application["OrderIdGenerator"] as OrderIdGenerator;
            }
        }

        private OrderIdGenerator()
        {
            long? LastOrderId = KidsUser_DataProvider.GetLatestOrderId();
            _OrderId = LastOrderId.HasValue ? Convert.ToInt64(LastOrderId.ToString().Substring(8)) : 1;

        }

        Int64 _OrderId;
        public long GetNextOrderId()
        {
            lock (_OrderIdLock)
            {
                _OrderId++;
                return Convert.ToInt64(PersianDateTime.MiladiToPersian(DateTime.Now) + _OrderId.ToString().PadLeft(9, '0'));
            }

        }

    }


}
