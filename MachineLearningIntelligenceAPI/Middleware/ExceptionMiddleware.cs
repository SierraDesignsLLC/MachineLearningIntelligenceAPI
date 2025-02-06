namespace MachineLearningIntelligenceAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                string message = $"{nameof(ExceptionMiddleware)} Unhandled exception! {ex.Message}  {ex.InnerException}";
                _logger.LogError(message);
                throw;
            }
        }

    }
}
