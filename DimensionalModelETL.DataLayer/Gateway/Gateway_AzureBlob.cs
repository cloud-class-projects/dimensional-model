using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DimensionalModelETL.DataLayer.Gateway
{
    /// <summary>
    /// Mediates between application and Azure Blob storage
    /// </summary>
    public class Gateway_AzureBlob
    {
        private CloudBlobContainer _container;

        /// <summary>
        /// Constructor
        /// </summary>
        public Gateway_AzureBlob(Configuration.Settings settings, string container)
        {
            // Initialize client
            string accountName = settings.BlobAccountName;
            string accountKey = settings.BlobAccountKey;
            string connectionString = settings.BlobConnectionString;
            connectionString = String.Format(connectionString, accountName, accountKey);

            _container = CloudStorageAccount
                        .Parse(connectionString)
                        .CreateCloudBlobClient()
                        .GetContainerReference(container);
        }


        /// <summary>
        /// Uploads a file to blob storage
        /// </summary>
        public void UploadData(string uri, string blobName)
        {
            // Retrieve reference to a blob
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blobName);

            blockBlob.UploadFromFile(uri, System.IO.FileMode.Open);
        }
    }
}
