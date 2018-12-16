using System.Threading.Tasks;

namespace IpData.Basic
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var client = new IpDataClient("API_KEY");
            var result = await client.Carrier("69.78.70.144");
        }
    }
}
