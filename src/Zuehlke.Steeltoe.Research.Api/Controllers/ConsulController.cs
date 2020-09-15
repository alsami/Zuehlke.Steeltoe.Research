using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.Discovery;

namespace Zuehlke.Steeltoe.Research.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ConsulController : ControllerBase
    {
        private readonly IConsulClient _consulClient;

        public ConsulController(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> ConsulServicesAsync()
        {
            var services = await _consulClient.Agent.Services();

            return services
                .Response
                .Select(keyValuePair => keyValuePair.Key);
        }
    }
}