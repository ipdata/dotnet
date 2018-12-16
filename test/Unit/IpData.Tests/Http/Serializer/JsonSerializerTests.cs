using IpData.Http.Serializer;
using IpData.Models;
using Xunit;
using FluentAssertions;

namespace IpData.Tests.Http.Serializer
{
    public class JsonSerializerTests
    {
        [Theory]
        [MemberData(nameof(TestData.IpInfoData), MemberType = typeof(TestData))]
        public void Deserialize_WhenCalled_ReturnedIpInfo(string json)
        {
            // Arrange
            var serializer = new JsonSerializer();

            // Act
            var actual = serializer.Deserialize<IpInfo>(json);

            // Assert
            actual.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        public void SerializeIpInfo_WhenCalled_ReturnedString(IpInfo ipInfo)
        {
            // Arrange
            var serializer = new JsonSerializer();

            // Act
            var actual = serializer.Serialize(ipInfo);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(TestData.CarrierData), MemberType = typeof(TestData))]
        public void Deserialize_WhenCalled_ReturnedCarrierInfo(string json)
        {
            // Arrange
            var serializer = new JsonSerializer();

            // Act
            var actual = serializer.Deserialize<CarrierInfo>(json);

            // Assert
            actual.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        public void SerializeCarrierInfo_WhenCalled_ReturnedString(CarrierInfo carrierInfo)
        {
            // Arrange
            var serializer = new JsonSerializer();

            // Act
            var actual = serializer.Serialize(carrierInfo);

            // Assert
            actual.Should().NotBeEmpty();
        }
    }
}
