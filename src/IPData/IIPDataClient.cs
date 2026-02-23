using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IPData.Models;

namespace IPData
{
    /// <summary>Interface of IPDataClient.</summary>
    public interface IIPDataClient
    {
        /// <summary>The IPData ApiKey.</summary>
        string ApiKey { get; }

        /// <summary>Fetch company for IP.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <returns>The company info <see cref="CompanyInfo"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<CompanyInfo> Company(string ip);

        /// <summary>Fetch carrier for IP.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <returns>The carrier info <see cref="CarrierInfo"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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

        /// <summary>Fetch ASN for IP.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <returns>The ASN info <see cref="AsnInfo"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<AsnInfo> Asn(string ip);

        /// <summary>Fetch timezone for IP.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <returns>The timezone info <see cref="Models.TimeZone"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<Models.TimeZone> TimeZone(string ip);

        /// <summary>Fetch currency for IP.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <returns>The currency info <see cref="Models.Currency"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<Currency> Currency(string ip);

        /// <summary>Fetch threat for IP.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <returns>The threat info <see cref="Models.Threat"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<Threat> Threat(string ip);

        /// <summary>Fetch IP info for your IP.</summary>
        /// <returns>The IP info <see cref="Models.IPLookupResult">.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<IPLookupResult> Lookup();

        /// <summary>Fetch localized IP info for your IP.</summary>
        /// <param name="culture">The culture info.</param>
        /// <returns>Localized IP info <see cref="IPLookupResult"/></returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<IPLookupResult> Lookup(CultureInfo culture);

        /// <summary>Fetch IP info for few IPs.</summary>
        /// <param name="ips">The list of IPv4 addresses.</param>
        /// <returns>The list of IP info <see cref="IPLookupResult"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<IEnumerable<IPLookupResult>> Lookup(IReadOnlyCollection<string> ips);

        /// <summary>Fetch IP info for IPv4 address.</summary>
        /// <param name="ip">The IPv4 Address.</param>
        /// <returns>The IP info for IPv4 address.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<IPLookupResult> Lookup(string ip);

        /// <summary>Fetch localized IP info for IPv4 address.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>Localized IP info <see cref="IPLookupResult"/>.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<IPLookupResult> Lookup(string ip, CultureInfo culture);

        /// <summary>Fetch single IP info fields from IPv4 address.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <param name="fieldSelector">The field selector for field to return.</param>
        /// <returns>The single IP info field value.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        Task<string> Lookup(string ip, Expression<Func<IPLookupResult, object>> fieldSelector);

        /// <summary>Fetch multiple IP info fields from IPv4 address.</summary>
        /// <param name="ip">The IPv4 address.</param>
        /// <param name="fieldSelectors">Field selectors for fields to return.</param>
        /// <returns>Multiple field IP info.</returns>
        /// <exception cref="Exceptions.BadRequestException">
        /// Thrown when IP address is private or invalid.
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
        /// Lookup("8.8.8.8", x => x.CountryName, x => x.City);
        /// </code>
        /// </example>
        Task<IPLookupResult> Lookup(string ip, params Expression<Func<IPLookupResult, object>>[] fieldSelectors);
    }
}