using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zuehlke.Steeltoe.Research.Client.Clients
{
    public interface ISteeltoeResearchApiClient
    {
        public Task<IEnumerable<string>> LoadDiscoveryServicesAsync();
    }
}