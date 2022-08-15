using MLS.Framework.Common;
using MLS.Framework.Common.Response;
using RockPaperScissors.Domain;
using RockPaperScissors.Domain.Exceptions;

namespace RockPaperScissors.Service
{
    public class RockPaperScissorsService : IRockPaperScissorsService
    {
        public RockPaperScissorsService()
        {

        }

        public MlsResponse<WeaponResponse> GetRandomWeapon()
        {
            try
            {
                int randomNumber = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, Enum.GetNames(typeof(WeaponsEnum)).Length);
                MlsResponse <WeaponResponse> weapon = new()
                {
                    Data = new WeaponResponse() { Weapon = Enum.GetName(typeof(WeaponsEnum), randomNumber) },
                    ResponseCode = ResponseCode.No_Error
                };

                return weapon;
            }
            catch (Exception)
            {
                throw new WeaponServiceFailedException("Weapon Service Failed Unexpectedly");
            }

        }
    }
}