using DBService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Automation;
namespace Automation
{
    public class ExchangeRateUpdateService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private Timer _timer;

        public ExchangeRateUpdateService(IServiceProvider services)
        {
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateExchangeRates, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void UpdateExchangeRates(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var updater = scope.ServiceProvider.GetRequiredService<FetchCurrencyConversionRates>();
                updater.UpdateExchangeRatesAsync().Wait(); // You may need to change this depending on the async context
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
