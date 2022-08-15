namespace RockPaperScissors.Domain.Exceptions
{
    public class WeaponServiceFailedException : Exception
    {
        public WeaponServiceFailedException()
        {

        }

        public WeaponServiceFailedException(string message):base(message)
        {
        }

        public WeaponServiceFailedException(string message, Exception inner) : base(message,inner)
        {
        }
    }
}
