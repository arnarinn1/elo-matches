USE Elo_Matches
GO

CREATE OR ALTER View vw_PlayerRanking
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
 FROM PlayerRanking r 

 JOIN Player p
   ON p.Id = r.Id

WHERE p.ActiveSince IS NOT NULL
GO
