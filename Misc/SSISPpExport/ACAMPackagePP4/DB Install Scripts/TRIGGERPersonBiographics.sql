/*
	See important comments in Utility.LogDataChanges about this trigger.

	In general, this trigger should not be created manually. Read the comments in Utility.LogDataChanges.

	If you rename a table that has one of these triggers, please follow the directions in the comments in Utility.LogDataChanges.

	2012-06-17   Brian Harrison   Modified to account for change of table name.
	2012-05-23   Brian Harrison   Created.
*/
ALTER TRIGGER [Data].[“Person.PersonBiographics” table — Trigger — Log Data Changes]
	ON [Data].[Person.PersonBiographics]
		AFTER INSERT, UPDATE, DELETE
AS 
BEGIN

	SET NOCOUNT ON;

	IF Utility.[Security.DataChangeLoggingIsEnabled]() = 0
		RETURN;

    SELECT * INTO #Deleted FROM deleted;

	SELECT * INTO #Inserted FROM inserted;

	EXEC Utility.LogDataChanges
		@schemaName = 'Data',
		@tableName = 'Person.PersonBiographics';

	-- 2013-02-15  Rich Guidi    Calling [App.Sbo].[SSIS.AccessControlTransfers.Insert] to 
	--                           insert records in [Data].[AccessControl.Transfers*] tables for SSIS export.
    Declare @ObjectID as INT
	Declare @StaffID as INT
	SET @ObjectID = (SELECT PersonID from inserted);
	SET @StaffID = 0; -- there is no column which identifies who changed the record;
	EXEC [App.Sbo].[SSIS.AccessControlTransfers.Insert] 'Person', @ObjectID, @StaffID;

END
