using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Kids.Common;
using Kids.Utility;

namespace Kids.EntitiesModel
{
    [Serializable]
    public class AccBill
    {
        public string tx_date { get; set; }

        public string tx_time { get; set; }

        public string brno { get; set; }

        public string de_num { get; set; }

        public string tx_type { get; set; }

        public string tx_trace { get; set; }

        public string sharh { get; set; }

        public string tx_comment { get; set; }

        public string card_num { get; set; }

        public string tx_amount { get; set; }

        public string tx_cbalance { get; set; }

        public string tx_code { get; set; }

        public string smtcode { get; set; }

        public string Ensharh { get; set; }

    }

    [Serializable]
    public class CustomerAccInfo
    {
        public string ac_num { get; set; }
        public string cu_id { get; set; }
        public string cu_lname { get; set; }
        public string cu_fname { get; set; }

    }

    [Serializable]
    public class LoanInfo
    {
        public string Brno_Amalyat { get; set; }
        public string tx_date { get; set; }
        public string tx_time { get; set; }
        public string SharhStructure { get; set; }
        public string Bed { get; set; }
        public string Bes { get; set; }
    }

    [Serializable]
    public class CustomerInfo
    {
        public string cu_lname { get; set; }

        public string cu_fname { get; set; }

        public string cu_FatherName { get; set; }

        public string SabtId { get; set; }

        public string BirthDayDate { get; set; }
    }

