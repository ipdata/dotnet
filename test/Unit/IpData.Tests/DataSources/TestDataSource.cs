using System.Collections.Generic;

namespace IpData.Tests.DataSources
{
    public static class TestDataSource
    {
        public static IEnumerable<object[]> EmptyOrWhitespaceString()
        {
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null };
        }

        public static IEnumerable<object[]> IpInfoData()
        {
            yield return new object[] { "{\"ip\":\"1.1.1.1\",\"is_eu\":false,\"city\":null,\"region\":null,\"region_code\":null,\"country_name\":\"Australia\",\"country_code\":\"AU\",\"continent_name\":\"Oceania\",\"continent_code\":\"OC\",\"latitude\":-33.494,\"longitude\":143.2104,\"postal\":null,\"calling_code\":\"61\",\"flag\":\"https://ipdata.co/flags/au.png\",\"emoji_flag\":\"\uD83C\uDDE6\uD83C\uDDFA\",\"emoji_unicode\":\"U+1F1E6 U+1F1FA\",\"asn\":{\"asn\":\"AS13335\",\"name\":\"Cloudflare, Inc.\",\"domain\":\"cloudflare.com\",\"route\":\"1.1.1.0/24\",\"type\":\"hosting\"},\"languages\":[{\"name\":\"English\",\"native\":\"English\"}],\"currency\":{\"name\":\"Australian Dollar\",\"code\":\"AUD\",\"symbol\":\"AU$\",\"native\":\"$\",\"plural\":\"Australian dollars\"},\"time_zone\":{\"name\":\"Australia/Sydney\",\"abbr\":\"AEST\",\"offset\":\"+1000\",\"is_dst\":false,\"current_time\":\"2019-09-27T03:47:04.927862+10:00\"},\"threat\":{\"is_tor\":false,\"is_proxy\":false,\"is_anonymous\":false,\"is_known_attacker\":false,\"is_known_abuser\":true,\"is_threat\":true,\"is_bogon\":false},\"count\":\"1\"}" };
        }

        public static IEnumerable<object[]> CarrierData()
        {
            yield return new object[] { "{\"name\":\"T-Mobile\",\"mcc\":\"310\",\"mnc\":\"160\"\r\n}" };
        }
    }
}
