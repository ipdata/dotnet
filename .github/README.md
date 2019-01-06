<a href="https://ipdata.co/">
    <img src="https://image.ibb.co/iDQdUS/ipdatalogo.png" alt="Ip Data Logo" title="IpData" align="left" height="60" />
</a>

# IpData [![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/alexkhil/IpData/blob/master/LICENSE) 

[ipdata.co](https://ipdata.co/) is a fast, reliable and clean service that allows you to look up the location of an IP Address and other data.

| Branch | Platform | Status | Coverage |
| ------ | ------ | ------ | ------ |
| **Master** | Linux\Windows\macOS | [![Build Status](https://dev.azure.com/alexkhildev/IpData/_apis/build/status/outer-loop?branchName=master)](https://dev.azure.com/alexkhildev/IpData/_build/latest?definitionId=4?branchName=master) | [![Coverage Status](https://coveralls.io/repos/github/alexkhil/IpData/badge.svg?branch=%28no+branch%29)](https://coveralls.io/github/alexkhil/IpData?branch=%28no+branch%29) |
| **Develop** | Linux\Windows\macOS | [![Build Status](https://dev.azure.com/alexkhildev/IpData/_apis/build/status/gated?branchName=develop)](https://dev.azure.com/alexkhildev/IpData/_build/latest?definitionId=3?branchName=develop) | [![Coverage Status](https://coveralls.io/repos/github/alexkhil/IpData/badge.svg?branch=develop)](https://coveralls.io/github/alexkhil/IpData?branch=develop) |

## Table of Content

- [Installing](#installing)
- [Usage](#usage)
  - [Basic Lookup](#basic-lookup)
  - [Bulk Lookup](#bulk-lookup)
  - [Carrier Lookup](#carrier-lookup)
- [Contributing](#contributing)
- [Versioning](#versioning)
- [License](#license)

## Installing

NuGet package install using package manager:

```bash
Install-Package IpData -Version 1.0.0
```

NuGet package install using .NET CLI:

```bash
dotnet add package IpData --version 1.0.0
```

## Usage

All usage examples you can find on `samples` folder.

### Basic Lookup

```C#
var client = new IpDataClient("API_KEY");

// Get ip data from my ip
var myIpInfo = await client.Lookup();
Console.WriteLine($"Country name for {myIpInfo.Ip} is {myIpInfo.CountryName}");

// Get localized ip data from my ip
var myIpInfoLocalized = await client.Lookup(CultureInfo.GetCultureInfo("zh-CN"));
Console.WriteLine($"Localized country name for {myIpInfoLocalized.Ip} is {myIpInfoLocalized.CountryName}");

// Get ip data from ip
var ipInfo = await client.Lookup("8.8.8.8");
Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");

// Get localized ip data from ip
var ipInfoLocalized = await client.Lookup("8.8.8.8", CultureInfo.GetCultureInfo("zh-CN"));
Console.WriteLine($"Localized country name for {myIpInfoLocalized.Ip} is {ipInfoLocalized.CountryName}");

// Get single field from ip
var countryName = await client.Lookup("8.8.8.8", x => x.CountryName);
Console.WriteLine($"Country name for 8.8.8.8 is {countryName}");
```

### Bulk Lookup

From ipdata.co docs:
> Note that bulk lookups are only available to paid users and are currently limited to a 100 at a time. Reach out to support if you need to lookup larger batches.

```C#
var client = new IpDataClient("API_KEY");

var ipInfoList = await client.Lookup(new string[] { "1.1.1.1", "2.2.2.2", "3.3.3.3" });
foreach (var ipInfo in ipInfoList)
{
    Console.WriteLine($"Country name for {ipInfo.Ip} is {ipInfo.CountryName}");
}
```

### Carrier Lookup

```C#
var client = new IpDataClient("API_KEY");

var carrierInfo = await client.Carrier("69.78.70.144");
Console.WriteLine($"Carrier name: {carrierInfo.Name}");
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
