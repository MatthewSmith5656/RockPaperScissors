using MLS.Framework.Common.Response;

namespace RockPaperScissors.Service
{
    public interface IRockPaperScissorsService
    {
        MlsResponse<WinnerResponse> GetWinner(string playerChoice);

        MlsResponse<WinnerResponse> GetAIWinner();
    }
}
