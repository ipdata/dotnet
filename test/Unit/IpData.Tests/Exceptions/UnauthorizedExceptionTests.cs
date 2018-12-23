using System.Net;
using FluentAssertions;
using IpData.Exceptions;
using Xunit;

namespace IpData.Tests.Exceptions
{
    public class UnauthorizedExceptionTests
    {
        [Fact]
        public void UnauthorizedException_WhenCreate_ShouldReturnStatusCode()
        {
            // Act
            var sut = new UnauthorizedException();

            // Assert
            sut.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void UnauthorizedException_WhenCreateWithoutParams_ShouldReturnApiError()
        {
            // Act
            var sut = new UnauthorizedException();

            // Assert
            sut.ApiError.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        public void UnauthorizedException_WhenCreateWithContent_ShouldReturnApiErrorWithMessage(string content)
        {
            // Act
            var sut = new UnauthorizedException(content);

            // Assert
            sut.ApiError.Message.Should().Be(content);
        }

        [Theory]
        [AutoMoqData]
        public void UnauthorizedException_WhenCreateWithContent_ShouldBeMessage(string content)
        {
            // Act
            var sut = new UnauthorizedException(content);

            // Assert
            sut.Message.Should().Be(content);
        }
    }
}
