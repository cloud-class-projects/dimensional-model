using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DimensionalModelETL.DataLayer.DataMappers;
using DimensionalModelETL.DataLayer.Gateway;
using DimensionalModelETL.DataLayer.Model;

namespace DimensionalModelETL.WorkflowLayer
{
    /// <summary>
    /// Pull in Azure Table Storage rows to create Product Dimension
    /// </summary>
    public class WF_ATS_CreateProductDimension
    {
        private Configuration.Settings _settings;
        private Gateway_AzureTable _tableGateway;
        private ProductDimensionMapper _mapper;
        private BlockingCollection<int> _batchQueue;
        private Action<DataTable> _response;

        public WF_ATS_CreateProductDimension(Configuration.Settings settings, Action<DataTable> response)
        {
            _settings = settings;
            _tableGateway = new Gateway_AzureTable(_settings, "Product");
            _mapper = new ProductDimensionMapper();
            _batchQueue = new BlockingCollection<int>(8); // Go with a higher count due to latencies
            _response = response;
        }


        public void ExecuteWF()
        {
            // Pull in connection info
            string connectionString = _settings.SQLServerConnectionString;
            string serverName = _settings.SQLServerName;
            string databaseName = _settings.SQLServerDatabase;
            string userId = _settings.SQLServerUserID;
            string password = _settings.SQLServerPassword;
            string sql = _settings.SQLQueries.DistinctProduct;
            connectionString = String.Format(connectionString, serverName, databaseName, userId, password);

            // Truncate the table before loading
            Gateway_SQLServer.TruncateTable(connectionString, "dw.dimProduct");

            // Pull in a lazy load version of the table
            IEnumerable<IDataRecord> azureSQLRows = Gateway_SQLServer.StreamData(connectionString, sql);

            // Stream results to table
            foreach (var sqlRow in azureSQLRows)
            {
                // Attempt to reserve a spot in queue
                _batchQueue.Add(0);
                Action<string> del = partitionKey =>
                    {
                        var queryResults = _tableGateway.RetrievePartitionContents<ProductEntity>(partitionKey);
                        var sqlRows = _mapper.CreateTypeIIDimension(queryResults, new List<string> { "SalesType" });

                        Gateway_SQLServer.InsertRecords(connectionString, sqlRows, "dw.dimProduct");
                        _batchQueue.Take(); // remove reservation from queue
                    };

                var key = sqlRow[0].ToString();

                Task.Run(()=> del(key));
            }

            // Output results to window
            DataTable dt = Gateway_SQLServer.QueryData(connectionString, _settings.SQLQueries.dimProduct);
            _response(dt);
        }
    }
}
