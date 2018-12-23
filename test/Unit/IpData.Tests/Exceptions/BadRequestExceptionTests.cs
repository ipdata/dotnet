using System.Net;
using FluentAssertions;
using IpData.Exceptions;
using Xunit;

namespace IpData.Tests.Exceptions
{
    public class BadRequestExceptionTests
    {
        [Fact]
        public void BadRequestException_WhenCreate_ShouldReturnStatusCode()
        {
            // Act
            var sut = new BadRequestException();

            // Assert
            sut.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void BadRequestException_WhenCreateWithoutParams_ShouldReturnApiError()
        {
            // Act
            var sut = new BadRequestException();

            // Assert
            sut.ApiError.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        public void BadRequestException_WhenCreateWithContent_ShouldReturnApiErrorWithMessage(string content)
        {
            // Act
            var sut = new BadRequestException(content);

            // Assert
            sut.ApiError.Message.Should().Be(content);
        }

        [Theory]
        [AutoMoqData]
        public void BadRequestException_WhenCreateWithContent_ShouldBeMessage(string content)
        {
            // Act
            var sut = new BadRequestException(content);

            // Assert
            sut.Message.Should().Be(content);
        }
    }
}
