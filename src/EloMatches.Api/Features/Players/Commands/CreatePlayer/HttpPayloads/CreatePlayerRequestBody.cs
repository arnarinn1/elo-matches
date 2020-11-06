#pragma warning disable 8618
namespace EloMatches.Api.Features.Players.Commands.CreatePlayer.HttpPayloads
{
    public class CreatePlayerRequestBody
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}