using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RockPaperScissors.Service;
using System.Net;
using RockPaperScissors.Domain.Enums;
using System.Linq;
using MLS.Framework.Common.ResponseModel;
using RockPaperScissors.Domain.Models.Response;

namespace RockPaperScissors
{
    public class RockPaperScissorsFunctions
    {
        private readonly IRockPaperScissorsService rockPaperScissorsService;
        private readonly ILogger<RockPaperScissorsFunctions> logger;
        public RockPaperScissorsFunctions(ILogger<RockPaperScissorsFunctions> logger, IRockPaperScissorsService rockPaperScissorsService)
        {
            this.rockPaperScissorsService = rockPaperScissorsService;
            this.logger = logger;
        }

        [FunctionName(nameof(GetWinner))]
        public IActionResult GetWinner(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "winner")] HttpRequest req)
        {
            logger.LogInformation($"C# HTTP trigger function {nameof(GetWinner)} started.");

            MlsResponse<WinnerResponse> weaponResponse = default;
            try
            {
                bool isValid = false;
                string playerOneChoice = req.Query["playerOneChoice"];

                //typically use fluentvalidation/would not have this logic here
                if (playerOneChoice != null && Enum.GetNames(typeof(WeaponsEnum)).Any(x => playerOneChoice.Contains(x)))
                {
                    isValid = true;
                }
                if (!isValid)
                {
                    var message = $"Invalid Weapon Choice";
                    logger.LogInformation(message);
                    return new BadRequestObjectResult(message);
                }
                weaponResponse = rockPaperScissorsService.GetWinner(playerOneChoice);
            }
            catch (Exception ex)
            {
                var error = $"Error occured whilst generating a winner: {ex.Message}";
                logger.LogError(error);
                return new ObjectResult(error)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
            logger.LogInformation($"C# HTTP trigger function {nameof(GetWinner)} determined {weaponResponse.Data.Winner} as the winner.");
            return new OkObjectResult(weaponResponse);
        }

        [FunctionName(nameof(GetAIWinner))]
        public IActionResult GetAIWinner(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "winner/ai")] HttpRequest req)
        {
            logger.LogInformation($"C# HTTP trigger function {nameof(GetAIWinner)} started.");
            MlsResponse<WinnerResponse> weaponResponse = default;
            try
            {
                weaponResponse = rockPaperScissorsService.GetAIWinner();
            }
            catch (Exception ex)
            {
                var error = $"Error occured whilst generating a winner: {ex.Message}";
                logger.LogError(error);
                return new ObjectResult(error)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
            logger.LogInformation($"C# HTTP trigger function {nameof(GetWinner)} determined {weaponResponse.Data.Winner} as the winner.");
            return new OkObjectResult(weaponResponse);
        }
    }
}
