using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Serialization;
using DimensionalModelETL.DataLayer.DataMappers;
using DimensionalModelETL.DataLayer.Gateway;
using DimensionalModelETL.DataLayer.Model;

namespace DimensionalModelETL.WorkflowLayer
{
    /// <summary>
    /// Transfers the contents of the product table to Azure Table Storage
    /// </summary>
    public class WF_ATS_UploadCurrentProduct
    {
        private Configuration.Settings _config;
        private Action<string> _response;

        public WF_ATS_UploadCurrentProduct(Configuration.Settings config, Action<string> response)
        {
            _config = config;
            _response = response;
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

            string partitionKey = "";
            foreach (var row in azureSQLRows.Take(1))
            {
                var list = new List<ProductEntity>();
                list.Add(ProductMapper.ConvertToEntity(row, DateTime.Today));
                partitionKey = list[0].PartitionKey;

                client.BulkUpsert(list);
            }


            // Display 1st row
            var result = client.RetrievePartitionContents<ProductEntity>(partitionKey, 1).First();
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(result.GetType());
            serializer.Serialize(stringwriter, result);
            _response(stringwriter.ToString());
        }
    }
}
