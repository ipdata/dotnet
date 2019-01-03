using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace IpData.Exceptions
{
    [Serializable]
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

        [ExcludeFromCodeCoverage]
        protected ForbiddenException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
