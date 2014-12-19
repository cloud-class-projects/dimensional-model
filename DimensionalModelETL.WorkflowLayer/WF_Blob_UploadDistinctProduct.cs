using System;
using System.Collections.Generic;
using System.Data;
using DimensionalModelETL.DataLayer.Gateway;

namespace DimensionalModelETL.WorkflowLayer
{
    /// <summary>
    /// Creates a list of distinct products for the ATS Map Reduce job
    /// </summary>
    public class WF_Blob_UploadDistinctProduct
    {
        private Configuration.Settings _config;

        public WF_Blob_UploadDistinctProduct(Configuration.Settings config)
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

            connectionString = String.Format(connectionString, serverName, databaseName, userId, password);


            // Pull in a lazy load version of the table
            IEnumerable<IDataRecord> azureSQLRows = Gateway_SQLServer.StreamData(connectionString, sql);

            // Intialize blob storage
            var client = new Gateway_AzureBlob(_config, "dimensionalmodelblobcontainer");

            // Create 100 days of "history" for each row and load into a text file
            var startDate = new DateTime(2014, 1, 1);
            string file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\TempFile.txt";
            using (var stream = new System.IO.StreamWriter(file))
            {
                foreach (var row in azureSQLRows)
                {
                    stream.Write(row["RowKey"].ToString() + Environment.NewLine);
                }
            }

            client.UploadData(file, "distinctproduct");
        }
    }
}
