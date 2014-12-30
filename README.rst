Installation Instructions
==================================

1. Make sure that you have .NET 4.5 installed on the machine
2. Download the solution files from GitHub
3. Open the solution using Visual Studio 2010+
4. Build the project in Visual Studio
5. Copy the output of the bin\Debug folder to the following VM: 
   (This VM must be used because you have to manually allow IPs for Azure SQL database)
   (If this isn't acceptable then you will need to contact me with you IP so I can let you in.)
   (The settings file with the sensitive keys and RDP file on the VM.  Not on GitHub.  I can also send you the updated config file).
6. You may have to copy the assemblies as well.
6. Run the executable file.
7. Test one:

	a. Click Load AST Current button
	b. This will pull in a row from Azure SQL Database
	c. It will send that row to Azure Table Storage
	d. It will query retrieve the row from Azure Table Storage and display it in a window

8. Test two:

	a. Click Create Product from AST button
	b. This will pull in the first 100 partition keys
	c. It will transform the rows into date ranges suitable for a dimensional table
	d. Each partition key should output 5 rows with the proper date ranges
	e. The results will be uploaded to Azure SQL Database
	f. The application will query the result table and display them on the screen

Notes
==============

The tests are pulling in small amounts of rows.  This is intentional.  If they work with a few rows they will work on many rows.  
Plus the VM is fairly tiny and can't handle a large workload.

Some of the buttons are disabled.  This is because clicking some of them would wipe out my carefully constructed data and begin a multi hour ETL process.  The HDInsight Cluster creation button is disabled because it requires account credentials and it takes 30+ minutes just to make the cluster.

Code Documentation
=============================

The C# application contains the following projects:
DimensionalModelETL					Contains the GUI forms.
DimensionalModelETL.DataLayer		Contains the classes for interacting with the external systems.
DimensionalModelETL.WorkflowLayer	Contains the WorkFlow logic for the application tasks.
DimensionalModelETL.SSIS			Contains a package used to upload the test data from a flat file into Azure SQL Database
DimensionalModelETL.Configuration	Contains classes that will encapsulate the settings file.
DimensionalModelETL.Test			Has a few unit tests.  Many were deleted. 

Application Structure:

DimensionalModelETL
	These are the WinForm objects.  These would not be present in an actual ETL application.  They are here to make testing easy.
	
DimensionalModelETL.DataLayer
	DataMappers
		These classes are used to convert from one object to another.
	Gateways
		These classes abstract external IO systems from the rest of the application.
	Model
		This contains the applications object model.  Right now it just has the Product object.
		
DimensionalModelETL.WorkflowLayer
	Each workflow contains a single public method that accepts no arguments.  These workflows are essentially the Command object pattern.
	This is where the vast majority of the control logic lives.
DimensionalModelETL.SSIS
	Holds one SSIS package that does the initial load into Azure SQL.  Requires SQL Server to execute, but isn't needed for the test.
DimensionalModelETL.Configuration
	I don't like using the app.config file.  I extracted all the sensative data into a Configuration.xml class.
	This assembly deserializes the xml file into a CLR object.  This makes it much easier to use.
DimensionalModelETL.Test
	I feel bad about this one.  It is more sparse than it should be.
	90% of this application is IO logic which is very hard to properly unit test.
