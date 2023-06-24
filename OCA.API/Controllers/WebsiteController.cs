using Microsoft.AspNetCore.Mvc;
using OCA.API.Interfaces;
using OCA.API.Models;

namespace CIP.API.Controllers
{
    [Route("cip/cryptocurrencies")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly ICryptocurrencyRetrieval _cryptocurrencyRetrieval;
        private readonly ILogger<WebsiteController> _logger;

        public WebsiteController(ICryptocurrencyRetrieval cryptocurrencyRetrieval, ILogger<WebsiteController> logger)
        {
            _cryptocurrencyRetrieval = cryptocurrencyRetrieval;
            _logger = logger;
        }

        [Route("get")]
        [HttpGet]
        public async Task<IEnumerable<Cryptocurrency>> Get()
        {
            try
            {
                return (await _cryptocurrencyRetrieval.Get()).OrderBy(x => x.Rank);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return Enumerable.Empty<Cryptocurrency>();
        }
    }
}
