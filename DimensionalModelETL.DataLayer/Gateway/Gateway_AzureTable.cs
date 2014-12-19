using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace DimensionalModelETL.DataLayer.Gateway
{
    /// <summary>
    /// Mediates between application and Azure Table Storage
    /// </summary>
    public class Gateway_AzureTable
    {
        private CloudTableClient _client;
        private CloudTable _table;
        private BlockingCollection<int> _batchQueue;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public Gateway_AzureTable(Configuration.Settings settings, string tableName)
        {
            // Initialize client
            string accountName = settings.KVPAccountName;
            string accountKey = settings.KVPAccountKey;
            string connectionString = settings.KVPConnectionString;
            connectionString = String.Format(connectionString, accountName, accountKey);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            _client = storageAccount.CreateCloudTableClient();
            _table = _client.GetTableReference(tableName);
            _table.CreateIfNotExists();

            // Initialize bulk queue
            _batchQueue = new BlockingCollection<int>(8);
        }


        /// <summary>
        /// Truncates the table
        /// Note that this operation can leave the table in an invalid state for
        /// some amount of time
        /// </summary>
        public void TruncateTable()
        {
            //_table.DeleteIfExists();
            _table.CreateIfNotExists();
        }


        /// <summary>
        /// Bulk inserts a set of entities
        /// The entity list is capped at 100
        /// </summary>
        /// <typeparam name="T">The entity that the table is based on.</typeparam>
        /// <param name="entities">A list of up to 100 entities</param>
        public void BulkInsert<T>(List<T> entities) where T : TableEntity, new()
        {
            var batch = new TableBatchOperation();
            foreach(T item in entities)
            {
                batch.Insert(item);
            }

            _batchQueue.Add(0);
            Task.Run( ()=> 
            {
                _table.ExecuteBatch(batch);
                _batchQueue.Take();
            });
            
        }


        /// <summary>
        /// Performs an upsert on a list of entities
        /// </summary>
        public void BulkUpsert<T>(List<T> entities) where T : TableEntity, new()
        {
            var batch = new TableBatchOperation();
            foreach(T item in entities)
            {
                batch.InsertOrMerge(item);
            }

            _batchQueue.Add(0);
            Task.Run( ()=> 
            {
                _table.ExecuteBatch(batch);
                _batchQueue.Take();
            });
        }


        /// <summary>
        /// Returns all rows in a given partition
        /// </summary>
        /// <typeparam name="T">The entity that the table is based on.</typeparam>
        /// <param name="partitionKey">The partition key to scan.</param>
        /// <param name="maxResults">Caps the amount of results to return.  0 means unlimited</param>
        /// <returns>Results of query</returns>
        public IEnumerable<T> RetrievePartitionContents<T>(string partitionKey, int maxResults=0) where T : TableEntity, new()
        {
            var query = new TableQuery<T>()
                .Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey))
                    .Take(maxResults==0 ? int.MaxValue : maxResults);

            return _table.ExecuteQuery<T>(query);
        }
    }
}
