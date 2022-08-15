using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
