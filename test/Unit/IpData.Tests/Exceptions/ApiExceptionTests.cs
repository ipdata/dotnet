using System.Globalization;
using FluentAssertions;
using FluentAssertions.Execution;
using IpData.Exceptions;
using Xunit;

namespace IpData.Tests.Exceptions
{
    public class ApiExceptionTests
    {
        [Fact]
        public void ApiException_WhenCreate_ShouldSetDefaultValues()
        {
            // Act
            var sut = new ApiException();

            // Assert
            using (new AssertionScope())
            {
                sut.StatusCode.Should().Be(0);
                sut.ApiError.Should().NotBeNull();
                sut.Message.Should().NotBeEmpty();
            }
        }

        [Theory, AutoMoqData]
        public void ApiExceptionException_WhenCreateWithContent_ShouldSetMessage(string content)
        {
            // Act
            var sut = new ApiException(content);

            // Assert
            using (new AssertionScope())
            {
                sut.ApiError.Message.Should().Be(content);
                sut.Message.Should().Be(content);   
            }
        }

        [Theory, AutoMoqData]
        public void ApiExceptionException_WhenCreateWithValidJsonContent_ShouldDeserializeToMessage(string message)
        {
            // Act
            var json = $"{{\"message\":\"{message}\"}}";
            var sut = new ApiException(json);

            // Assert
            using (new AssertionScope())
            {
                sut.ApiError.Message.Should().Be(message);
                sut.Message.Should().Be(message);   
            }
        }
    }
}
