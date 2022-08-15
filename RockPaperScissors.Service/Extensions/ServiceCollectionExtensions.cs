using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RockPaperScissors.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddRockPaperScissorsService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IRockPaperScissorsService),typeof(RockPaperScissorsService));
        }
    }
}
