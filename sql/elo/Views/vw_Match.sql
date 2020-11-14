USE Elo_Matches
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