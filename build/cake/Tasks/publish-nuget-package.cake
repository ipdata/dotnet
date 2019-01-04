Task("Publish-NuGet-Package")
    .IsDependentOn("Create-NuGet-Package")
    .Does(() =>
    {
        var packages = GetFiles("../../artifacts/IpData.*.nupkg");

        NuGetPush(packages, new NuGetPushSettings {
          ApiKey = NuGetApiKey
        });
    });