using System.Data.EntityClient;
using System.Data.SqlClient;
using Kids.Common;


namespace Kids.EntitiesModel
{
    public  class BaseDataProvider
    {
        protected const int DefaultPageSize = 100;
        public static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(SystemConfigs.KidsConnectionString);

                EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder
                    {
                        Provider = "System.Data.SqlClient",
                        ProviderConnectionString = sqlBuilder.ToString(),
                        #if COMMON
                            Metadata =@"res://*/Configs.csdl|res://*/Configs.ssdl|res://*/Configs.msl"
                        #endif
                        #if DATAPROVIDER
                            Metadata = @"res://*/BMIKids_Model.csdl|res://*/BMIKids_Model.ssdl|res://*/BMIKids_Model.msl"
                        #endif
                    };

                return entityBuilder.ToString();
            }
        }

    }
}
