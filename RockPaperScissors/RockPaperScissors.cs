using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RockPaperScissors.Service;
using MLS.Framework.Common;
using System.Net;

namespace RockPaperScissors
{
    public class RockPaperScissors
    {
        private readonly IRockPaperScissorsService rockPaperScissorsService;
        private readonly ILogger<RockPaperScissors> logger;
        public RockPaperScissors(ILogger<RockPaperScissors> logger,IRockPaperScissorsService rockPaperScissorsService)
        {
            this.rockPaperScissorsService = rockPaperScissorsService;
            this.logger = logger;
        }

        [FunctionName(nameof(GetRandomWeapon))]
        public IActionResult GetRandomWeapon(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "weapon")] HttpRequest req)
        {
            logger.LogInformation($"C# HTTP trigger function {nameof(GetRandomWeapon)} started.");
            MlsResponse<WeaponResponse> weaponResponse = default;
            try
            {
                weaponResponse = rockPaperScissorsService.GetRandomWeapon();
            }
            catch (Exception ex)
            {
                var error = $"Error occured whilst selecting a weapon: {ex.Message}";
                logger.LogError(error);
                return new ObjectResult(error)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
            logger.LogInformation($"C# HTTP trigger function {nameof(GetRandomWeapon)} produced {weaponResponse.Data.Weapon}.");
            return new OkObjectResult(weaponResponse);
        }
    }
}
