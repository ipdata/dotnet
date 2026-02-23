using System;
using System.Globalization;
using System.Threading.Tasks;

namespace IPData.Lookup
{
    public static class Program
    {
        public static async Task Main()
        {
            var client = new IPDataClient("API_KEY");

            // Get IP data from my IP
            var myIPLookupResult = await client.Lookup();
            Console.WriteLine($"Country name for {myIPLookupResult.Ip} is {myIPLookupResult.CountryName}");

            // Get IP data from IP
            var ipInfo = await client.Lookup("8.8.8.8");
            Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");

            // Get single field from IP
            var countryName = await client.Lookup("8.8.8.8", x => x.CountryName);
            Console.WriteLine($"Country name for 8.8.8.8 is {countryName}");
            
            // Get multiple fields from IP
            var geolocation = await client.Lookup("8.8.8.8", x => x.Latitude, x => x.Longitude);
            Console.WriteLine($"Geolocation for 8.8.8.8 is lat: {geolocation.Latitude} long: {geolocation.Longitude}");

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
            var ipInfoList = await client.Lookup(new[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" });
            foreach (var info in ipInfoList)
            {
                Console.WriteLine($"Country name for {info.Ip} is {info.CountryName}");
            }
        }
    }
}
