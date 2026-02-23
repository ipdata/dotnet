using System.Collections.Generic;

namespace IPData.Tests.DataSources
{
    public static class TestDataSource
    {
        public static IEnumerable<object[]> EmptyOrWhitespaceString()
        {
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null };
        }

        public static IEnumerable<object[]> IPLookupResultData()
        {
            yield return new object[] { "{\"ip\":\"91.225.201.108\",\"is_eu\":false,\"city\":\"Lviv\",\"region\":\"L'vivs'ka Oblast'\",\"region_code\":\"46\",\"region_type\":\"oblast\",\"country_name\":\"Ukraine\",\"country_code\":\"UA\",\"continent_name\":\"Europe\",\"continent_code\":\"EU\",\"latitude\":49.8486,\"longitude\":24.0323,\"postal\":\"79000\",\"calling_code\":\"380\",\"flag\":\"https:\\/\\/ipdata.co\\/flags\\/ua.png\",\"emoji_flag\":\"\uD83C\uDDFA\uD83C\uDDE6\",\"emoji_unicode\":\"U+1F1FA U+1F1E6\",\"asn\":{\"asn\":\"AS49824\",\"name\":\"PC \\\"Astra-net\\\"\",\"domain\":\"astra.in.ua\",\"route\":\"91.225.200.0\\/22\",\"type\":\"isp\"},\"company\":{\"name\":\"Astra-net\",\"domain\":\"astra.in.ua\",\"network\":\"91.225.200.0\\/22\",\"type\":\"isp\"},\"carrier\":{\"name\":\"Kyivstar\",\"mcc\":\"255\",\"mnc\":\"03\"},\"languages\":[{\"name\":\"Ukrainian\",\"native\":\"\u0423\u043A\u0440\u0430\u0457\u043D\u0441\u044C\u043A\u0430\",\"code\":\"uk\"}],\"currency\":{\"name\":\"Ukrainian Hryvnia\",\"code\":\"UAH\",\"symbol\":\"\u20B4\",\"native\":\"\u20B4\",\"plural\":\"Ukrainian hryvnias\"},\"time_zone\":{\"name\":\"Europe\\/Kiev\",\"abbr\":\"EET\",\"offset\":\"+0200\",\"is_dst\":false,\"current_time\":\"2020-01-30T23:16:19.129316+02:00\"},\"threat\":{\"is_tor\":false,\"is_icloud_relay\":false,\"is_proxy\":false,\"is_datacenter\":false,\"is_anonymous\":false,\"is_known_attacker\":false,\"is_known_abuser\":false,\"is_threat\":false,\"is_bogon\":false,\"blocklists\":[]},\"status\":200}" };
        }

        public static IEnumerable<object[]> CarrierData()
        {
            yield return new object[] { "{\"name\":\"T-Mobile\",\"mcc\":\"310\",\"mnc\":\"160\"\r\n}" };
        }

        public static IEnumerable<object[]> CompanyData()
        {
            yield return new object[] { "{\"name\":\"Google LLC\",\"domain\":\"google.com\",\"network\":\"8.8.8.0\\/24\",\"type\":\"business\"}" };
        }
    }
}
