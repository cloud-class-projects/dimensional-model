using DimensionalModelETL.DataLayer.Gateway;

namespace DimensionalModelETL.WorkflowLayer
{
    /// <summary>
    /// Creates a HDInsight cluster
    /// </summary>
    public class WF_Azure_CreateHDInsightCluster
    {
        private Configuration.Settings _config;

        public WF_Azure_CreateHDInsightCluster(Configuration.Settings config)
        {
            _config = config;
        }

        public void ExecuteWF()
        {
            // Use PowerShell to create a new cluster
            new Gateway_HDInsight(_config).ProvisionCluster(1);
        }
    }
}
