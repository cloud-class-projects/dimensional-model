using System;
using DimensionalModelETL.DataLayer.Gateway;
using DimensionalModelETL.DataLayer.Model;

namespace DimensionalModelETL.WorkflowLayer
{
    /// <summary>
    /// Returns the contents of the product table from Azure Table Storage
    /// </summary>
    public class WF_ATS_QueryPartition
    {
        private Configuration.Settings _config;

        public WF_ATS_QueryPartition(Configuration.Settings config)
        {
            _config = config;
        }

        public void ExecuteWF()
        {
            var client = new Gateway_AzureTable(_config, "Product");

            var results = client.RetrievePartitionContents<ProductEntity>("20141213", 100);

            foreach (var item in results)
            {
                Console.Write(item.RowKey);
            }
        }
    }
}
