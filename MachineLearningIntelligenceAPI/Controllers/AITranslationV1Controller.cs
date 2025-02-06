using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess;
using MachineLearningIntelligenceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using MachineLearningIntelligenceAPI.Common.Utils.Converters;

namespace MachineLearningIntelligenceAPI.Controllers
{
    [ApiController]
    [Route(BaseRoutePrefix + V1Controller)]
    public class AITranslationV1Controller : ControllerBase
    {
        public const string V1Controller = "v1/translation/";

        private readonly ILogger<AITranslationV1Controller> _logger;
        private IAITranslationService _aiTranslationService { get; set; }

        public AITranslationV1Controller(RequestSessionInformation requestSessionInformation, ILogger<AITranslationV1Controller> logger, IAITranslationService aiTranslationService,
            IAuthService authService)
            : base(requestSessionInformation, logger, authService)
        {
            _logger = logger;
            _aiTranslationService = aiTranslationService;
        }

        /// <summary>
        /// Translate the request input strings for requested language
        /// </summary>
        /// <returns>Returns the requested translations 1:1 from the request</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<string>>> TranslateWithAI([FromBody] TranslationRequestV1Dto request)
        {
            try
            {
                // set up rate limit
                //_httpClient.DefaultRequestHeaders.Add("RateLimit-WaitTimeMilliseconds", Constants.RedditPostWaitTimeMilliseconds.ToString());
                if(request.InputStrings == null || request.InputStrings.Count == 0 || request.Language == null)
                {
                    return BadRequest();
                }

                _logger.Log(LogLevel.Information, $"Request IP: {HttpContext.Connection.RemoteIpAddress} {nameof(TranslateWithAI)}");
                var modelRequest = TranslationRequestDataConverter.DtoToModel(request);
                var response = await _aiTranslationService.TranslationWithAI(modelRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GenerateHttpResponseFromException(ex);
            }
        }
    }
}
