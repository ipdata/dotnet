using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IpData.Models;

namespace IpData
{
    public interface IIpDataClient
    {
        string ApiKey { get; }

        Task<CarrierInfo> Carrier(string ip);

        Task<IpInfo> Lookup();

        Task<IpInfo> Lookup(CultureInfo culture);

        Task<IEnumerable<IpInfo>> Lookup(IReadOnlyCollection<string> ips);

        Task<IpInfo> Lookup(string ip);

        Task<IpInfo> Lookup(string ip, CultureInfo culture);

        Task<string> Lookup(string ip, Expression<Func<IpInfo, object>> fieldSelector);
    }
}