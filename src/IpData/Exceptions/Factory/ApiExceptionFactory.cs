using System;
using System.Collections.Generic;
using System.Net;

namespace IpData.Exceptions.Factory
{
    internal class ApiExceptionFactory : IApiExceptionFactory
    {
        private static readonly Dictionary<HttpStatusCode, Func<string, ApiException>> _httpExceptionMap = new Dictionary<HttpStatusCode, Func<string, ApiException>>
        {
            { HttpStatusCode.BadRequest, CreateBadRequestException },
            { HttpStatusCode.Unauthorized, CreateUnauthorizedException },
            { HttpStatusCode.Forbidden, CreateForbiddenException }
        };

        public ApiException Create(HttpStatusCode statusCode, string content)
        {
            if (_httpExceptionMap.TryGetValue(statusCode, out var exception))
            {
                return exception(content);
            }

            return new ApiException(content);
        }

        private static ApiException CreateBadRequestException(string content)
        {
            return new BadRequestException(content);
        }

        private static ApiException CreateUnauthorizedException(string content)
        {
            return new UnauthorizedException(content);
        }

        private static ApiException CreateForbiddenException(string content)
        {
            return new ForbiddenException(content);
        }
    }
}