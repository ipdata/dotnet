using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using IpData.Http.Serializer;
using IpData.Models;

namespace IpData.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        private static readonly ISerializer _serializer = new JsonSerializer();

        [NonSerialized]
        private HttpStatusCode statusCode;

        [NonSerialized]
        private ApiError apiError;

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

        [ExcludeFromCodeCoverage]
        protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        /// <summary>Gets a message that describes the current exception.</summary>
        public override string Message => ApiErrorMessage ?? "An error occurred with this API request";

        /// <summary>Gets or sets the status code.</summary>
        public HttpStatusCode StatusCode
        {
            get => statusCode;
            protected set => statusCode = value;
        }

        /// <summary>Gets or sets the <see cref="Models.ApiError"> object.</summary>
        public ApiError ApiError
        {
            get => apiError;
            protected set => apiError = value;
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
