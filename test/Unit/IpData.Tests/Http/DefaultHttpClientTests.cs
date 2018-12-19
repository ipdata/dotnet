using FluentAssertions;
using System.Net.Http;
using Xunit;

namespace IpData.Tests.Http
{
    public class DefaultHttpClientTests
    {
        [Theory]
        [AutoMoqData]
        public void SendAsync_WhenCalled_ReturnedHttpResponseMessage(
            HttpRequestMessage requestMessage)
        {
            // Arrange
            var sut = new DefaultHttpClient();

            // Act
            var actual = sut.SendAsync(requestMessage);

            // Assert
            actual.Should().NotBeNull();
        }
    }
}
