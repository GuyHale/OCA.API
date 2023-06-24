using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCA.API.Helpers;
using OCA.API.Interfaces;
using OCA.API.Models.Responses;

namespace CIP.API.Controllers
{
    [Route("cip/apikey")]
    [ApiController]
    public class ApiKeyController : ControllerBase
    {
        private readonly ICustomAuthenticationService _customAuthenticationService;
        private readonly ILogger<ApiKeyController> _logger;

        public ApiKeyController(ICustomAuthenticationService customAuthenticationService, ILogger<ApiKeyController> logger)
        {
            _customAuthenticationService = customAuthenticationService;
            _logger = logger;
        }

        [Route("create/{apiKey}")]
        [HttpGet]
        public async Task<ICustomResponse> CreateApiKey(string apiKey)
        {
            try
            {
                return await _customAuthenticationService.CreateApiKey(apiKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            }
            return ApiResponseHelpers.ServerError<ApiKeyCreationResponse>();
        }
    }
}
