using MachineLearningIntelligenceAPI.Common;

namespace MachineLearningIntelligenceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureAppConfiguration((builder) =>
                {
                    //Setting up the hostContext.Configuration for consumption in ConfigureServices
                    //Depending on your configuration you may not need this, it is included here as an example
                    builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                });
    }
}