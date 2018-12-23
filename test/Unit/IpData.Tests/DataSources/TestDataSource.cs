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
            yield return new object[] { "{\"ip\":\"1.1.1.1\",\"is_eu\":false,\"city\":\"\",\"region\":\"\",\"region_code\":\"\",\"country_name\":\"Australia\",\"country_code\":\"AU\",\"continent_name\":\"Oceania\",\"continent_code\":\"OC\",\"latitude\":-33.494,\"longitude\":143.2104,\"asn\":\"AS13335\",\"organisation\":\"Cloudflare,Inc.\",\"postal\":\"\",\"calling_code\":\"61\",\"flag\":\"https://ipdata.co/flags/au.png\",\"emoji_flag\":\"\\ud83c\\udde6\\ud83c\\uddfa\",\"emoji_unicode\":\"U+1F1E6U+1F1FA\",\"languages\":[{\"name\":\"English\",\"native\":\"English\"}],\"currency\":{\"name\":\"AustralianDollar\",\"code\":\"AUD\",\"symbol\":\"AU$\",\"native\":\"$\",\"plural\":\"Australiandollars\"},\"time_zone\":{\"name\":\"\",\"abbr\":\"\",\"offset\":\"\",\"is_dst\":\"\",\"current_time\":\"\"},\"threat\":{\"is_tor\":false,\"is_proxy\":false,\"is_anonymous\":false,\"is_known_attacker\":false,\"is_known_abuser\":false,\"is_threat\":false,\"is_bogon\":false},\"count\":\"0\"}" };
        }

        public static IEnumerable<object[]> CarrierData()
        {
            yield return new object[] { "{\"name\":\"T-Mobile\",\"mcc\":\"310\",\"mnc\":\"160\"\r\n}" };
        }
    }
}
