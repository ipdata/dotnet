using System;
using System.Globalization;
using System.Threading.Tasks;

namespace IpData.Lookup
{
    public static class Program
    {
        public static async Task Main()
        {
            var client = new IpDataClient("ea1d25cb36a3c66b3fdd02c33129e508106835d8a102b8ed65d8eb82");

            // Get IP data from my IP
            var myIpInfo = await client.Lookup("1.1.1.1");
            Console.WriteLine($"Country name for {myIpInfo.Ip} is {myIpInfo.CountryName}");

            // Get localized IP data from my IP
            var myIpInfoLocalized = await client.Lookup(CultureInfo.GetCultureInfo("zh-CN"));
            Console.WriteLine($"Localized country name for {myIpInfoLocalized.Ip} is {myIpInfoLocalized.CountryName}");

            // Get IP data from IP
            var ipInfo = await client.Lookup("8.8.8.8");
            Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");

            // Get localized IP data from IP
            var ipInfoLocalized = await client.Lookup("8.8.8.8", CultureInfo.GetCultureInfo("zh-CN"));
            Console.WriteLine($"Localized country name for {myIpInfoLocalized.Ip} is {ipInfoLocalized.CountryName}");

            // Get single field from IP
            var countryName = await client.Lookup("8.8.8.8", x => x.CountryName);
            Console.WriteLine($"Country name for 8.8.8.8 is {countryName}");

            // Get carrier info from IP
            var carrierInfo = await client.Carrier("69.78.70.144");
            Console.WriteLine($"Carrier name: {carrierInfo.Name}");

            // Get ASN info from IP
            var asnInfo = await client.Asn("69.78.70.144");
            Console.WriteLine($"ASN name: {asnInfo.Name}");

            // Get timezone info from IP
            var timezoneInfo = await client.TimeZone("69.78.70.144");
            Console.WriteLine($"TimeZone name: {timezoneInfo.Name}");

            // Get currency info from IP
            var currencyInfo = await client.Currency("69.78.70.144");
            Console.WriteLine($"Currency name: {currencyInfo.Name}");

            // Get threat info from IP
            var threatInfo = await client.Threat("69.78.70.144");
            Console.WriteLine($"Threat is Tor: {threatInfo.IsTor}");

            // Get IP data for multiple IPs
            var ipInfoList = await client.Lookup(new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" });
            foreach (var info in ipInfoList)
            {
                Console.WriteLine($"Country name for {info.Ip} is {info.CountryName}");
            }
        }
    }
}
