using System;
using System.Net;

namespace IpData.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException()
            : this(null)
        {
        }

        public BadRequestException(string responseContent)
            : this(responseContent, null)
        {
        }

        public BadRequestException(string responseContent, Exception innerException)
            : base(HttpStatusCode.BadRequest, responseContent, innerException)
        {
        }
    }
}
