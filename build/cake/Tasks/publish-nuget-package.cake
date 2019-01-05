Task("Publish-NuGet-Package")
    .IsDependentOn("Create-NuGet-Package")
    .Does(() =>
    {
        var packages = GetFiles("../../artifacts/IpData.*.nupkg");

        NuGetPush(packages, new NuGetPushSettings {
            Source = "https://api.nuget.org/v3/index.json",
            ApiKey = NuGetApiKey
        });
    });