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
    public class AICommandV1Controller : ControllerBase
    {
        public const string V1Controller = "v1/command/";

        private readonly ILogger<AICommandV1Controller> _logger;
        private IAICommandService _aiCommandService { get; set; }

        public AICommandV1Controller(RequestSessionInformation requestSessionInformation, ILogger<AICommandV1Controller> logger, IAICommandService aiCommandService,
            IAuthService authService)
            : base(requestSessionInformation, logger, authService)
        {
            _logger = logger;
            _aiCommandService = aiCommandService;
        }

        /// <summary>
        /// Respond to passed in command
        /// </summary>
        /// <returns>Returns response of the command</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> RespondToCommand([FromBody] CommandRequestV1Dto request)
        {
            try
            {
                // set up rate limit
                //_httpClient.DefaultRequestHeaders.Add("RateLimit-WaitTimeMilliseconds", Constants.RedditPostWaitTimeMilliseconds.ToString());
                if (string.IsNullOrEmpty(request.InputString) || string.IsNullOrEmpty(request.Prompt))
                {
                    _logger.Log(LogLevel.Error, $"{BadRequestString.MissingFields}");
                    return BadRequest(BadRequestString.MissingFields);
                }

                _logger.Log(LogLevel.Information, $"Request IP: {HttpContext.Connection.RemoteIpAddress} {nameof(RespondToCommand)}");
                var modelRequest = CommandRequestDataConverter.DtoToModel(request);
                var response = await _aiCommandService.RespondToCommand(modelRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GenerateHttpResponseFromException(ex);
            }
        }
    }
}
