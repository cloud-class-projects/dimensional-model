using DimensionalModelETL.DataLayer.Gateway;

namespace DimensionalModelETL.WorkflowLayer
{
    /// <summary>
    /// Executes a PowerShell script that generates a pig job
    /// </summary>
    public class WF_Azure_ExecutePigJob
    {
        private Configuration.Settings _settings;

        public WF_Azure_ExecutePigJob(Configuration.Settings settings)
        {
            _settings = settings;
        }

        public void ExecuteWF()
        {
            new Gateway_HDInsight(_settings).ProvisionCluster(1);
        }
    }
}
