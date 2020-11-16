using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Metrics.Logging.Endpoint.CompositionRoot
{
    public class HostedServiceForBusControl : IHostedService
    {
        private readonly IBusControl _busControl;

        public HostedServiceForBusControl(IBusControl busControl)
        {
            _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync(cancellationToken);
        }
    }
}