using IpData.Http.Serializer;
using IpData.Models;
using System;
using System.Net;

namespace IpData.Exceptions
{
    public class ApiException : Exception
    {
        private static ISerializer _serializer = new JsonSerializer();

        public ApiException(string responseContent)
            : this(0, responseContent, null)
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

        public override string Message
        {
            get { return ApiErrorMessage ?? "An error occurred with this API request"; }
        }

        public HttpStatusCode StatusCode { get; private set; }

        public ApiError ApiError { get; protected set; }

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

            return _serializer.Deserialize<ApiError>(responseContent) ?? new ApiError(responseContent);
        }
    }
}
