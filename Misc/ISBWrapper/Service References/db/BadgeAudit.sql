USE [CS_BOSD -- Dev]
GO



/****** Object:  Table [Data].[Person.Badges]    Script Date: 10/15/2012 13:42:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Data].[Audit.CMSBadges]') AND type in (N'U'))
DROP TABLE [Data].[Audit.CMSBadges]
GO

USE [CS_BOSD -- Dev]
GO

/****** Object:  Table [Data].[Audit.CMSBadges]    Script Date: 10/15/2012 13:42:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [Data].[Audit.CMSBadges](
	[BadgeID] [int] NOT NULL,
	[LastAudited] [datetime] NULL,
	[LastTransmitted] [datetime] NULL,
	[Status] [int] DEFAULT 0,  -- Passed = 1, Unknown = 0, Failed = -1
	[Note] [varchar](250) NULL,
	
 CONSTRAINT [“Audit.CMSBadges” table — Unique Key on “BadgeID”] PRIMARY KEY CLUSTERED 
(
	[BadgeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [Data].[Audit.CMSBadges]  WITH CHECK ADD  CONSTRAINT [“Audit.CMSBadges” --> “Person.Badges”] FOREIGN KEY([BadgeID])
REFERENCES [Data].[Person.Badges] ([BadgeID])
GO

ALTER TABLE [Data].[Audit.CMSBadges] CHECK CONSTRAINT [“Audit.CMSBadges” --> “Person.Badges”]
GO




-- TEST DATA

-- NEW Badges  (status is unknown/0)
-- WAITING for an answer badges (status is unknown/0 and transmit date is NOT NULL)
-- OLD Badges  (status is passed/1 and Last Audited date is > RE-AUDIT THRESHOLD)
-- FAILED badges - badges that failed audit (status is failed/-1)



-- NEW Badges  (status is unknown/0)
-------------
INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, Status, Note)
VALUES
(3828301, 0, 'initialized as new')
INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, Status, Note)
VALUES
(3998671, 0, 'initialized as new')
INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, Status, Note)
VALUES
(3998668, 0, 'initialized as new')
INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, Status, Note)
VALUES
(3998665, 0, 'initialized as new')


-- WAITING badges (status is unknown/0 and transmit date is NOT NULL)
-----------------
INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, LastAudited, LastTransmitted, Status, Note)
VALUES
(3998662, '10/1/2012',  DATEADD(DAY, -1, GETDATE()), 0, 'initialized as waiting')

INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, LastTransmitted, Status, Note)
VALUES
(3998659, DATEADD(DAY, -30, GETDATE()), 0, 'initialized as waiting')  -- 30 days old

INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, LastAudited, LastTransmitted, Status, Note)
VALUES
(3998656, '10/1/2012',  DATEADD(MINUTE, -1, GETDATE()), 0, 'initialized as waiting')


-- OLD Badges  (status is Passed and Last Audited date is > RE-AUDIT THRESHOLD)
-------------
INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, LastAudited, LastTransmitted, Status, Note)
VALUES
(3998653, '2/5/2010', '2/4/2010', 1, 'initialized as old')
INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, LastAudited, LastTransmitted, Status, Note)
VALUES
(3998650, '2/5/2011', '2/4/2011', 1, 'initialized as old')
INSERT INTO [Data].[Audit.CMSBadges]  -- audited but never transmitted
(BadgeID, LastAudited, Status, Note)
VALUES
(3998647, '2/5/2012', 1, 'initialized as old (audited but not transmitted)')


-- FAILED badges
INSERT INTO [Data].[Audit.CMSBadges]
(BadgeID, LastAudited, LastTransmitted, Status, Note)
VALUES
(3998644, '2/5/2010', '2/4/2010', -1, 'initialized as failed')
INSERT INTO [Data].[Audit.CMSBadges]   -- never audited
(BadgeID, LastTransmitted, Status, Note)
VALUES
(3998641, '2/4/2010', -1, 'initialized as failed (never audited)')
INSERT INTO [Data].[Audit.CMSBadges]  -- never transmitted
(BadgeID, LastAudited, Status, Note)
VALUES
(3998635, '2/4/2010', -1, 'initialized as failed (never transmitted)')







-- NEW Badges  (status is unknown/0)
select * 
from [Data].[Audit.CMSBadges]
where Status = 0
and LastTransmitted IS NULL


-- WAITING for an answer badges (status is unknown/0 and transmit date is NOT NULL)
select * 
from [Data].[Audit.CMSBadges]
where Status = 0
and LastTransmitted IS NOT NULL


-- OLD Badges  (status is passed/1 and Last Audited date is > RE-AUDIT THRESHOLD)
DECLARE @THRESHOLD INT
SET @THRESHOLD=365
select * --BadgeID, DATEDIFF(DAY, LastAudited, GETDATE()) 
from [Data].[Audit.CMSBadges]
where Status = 1
and DATEDIFF(DAY, LastAudited, GETDATE()) >= @THRESHOLD

-- FAILED badges - badges that failed audit (status is failed/-1)
select * 
from [Data].[Audit.CMSBadges]
where Status = -1




-- to select badges to transmit
DECLARE @THRESHOLD INT
SET @THRESHOLD=365
select BadgeID, Status, DATEDIFF(DAY, LastAudited, GETDATE()), Note
from [Data].[Audit.CMSBadges]
where ( 
		(Status < 1)
		or 
		( (Status = 1) and DATEDIFF(DAY, LastAudited, GETDATE()) >= @THRESHOLD)
	)
order by Status

select BadgeID, Status, DATEDIFF(DAY, LastAudited, GETDATE()), Note 
from [Data].[Audit.CMSBadges]
order by Status
