using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace IPData.Exceptions
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

        /// <summary>Initializes a new instance of the <see cref="ForbiddenException"/> class with serialized data.</summary>
        /// <remarks>
        /// Formatter-based serialization is obsolete on .NET 8 and later (SYSLIB0051). The constructor is kept on
        /// every target so the type has the same shape everywhere, and carries that obsoletion on .NET 8 and later.
        /// </remarks>
        [ExcludeFromCodeCoverage]
#if NET8_0_OR_GREATER
        [Obsolete(SerializationObsoleteMessage, DiagnosticId = SerializationDiagnosticId, UrlFormat = SerializationUrlFormat)]
#endif
        protected ForbiddenException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
