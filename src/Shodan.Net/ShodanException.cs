using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shodan.Net
{
    public class ShodanException : Exception
    {
        public ShodanException()
        {
        }

        public ShodanException(string message) : base(message)
        {
        }

        public ShodanException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}