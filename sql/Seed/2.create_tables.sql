USE Elo_Matches
GO

PRINT 'DomainEventOccurrence - Creating table'

CREATE TABLE DomainEventOccurrence
( Id                      INT IDENTITY(1,1) NOT NULL
, AggregateId             VARCHAR(64)       NOT NULL
, AggregateType           VARCHAR(64)       NOT NULL
, TypeName                VARCHAR(128)      NOT NULL
, SerializedData          VARCHAR(1024)     NOT NULL
, OccurrenceDate          DATETIME          NOT NULL
, TransactionId           UNIQUEIDENTIFIER      NULL
)
GO

ALTER TABLE DomainEventOccurrence
ADD CONSTRAINT PK_DomainEvent PRIMARY KEY CLUSTERED (Id);
GO

CREATE NONCLUSTERED INDEX Idx_DomainEvent_AggregateId ON DomainEventOccurrence (AggregateId, TransactionId)
GO

PRINT 'DomainEventOccurrence - Table created'


PRINT 'Player - Creating table'

CREATE TABLE Player
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

ALTER TABLE Player
ADD CONSTRAINT PK_Player PRIMARY KEY NONCLUSTERED (Id);
GO

CREATE CLUSTERED INDEX Idx_Clustered_Player_SequenceId ON Player (SequenceId)
GO

CREATE UNIQUE INDEX Idx_Unique_Player_UserName ON Player (UserName)
GO

PRINT 'Player - Table created'


PRINT 'PlayerRanking - Creating table'

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

PRINT 'PlayerRanking - Table created'


PRINT 'Match - Creating table'

CREATE TABLE Match
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

ALTER TABLE Match
ADD CONSTRAINT PK_Match PRIMARY KEY CLUSTERED (Id);
GO

CREATE NONCLUSTERED INDEX Idx_Match_MatchDate ON Match (MatchDate)
GO

PRINT 'Match - Table created'


PRINT 'PlayerLeaderBoard - Creating table'

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

PRINT 'PlayerLeaderBoard - Table created'