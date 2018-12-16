using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IpData.Models;

namespace IpData
{
    public interface IIpDataClient
    {
        string ApiKey { get; }

        string Culture { get; }

        Task<IpInfo> Lookup();

        Task<IpInfo> Lookup(string ip);

        Task<IpInfo> Lookup(string ip, Expression<Func<IpInfo, object>> fieldSelector);

        Task<IEnumerable<IpInfo>> Lookup(IReadOnlyCollection<string> ips);

        Task<CarrierInfo> Carrier(string ip);
    }
}