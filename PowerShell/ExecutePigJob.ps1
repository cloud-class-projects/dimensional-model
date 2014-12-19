Param(
	[string]$clusterName,         	# The name you will name your HDInsight cluster.
	[string]$HDUserName,			# This will create the credentials to log into the cluster
	[string]$HDIPassword			# This will create the credentials to log into the cluster
)
$statusFolder = "/PigDimensionalResult"  #Specify the folder to dump results

$0 = '$0';
$QueryString =  "ROWS = LOAD 'wasb://dimensionalmodelblobcontainer@dimensionalmodelblob.blob.core.windows.net/product' USING PigStorage(',');" +
                "KEYS = FOREACH data GENERATE $0 AS key:chararray, $1 AS key2:chararray, TOBAG($2 ..) AS bow:{(pair)};" +
                "FILTEREDKEYS = FILTER ROWS by KEYS is not null;" +
                "GROUPEDLEVELS = GROUP FILTEREDKEYS by KEYS;" +
                "DUMP RESULT;" 

$pigJobDefinition = New-AzureHDInsightPigJobDefinition -Query $QueryString -StatusFolder $statusFolder 

#Credentials
$secpasswd = ConvertTo-SecureString $HDIPassword -AsPlainText -Force
$creds = New-Object System.Management.Automation.PSCredential ($HDUserName, $secpasswd)

# Submit the Pig job
$pigJob = Start-AzureHDInsightJob -Cluster $clusterName -JobDefinition $pigJobDefinition #-Credential $creds

# Wait for the Pig job to complete
Wait-AzureHDInsightJob -Job $pigJob -WaitTimeoutInSeconds 3600

# Print the standard error and the standard output of the Pig job.
Get-AzureHDInsightJobOutput -Cluster $clusterName -JobId $pigJob.JobId -StandardOutput


