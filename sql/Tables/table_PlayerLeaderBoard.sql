USE Foosball
GO

CREATE TABLE PlayerLeaderBoard
( Id            UNIQUEIDENTIFIER  NOT NULL
, SequenceId    INT IDENTITY(1,1) NOT NULL
, EloRating     DECIMAL(10,6)     NOT NULL
, Rank          INT               NOT NULL
)
GO

ALTER TABLE PlayerLeaderBoard
ADD CONSTRAINT PK_PlayerLeaderBoard PRIMARY KEY NONCLUSTERED (Id);
GO

CREATE CLUSTERED INDEX Idx_PlayerLeaderBoard_SequenceId ON PlayerLeaderBoard (SequenceId)
GO