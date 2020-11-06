namespace EloMatches.Api.Features.Matches.Commands.RegisterMatch.HttpPayloads
{
    public class RegisterMatchResponseBody
    {
        public int MatchId { get; set; }

        public RegisterMatchResponseBody(int matchId)
        {
            MatchId = matchId;
        }

        public RegisterMatchResponseBody() { }
    }
}