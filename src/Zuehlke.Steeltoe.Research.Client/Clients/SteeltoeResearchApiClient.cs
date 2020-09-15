using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Zuehlke.Steeltoe.Research.Client.Services;

namespace Zuehlke.Steeltoe.Research.Client.Clients
{
    public class SteeltoeResearchApiClient : ISteeltoeResearchApiClient
    {
        private readonly HttpClient _httpClient;

        public SteeltoeResearchApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> LoadDiscoveryServicesAsync()
        {
            const string url = "http://zsra/api/consul";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<IEnumerable<string>>(await response.Content.ReadAsStringAsync());
        }
    }
}