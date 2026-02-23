using System;
using System.Collections.Generic;
using System.Net;

namespace IPData.Exceptions.Factory
{
    internal class ApiExceptionFactory : IApiExceptionFactory
    {
        private static readonly Dictionary<HttpStatusCode, Func<string, ApiException>> httpExceptionMap = new Dictionary<HttpStatusCode, Func<string, ApiException>>
        {
            { HttpStatusCode.BadRequest, CreateBadRequestException },
            { HttpStatusCode.Unauthorized, CreateUnauthorizedException },
            { HttpStatusCode.Forbidden, CreateForbiddenException }
        };

        public ApiException Create(HttpStatusCode statusCode, string content) =>
            httpExceptionMap.TryGetValue(statusCode, out var exception)
            ? exception(content)
            : new ApiException(content);

        private static ApiException CreateBadRequestException(string content) => new BadRequestException(content);

        private static ApiException CreateUnauthorizedException(string content) => new UnauthorizedException(content);

        private static ApiException CreateForbiddenException(string content) => new ForbiddenException(content);
    }
}