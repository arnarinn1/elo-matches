using System;
using System.Threading.Tasks;
using EloMatches.IntegrationEvents.Events.Players;
using EloMatches.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace EloMatches.SignalR.IntegrationEventHandlers.PlayerCreated
{
    public class PlayerCreatedConsumer : IConsumer<PlayerCreatedIntegrationEvent>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public PlayerCreatedConsumer(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public Task Consume(ConsumeContext<PlayerCreatedIntegrationEvent> context)
        {
            return _hubContext.Clients.All.SendAsync("PlayerCreatedNotification", new
            {
                Id = context.Message.PlayerId
            });
        }
    }
}