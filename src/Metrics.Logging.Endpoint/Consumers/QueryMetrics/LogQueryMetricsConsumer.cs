using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MassTransit;
using Metrics.Logging.QueryMetrics;

namespace Metrics.Logging.Endpoint.Consumers.QueryMetrics
{
    public class LogQueryMetricsConsumer : IConsumer<LogQueryMetrics>
    {
        private readonly IDbConnection _dbConnection;

        public LogQueryMetricsConsumer(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task Consume(ConsumeContext<LogQueryMetrics> context)
        {
            var parameters = new
            {
                context.Message.CorrelationId,
                context.Message.TimeStarted,
                context.Message.TimeFinished,
                totalMilliseconds = (context.Message.TimeFinished - context.Message.TimeStarted).TotalMilliseconds,
                context.Message.QueryTypeName,
                context.Message.ExceptionMessage
            };

            await _dbConnection.ExecuteAsync("met.CreateQueryMetrics", parameters, commandType:CommandType.StoredProcedure);
        }
    }
}