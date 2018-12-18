using System;
using System.Net;

namespace IpData.Exceptions.Factory
{
    internal interface IApiExceptionFactory
    {
        Exception Create(HttpStatusCode statusCode, string content);
    }
}