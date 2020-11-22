using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace EloMatches.SignalR.ConsoleClientTester
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var connector = new NotificationHubConnector("https://localhost:44316");

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(110));

            var result = await connector.StartAsync(source.Token);

            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
            }
        }
    }

    public class NotificationHubConnector
    {
        private string _notificationHubBaseUrl;

        private string _hubName = "notification-hub";

        private readonly HubConnection _connection;

        public NotificationHubConnector(string notificationHubBaseUrl)
        {
            _notificationHubBaseUrl = notificationHubBaseUrl;

            _connection = new HubConnectionBuilder()
                .WithUrl($"{notificationHubBaseUrl}/{_hubName}", HttpTransportType.WebSockets)
                .WithAutomaticReconnect()
                .ConfigureLogging(logging => {
                    logging.SetMinimumLevel(LogLevel.Debug);
                    logging.AddConsole();
                })
                .Build();

            _connection.Reconnecting += async (error) =>
            {
                await Console.Out.WriteAsync(error.Message);
            };

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };

            _connection.On<PlayerCreatedNotification>("PlayerCreatedNotification", (notification) =>
            {
                Console.WriteLine(notification.Id);
            });
        }

        public async Task<bool> StartAsync(CancellationToken token)
        {
            // Keep trying to until we can start or the token is canceled.
            while (true)
            {
                try
                {
                    await _connection.StartAsync(token);
                    return true;
                }
                catch when (token.IsCancellationRequested)
                {
                    return false;
                }
                catch
                {
                    await Task.Delay(3000, token);
                }
            }
        }
    }

    public class PlayerCreatedNotification
    {
        public Guid Id { get; set; }
    }
}
