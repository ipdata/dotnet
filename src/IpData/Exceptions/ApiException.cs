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
        private static ISerializer _serializer = new JsonSerializer();

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

        public override string Message
        {
            get { return ApiErrorMessage ?? "An error occurred with this API request"; }
        }

        public HttpStatusCode StatusCode {
            get { return statusCode; }
            protected set { statusCode = value; }
        }

        public ApiError ApiError
        {
            get { return apiError; }
            protected set { apiError = value; }
        }

        protected string ApiErrorMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ApiError?.Message))
                {
                    return null;
                }

                return ApiError.Message;
            }
        }

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
            catch (Exception)
            {
                return new ApiError(responseContent);
            }
        }
    }
}
