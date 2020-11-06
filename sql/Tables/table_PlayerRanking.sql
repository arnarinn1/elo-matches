USE Elo_Matches
GO

CREATE TABLE PlayerRanking
( Id                      UNIQUEIDENTIFIER  NOT NULL
, SequenceId              INT IDENTITY(1,1) NOT NULL
 
, CurrentEloRating        DECIMAL(10,6)     NOT NULL
, AverageEloRating        DECIMAL(10,6)     NOT NULL
, LowestEloRating         DECIMAL(10,6)     NOT NULL
, HighestEloRating        DECIMAL(10,6)     NOT NULL
, EloRatingDifference     DECIMAL(10,6)     NOT NULL

, PlayedGames             INT               NOT NULL        
, Wins                    INT               NOT NULL
, Losses                  INT               NOT NULL
, WinDifferential         INT               NOT NULL
, WinPercentage           DECIMAL(5,4)      NOT NULL 

, ScorePlus               INT               NOT NULL
, ScoreMinus              INT               NOT NULL
, ScoreDifferential       INT               NOT NULL

, StreakCount             INT               NOT NULL
, StreakType              INT               NOT NULL -- 1=Winning, 2=Losing

, LastMatchDate           DATETIME              NULL
)
GO

ALTER TABLE PlayerRanking
ADD CONSTRAINT PK_PlayerRanking PRIMARY KEY NONCLUSTERED (Id);
GO

CREATE CLUSTERED INDEX Idx_PlayerRanking_SequenceId ON PlayerRanking (SequenceId)
GO