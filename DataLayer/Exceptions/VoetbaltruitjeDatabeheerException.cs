using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Exceptions
{
    public class VoetbaltruitjeDatabeheerException : Exception
    {
        public VoetbaltruitjeDatabeheerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
