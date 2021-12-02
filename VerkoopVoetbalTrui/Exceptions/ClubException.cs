using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public class ClubException : Exception
    {
        public ClubException(string message) : base(message)
        {
        }
    }
}
