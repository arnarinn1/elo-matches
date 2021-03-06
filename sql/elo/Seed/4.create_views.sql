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


CREATE OR ALTER View elo.vw_PlayerRanking
( Id
, SequenceId
 
, UserName
, DisplayName

, CurrentEloRating
, AverageEloRating
, LowestEloRating
, HighestEloRating
, EloRatingDifference

, PlayedGames
, Wins
, Losses
, WinDifferential
, WinPercentage

, ScorePlus
, ScoreMinus
, ScoreDifferential

, StreakCount
, OnWinningStreak

, LastMatchDate
)
AS
SELECT r.Id
     , r.SequenceId

	 , p.UserName
	 , p.DisplayName
      
     , r.CurrentEloRating
     , r.AverageEloRating
     , r.LowestEloRating
     , r.HighestEloRating
     , r.EloRatingDifference
     
     , r.PlayedGames
     , r.Wins
     , r.Losses
     , r.WinDifferential
     , r.WinPercentage
     
     , r.ScorePlus
     , r.ScoreMinus
     , r.ScoreDifferential
     
     , r.StreakCount
     , CAST(CASE WHEN r.StreakType = 1 THEN 1 ELSE 0 END AS BIT)
     
     , r.LastMatchDate
 FROM elo.PlayerRanking r 

 JOIN elo.Player p
   ON p.Id = r.Id

WHERE p.ActiveSince IS NOT NULL
GO


CREATE OR ALTER View elo.vw_Match
( Id
 
, PlayerIdOfWinner
, UserNameOfWinner
, DisplayNameOfWinner

, PlayerIdOfLoser
, UserNameOfLoser
, DisplayNameOfLoser

, WinnerScore
, LoserScore

, WinnerGameNumber
, LoserGameNumber

, EloRatingGainedForWinner
, TotalEloRatingBeforeGameForWinner
, TotalEloRatingAfterGameForWinner
, WinChanceBasedOnEloRatingForWinner

, EloRatingLostForLoser
, TotalEloRatingBeforeGameForLoser
, TotalEloRatingAfterGameForLoser
, WinChanceBasedOnEloRatingForLoser

, MatchDate
, EntryDate
)

AS
SELECT m.Id
 
     , winner.Id
     , winner.UserName
     , winner.DisplayName
     
     , loser.Id
     , loser.UserName
     , loser.DisplayName
     
     , m.WinnerScore
     , m.LoserScore

	 , m.WinnerGameNumber
	 , m.LoserGameNumber
     
     , m.EloRatingGainedForWinner
     , m.TotalEloRatingBeforeGameForWinner
     , m.TotalEloRatingAfterGameForWinner
     , m.WinChanceBasedOnEloRatingForWinner
     
     , m.EloRatingLostForLoser
     , m.TotalEloRatingBeforeGameForLoser
     , m.TotalEloRatingAfterGameForLoser
     , m.WinChanceBasedOnEloRatingForLoser
     
     , m.MatchDate
     , m.EntryDate

  FROM elo.Match m
  
  JOIN elo.Player winner
    ON winner.Id = m.PlayerIdOfWinner

  JOIN elo.Player loser
    ON loser.Id = m.PlayerIdOfLoser
GO


CREATE OR ALTER View elo.vw_DomainEventOccurrence
( Id
, AggregateId
, AggregateType
, TypeName
, SerializedData
, OccurrenceDate
, TransactionId
)
AS
SELECT d.Id
     , d.AggregateId
     , d.AggregateType
     , d.TypeName
     , d.SerializedData
     , d.OccurrenceDate
     , d.TransactionId
 FROM elo.DomainEventOccurrence d
GO