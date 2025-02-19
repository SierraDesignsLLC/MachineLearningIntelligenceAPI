using MachineLearningIntelligenceAPI.Common;
using MachineLearningIntelligenceAPI.DataAccess;
using MachineLearningIntelligenceAPI.DataAccess.Daos;
using MachineLearningIntelligenceAPI.DataAccess.Daos.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess.Repositories;
using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess.Services;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MachineLearningIntelligenceAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private string CorsWhitelist = "_corsWhitelist";
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Build the intermediate service provider
            //var sp = services.BuildServiceProvider();

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsWhitelist,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://example.com",
                                                          "http://localhost:4200")
                                                            .AllowAnyHeader()
                                                            .AllowAnyMethod();
                                  });
            });

            // http client configuration
            services.AddHttpClient(ConnectionStrings.RedditService, httpClient =>
            {
                httpClient.BaseAddress = new Uri(_configuration.GetConnectionString(ConnectionStrings.RedditService));

                // TODO add passthrough auth?
            });
            services.AddHttpClient(ConnectionStrings.MachineLearningIntelligenceAPI, httpClient =>
            {
                httpClient.BaseAddress = new Uri(_configuration.GetConnectionString(ConnectionStrings.MachineLearningIntelligenceAPI));
            });

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressMapClientErrors = true; //TODO make these false for pre prod
                    options.SuppressModelStateInvalidFilter = true; //TODO make these false for pre prod
                });

            //Dependency Injection
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ExceptionMiddleware>();
            services.AddScoped<RequestSessionInformation>();
            services.AddScoped<RequestSessionInformationMiddleware>();

            services.AddTransient<IAIAnalysisService, AIAnalysisService>();
            services.AddTransient<IAICommandService, AICommandService>();
            services.AddTransient<IAIConversationService, AIConversationService>();
            services.AddTransient<IAITranslationService, AITranslationService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IRedditService, RedditService>();

            services.AddTransient<IAccountAutomationDataDao, AccountAutomationDataDao>();

            services.AddTransient<IAIAnalysisRepository, AIAnalysisRepository>();
            services.AddTransient<IAICommandRepository, AICommandRepository>();
            services.AddTransient<IAIConversationRepository, AIConversationRepository>();
            services.AddTransient<IAITranslationRepository, AITranslationRepository>();
            services.AddTransient<IMachineLearningIntelligenceAPIRepository, MachineLearningIntelligenceAPIRepository>();
            services.AddTransient<IRedditServiceRepository, RedditServiceRepository>();

            //Application insights
            //services.AddApplicationInsightsTelemetry();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Gateway Microservice",
                    Description = "Gateway for social media automation management",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Sierra Designs LLC",
                        Url = new Uri("https://example.com/contact")
                    },
                    /*License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }*/
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-7.0 middleware order
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(CorsWhitelist);
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseMiddleware<RequestSessionInformationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
        }
    }
}
