/****** Object:  StoredProcedure [App.Sbo].[SSIS.AccessControlTransfers.Insert]    Script Date: 3/12/2013 10:12:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Stored procedure to insert AccessControlTransfers* records

	02/15/2013 Rich Guidi  Created.
*/
ALTER PROCEDURE [App.Sbo].[SSIS.AccessControlTransfers.Insert]
(@SubjectArea varchar(10), -- eg (Person, Badge, Division)
 @ObjectID int, -- eg PersonID, BadgeID, DivisionID
 @StaffID int --user triggering the data update
)
AS

BEGIN
/*
	Called by triggers on tables:
	- Person.PersonBiographics @SubjectArea=Person, @ObjectID=PersonID
	- Person.Badges            @SubjectArea=Badge, @ObjectID=BadgeID
	- Division                 @SubjectArea=Division, @ObjectID=DivisionID

	Inserts or changes to these tables will result in inserts to the appropriate transfers tables 
	-if- an open record does not already exist 
	(TRANSMIT START is null for the data type for matching TransferPersons or TransfersDivisions record)

	NOTE: In the SSIS package, during processing, the following updates occur:
		UPDATE [Data].[AccessControl.Transfers].[TransmitStart]
		UPDATE [Data].[AccessControl.TransferPersons].[WhenTransmitted]
		UPDATE [Data].[AccessControl.TransferBadges].[WhenTransmitted]
		UPDATE [Data].[AccessControl.TransferDivisions].[WhenTransmitted]
		UPDATE [Data].[AccessControl.Transfers].[TransmitEnd]

*/
	DECLARE @TransferId int
	DECLARE @TransferIdCnt int
	DECLARE @ErrMsg VARCHAR(200);
	DECLARE 
		@errorMessage   nvarchar(4000),
		@errorNumber    int,
		@errorSeverity  int,
		@errorState     int,
		@errorLine      int,
		@errorProcedure nvarchar(200);

	SET @ErrMsg = '';

	    -- Check to see if an open item already exists.
		--(TRANSMIT START is null for the data type for matching TransferPersons or TransfersDivisions record)
		SELECT @TransferIdCnt = sum(cnt) from (
			SELECT count(*) as cnt
			FROM [Data].[AccessControl.Transfers] t
			INNER JOIN [Data].[AccessControl.TransferPersons] p on p.TransferId = t.TransferId
			WHERE IsNull(t.[TransmitStart],'') = ''
			and t.[DataType] = @SubjectArea
			and p.PersonID = @ObjectID
			UNION
			SELECT count(*) as cnt
			FROM [Data].[AccessControl.Transfers] t
			INNER JOIN [Data].[AccessControl.TransferBadges] p on p.TransferId = t.TransferId
			WHERE IsNull(t.[TransmitStart],'') = ''
			and t.[DataType] = @SubjectArea
			and p.BadgeID = @ObjectID
			UNION
			SELECT count(*) as cnt
			FROM [Data].[AccessControl.Transfers] t
			INNER JOIN [Data].[AccessControl.TransferDivisions] d on d.TransferId = t.TransferId
			WHERE IsNull(t.[TransmitStart],'') = ''
			and t.[DataType] = @SubjectArea
			and d.DivisionID = @ObjectID
		) a

		If (@TransferIdCnt = 0)
			BEGIN
				BEGIN TRY
					INSERT INTO [Data].[AccessControl.Transfers]([DataType],[WhenCreated],[StaffID])
					VALUES(@SubjectArea,getdate(),@StaffID)

					SET @TransferId = SCOPE_IDENTITY();
				END TRY
				BEGIN CATCH
					SET @errorNumber = Error_Number();
					SET @errorSeverity = Error_Severity();
					SET @errorState = Error_State();
					SET @errorLine = Error_Line();
					SET @ErrMsg = Left('Error: '+ @SubjectArea + ' ID=' + convert(varchar,@ObjectID) +': ' + ERROR_MESSAGE(),200);
				END CATCH

				If (@ErrMsg = '')
					BEGIN
						If (@SubjectArea = 'Person')
							BEGIN TRY
								--If a person or badge change ...
								INSERT INTO [Data].[AccessControl.TransferPersons]([TransferID],[PersonID])
								VALUES (@TransferId ,@ObjectID)
							END TRY
							BEGIN CATCH
								SET @errorNumber = Error_Number();
								SET @errorSeverity = Error_Severity();
								SET @errorState = Error_State();
								SET @errorLine = Error_Line();
								SET @ErrMsg = Left('TransferPersons Error: ID=' + convert(varchar,@ObjectID) +': ' + ERROR_MESSAGE(),200);
							END CATCH

						Else If (@SubjectArea = 'Badge')
							BEGIN TRY
								--If a Badge change ...
								INSERT INTO [Data].[AccessControl.TransferBadges]([TransferID],[BadgeID])
								VALUES (@TransferId,@ObjectID)
							END TRY
							BEGIN CATCH
								SET @errorNumber = Error_Number();
								SET @errorSeverity = Error_Severity();
								SET @errorState = Error_State();
								SET @errorLine = Error_Line();
								SET @ErrMsg = Left('TransferBadges Error: ID=' + convert(varchar,@ObjectID) +': ' + ERROR_MESSAGE(),200);
							END CATCH

						Else If (@SubjectArea = 'Division')
							BEGIN TRY
								--If a Division change ...
								INSERT INTO [Data].[AccessControl.TransferDivisions]([TransferID],[DivisionID])
								VALUES (@TransferId,@ObjectID)
							END TRY
							BEGIN CATCH
								SET @errorNumber = Error_Number();
								SET @errorSeverity = Error_Severity();
								SET @errorState = Error_State();
								SET @errorLine = Error_Line();
								SET @ErrMsg = Left('TransferDivisions Error: ID=' + convert(varchar,@ObjectID) +': ' + ERROR_MESSAGE(),200);
							END CATCH
					END
			END --@TransferIdCnt = 0
	
	If (@ErrMsg <> '')
		BEGIN
			SET @errorProcedure = 'SSIS.AccessControlTransfers.Insert';
			SET @errorMessage = @ErrMsg;

			    RAISERROR ( @errorMessage, 
							@errorSeverity, 
							1,               
							@errorNumber,
							@errorSeverity,
							@errorState,
							@errorProcedure,
							@errorLine);
		END

END
