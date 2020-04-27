using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using IpData.Exceptions;
using Xunit;

namespace IpData.Tests.Exceptions
{
    public class BadRequestExceptionTests
    {
        [Fact]
        public void BadRequestException_WhenCreate_ShouldSetDefaultValues()
        {
            // Act
            var sut = new BadRequestException();

            // Assert
            using (new AssertionScope())
            {
                sut.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                sut.ApiError.Should().NotBeNull();
            }
        }

        [Theory, AutoMoqData]
        public void BadRequestException_WhenCreateWithContent_ShouldSetMessage(string content)
        {
            // Act
            var sut = new BadRequestException(content);

            // Assert
            using (new AssertionScope())
            {
                sut.ApiError.Message.Should().Be(content);
                sut.Message.Should().Be(content);
            }
        }
    }
}
