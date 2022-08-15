using MLS.Framework.Common;

namespace RockPaperScissors.Service
{
    public interface IRockPaperScissorsService
    {
        MlsResponse<WeaponResponse> GetRandomWeapon();
    }
}
