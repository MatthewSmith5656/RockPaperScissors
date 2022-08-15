using MLS.Framework.Common.ResponseModel;
using RockPaperScissors.Domain.Models.Response;

namespace RockPaperScissors.Service
{
    public interface IRockPaperScissorsService
    {
        MlsResponse<WinnerResponse> GetWinner(string playerChoice);

        MlsResponse<WinnerResponse> GetAIWinner();
    }
}
