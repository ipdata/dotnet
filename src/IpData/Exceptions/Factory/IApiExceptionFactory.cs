using System;
using System.Net;

namespace IpData.Exceptions.Factory
{
    internal interface IApiExceptionFactory
    {
        ApiException Create(HttpStatusCode statusCode, string content);
    }
}