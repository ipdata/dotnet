using FluentAssertions;
using System;
using Xunit;

namespace IpData.Tests
{
    public class IpDataClientTests
    {
        [Theory]
        [MemberData(nameof(TestData.EmptyOrWhitespaceString), MemberType = typeof(TestData))]
        public void IpDataClient_WhenCreatedWithInvalidApiKey_ThrowArgumentOutOfRangeException(
            string apiKey)
        {
            // Arrange/Act
            Action act = () => new IpDataClient(apiKey);

            // Assert
            act.Should()
               .Throw<ArgumentException>()
               .And.ParamName.Should().Be(nameof(apiKey));
        }

        [Theory]
        [AutoMoqData]
        public void IpDataClient_WhenCreatedWithApiKey_ReturnedApiKey(
            string apiKey)
        {
            // Arrange
            var sut = new IpDataClient(apiKey);

            // Act/Assert
            sut.ApiKey.Should().Be(apiKey);
        }
    }
}
