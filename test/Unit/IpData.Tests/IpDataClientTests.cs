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

        [Theory]
        [MemberData(nameof(TestData.EmptyOrWhitespaceString), MemberType = typeof(TestData))]
        public void IpDataClient_WhenCreatedWithInvalidLanguage_ThrowArgumentOutOfRangeException(
            string language)
        {
            // Arrange/Act
            Action act = () => new IpDataClient("1", language);

            // Assert
            act.Should()
               .Throw<ArgumentException>()
               .And.ParamName.Should().Be(nameof(language));
        }

        [Theory]
        [AutoMoqData]
        public void IpDataClient_WhenCreatedWithLanguage_ReturnedLanguage(
            string apiKey,
            string language)
        {
            // Arrange
            var sut = new IpDataClient(apiKey, language);

            // Act/Assert
            sut.Language.Should().Be(language);
        }
    }
}
