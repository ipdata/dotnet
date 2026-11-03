using System.Collections.Generic;
using System.Linq;
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

        [Theory]
        [MemberData(nameof(TestDataSource.IPLookupResultData), MemberType = typeof(TestDataSource))]
        public void Deserialize_WhenCalled_ReadsLanguages(string json)
        {
            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            var language = actual.Languages.Should().ContainSingle().Which;
            language.Name.Should().Be("Ukrainian");
            language.Native.Should().Be("\u0423\u043A\u0440\u0430\u0457\u043D\u0441\u044C\u043A\u0430");
            language.Code.Should().Be("uk");
        }

        [Theory]
        [MemberData(nameof(TestDataSource.AnycastData), MemberType = typeof(TestDataSource))]
        public void Deserialize_AnycastResponse_ReadsLanguages(string json)
        {
            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            var language = actual.Languages.Should().ContainSingle().Which;
            language.Name.Should().Be("English");
            language.Native.Should().Be("English");
            language.Code.Should().Be("en");
        }

        [Fact]
        public void Deserialize_WhenLanguagesAbsent_ReadsEmptyList()
        {
            // Arrange
            var json = "{\"ip\":\"1.1.1.1\"}";

            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.Languages.Should().NotBeNull().And.BeEmpty();
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

        [Theory]
        [MemberData(nameof(TestDataSource.IPLookupResultData), MemberType = typeof(TestDataSource))]
        public void Deserialize_WhenSatelliteAndAnycastKeysAbsent_ReadFalse(string json)
        {
            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.IsSatellite.Should().BeFalse();
            actual.IsAnycast.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(TestDataSource.SatelliteData), MemberType = typeof(TestDataSource))]
        public void Deserialize_SatelliteResponse_ReadsIsSatellite(string json)
        {
            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.Ip.Should().Be("98.97.34.0");
            actual.IsSatellite.Should().BeTrue();
            actual.IsAnycast.Should().BeFalse();
            actual.IsEu.Should().BeFalse();
            actual.Asn.Name.Should().Be("Space Exploration Technologies Corporation");
            actual.Threat.Blocklists.Should().BeEmpty();
            actual.Count.Should().Be(0);
        }

        [Theory]
        [MemberData(nameof(TestDataSource.AnycastData), MemberType = typeof(TestDataSource))]
        public void Deserialize_AnycastResponse_ReadsIsAnycastAndDatedBlocklistEntries(string json)
        {
            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.Ip.Should().Be("8.8.8.8");
            actual.IsAnycast.Should().BeTrue();
            actual.IsSatellite.Should().BeFalse();
            actual.City.Should().BeNull();
            actual.Threat.IsDatacenter.Should().BeTrue();
            actual.Threat.Blocklists.Should().HaveCount(2);

            var cloud = actual.Threat.Blocklists[0];
            cloud.Name.Should().Be("ipdata");
            cloud.Site.Should().Be("https://ipdata.co");
            cloud.Type.Should().Be("cloud");
            cloud.FirstSeen.Should().Be("2026-11-02");
            cloud.LastSeen.Should().Be("2026-11-02");
            cloud.DaysSeen.Should().Be(1);

            actual.Threat.Blocklists[1].Type.Should().Be("open_resolvers");
            actual.Threat.Blocklists[1].DaysSeen.Should().Be(1);
        }

        [Theory]
        [MemberData(nameof(TestDataSource.VpnData), MemberType = typeof(TestDataSource))]
        public void Deserialize_VpnResponse_LeavesFlagsFalseAndBlocklistDatesNullWhenAbsent(string json)
        {
            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.Ip.Should().Be("187.13.250.1");
            actual.IsSatellite.Should().BeFalse();
            actual.IsAnycast.Should().BeFalse();
            actual.Threat.IsThreat.Should().BeTrue();
            actual.Threat.Blocklists.Should().HaveCount(6);

            var gluetun = actual.Threat.Blocklists[0];
            gluetun.Name.Should().Be("Gluetun");
            gluetun.FirstSeen.Should().Be("2026-10-31");
            gluetun.LastSeen.Should().Be("2026-11-02");
            gluetun.DaysSeen.Should().Be(2);

            var undated = actual.Threat.Blocklists[2];
            undated.Name.Should().Be("ipdata");
            undated.Type.Should().Be("vpn");
            undated.FirstSeen.Should().BeNull();
            undated.LastSeen.Should().BeNull();
            undated.DaysSeen.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(TestDataSource.BulkData), MemberType = typeof(TestDataSource))]
        public void Deserialize_BulkResponse_CarriesNewFieldsPerItem(string json)
        {
            // Act
            var actual = _sut.Deserialize<IEnumerable<IPLookupResult>>(json).ToList();

            // Assert
            actual.Should().HaveCount(3);
            actual[0].IsSatellite.Should().BeTrue();
            actual[0].IsAnycast.Should().BeFalse();
            actual[1].IsAnycast.Should().BeTrue();
            actual[1].IsSatellite.Should().BeFalse();
            actual[1].Threat.Blocklists[0].DaysSeen.Should().Be(1);
            actual[1].Languages.Should().ContainSingle().Which.Code.Should().Be("en");
            actual[2].IsSatellite.Should().BeFalse();
            actual[2].IsAnycast.Should().BeFalse();
            actual[2].Threat.Blocklists[2].DaysSeen.Should().BeNull();
        }

        [Fact]
        public void Deserialize_UnknownKeys_AreIgnored()
        {
            // Arrange
            var json = "{\"ip\":\"1.1.1.1\",\"some_future_field\":{\"nested\":true}," +
                       "\"threat\":{\"blocklists\":[{\"name\":\"ipdata\",\"some_future_field\":1}]}}";

            // Act
            var actual = _sut.Deserialize<IPLookupResult>(json);

            // Assert
            actual.Ip.Should().Be("1.1.1.1");
            actual.Threat.Blocklists.Should().ContainSingle().Which.Name.Should().Be("ipdata");
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
