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

            // Get localized ip data from my ip
            var myIpInfoLocalized = await client.Lookup(CultureInfo.GetCultureInfo("zh-CN"));
        }
    }
}
