using System;
using System.Globalization;
using System.Threading.Tasks;

namespace IpData.Basic
{
    public static class Program
    {
        public static async Task Main()
        {
            var client = new IpDataClient("API_KEY");

            // Get ip data from my ip
            var myIpInfo = await client.Lookup();
            Console.WriteLine($"Country name for {myIpInfo.Ip} is {myIpInfo.CountryName}");

            // Get localized ip data from my ip
            var myIpInfoLocalized = await client.Lookup(CultureInfo.GetCultureInfo("zh-CN"));
            Console.WriteLine($"Localized country name for {myIpInfoLocalized.Ip} is {myIpInfoLocalized.CountryName}");

            // Get ip data from ip
            var ipInfo = await client.Lookup("8.8.8.8");
            Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");

            // Get localized ip data from ip
            var ipInfoLocalized = await client.Lookup("8.8.8.8", CultureInfo.GetCultureInfo("zh-CN"));
            Console.WriteLine($"Localized country name for {myIpInfoLocalized.Ip} is {ipInfoLocalized.CountryName}");

            // Get single field from ip
            var countryName = await client.Lookup("8.8.8.8", x => x.CountryName);
            Console.WriteLine($"Country name for 8.8.8.8 is {countryName}");
        }
    }
}
