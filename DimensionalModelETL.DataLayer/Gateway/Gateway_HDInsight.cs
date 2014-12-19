using System;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace DimensionalModelETL.DataLayer.Gateway
{
    public class Gateway_HDInsight
    {
        private Configuration.Settings _settings;

        /// <summary>
        /// Constructor
        /// </summary>
        public Gateway_HDInsight(Configuration.Settings settings)
        {
            _settings = settings;
        }


        /// <summary>
        /// Call powershell script to create cluster
        /// </summary>
        public void ProvisionCluster(int clusterSize)
        {
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                // Pass parameters to the powershell file
                string script = String.Format(@"C:\Users\bnielsen\OneDrive\College\OpenSource\HDInsight\DimensionalModelETL\PowerShell\CreateHDInsightCluster.ps1 "
                    + "-storageAccountName \"{0}\" "
                    + "-containerName \"{1}\" "
                    + "-clusterName \"{2}\" "
                    + "-location \"{3}\" "
                    + "-clusterNodes {4} "
                    + "-storageAccountKey {5} "
                    + "-HDUserName \"{6}\" "
                    + "-HDIPassword \"{7}\" ",
                    _settings.BlobAccountName,
                    _settings.BlobContainerName,
                    "braenielDimensionalCluster",
                    "North Central US",
                    clusterSize,
                    _settings.BlobAccountKey,
                    _settings.HDIUserID,
                    _settings.HDIPassword);

                powerShellInstance.AddScript(script);

                Collection<PSObject> outputList = powerShellInstance.Invoke();
            }
        }


        /// <summary>
        /// Executes a pig job via PowerShell
        /// </summary>
        public void ExecutePigJob()
        {
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                // Pass parameters to the powershell file
                string script = String.Format(@"C:\Users\bnielsen\OneDrive\College\OpenSource\HDInsight\DimensionalModelETL\PowerShell\ExecutePigJob.ps1"
                    + "-clusterName \"{0}\" "
                    + "-HDUserName \"{1}\" "
                    + "-HDIPassword \"{2}\" ",
                    "braenielDimensionalCluster",
                    _settings.HDIUserID,
                    _settings.HDIPassword);

                powerShellInstance.AddScript(script);

                Collection<PSObject> outputList = powerShellInstance.Invoke();
            }
        }
    }
}
