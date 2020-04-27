using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using IpData.Exceptions;
using Xunit;

namespace IpData.Tests.Exceptions
{
    public class UnauthorizedExceptionTests
    {
        [Fact]
        public void UnauthorizedException_WhenCreate_ShouldSetDefaultValues()
        {
            // Act
            var sut = new UnauthorizedException();

            // Assert
            using (new AssertionScope())
            {
                sut.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
                sut.ApiError.Should().NotBeNull();
            }
        }

        [Theory, AutoMoqData]
        public void UnauthorizedException_WhenCreateWithContent_ShouldSetMessage(string content)
        {
            // Act
            var sut = new UnauthorizedException(content);

            // Assert
            using (new AssertionScope())
            {
                sut.ApiError.Message.Should().Be(content);
                sut.Message.Should().Be(content);
            }
        }
    }
}
