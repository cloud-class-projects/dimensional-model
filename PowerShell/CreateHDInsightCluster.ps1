Param(
	[string]$storageAccountName,	# Azure storage account that hosts the default container.
	[string]$containerName,         # Azure Blob container that is used as the default file system for the HDInsight cluster.
	[string]$clusterName,         	# The name you will name your HDInsight cluster.
	[string]$location,             	# The location of the HDInsight cluster. It must in the same data center as the storage account.
	[int]$clusterNodes, 			# The number of nodes in the HDInsight cluster.
	[string]$storageAccountKey,		# AccountKey to the specified storage account
	[string]$HDUserName,			# This will create the credentials to log into the cluster
	[string]$HDIPassword			# This will create the credentials to log into the cluster
)

# Create a new HDInsight cluster
$secpasswd = ConvertTo-SecureString $HDIPassword -AsPlainText -Force
$defCred = New-Object System.Management.Automation.PSCredential ($HDUserName, $secpasswd)

# Load certificate
$mgmtCert = Get-Item cert:\\CurrentUser\My\9DB6F17DA89A317C6A86CF3E25EF4FE38F86B682

# Create cluster
New-AzureHDInsightCluster -Name $clusterName -Location $location -DefaultStorageAccountName "$storageAccountName.blob.core.windows.net" -DefaultStorageAccountKey $storageAccountKey -DefaultStorageContainerName $containerName  -ClusterSizeInNodes $clusterNodes -Certificate $mgmtCert -Credential $defCred
