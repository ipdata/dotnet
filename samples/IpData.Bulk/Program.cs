using System;
using System.Threading.Tasks;

namespace IpData.Bulk
{
    public static class Program
    {
        public static async Task Main()
        {
            var client = new IpDataClient("API_KEY");

            // Get ip data for multiple ips
            var ipInfoList = await client.Lookup(new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" });
            foreach (var ipInfo in ipInfoList)
            {
                Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");
            }
        }
    }
}
