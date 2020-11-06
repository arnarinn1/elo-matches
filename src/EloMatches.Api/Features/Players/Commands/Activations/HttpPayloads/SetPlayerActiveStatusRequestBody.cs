namespace EloMatches.Api.Features.Players.Commands.Activations.HttpPayloads
{
    public class SetPlayerActiveStatusRequestBody
    {
        public PlayerActiveStatusKind PlayerActiveStatus { get; set; }
    }
}