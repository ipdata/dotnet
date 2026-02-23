using FluentAssertions;
using System;
using System.Net.Http;
using Xunit;

namespace IPData.Tests.Http
{
    public class HttpClientAdapterTests
    {
        [Theory, AutoMoqData]
        public void SendAsync_WhenCalled_ShouldReturnHttpResponseMessage(
            HttpRequestMessage requestMessage)
        {
            // Arrange
            var sut = new HttpClientAdapter();

            // Act
            var actual = sut.SendAsync(requestMessage);

            // Assert
            actual.Should().NotBeNull();
        }

        [Fact]
        public void HttpClientAdapter_WhenCreateWithInvalidHttpClient_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new HttpClientAdapter(null);

            // Assert
            act.Should()
               .Throw<ArgumentNullException>()
               .Where(e => e.ParamName == "httpClient")
               .Where(e => e.Message.Contains("The httpClient can't be null"));
        }
    }
}
