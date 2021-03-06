USE Elo_Matches
GO

CREATE OR ALTER View elo.vw_Player
( Id
, SequenceId
, UserName
, DisplayName
, Email
, IsActive
, ActiveSince
, DeactivatedSince
, EntryDate
)
AS
SELECT Id
     , SequenceId
     , UserName
	 , DisplayName
	 , Email
	 , CAST(CASE WHEN ActiveSince IS NOT NULL THEN 1 ELSE 0 END AS BIT)
	 , ActiveSince
	 , DeactivatedSince
     , EntryDate
 FROM elo.Player
GO