using System.Data;
using System.Threading.Tasks;
using Dapper;
using MassTransit;
using Microsoft.Data.SqlClient;

namespace Metrics.Logging.Consumers.CommandMetrics
{
    public class LogCommandMetricsConsumer : IConsumer<LogCommandMetrics>
    {
        public async Task Consume(ConsumeContext<LogCommandMetrics> context)
        {
            await using var connection = new SqlConnection("Server=localhost;Database=Metrics;Trusted_Connection=True;MultipleActiveResultSets=true");

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

            await connection.ExecuteAsync("met.CreateCommandMetrics", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}