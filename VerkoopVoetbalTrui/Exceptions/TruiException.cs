using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public class TruiException : Exception
    {
        public TruiException(string message) : base(message)
        {

        }
    }
}
