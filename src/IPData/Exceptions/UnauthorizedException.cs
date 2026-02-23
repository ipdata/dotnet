using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace IPData.Exceptions
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

        [ExcludeFromCodeCoverage]
        protected UnauthorizedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
