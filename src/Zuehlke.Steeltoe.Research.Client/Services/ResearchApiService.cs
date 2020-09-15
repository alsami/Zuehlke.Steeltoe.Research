using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zuehlke.Steeltoe.Research.Client.Clients;

namespace Zuehlke.Steeltoe.Research.Client.Services
{
    public class ResearchApiService : IResearchApiService, IDisposable
    {
        private readonly ILogger<ResearchApiService> _logger;

        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(50);
        private readonly ISteeltoeResearchApiClient _researchApiClient;

        public ResearchApiService(ISteeltoeResearchApiClient researchApiClient,
            ILogger<ResearchApiService> logger)
        {
            _researchApiClient = researchApiClient;
            _logger = logger;
        }

        public void Dispose()
        {
            _mutex.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task RequestApiRandomAsync(int count)
        {
            var requestLoadTasks = Enumerable.Range(0, count)
                .Select(async range =>
                {
                    try
                    {
                        await _mutex.WaitAsync();
                        var services = await _researchApiClient.LoadDiscoveryServicesAsync();
                        _logger.LogInformation(
                            "Loaded services from Research-API! Request: {request} Services: {services}",
                            range + 1, string.Join(", ", services));
                    }
                    finally
                    {
                        _mutex.Release(1);
                    }
                });

            return Task.WhenAll(requestLoadTasks);
        }
    }
}