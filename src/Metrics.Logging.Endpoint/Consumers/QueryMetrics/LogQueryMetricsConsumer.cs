using System.Data;
using System.Threading.Tasks;
using Dapper;
using MassTransit;
using Metrics.Logging.QueryMetrics;
using Microsoft.Data.SqlClient;

namespace Metrics.Logging.Endpoint.Consumers.QueryMetrics
{
    public class LogQueryMetricsConsumer : IConsumer<LogQueryMetrics>
    {
        public async Task Consume(ConsumeContext<LogQueryMetrics> context)
        {
            await using var connection = new SqlConnection("Server=localhost;Database=Metrics;Trusted_Connection=True;MultipleActiveResultSets=true");

            var totalMilliseconds = (context.Message.TimeFinished - context.Message.TimeStarted).TotalMilliseconds;

            var parameters = new
            {
                context.Message.CorrelationId,
                context.Message.TimeStarted,
                context.Message.TimeFinished,
                totalMilliseconds,
                context.Message.QueryTypeName,
                context.Message.ExceptionMessage
            };

            await connection.ExecuteAsync("met.CreateQueryMetrics", parameters, commandType:CommandType.StoredProcedure);
        }
    }
}