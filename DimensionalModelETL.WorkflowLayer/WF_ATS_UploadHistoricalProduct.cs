using System;
using System.Collections.Generic;
using System.Data;
using DimensionalModelETL.DataLayer.DataMappers;
using DimensionalModelETL.DataLayer.Gateway;
using DimensionalModelETL.DataLayer.Model;

namespace DimensionalModelETL.WorkflowLayer
{
    /// <summary>
    /// Transfers the contents of the product table to Azure Table Storage
    /// </summary>
    public class WF_ATS_UploadHistoricalProduct
    {
        private Configuration.Settings _config;

        public WF_ATS_UploadHistoricalProduct(Configuration.Settings config)
        {
            _config = config;
        }


        public void ExecuteWF()
        {
            // Pull in connection info
            string connectionString = _config.SQLServerConnectionString;
            string serverName = _config.SQLServerName;
            string databaseName = _config.SQLServerDatabase;
            string userId = _config.SQLServerUserID;
            string password = _config.SQLServerPassword;
            string sql = _config.SQLQueries.Product;

            // Format connection
            connectionString = String.Format(connectionString, serverName, databaseName, userId, password);

            // Pull in a lazy load version of the table
            IEnumerable<IDataRecord> azureSQLRows = Gateway_SQLServer.StreamData(connectionString, sql);
            var client = new Gateway_AzureTable(_config, "Product");
            client.TruncateTable();
     
            // Create 100 days of "history" for each row
            var startDate = new DateTime(2014, 1, 1);
            foreach (var row in azureSQLRows)
            {
                var list = new List<ProductEntity>();
                for (int i = 0; i < 100; i++)
                {
                    list.Add(ProductMapper.ConvertToEntity(row, startDate.AddDays(i)));
                }

                client.BulkInsert(list);
            }
        }
    }
}
