using System;
using System.Threading.Tasks;

namespace IpData.Carrier
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var client = new IpDataClient("API_KEY");
            var carrierInfo = await client.Carrier("69.78.70.144");
        }
    }
}
