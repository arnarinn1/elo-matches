USE Elo_Matches
GO

CREATE TABLE elo.Player
( Id                      UNIQUEIDENTIFIER  NOT NULL
, SequenceId              INT IDENTITY(1,1) NOT NULL
, UserName                VARCHAR(128)      NOT NULL
, DisplayName             VARCHAR(128)      NOT NULL
, Email                   VARCHAR(128)      NOT NULL
, ActiveSince             DATETIME              NULL
, DeactivatedSince        DATETIME              NULL
, EntryDate               DATETIME          NOT NULL
)
GO

ALTER TABLE elo.Player
ADD CONSTRAINT PK_Player PRIMARY KEY NONCLUSTERED (Id);
GO

CREATE CLUSTERED INDEX Idx_Clustered_Player_SequenceId ON elo.Player (SequenceId)
GO

CREATE UNIQUE INDEX Idx_Unique_Player_UserName ON elo.Player (UserName)
GO