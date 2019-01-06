using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IpData.Models;

namespace IpData
{
    /// <summary>Interface of IpDataClient.</summary>
    public interface IIpDataClient
    {
        /// <summary>The IpData ApiKey.</summary>
        string ApiKey { get; }

        /// <summary>Fetch carrier for ip.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <returns>The carrier info <see cref="CarrierInfo"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when ip address is private or invalid.
        /// </exception>
        /// <exception cref="Exceptions.ForbiddenException">
        /// Thrown when you have exceeded your daily plan quota.
        /// </exception>
        /// <exception cref="Exceptions.UnauthorizedException">
        /// Thrown when API key is not provided.
        /// </exception>
        /// <exception cref="Exceptions.ApiException">
        /// Thrown when unexpected case occurred.
        /// </exception>
        Task<CarrierInfo> Carrier(string ip);

        /// <summary>Fetch ip info for your ip.</summary>
        /// <returns>The ip info <see cref="IpInfo">.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when ip address is private or invalid.
        /// </exception>
        /// <exception cref="Exceptions.ForbiddenException">
        /// Thrown when you have exceeded your daily plan quota.
        /// </exception>
        /// <exception cref="Exceptions.UnauthorizedException">
        /// Thrown when API key is not provided.
        /// </exception>
        /// <exception cref="Exceptions.ApiException">
        /// Thrown when unexpected case occurred.
        /// </exception>
        Task<IpInfo> Lookup();

        /// <summary>Fetch localized ip info for your ip</summary>
        /// <param name="culture">The culture info.</param>
        /// <returns>Localized ip info <see cref="IpInfo"/></returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when ip address is private or invalid.
        /// </exception>
        /// <exception cref="Exceptions.ForbiddenException">
        /// Thrown when you have exceeded your daily plan quota.
        /// </exception>
        /// <exception cref="Exceptions.UnauthorizedException">
        /// Thrown when API key is not provided.
        /// </exception>
        /// <exception cref="Exceptions.ApiException">
        /// Thrown when unexpected case occurred.
        /// </exception>
        /// <example>
        /// <code>
        /// Lookup(CultureInfo.GetCultureInfo("zh-CN"));
        /// </code>
        /// </example>
        Task<IpInfo> Lookup(CultureInfo culture);

        /// <summary>Fetch ip info for few ips.</summary>
        /// <param name="ips">The list of IPv4 addresses.</param>
        /// <returns>The list of ip info <see cref="IpInfo"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when ip address is private or invalid.
        /// </exception>
        /// <exception cref="Exceptions.ForbiddenException">
        /// Thrown when you have exceeded your daily plan quota.
        /// </exception>
        /// <exception cref="Exceptions.UnauthorizedException">
        /// Thrown when API key is not provided.
        /// </exception>
        /// <exception cref="Exceptions.ApiException">
        /// Thrown when unexpected case occurred.
        /// </exception>
        Task<IEnumerable<IpInfo>> Lookup(IReadOnlyCollection<string> ips);

        /// <summary>Fetch ip info for IPv4 address.</summary>
        /// <param name="ip">The IPv4 Address.</param>
        /// <returns>The ip info for IPv4 address.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when ip address is private or invalid.
        /// </exception>
        /// <exception cref="Exceptions.ForbiddenException">
        /// Thrown when you have exceeded your daily plan quota.
        /// </exception>
        /// <exception cref="Exceptions.UnauthorizedException">
        /// Thrown when API key is not provided.
        /// </exception>
        /// <exception cref="Exceptions.ApiException">
        /// Thrown when unexpected case occurred.
        /// </exception>
        Task<IpInfo> Lookup(string ip);

        /// <summary>Fetch localized ip info for IPv4 address.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>Localized ip info <see cref="IpInfo"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when ip address is private or invalid.
        /// </exception>
        /// <exception cref="Exceptions.ForbiddenException">
        /// Thrown when you have exceeded your daily plan quota.
        /// </exception>
        /// <exception cref="Exceptions.UnauthorizedException">
        /// Thrown when API key is not provided.
        /// </exception>
        /// <exception cref="Exceptions.ApiException">
        /// Thrown when unexpected case occurred.
        /// </exception>
        /// <example>
        /// <code>
        /// Lookup("8.8.8.8", CultureInfo.GetCultureInfo("zh-CN"));
        /// </code>
        /// </example>
        Task<IpInfo> Lookup(string ip, CultureInfo culture);

        /// <summary>Fetch single ip info filed from IPv4 address.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <param name="fieldSelector">The field selector for field to return.</param>
        /// <returns>The single ip info field value.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when ip address is private or invalid.
        /// </exception>
        /// <exception cref="Exceptions.ForbiddenException">
        /// Thrown when you have exceeded your daily plan quota.
        /// </exception>
        /// <exception cref="Exceptions.UnauthorizedException">
        /// Thrown when API key is not provided.
        /// </exception>
        /// <exception cref="Exceptions.ApiException">
        /// Thrown when unexpected case occurred.
        /// </exception>
        /// <example>
        /// <code>
        /// Lookup("8.8.8.8", x => x.CountryName);
        /// </code>
        /// </example>
        Task<string> Lookup(string ip, Expression<Func<IpInfo, object>> fieldSelector);
    }
}