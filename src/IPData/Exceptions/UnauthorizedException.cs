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

        /// <summary>Initializes a new instance of the <see cref="UnauthorizedException"/> class with serialized data.</summary>
        /// <remarks>
        /// Formatter-based serialization is obsolete on .NET 8 and later (SYSLIB0051). The constructor is kept on
        /// every target so the type has the same shape everywhere, and carries that obsoletion on .NET 8 and later.
        /// </remarks>
        [ExcludeFromCodeCoverage]
#if NET8_0_OR_GREATER
        [Obsolete(SerializationObsoleteMessage, DiagnosticId = SerializationDiagnosticId, UrlFormat = SerializationUrlFormat)]
#endif
        protected UnauthorizedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
