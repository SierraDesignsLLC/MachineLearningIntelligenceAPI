using MachineLearningIntelligenceAPI.Common.Utils.Converters;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess;
using MachineLearningIntelligenceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using MachineLearningIntelligenceAPI.Common.StringConstants;

namespace MachineLearningIntelligenceAPI.Controllers
{
    [ApiController]
    [Route(BaseRoutePrefix + V1Controller)]
    public class AIAnalysisV1Controller : ControllerBase
    {
        public const string V1Controller = "v1/analysis/";

        private readonly ILogger<AIAnalysisV1Controller> _logger;
        private IAIAnalysisService _aiAnalysisService { get; set; }

        public AIAnalysisV1Controller(RequestSessionInformation requestSessionInformation, ILogger<AIAnalysisV1Controller> logger, IAIAnalysisService aiAnalysisService,
            IAuthService authService)
            : base(requestSessionInformation, logger, authService)
        {
            _logger = logger;
            _aiAnalysisService = aiAnalysisService;
        }

        /// <summary>
        /// Analyze the passed in user engagements from the perspective of an expert social media manager
        /// </summary>
        /// <returns>Returns analysis of the data</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<string>>> AnalyzeEngagements([FromBody] AnalyzeRequestV1Dto request)
        {
            try
            {
                // set up rate limit
                //_httpClient.DefaultRequestHeaders.Add("RateLimit-WaitTimeMilliseconds", Constants.RedditPostWaitTimeMilliseconds.ToString());
                if (request.InputStrings == null || request.InputStrings.Count == 0 || request.Culture == null)
                {
                    _logger.Log(LogLevel.Error, $"{BadRequestString.MissingFields}");
                    return BadRequest(BadRequestString.MissingFields);
                }

                _logger.Log(LogLevel.Information, $"Request IP: {HttpContext.Connection.RemoteIpAddress} {nameof(AnalyzeEngagements)}");
                var modelRequest = AnalyzeRequestDataConverter.DtoToModel(request);
                var response = await _aiAnalysisService.AnalyzeEngagements(modelRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GenerateHttpResponseFromException(ex);
            }
        }
    }
}
