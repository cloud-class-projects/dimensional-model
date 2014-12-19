Param(
	[string]$clusterName,         	# The name you will name your HDInsight cluster.
	[string]$HDUserName,			# This will create the credentials to log into the cluster
	[string]$HDIPassword			# This will create the credentials to log into the cluster
)

clear
$clusterName ="https://braenieldimensionalcluster.azurehdinsight.net"
$HDUserName= "afeavegeafvv456z4"
$HDIPassword ="42890hTq2894th240!-n0893"

$mrMapper = "DimensionalModelETL.Streaming.Mapper.exe"
$mrReducer = "DimensionalModelETL.Streaming.Reducer.exe"
$mrMapperFile = "/streamingapp/DimensionalModelETL.Streaming.Mapper.exe"
$mrReducerFile = "/streamingapp/DimensionalModelETL.Streaming.Reducer.exe"
$mrConfigurationFile = "/streamingapp/Configuration.xml"
$mrInput = "wasb://dimensionalmodelblobcontainer@dimensionalmodelblob.blob.core.windows.net/distinctproduct"
$timestamp = Get-Date -Format "yyyyMMddHHss"
$mrOutput = "wasb://dimensionalmodelblobcontainer@dimensionalmodelblob.blob.core.windows.net/DimensionalModelResult/$timestamp.txt"
$mrStatusOutput = "/DimensionalModelResult/"

$mrJobDef = New-AzureHDInsightStreamingMapReduceJobDefinition -JobName "DimensionalModelJob" -StatusFolder $mrStatusOutput -Mapper $mrMapper -Reducer $mrReducer -InputPath $mrInput -OutputPath $mrOutput
$mrJobDef.Files.Add($mrMapperFile)
$mrJobDef.Files.Add($mrReducerFile)

Get-ChildItem "C:\Users\bnielsen\OneDrive\College\OpenSource\HDInsight\DimensionalModelETL\DimensionalModelETL.Streaming.Mapper\bin\Debug" -File | `
Foreach-Object{
    $mrJobDef.Files.Add("/streamingapp/$_")
}

$secpasswd = ConvertTo-SecureString $HDIPassword -AsPlainText -Force
$creds = New-Object System.Management.Automation.PSCredential ($HDUserName, $secpasswd)

$mrJob = Start-AzureHDInsightJob -Cluster $clusterName -Credential $creds -JobDefinition $mrJobDef
Wait-AzureHDInsightJob -Credential $creds -job $mrJob -WaitTimeoutInSeconds 3600
