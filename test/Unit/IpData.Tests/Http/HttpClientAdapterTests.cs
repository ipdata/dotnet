using FluentAssertions;
using System;
using System.Net.Http;
using Xunit;

namespace IpData.Tests.Http
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
            act.Should().Throw<ArgumentNullException>()
                .Which.Message.Should().NotBeNullOrEmpty();
        }
    }
}
