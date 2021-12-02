using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Exceptions
{
    public class KlantDatabeheerException : Exception
    {
        public KlantDatabeheerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
