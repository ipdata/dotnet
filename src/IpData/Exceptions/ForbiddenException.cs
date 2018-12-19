using System;
using System.Net;

namespace IpData.Exceptions
{
    public class ForbiddenException : ApiException
    {
        public ForbiddenException()
            : this(null)
        {
        }

        public ForbiddenException(string responseContent)
            : this(responseContent, null)
        {
        }

        public ForbiddenException(string responseContent, Exception innerException)
            : base(HttpStatusCode.Forbidden, responseContent, innerException)
        {
        }
    }
}
