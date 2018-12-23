using System.Net;
using FluentAssertions;
using IpData.Exceptions;
using Xunit;

namespace IpData.Tests.Exceptions
{
    public class ForbiddenExceptionTests
    {
        [Fact]
        public void ForbiddenException_WhenCreate_ShouldReturnStatusCode()
        {
            // Act
            var sut = new ForbiddenException();

            // Assert
            sut.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public void ForbiddenException_WhenCreateWithoutParams_ShouldReturnApiError()
        {
            // Act
            var sut = new ForbiddenException();

            // Assert
            sut.ApiError.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        public void ForbiddenException_WhenCreateWithContent_ShouldReturnApiErrorWithMessage(string content)
        {
            // Act
            var sut = new ForbiddenException(content);

            // Assert
            sut.ApiError.Message.Should().Be(content);
        }

        [Theory]
        [AutoMoqData]
        public void ForbiddenException_WhenCreateWithContent_ShouldBeMessage(string content)
        {
            // Act
            var sut = new ForbiddenException(content);

            // Assert
            sut.Message.Should().Be(content);
        }
    }
}
