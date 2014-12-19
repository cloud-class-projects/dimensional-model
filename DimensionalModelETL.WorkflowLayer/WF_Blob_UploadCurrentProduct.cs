using System;
using System.Collections.Generic;
using System.Data;
using DimensionalModelETL.DataLayer.DataMappers;
using DimensionalModelETL.DataLayer.Gateway;

namespace DimensionalModelETL.WorkflowLayer
{
    /// <summary>
    /// Transfers the contents of the product table to Azure Table Storage
    /// </summary>
    public class WF_Blob_UploadCurrentProduct
    {
        private Configuration.Settings _config;

        public WF_Blob_UploadCurrentProduct(Configuration.Settings config)
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
            int count = 0;
            var startDate = new DateTime(2014, 1, 1);
            using (var stream = new System.IO.StreamWriter(@"C:\TempFile.txt"))
            {
                foreach (var row in azureSQLRows)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        stream.Write(ProductMapper.ConvertToCSV(row, startDate.AddDays(i)));
                    }

                
                    count += 100;
                    if (count % 10000 == 0)
                    {
                        Console.WriteLine(count);
                    }
                }
            }

            Console.Write("Starting upload.");
            client.UploadData(@"C:\TempFile.txt", "productsnapshot");
        }
    }
}
