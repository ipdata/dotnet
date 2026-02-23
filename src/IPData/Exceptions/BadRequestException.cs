using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace IPData.Exceptions
{
    [Serializable]
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

        [ExcludeFromCodeCoverage]
        protected BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
