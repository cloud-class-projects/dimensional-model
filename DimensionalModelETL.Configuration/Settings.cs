
namespace DimensionalModelETL.Configuration
{
    /// <summary>
    /// Holds the settings information
    /// </summary>
    public class Settings
    {
        public string KVPAccountName { get; set; }
        public string KVPAccountKey { get; set; }
        public string KVPConnectionString { get; set; }
        
        public string BlobAccountName {get; set;}
        public string BlobAccountKey {get; set;}
        public string BlobContainerName { get; set; }
        public string BlobConnectionString {get; set;}
        
        public string SubscriptionID {get; set;}
	    public string HDICertificateName {get; set;}
	    public string HDIClusterName {get; set;}
	    public string HDIUserID {get; set;}
        public string HDIPassword { get; set; }

        public string SQLServerName {get; set;}
        public string SQLServerDatabase {get; set;}
        public string SQLServerUserID {get; set;}
        public string SQLServerPassword {get; set;}
        public string SQLServerConnectionString {get; set;}
        public SQL SQLQueries { get; set; }

        public class SQL
        {
            public string Product { get; set; }
            public string dimProduct { get; set; }
            public string DistinctProduct { get; set; }
        }
    }
}
