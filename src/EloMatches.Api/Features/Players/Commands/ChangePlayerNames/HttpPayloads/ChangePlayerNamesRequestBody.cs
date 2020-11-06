#pragma warning disable 8618
namespace EloMatches.Api.Features.Players.Commands.ChangePlayerNames.HttpPayloads
{
    public class ChangePlayerNamesRequestBody
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}