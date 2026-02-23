using System.Globalization;
using FluentAssertions;
using IPData.Exceptions;
using Xunit;

namespace IPData.Tests.Exceptions
{
    public class ApiExceptionTests
    {
        [Fact]
        public void ApiException_WhenCreate_ShouldReturnStatusCode()
        {
            // Act
            var sut = new ApiException();

            // Assert
            sut.StatusCode.Should().Be(0);
        }

        [Fact]
        public void ApiExceptionException_WhenCreateWithoutParams_ShouldReturnApiError()
        {
            // Act
            var sut = new ApiException();

            // Assert
            sut.ApiError.Should().NotBeNull();
        }

        [Fact]
        public void ApiExceptionException_WhenCreateWithoutParams_ShouldBeDefaultMessage()
        {
            // Act
            var sut = new ApiException();

            // Assert
            sut.Message.Should().Be("An error occurred with this API request");
        }

        [Theory, AutoMoqData]
        public void ApiExceptionException_WhenCreateWithContent_ShouldReturnApiErrorWithMessage(string content)
        {
            // Act
            var sut = new ApiException(content);

            // Assert
            sut.ApiError.Message.Should().Be(content);
        }

        [Theory, AutoMoqData]
        public void ApiExceptionException_WhenCreateWithContent_ShouldBeMessage(string content)
        {
            // Act
            var sut = new ApiException(content);

            // Assert
            sut.Message.Should().Be(content);
        }

        [Theory, AutoMoqData]
        public void ApiExceptionException_WhenCreateWithValidJsonContent_ShouldDeserializeApiError(string message)
        {
            // Act
            var json = $"{{\"message\":\"{message}\"}}";
            var sut = new ApiException(json);

            // Assert
            sut.ApiError.Message.Should().Be(message);
        }

        [Theory, AutoMoqData]
        public void ApiExceptionException_WhenCreateWithValidJsonContent_ShouldBeDeserializedMessage(string message)
        {
            // Act
            var json = $"{{\"message\":\"{message}\"}}";
            var sut = new ApiException(json);

            // Assert
            sut.Message.Should().Be(message);
        }
    }
}
