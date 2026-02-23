using System;
using System.Net;

namespace IPData.Exceptions.Factory
{
    internal interface IApiExceptionFactory
    {
        ApiException Create(HttpStatusCode statusCode, string content);
    }
}