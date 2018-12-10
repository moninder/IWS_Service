USE [CS_BOSD -- Dev]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  StoredProcedure [App.Sbo].[IWS.BiometricUpdate]    Script Date: 09/21/2012 08:20:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[App.Sbo].[IWS.BiometricUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [App.Sbo].[IWS.BiometricUpdate]
GO

/*
	Updates the signature image for a given person.

	09/14/2012	Lance Atchison	Created.
*/
CREATE PROCEDURE [App.Sbo].[IWS.BiometricUpdate]
(
	@PersonGUID uniqueidentifier = NULL
	,@SignatureImage varbinary(max) 
)
AS
BEGIN
	-- returns the number of affected rows.
	UPDATE	[Data].[Person.AuthorizedSigners] 
	SET		SignatureImage = @SignatureImage
	FROM	[Data].[Person.AuthorizedSigners] as DPA
	INNER JOIN	[Data].[Person.Persons] as DPP
		ON DPA.PersonID = DPP.PersonID
	INNER JOIN 	[Data].[Person.PersonDivisionXref] as DPPDX
		ON DPP.PersonID = DPPDX.PersonID 
		AND DPA.DivisionID = DPPDX.DivisionID
	WHERE	DPP.PersonID_IWS = @PersonGUID
END
GO


/****** Object:  StoredProcedure [App.Sbo].[IWS.ExpireBadge]    Script Date: 09/21/2012 08:21:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[App.Sbo].[IWS.ExpireBadge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [App.Sbo].[IWS.ExpireBadge]
GO

/*
	Sets a given badge to 'expired'
	
	09/18/2012	Lance Atchison	Created.
*/
CREATE PROCEDURE [App.Sbo].[IWS.ExpireBadge]
(
	@IWS_PersonGUID uniqueidentifier = NULL,
	@IWS_CardID int
)
AS
BEGIN
	DECLARE @thisInstant DATETIME
	DECLARE @StaffID int


	SET @thisInstant = GETDATE()
	SET @StaffID = 9999				-- FIX THIS ATCHISON - we have to set up a Staff person that is CS System and connect these record to it

	-- 'expire' the active badge period
	UPDATE	DPBSP 
	SET		WhenExpires = @thisInstant   
	FROM	[Data].[Person.BadgeStatusPeriods] as DPBSP
	INNER JOIN [Data].[Person.Persons] AS DPP
	ON DPP.PersonID_IWS = @IWS_PersonGUID
	INNER JOIN 	[Data].[Person.PersonDivisionXref] AS DPPDX
	ON DPPDX.PersonID = DPP.PersonID
	INNER JOIN [Data].[Person.Badges] AS DPB
	ON DPPDX.PersonDivisionXrefID = DPB.PersonDivisionXrefID
	AND	DPBSP.BadgeID = DPB.BadgeID
	AND DPB.BadgeID_IWS = @IWS_CardID
	WHERE DPBSP.Whenexpires = '12/31/9999'	
	--AND		DPBSP.BadgeStatusCode = ‘ACTV’   don’t require Active, it should be an active badge but it’s possible the badge is Lost or Invalid.
	
	-- 'activate' the expired badge period
	INSERT INTO [Data].[Person.BadgeStatusPeriods]
	(WhenBecomesActive, WhenExpires, BadgeStatusCode, StaffID, BadgeID)
	VALUES	(@thisInstant, '12/31/9999', 'EXPR', @StaffID,
			(	SELECT BadgeID
				FROM [Data].[Person.Badges]
				WHERE BadgeID_IWS = @IWS_CardID )
	)


END
GO


/****** Object:  StoredProcedure [App.Sbo].[IWS.InitiateBackgroundCheck]    Script Date: 09/21/2012 08:22:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[App.Sbo].[IWS.InitiateBackgroundCheck]') AND type in (N'P', N'PC'))
DROP PROCEDURE [App.Sbo].[IWS.InitiateBackgroundCheck]
GO

/*
	IWS has initiated a BC with TSC, so we have to INSERT a record into TSCTransactions

	09/20/2012	Lance Atchison	Created.
*/
CREATE PROCEDURE [App.Sbo].[IWS.InitiateBackgroundCheck]
(
	@IWS_PersonGUID uniqueidentifier = NULL,
	@TSCTransactionTypeID int,
	@TransactionControlNumber VARCHAR(25),
	@TransactionDate DATETIME,
	@ProgramIdentification VARCHAR(10),
	@ResponseIdentification VARCHAR(20),
	@Status VARCHAR(20),
	@StatusText VARCHAR(max),
	@xmlData XML,
	@Direction VARCHAR(15)
)
AS
BEGIN
	INSERT INTO [Data].[BackgroundCheck.TSCTransactions]
		(TransactionDate, 
		ProgramIdentification,
		ResponseIdentification,
		TransactionControlNumber,
		TSCTransactionTypeID,
		Status,
		StatusText,
		xmlData,
		Direction,
		PersonDivisionCheckID)
	VALUES	
		(@TransactionDate, 
		@ProgramIdentification,
		@ResponseIdentification,
		@TransactionControlNumber,
		@TSCTransactionTypeID,
		@Status,
		@StatusText,
		@xmlData,
		@Direction,
			(SELECT  PersonDivisionCheckID
			FROM	[Data].[BackgroundCheck.PersonDivisionChecks] AS PDC
			INNER JOIN [Data].[Person.PersonDivisionXref] AS PDX
			ON		PDX.PersonDivisionXrefID = PDC.PersonDivisionXrefID
			INNER JOIN [Data].[Person.Persons] AS PP
			ON		PDX.PersonID = PP.PersonID
			AND		PP.PersonID_IWS = @IWS_PersonGUID)
		)

END
GO

/****** Object:  StoredProcedure [App.Sbo].[Iws.IsDbAlive]    Script Date: 09/21/2012 08:22:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[App.Sbo].[Iws.IsDbAlive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [App.Sbo].[Iws.IsDbAlive]
GO

/*
	08/22/2012   Michael Villaronga	 Created
	
	Used to validate DB Connectivity from the IWS Web service through to the client
*/
CREATE PROCEDURE [App.Sbo].[Iws.IsDbAlive]
WITH RECOMPILE
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT 'Success'

END
GO

/****** Object:  StoredProcedure [App.Sbo].[IWS.UpdateBackgroundCheck]    Script Date: 09/21/2012 08:22:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[App.Sbo].[IWS.UpdateBackgroundCheck]') AND type in (N'P', N'PC'))
DROP PROCEDURE [App.Sbo].[IWS.UpdateBackgroundCheck]
GO

/*
	IWS has received data on a BC from and agency, so we have to INSERT a record into AgencyTransactions

	09/20/2012	Lance Atchison	Created.
*/
CREATE PROCEDURE [App.Sbo].[IWS.UpdateBackgroundCheck]
(
	@IWS_PersonGUID uniqueidentifier = NULL,
	@AgencyCode VARCHAR(5),
	@CheckTypeCode VARCHAR(5),
	@TransactionTypeCode VARCHAR(5),
	@TransactionDate DATETIME,
	@TransactionControlNumber VARCHAR(25),
	@Result VARCHAR(40),
	@ResultDate DATETIME,
	@ResultDetails VARCHAR(max),
	@ResultDetailDate DATETIME
	)
AS
BEGIN
	INSERT INTO [Data].[BackgroundCheck.AgencyTransactions]
		(AgencyCode,
		CheckTypeCode,
		TransactionTypeCode,
		TransactionDate,
		TransactionControlNumber,
		Result,
		ResultDate,
		ResultDetails,
		ResultDetailDate,
		PersonDivisionCheckID)
	VALUES	
		(@AgencyCode,
		@CheckTypeCode,
		@TransactionTypeCode, 
		@TransactionDate, 
		@TransactionControlNumber, 
		@Result,
		@ResultDate, 
		@ResultDetails,
		@ResultDetailDate,
			(SELECT  PersonDivisionCheckID
			FROM	[Data].[BackgroundCheck.PersonDivisionChecks] AS PDC
			INNER JOIN [Data].[Person.PersonDivisionXref] AS PDX
			ON		PDX.PersonDivisionXrefID = PDC.PersonDivisionXrefID
			INNER JOIN [Data].[Person.Persons] AS PP
			ON		PDX.PersonID = PP.PersonID
			AND		PP.PersonID_IWS = @IWS_PersonGUID)
		)

END
GO

/****** Object:  StoredProcedure [App.Sbo].[IWS.UpdateBadgeID]    Script Date: 09/21/2012 08:23:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[App.Sbo].[IWS.UpdateBadgeID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [App.Sbo].[IWS.UpdateBadgeID]
GO


/*
	Updates IWS' BadgeID for a given BOAA Person/Badge.

	09/18/2012	Lance Atchison	Created.
*/
CREATE PROCEDURE [App.Sbo].[IWS.UpdateBadgeID]
(
	@BOAA_BadgeID int
	, @IWS_CardID int
)
AS
BEGIN
	-- returns the number of affected rows.
	UPDATE [Data].[Person.Badges] 
	SET		BadgeID_IWS = @IWS_CardID
	WHERE	BadgeID = @BOAA_BadgeID
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[App.Sbo].[IWS.GetProvisionedAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [App.Sbo].[IWS.GetProvisionedAccess]
GO

/*
	Get the access for a badge
	
	09/25/2012	Lance Atchison	Created.
*/
CREATE PROCEDURE [App.Sbo].[IWS.GetProvisionedAccess]
(
	@IWS_CardID int
)
AS
BEGIN
	SELECT 
			IWS_CardID=DPB.BadgeID_IWS, 
			PIN=DPPBIO.PIN,
			CategoryName=DAC.CategoryName, 
			CategoryID=DAC.SourceCategoryPKID,
			AccessType=DPBAP.AccessType, 
			WhenBecomesActive=DPBAP.WhenBecomesActive, 
			WhenExpires=DPBAP.WhenExpires
	FROM [Data].[Person.Persons] AS DPP
	INNER JOIN 	[Data].[Person.PersonDivisionXref] AS DPPDX
	ON DPPDX.PersonID = DPP.PersonID
	INNER JOIN 	[Data].[Person.PersonBiographics] AS DPPBIO
	ON DPPBIO.PersonID = DPP.PersonID
	INNER JOIN [Data].[Person.Badges] AS DPB
	ON DPPDX.PersonDivisionXrefID = DPB.PersonDivisionXrefID
	INNER JOIN [Data].[Person.BadgeAccessPeriods] as DPBAP
	ON DPB.BadgeID = DPBAP.BadgeID
	INNER JOIN [Data].[Access.Categories] as DAC
	ON DPBAP.CategoryID = DAC.CategoryID
	WHERE DPB.BadgeID_IWS = @IWS_CardID
END
GO
