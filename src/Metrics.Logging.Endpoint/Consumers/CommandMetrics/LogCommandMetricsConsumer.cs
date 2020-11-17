using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MassTransit;
using Metrics.Logging.CommandMetrics;

namespace Metrics.Logging.Endpoint.Consumers.CommandMetrics
{
    public class LogCommandMetricsConsumer : IConsumer<LogCommandMetrics>
    {
        private readonly IDbConnection _dbConnection;

        public LogCommandMetricsConsumer(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task Consume(ConsumeContext<LogCommandMetrics> context)
        {
            var totalMilliseconds = (context.Message.TimeFinished - context.Message.TimeStarted).TotalMilliseconds;

            var parameters = new
            {
                context.Message.CorrelationId,
                context.Message.TimeStarted,
                context.Message.TimeFinished,
                totalMilliseconds,
                context.Message.CommandTypeName,
                context.Message.ExceptionMessage
            };

            await _dbConnection.ExecuteAsync("met.CreateCommandMetrics", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}