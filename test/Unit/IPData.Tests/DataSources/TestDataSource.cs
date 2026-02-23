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
