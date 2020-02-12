using FluentAssertions;
using IpData.Http.Serializer;
using IpData.Models;
using IpData.Tests.DataSources;
using Xunit;

namespace IpData.Tests.Http.Serializer
{
    public class JsonSerializerTests
    {
        private readonly JsonSerializer _sut = new JsonSerializer();

        [Theory]
        [MemberData(nameof(TestDataSource.IpInfoData), MemberType = typeof(TestDataSource))]
        public void Deserialize_WhenCalled_ReturnedIpInfo(string json)
        {
            // Act
            var actual = _sut.Deserialize<IpInfo>(json);

            // Assert
            actual.Should().NotBeNull();
        }

        [Theory, AutoMoqData]
        public void SerializeIpInfo_WhenCalled_ReturnedString(IpInfo ipInfo)
        {
            // Act
            var actual = _sut.Serialize(ipInfo);

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Theory]
        [MemberData(nameof(TestDataSource.CarrierData), MemberType = typeof(TestDataSource))]
        public void Deserialize_WhenCalled_ReturnedCarrierInfo(string json)
        {
            // Act
            var actual = _sut.Deserialize<CarrierInfo>(json);

            // Assert
            actual.Should().NotBeNull();
        }

        [Theory, AutoMoqData]
        public void SerializeCarrierInfo_WhenCalled_ReturnedString(CarrierInfo carrierInfo)
        {
            // Act
            var actual = _sut.Serialize(carrierInfo);

            // Assert
            actual.Should().NotBeEmpty();
        }
    }
}
