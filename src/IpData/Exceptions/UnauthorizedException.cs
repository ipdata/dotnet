using System;
using System.Net;

namespace IpData.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException() : this(null)
        {

        }

        public UnauthorizedException(string message)
            : this(message, null)
        {
        }

        public UnauthorizedException(string message, Exception innerException)
            : base(HttpStatusCode.Unauthorized, message, innerException)
        {
        }
    }
}
