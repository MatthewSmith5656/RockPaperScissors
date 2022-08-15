using MLS.Framework.Common.Enums;
using MLS.Framework.Common.ResponseModel;
using RockPaperScissors.Domain.Enums;
using RockPaperScissors.Domain.Exceptions;
using RockPaperScissors.Domain.Models.Response;

namespace RockPaperScissors.Service
{
    public class RockPaperScissorsService : IRockPaperScissorsService
    {
        public RockPaperScissorsService()
        {

        }

        public MlsResponse<WinnerResponse> GetWinner(string playerChoice)
        {
            try
            {
                MlsResponse<WinnerResponse> winner = new()
                {
                    Data = PlayGame(playerChoice, GenerateWeaponChoice()),
                    ResponseCode = ResponseCode.No_Error
                };

                return winner;
            }
            catch (Exception)
            {
                throw new WeaponServiceFailedException("Weapon Service Failed Unexpectedly");
            }
        }

        public MlsResponse<WinnerResponse> GetAIWinner()
        {
            try
            {
                MlsResponse<WinnerResponse> winner = new()
                {
                    Data = PlayGame(GenerateWeaponChoice(), GenerateWeaponChoice()),
                    ResponseCode = ResponseCode.No_Error
                };

                return winner;
            }
            catch (Exception)
            {
                throw new WeaponServiceFailedException("Weapon Service Failed Unexpectedly");
            }
        }

        private WinnerResponse PlayGame(string playerOneChoice, string playerTwoChoice)
        {
            WinnerResponse winner = new()
            {
                PlayerOneWeapon = playerOneChoice,
                PlayerTwoWeapon = playerTwoChoice,
                Winner = DetermineWinner(playerOneChoice, playerTwoChoice),
            };
            return winner;
        }

        private string DetermineWinner(string playerOneChoice, string playerTwoChoice)
        {
            var winner = WinnerEnum.Tie;
            switch(Enum.Parse<WeaponsEnum>(playerOneChoice))
            {
                case WeaponsEnum.Rock:
                    if (playerTwoChoice == "Rock")
                        winner = WinnerEnum.Tie;
                    if (playerTwoChoice == "Paper")
                        winner = WinnerEnum.PlayerTwo;
                    if (playerTwoChoice =="Scissors")
                        winner = WinnerEnum.PlayerOne;
                    break;
                case WeaponsEnum.Paper:
                    if (playerTwoChoice == "Rock")
                        winner = WinnerEnum.PlayerOne;
                    if (playerTwoChoice == "Paper")
                        winner = WinnerEnum.Tie;
                    if (playerTwoChoice == "Scissors")
                        winner = WinnerEnum.PlayerTwo;
                    break;
                case WeaponsEnum.Scissors:
                    if (playerTwoChoice == "Rock")
                        winner = WinnerEnum.PlayerTwo;
                    if (playerTwoChoice == "Paper")
                        winner = WinnerEnum.PlayerOne;
                    if (playerTwoChoice == "Scissors")
                        winner = WinnerEnum.Tie;
                    break;
                default: throw new WeaponServiceFailedException("Weapon does not exist");

            }
            return winner.ToString();
        }


        private static string? GenerateWeaponChoice()
        {
            int randomNumber = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, Enum.GetNames(typeof(WeaponsEnum)).Length);
            var weapon = Enum.GetName(typeof(WeaponsEnum), randomNumber);
            return weapon;
        }

    }
}