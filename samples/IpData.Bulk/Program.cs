using System.Threading.Tasks;

namespace IpData.Bulk
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var client = new IpDataClient("API_KEY");

            // Get ip data for multiple ips
            var ipInfoList = await client.Lookup(new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" });
        }
    }
}
