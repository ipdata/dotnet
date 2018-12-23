using System;
using System.Globalization;
using System.Threading.Tasks;

namespace IpData.Basic
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var client = new IpDataClient("API_KEY");

            // Get ip data from my ip
            var myIpInfo = await client.Lookup();
            Console.WriteLine($"Country name for {myIpInfo.Ip} is {myIpInfo.CountryName}");

            // Get localized ip data from my ip
            var myIpInfoLocalized = await client.Lookup(CultureInfo.GetCultureInfo("zh-CN"));
            Console.WriteLine($"Localized country name for {myIpInfoLocalized.Ip} is {myIpInfoLocalized.CountryName}");
        }
    }
}
