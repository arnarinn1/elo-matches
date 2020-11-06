using EloMatches.Query.Pipeline;
using EloMatches.Query.Projections.Matches;

namespace EloMatches.Api.Features.Matches.Queries.MatchById
{
    public class MatchByIdQuery : IQuery<MatchProjection>
    {
        public MatchByIdQuery(int matchId)
        {
            MatchId = matchId;
        }

        public int MatchId { get; }
    }
}