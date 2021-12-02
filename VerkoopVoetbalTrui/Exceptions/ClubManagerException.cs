using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public class ClubManagerException : Exception
    {
        public ClubManagerException(string message) : base(message)
        {
        }
    }
}
