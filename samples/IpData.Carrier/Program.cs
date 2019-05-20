using System;
using System.Threading.Tasks;

namespace IpData.Carrier
{
    public static class Program
    {
        public static async Task Main()
        {
            var client = new IpDataClient("API_KEY");

            // Get carrier info from ip
            var carrierInfo = await client.Carrier("69.78.70.144");
            Console.WriteLine($"Carrier name: {carrierInfo.Name}");
        }
    }
}
