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
        [MemberData(nameof(TestDataSource.IPLookupResultData), MemberType = typeof(TestDataSource))]
        public void Deserialize_WhenCalled_ReturnedIPLookupResult(string json)
        {
            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.Should().NotBeNull();
        }

        [Fact]
        public void Deserialize_CountAsString_ParsedCorrectly()
        {
            // Arrange - API returns count as a string (issue #35)
            var json = "{\"count\": \"213586\"}";

            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.Count.Should().Be(213586);
        }

        [Fact]
        public void Deserialize_CountAsNumber_ParsedCorrectly()
        {
            // Arrange
            var json = "{\"count\": 213586}";

            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.Count.Should().Be(213586);
        }

        [Theory, AutoMoqData]
        public void SerializeIPLookupResult_WhenCalled_ReturnedString(IPLookupResult ipInfo)
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
