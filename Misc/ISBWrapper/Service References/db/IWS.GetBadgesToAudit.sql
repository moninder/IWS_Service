USE [CS_BOSD -- Dev]
GO

/****** Object:  StoredProcedure [App.Sbo].[IWS.BiometricUpdate]    Script Date: 10/15/2012 16:15:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[App.Sbo].[IWS.GetBadgesToAudit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [App.Sbo].[IWS.GetBadgesToAudit]
GO

USE [CS_BOSD -- Dev]
GO

/****** Object:  StoredProcedure [App.Sbo].[IWS.GetBadgesToAudit]    Script Date: 10/15/2012 16:15:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
	Return BadgeIDs that need audit.

	11/15/2012	Lance Atchison	Created.
	
	EXEC [App.Sbo].[IWS.GetBadgesToAudit] @THRESHOLD=366
	
*/
CREATE PROCEDURE [App.Sbo].[IWS.GetBadgesToAudit]
(
	@THRESHOLD INT 
)
AS
BEGIN
	select BadgeID
	from [Data].[Audit.CMSBadges]
	where ( 
			(Status < 1)
			or 
			( (Status = 1) and DATEDIFF(DAY, LastAudited, GETDATE()) >= @THRESHOLD)
		)
	order by Status
END

GO


