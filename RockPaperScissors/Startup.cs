using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Service;

[assembly: FunctionsStartup(typeof(RockPaperScissors.Startup))]
namespace RockPaperScissors
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddScoped<IRockPaperScissorsService, RockPaperScissorsService>();
        }
    }
}
