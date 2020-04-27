using System;
using System.Net;
using FluentAssertions;
using IpData.Exceptions;
using IpData.Exceptions.Factory;
using Xunit;

namespace IpData.Tests.Exceptions.Factory
{
    public class ApiExceptionFactoryTests
    {
        [Theory]
        [InlineData(HttpStatusCode.InternalServerError, typeof(ApiException))]
        [InlineData(HttpStatusCode.Unauthorized, typeof(UnauthorizedException))]
        [InlineData(HttpStatusCode.BadRequest, typeof(BadRequestException))]
        [InlineData(HttpStatusCode.Forbidden, typeof(ForbiddenException))]
        public void Create_WhenCalledWithStatusCode_ShouldThrowExpectedException(HttpStatusCode statusCode, Type expected)
        {
            // Arrange
            var sut = new ApiExceptionFactory();

            // Act
            var actual = sut.Create(statusCode, string.Empty);

            // Assert
            actual.Should().BeOfType(expected);
        }
    }
}
