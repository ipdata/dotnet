using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IPData.Tests.DataSources
{
    public static class TestDataSource
    {
        public static IEnumerable<object[]> EmptyOrWhitespaceString()
        {
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null };
        }

        public static IEnumerable<object[]> IPLookupResultData()
        {
            yield return new object[] { ReadJsonFile("IPLookupResult.json") };
        }

        public static IEnumerable<object[]> CarrierData()
        {
            yield return new object[] { ReadJsonFile("Carrier.json") };
        }

        public static IEnumerable<object[]> CompanyData()
        {
            yield return new object[] { ReadJsonFile("Company.json") };
        }

        public static IEnumerable<object[]> SatelliteData()
        {
            yield return new object[] { ReadJsonFile("IPLookupResultSatellite.json") };
        }

        public static IEnumerable<object[]> AnycastData()
        {
            yield return new object[] { ReadJsonFile("IPLookupResultAnycast.json") };
        }

        public static IEnumerable<object[]> VpnData()
        {
            yield return new object[] { ReadJsonFile("IPLookupResultVpn.json") };
        }

        public static IEnumerable<object[]> BulkData()
        {
            var items = new[]
            {
                ReadJsonFile("IPLookupResultSatellite.json"),
                ReadJsonFile("IPLookupResultAnycast.json"),
                ReadJsonFile("IPLookupResultVpn.json")
            };

            yield return new object[] { "[" + string.Join(",", items) + "]" };
        }

        private static string ReadJsonFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"IPData.Tests.DataSources.TestData.{fileName}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
