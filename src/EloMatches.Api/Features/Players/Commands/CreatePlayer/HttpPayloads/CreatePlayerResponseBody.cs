using System;

namespace EloMatches.Api.Features.Players.Commands.CreatePlayer.HttpPayloads
{
    public class CreatePlayerResponseBody
    {
        public Guid PlayerId { get; set; }

        public CreatePlayerResponseBody(Guid playerId)
        {
            PlayerId = playerId;
        }

        public CreatePlayerResponseBody() { }
    }
}