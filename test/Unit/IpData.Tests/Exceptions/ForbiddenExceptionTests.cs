using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using IpData.Exceptions;
using Xunit;

namespace IpData.Tests.Exceptions
{
    public class ForbiddenExceptionTests
    {
        [Fact]
        public void ForbiddenException_WhenCreate_ShouldSetDefaultValues()
        {
            // Act
            var sut = new ForbiddenException();

            // Assert
            using (new AssertionScope())
            {
                sut.StatusCode.Should().Be(HttpStatusCode.Forbidden);
                sut.ApiError.Should().NotBeNull();
            }
        }

        [Theory, AutoMoqData]
        public void ForbiddenException_WhenCreateWithContent_ShouldSetMessage(string content)
        {
            // Act
            var sut = new ForbiddenException(content);

            // Assert
            using (new AssertionScope())
            {
                sut.ApiError.Message.Should().Be(content);
                sut.Message.Should().Be(content);
            }
        }
    }
}
