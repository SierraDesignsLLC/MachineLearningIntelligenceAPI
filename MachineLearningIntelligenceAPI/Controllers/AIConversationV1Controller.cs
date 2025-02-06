using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess;
using MachineLearningIntelligenceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using MachineLearningIntelligenceAPI.Common.Utils.Converters;

namespace MachineLearningIntelligenceAPI.Controllers
{
    [ApiController]
    [Route(BaseRoutePrefix + V1Controller)]
    public class AIConversationV1Controller : ControllerBase
    {
        public const string V1Controller = "v1/conversation/";

        private readonly ILogger<AIConversationV1Controller> _logger;
        private IAIConversationService _aiConversationService { get; set; }

        public AIConversationV1Controller(RequestSessionInformation requestSessionInformation, ILogger<AIConversationV1Controller> logger, IAIConversationService aiConversationService,
            IAuthService authService) 
            : base(requestSessionInformation, logger, authService)
        {
            _logger = logger;
            _aiConversationService = aiConversationService;
        }

        /// <summary>
        /// Have an AI conversation
        /// </summary>
        /// <returns>Response to conversation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> TalkToAI([FromBody] ConversationRequestV1Dto request)
        {
            try
            {
                // set up rate limit
                //_httpClient.DefaultRequestHeaders.Add("RateLimit-WaitTimeMilliseconds", Constants.RedditPostWaitTimeMilliseconds.ToString());
                if (request.InputString == null)
                {
                    return BadRequest();
                }

                _logger.Log(LogLevel.Information, $"Request IP: {HttpContext.Connection.RemoteIpAddress} {nameof(TalkToAI)}");
                var modelRequest = ConversationRequestDataConverter.DtoToModel(request);
                var response = await _aiConversationService.TalkToAI(modelRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GenerateHttpResponseFromException(ex);
            }
        }
    }
}
