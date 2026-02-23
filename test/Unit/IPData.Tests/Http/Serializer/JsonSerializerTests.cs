using FluentAssertions;
using IPData.Http.Serializer;
using IPData.Models;
using IPData.Tests.DataSources;
using Xunit;

namespace IPData.Tests.Http.Serializer
{
    public class JsonSerializerTests
    {
        private readonly JsonSerializer _sut = new JsonSerializer();

        [Theory]
        [MemberData(nameof(TestDataSource.IPInfoData), MemberType = typeof(TestDataSource))]
        public void Deserialize_WhenCalled_ReturnedIPInfo(string json)
        {
            // Act
            var actual = _sut.Deserialize<IPInfo>(json);

            // Assert
            actual.Should().NotBeNull();
        }

        [Theory, AutoMoqData]
        public void SerializeIPInfo_WhenCalled_ReturnedString(IPInfo ipInfo)
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

        [Theory]
        [MemberData(nameof(TestDataSource.CompanyData), MemberType = typeof(TestDataSource))]
        public void Deserialize_WhenCalled_ReturnedCompanyInfo(string json)
        {
            // Act
            var actual = _sut.Deserialize<CompanyInfo>(json);

            // Assert
            actual.Should().NotBeNull();
        }

        [Theory, AutoMoqData]
        public void SerializeCompanyInfo_WhenCalled_ReturnedString(CompanyInfo companyInfo)
        {
            // Act
            var actual = _sut.Serialize(companyInfo);

            // Assert
            actual.Should().NotBeEmpty();
        }
    }
}
