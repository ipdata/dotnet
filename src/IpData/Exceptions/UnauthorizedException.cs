using System;
using System.Net;
using System.Runtime.Serialization;

namespace IpData.Exceptions
{
    [Serializable]
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

        protected UnauthorizedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
