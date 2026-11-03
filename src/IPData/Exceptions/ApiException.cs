using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using IPData.Http.Serializer;
using IPData.Models;

namespace IPData.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
#if NET8_0_OR_GREATER
        /// <summary>The message of the obsoletion that .NET 8 and later put on formatter-based serialization.</summary>
        internal const string SerializationObsoleteMessage =
            "This API supports obsolete formatter-based serialization. It should not be called or extended by application code.";

        /// <summary>The diagnostic id of that obsoletion.</summary>
        internal const string SerializationDiagnosticId = "SYSLIB0051";

        /// <summary>The documentation link of that obsoletion.</summary>
        internal const string SerializationUrlFormat = "https://aka.ms/dotnet-warnings/{0}";
#endif

        private static readonly ISerializer _serializer = new JsonSerializer();

        [NonSerialized]
        private HttpStatusCode _statusCode;

        [NonSerialized]
        private ApiError _apiError;

        public ApiException()
            : this(null)
        {
        }

        public ApiException(string responseContent)
            : this(responseContent, null)
        {
        }

        public ApiException(string responseContent, Exception innerException)
            : this(0, responseContent, innerException)
        {
        }

        protected ApiException(HttpStatusCode statusCode, string responseContent, Exception innerException)
            : this(statusCode, GetApiErrorFromExceptionMessage(responseContent), innerException)
        {
        }

        protected ApiException(HttpStatusCode statusCode, ApiError apiError, Exception innerException)
            : base(null, innerException)
        {
            StatusCode = statusCode;
            ApiError = apiError ?? throw new ArgumentNullException(nameof(apiError), "ApiError can't be null");
        }

        /// <summary>Initializes a new instance of the <see cref="ApiException"/> class with serialized data.</summary>
        /// <remarks>
        /// Formatter-based serialization is obsolete on .NET 8 and later (SYSLIB0051). The constructor is kept on
        /// every target so the type has the same shape everywhere, and carries that obsoletion on .NET 8 and later.
        /// </remarks>
        [ExcludeFromCodeCoverage]
#if NET8_0_OR_GREATER
        [Obsolete(SerializationObsoleteMessage, DiagnosticId = SerializationDiagnosticId, UrlFormat = SerializationUrlFormat)]
#endif
        protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        /// <summary>Gets a message that describes the current exception.</summary>
        public override string Message => ApiErrorMessage ?? "An error occurred with this API request";

        /// <summary>Gets or sets the status code.</summary>
        public HttpStatusCode StatusCode
        {
            get => _statusCode;
            protected set => _statusCode = value;
        }

        /// <summary>Gets or sets the <see cref="Models.ApiError"> object.</summary>
        public ApiError ApiError
        {
            get => _apiError;
            protected set => _apiError = value;
        }

        /// <summary>Gets the API error message.</summary>
        protected string ApiErrorMessage =>
            string.IsNullOrWhiteSpace(ApiError?.Message) ? null : ApiError.Message;
        
        /// <summary>Gets the API error from exception message.</summary>
        /// <param name="responseContent">Content of the response.</param>
        /// <returns>The <see cref="Models.ApiError"/> object</returns>
        private static ApiError GetApiErrorFromExceptionMessage(string responseContent)
        {
            if (string.IsNullOrEmpty(responseContent))
            {
                return new ApiError(responseContent);
            }

            try
            {
                return _serializer.Deserialize<ApiError>(responseContent);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception)
            {
                return new ApiError(responseContent);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
