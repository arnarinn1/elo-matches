USE Elo_Matches
GO

CREATE TABLE elo.Match
( Id                                 INT IDENTITY(1,1) NOT NULL
 
, PlayerIdOfWinner                   UNIQUEIDENTIFIER  NOT NULL
, PlayerIdOfLoser                    UNIQUEIDENTIFIER  NOT NULL

, WinnerScore                        INT               NOT NULL
, LoserScore                         INT               NOT NULL
 
, WinnerGameNumber                   INT               NOT NULL
, LoserGameNumber                    INT               NOT NULL

, EloRatingGainedForWinner           DECIMAL(10,6)     NOT NULL
, TotalEloRatingBeforeGameForWinner  DECIMAL(10,6)     NOT NULL
, TotalEloRatingAfterGameForWinner   DECIMAL(10,6)     NOT NULL
, WinChanceBasedOnEloRatingForWinner DECIMAL(5,4)      NOT NULL

, EloRatingLostForLoser              DECIMAL(10,6)     NOT NULL
, TotalEloRatingBeforeGameForLoser   DECIMAL(10,6)     NOT NULL
, TotalEloRatingAfterGameForLoser    DECIMAL(10,6)     NOT NULL
, WinChanceBasedOnEloRatingForLoser  DECIMAL(5,4)      NOT NULL

, MatchDate                          DATETIME          NOT NULL
, EntryDate                          DATETIME          NOT NULL 
)
GO

ALTER TABLE elo.Match
ADD CONSTRAINT PK_Match PRIMARY KEY CLUSTERED (Id);
GO

CREATE NONCLUSTERED INDEX Idx_Match_MatchDate ON elo.Match (MatchDate)
GO