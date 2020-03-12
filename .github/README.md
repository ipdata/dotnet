
# ipdata 
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/alexkhil/IpData/blob/master/LICENSE) [![IpData](https://img.shields.io/nuget/v/IpData.svg)](https://www.nuget.org/packages/IpData/) [![Build Status](https://dev.azure.com/alexkhildev/IpData/_apis/build/status/outer-loop?branchName=master)](https://dev.azure.com/alexkhildev/IpData/_build/latest?definitionId=4?branchName=master) [![Coverage Status](https://img.shields.io/azure-devops/coverage/alexkhildev/ipdata/4/master)](https://img.shields.io/azure-devops/coverage/alexkhildev/ipdata/4/master)

[ipdata.co](https://ipdata.co/) is a fast, reliable and clean service that allows you to look up the location of an IP Address and other data.

## Table of Content

- [Install](#install)
- [Lookup](#lookup)
  - [Basic](#basic)
  - [Bulk](#bulk)
  - [Carrier](#carrier)
  - [Asn](#asn)
  - [Timezone](#timezone)
  - [Currency](#currency)
  - [Threat](#threat)
- [Contributing](#contributing)
- [Versioning](#versioning)
- [License](#license)

## Install

NuGet package install using package manager:

```bash
Install-Package IpData -Version 2.0.1
```

NuGet package install using .NET CLI:

```bash
dotnet add package IpData --version 2.0.1
```

## Lookup

All usage examples you can find on `samples` folder.

### Basic

```csharp
var client = new IpDataClient("API_KEY");

// Get IP data from my IP
var myIpInfo = await client.Lookup();
Console.WriteLine($"Country name for {myIpInfo.Ip} is {myIpInfo.CountryName}");

// Get IP data from IP
var ipInfo = await client.Lookup("8.8.8.8");
Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");

// Get single field from IP
var countryName = await client.Lookup("8.8.8.8", x => x.CountryName);
Console.WriteLine($"Country name for 8.8.8.8 is {countryName}");

// Get multiple fields from IP
var geolocation = await client.Lookup("8.8.8.8", x => x.Latitude, x => x.Longitude);
Console.WriteLine($"Geolocation for 8.8.8.8 is lat: {geolocation.Latitude} long: {geolocation.Longitude}");
```

### Bulk

From ipdata.co docs:
> Note that bulk lookups are only available to paid users and are currently limited to a 100 at a time. Reach out to support if you need to lookup larger batches.

```csharp
var client = new IpDataClient("API_KEY");

var ipInfoList = await client.Lookup(new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" });
foreach (var ipInfo in ipInfoList)
{
    Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");
}
```

### Carrier

```csharp
var client = new IpDataClient("API_KEY");

var carrierInfo = await client.Carrier("69.78.70.144");
Console.WriteLine($"Carrier name: {carrierInfo.Name}");
```

### ASN

```csharp
var client = new IpDataClient("API_KEY");

var asnInfo = await client.Asn("69.78.70.144");
Console.WriteLine($"ASN name: {asnInfo.Name}");
```

### Timezone

```csharp
var client = new IpDataClient("API_KEY");

var timezoneInfo = await client.TimeZone("69.78.70.144");
Console.WriteLine($"TimeZone name: {timezoneInfo.Name}");
```

### Currency

```csharp
var client = new IpDataClient("API_KEY");

var currencyInfo = await client.Currency("69.78.70.144");
Console.WriteLine($"Currency name: {currencyInfo.Name}");
```

### Threat

```csharp
var client = new IpDataClient("API_KEY");

var threatInfo = await client.Threat("69.78.70.144");
Console.WriteLine($"Threat is Tor: {threatInfo.IsTor}");
```

## Contributing

Please read [CONTRIBUTING.md][CONTRIBUTING] for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer] for versioning. For the versions available, see the tags on this repository. 

## License

This project is licensed under the MIT License - see the [LICENSE.md][LICENSE] file for details


[AzureStatus]: https://dev.azure.com/alexkhildev/IpData/_apis/build/status/gated?branchName=master
[IpDataLogo]: https://image.ibb.co/iDQdUS/ipdatalogo.png
[SemVer]: http://semver.org/
[CONTRIBUTING]: https://github.com/alexkhil/IpData/blob/master/.github/CONTRIBUTING.md
[LICENSE]: https://github.com/alexkhil/IpData/blob/master/LICENSE
