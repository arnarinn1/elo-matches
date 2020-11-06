USE Foosball
GO

CREATE OR ALTER View vw_Match
( Id
 
, PlayerIdOfWinner
, UserNameOfWinner
, DisplayNameOfWinner

, PlayerIdOfLoser
, UserNameOfLoser
, DisplayNameOfLoser

, WinnerScore
, LoserScore

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

  FROM Match m
  
  JOIN Player winner
    ON winner.Id = m.PlayerIdOfWinner

  JOIN Player loser
    ON loser.Id = m.PlayerIdOfLoser
GO