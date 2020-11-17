namespace EloMatches.Api.ConfigurationModels
{
    public class MassTransitConfiguration
    {
        public bool UseRabbitMqTransport { get; set; }
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}