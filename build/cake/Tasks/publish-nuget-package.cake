Task("Publish-NuGet-Package")
    .IsDependentOn("Create-NuGet-Package")
    .Does(() =>
    {
        var packages = GetFiles("../../artifacts/IpData.*.nupkg");

        if (IsNuGetPublished(packages[0]))
        {
            Information($"{packages[0].FullPath} already published");
            return;
        }

        NuGetPush(packages, new NuGetPushSettings {
            Source = "https://api.nuget.org/v3/index.json",
            ApiKey = NuGetApiKey
        });
    });