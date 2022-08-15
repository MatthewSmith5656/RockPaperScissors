using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
