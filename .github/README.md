
# ipdata 
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/alexkhil/IPData/blob/master/LICENSE) [![IPData](https://img.shields.io/nuget/v/IPData.svg)](https://www.nuget.org/packages/IPData/) [![Build Status](https://dev.azure.com/alexkhildev/IPData/_apis/build/status/outer-loop?branchName=master)](https://dev.azure.com/alexkhildev/IPData/_build/latest?definitionId=4?branchName=master) [![Coverage Status](https://img.shields.io/azure-devops/coverage/alexkhildev/ipdata/4/master)](https://img.shields.io/azure-devops/coverage/alexkhildev/ipdata/4/master)

[ipdata.co](https://ipdata.co/) is a fast, reliable and clean service that allows you to look up the location of an IP Address and other data.

## Table of Content

- [Install](#install)
- [Lookup](#lookup)
  - [Basic](#basic)
  - [Bulk](#bulk)
  - [Carrier](#carrier)
  - [Company](#company)
  - [Asn](#asn)
  - [Timezone](#timezone)
  - [Currency](#currency)
  - [Threat](#threat)
- [EU Endpoint](#eu-endpoint)
- [Contributing](#contributing)
- [Versioning](#versioning)
- [License](#license)

## Install

NuGet package install using package manager:

```bash
Install-Package IPData -Version 3.0.0
```

NuGet package install using .NET CLI:

```bash
dotnet add package IPData --version 3.0.0
```

## Lookup

All usage examples you can find on `samples` folder.

### Basic

```csharp
var client = new IPDataClient("API_KEY");

// Get IP data from my IP
var myIPInfo = await client.Lookup();
Console.WriteLine($"Country name for {myIPInfo.Ip} is {myIPInfo.CountryName}");

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
var client = new IPDataClient("API_KEY");

var ipInfoList = await client.Lookup(new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" });
foreach (var ipInfo in ipInfoList)
{
    Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");
}
```

### Carrier

```csharp
var client = new IPDataClient("API_KEY");

var carrierInfo = await client.Carrier("69.78.70.144");
Console.WriteLine($"Carrier name: {carrierInfo.Name}");
```

### Company

```csharp
var client = new IPDataClient("API_KEY");

var companyInfo = await client.Company("69.78.70.144");
Console.WriteLine($"Company name: {companyInfo.Name}");
```

### ASN

```csharp
var client = new IPDataClient("API_KEY");

var asnInfo = await client.Asn("69.78.70.144");
Console.WriteLine($"ASN name: {asnInfo.Name}");
```

### Timezone

```csharp
var client = new IPDataClient("API_KEY");

var timezoneInfo = await client.TimeZone("69.78.70.144");
Console.WriteLine($"TimeZone name: {timezoneInfo.Name}");
```

### Currency

```csharp
var client = new IPDataClient("API_KEY");

var currencyInfo = await client.Currency("69.78.70.144");
Console.WriteLine($"Currency name: {currencyInfo.Name}");
```

### Threat

```csharp
var client = new IPDataClient("API_KEY");

var threatInfo = await client.Threat("69.78.70.144");
Console.WriteLine($"Threat is Tor: {threatInfo.IsTor}");
```

## EU Endpoint

To ensure your data stays in the EU, use the EU endpoint by passing a custom base URL:

```csharp
var client = new IPDataClient("API_KEY", new Uri("https://eu-api.ipdata.co"));

var ipInfo = await client.Lookup("8.8.8.8");
```

## Contributing

Please read [CONTRIBUTING.md][CONTRIBUTING] for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer] for versioning. For the versions available, see the tags on this repository. 

## License

This project is licensed under the MIT License - see the [LICENSE.md][LICENSE] file for details


[AzureStatus]: https://dev.azure.com/alexkhildev/IPData/_apis/build/status/gated?branchName=master
[IPDataLogo]: https://image.ibb.co/iDQdUS/ipdatalogo.png
[SemVer]: http://semver.org/
[CONTRIBUTING]: https://github.com/alexkhil/IPData/blob/master/.github/CONTRIBUTING.md
[LICENSE]: https://github.com/alexkhil/IPData/blob/master/LICENSE
