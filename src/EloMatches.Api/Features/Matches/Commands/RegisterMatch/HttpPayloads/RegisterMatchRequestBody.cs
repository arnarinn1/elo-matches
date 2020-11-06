using System;

namespace EloMatches.Api.Features.Matches.Commands.RegisterMatch.HttpPayloads
{
    public class RegisterMatchRequestBody
    {
        public Guid WinnerPlayerId { get; set; }
        public Guid LoserPlayerId { get; set; }

        public int WinnerScore { get; set; }
        public int LoserScore { get; set; }
        
        public DateTime? MatchDate { get; set; }
    }
}