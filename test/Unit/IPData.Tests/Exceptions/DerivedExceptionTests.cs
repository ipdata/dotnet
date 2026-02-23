using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using IPData.Exceptions;
using Xunit;

namespace IPData.Tests.Exceptions
{
    public class DerivedExceptionTests
    {
        public static IEnumerable<object[]> ExceptionStatusCodeData()
        {
            yield return new object[] { new BadRequestException(), HttpStatusCode.BadRequest };
            yield return new object[] { new ForbiddenException(), HttpStatusCode.Forbidden };
            yield return new object[] { new UnauthorizedException(), HttpStatusCode.Unauthorized };
        }

        public static IEnumerable<object[]> ExceptionData()
        {
            yield return new object[] { new BadRequestException() };
            yield return new object[] { new ForbiddenException() };
            yield return new object[] { new UnauthorizedException() };
        }

        [Theory]
        [MemberData(nameof(ExceptionStatusCodeData))]
        public void Exception_WhenCreate_ShouldReturnCorrectStatusCode(
            ApiException sut, HttpStatusCode expectedStatusCode)
        {
            sut.StatusCode.Should().Be(expectedStatusCode);
        }

        [Theory]
        [MemberData(nameof(ExceptionData))]
        public void Exception_WhenCreateWithoutParams_ShouldReturnApiError(
            ApiException sut)
        {
            sut.ApiError.Should().NotBeNull();
        }

        public static IEnumerable<object[]> ExceptionWithContentData()
        {
            var content = "test error message";
            yield return new object[] { new BadRequestException(content), content };
            yield return new object[] { new ForbiddenException(content), content };
            yield return new object[] { new UnauthorizedException(content), content };
        }

        [Theory]
        [MemberData(nameof(ExceptionWithContentData))]
        public void Exception_WhenCreateWithContent_ShouldReturnApiErrorWithMessage(
            ApiException sut, string expectedMessage)
        {
            sut.ApiError.Message.Should().Be(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(ExceptionWithContentData))]
        public void Exception_WhenCreateWithContent_ShouldBeMessage(
            ApiException sut, string expectedMessage)
        {
            sut.Message.Should().Be(expectedMessage);
        }
    }
}