    public static class BMICustomer_DataProvider
    {
        public static List<CustomerAccInfo> GetAccByMellicode(string MelliCode)
        {
            //exec dbo.GetAccByMellicode '3873710730'
            using (var cnn = new SqlConnection(SystemConfigs.Cnn_Central_Current1))
            {
                var cmd = new SqlCommand("GetAccByMellicode", cnn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@MelliCode", MelliCode);

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                return (from DataRow row in dt.Rows
                        select new CustomerAccInfo
                            {
                                ac_num = row["ac_num"].ToString().PadLeft(13, '0'),
                                cu_id = row["cu_id"].ToString().PadLeft(10, '0'),
                                cu_fname = row["cu_fname"].ToString().Replace("ي", "ی").Replace((char)1609, 'ی'),
                                cu_lname = row["cu_lname"].ToString().Replace("ي", "ی").Replace((char)1609, 'ی'),
                            }).ToList();
            }
        }

        public static CustomerInfo GetCustInfoByCuid(string cuid)
        {
            //exec dbo.GetAccByMellicode '3873710730'
            using (var cnn = new SqlConnection(SystemConfigs.Cnn_MelliCode))
            {
                var cmd = new SqlCommand("GetCustInfoByCuid", cnn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@cuid", cuid);

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);


                //cu_lname	cu_fname	cu_FatherName	SabtId	BirthDayDate

                List<CustomerInfo> retlist = (from DataRow row in dt.Rows
                                              select new CustomerInfo
                                                  {
                                                      cu_lname = row["cu_lname"].ToString().Trim().Replace("ي", "ی").Replace((char)1609, 'ی'),
                                                      cu_fname = row["cu_fname"].ToString().Trim().Replace("ي", "ی").Replace((char)1609, 'ی'),
                                                      cu_FatherName = row["cu_FatherName"].ToString().Trim().Replace("ي", "ی").Replace((char)1609, 'ی'),
                                                      SabtId = row["SabtId"].ToString().Trim(),
                                                      BirthDayDate = row["BirthDayDate"].ToString().Trim(),
                                                  }).ToList();

                return retlist.FirstOrDefault();
            }
        }

        public static long GetAccRemain(KidsUser user, out string LastTransactionDate)
        {
            var AccBill = GetAccBill(user.ChildAccNo, PersianDateTime.Now.AddDays(-40).ToString(), PersianDateTime.Now.ToString()).LastOrDefault();
            if (AccBill != null)
            {
                LastTransactionDate = AccBill.tx_date;
                return AccBill.tx_cbalance.ToLong();
            }
            LastTransactionDate = "";
            return 0;
        }
        public static List<AccBill> GetAccBill(string AccNumber, string FromData, string ToDate)
        {
            if (SystemConfigs.Cnn_Central_Current == null)
                SystemConfigs.Cnn_Central_Current = SystemConfigs.Cnn_Central_Current1;
            try
            {
                return GetAccBill2(AccNumber, FromData, ToDate);
            }
            catch
            {
                SystemConfigs.Cnn_Central_Current = SystemConfigs.Cnn_Central_Current == SystemConfigs.Cnn_Central_Current2 ?
                                              SystemConfigs.Cnn_Central_Current1 :
                                              SystemConfigs.Cnn_Central_Current2;

                return GetAccBill2(AccNumber, FromData, ToDate);
            }
        }

        private static List<AccBill> GetAccBill2(string AccNumber, string FromData, string ToDate)
        {
            using (var cnn = new SqlConnection(SystemConfigs.Cnn_Central_Current))
            {
                if (FromData.Length > 2) FromData = FromData.Substring(2);
                if (ToDate.Length > 2) ToDate = ToDate.Substring(2);

                var cmd = new SqlCommand("SPNB_GetUserCAccBill_2", cnn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@acNum", AccNumber);
                cmd.Parameters.AddWithValue("@date1", ToDate);
                cmd.Parameters.AddWithValue("@date2", FromData);


                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                return (from DataRow row in dt.Rows
                        select new AccBill
                            {
                                brno = row["brno"].ToString().Trim(),
                                card_num = row["card_num"].ToString().Trim(),
                                de_num = row["de_num"].ToString().Trim(),
                                Ensharh = row["Ensharh"].ToString().Trim(),
                                sharh = row["sharh"].ToString().Trim(),
                                smtcode = row["smtcode"].ToString().Trim(),
                                tx_amount = row["tx_amount"].ToString().Trim(),
                                tx_cbalance = row["tx_cbalance"].ToString().Trim(),
                                tx_code = row["tx_code"].ToString().Trim(),
                                tx_comment = row["tx_comment"].ToString().Trim(),
                                tx_date = row["tx_date"].ToString().Trim(),
                                tx_time = row["tx_time"].ToString().Trim(),
                                tx_trace = row["tx_trace"].ToString().Trim(),
                                tx_type = row["tx_type"].ToString().Trim(),
                            }).ToList();
            }
        }

        public static List<LoanInfo> GetLoanBill(string loanNum, string FromData, string ToDate, int brno)
        {
            //exec dbo.SP_SoratHesab_GetLoanAccBillWithWClause  '6301542125008','13900601','13910901',215
            using (var cnn = new SqlConnection(SystemConfigs.Cnn_TashilatSiba))
            {
                var cmd = new SqlCommand("SP_SoratHesab_GetLoanAccBillWithWClause", cnn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@loanNum", loanNum);
                cmd.Parameters.AddWithValue("@FromDate", PersianDateTime.MiladiToPersian(FromData).ToString());
                cmd.Parameters.AddWithValue("@ToDate", PersianDateTime.MiladiToPersian(ToDate).ToString());
                cmd.Parameters.AddWithValue("@brno", brno);

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                return (from DataRow row in dt.Rows
                        select new LoanInfo
                            {
                                Brno_Amalyat = row["Brno_Amalyat"].ToString(),
                                tx_date = row["tx_date"].ToString(),
                                tx_time = row["tx_time"].ToString(),
                                SharhStructure = row["SharhStructure"].ToString(),
                                Bed = row["Bed"].ToString(),
                                Bes = row["Bes"].ToString(),
                            }).ToList();
            }
        }


        public static List<CustomerInfo> GetCustInfoByMelliCode(string MelliCode)
        {
            var accList = GetAccByMellicode(MelliCode);
            if (accList != null)
            {
                var cuidList = accList.Select(o => o.cu_id).Distinct();
                return cuidList.Select(GetCustInfoByCuid).ToList();

            }
            return null;

        }

        public static List<CustomerAccInfo> GetLoanAccByCUID(string MelliCode = null, string CustomerNo = null)
        {
            //exec dbo.SP_ArezooWebSite_GetLoanAccByCUID '3873710730'
            using (var cnn = new SqlConnection(SystemConfigs.Cnn_TashilatSiba))
            {
                var cmd = new SqlCommand("SP_ArezooWebSite_GetLoanAccByCUID", cnn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@CUIDorCodeMelli", string.IsNullOrWhiteSpace(MelliCode) ? CustomerNo : MelliCode);
                cmd.Parameters.AddWithValue("@IsCodeMelli", !string.IsNullOrWhiteSpace(MelliCode));

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                return (from DataRow row in dt.Rows
                        select new CustomerAccInfo
                            {
                                ac_num = row["ShTashilat"].ToString().PadLeft(13, '0'),
                                cu_id = "",
                                cu_fname = "",
                                cu_lname = "",
                            }).ToList();
            }
        }
    }
}
