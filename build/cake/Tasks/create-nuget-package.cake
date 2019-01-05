Task("Create-NuGet-Package")
    .IsDependentOn("Publish-Coverage")
    .Does(() =>
    {
        var settings = new NuGetPackSettings
        {
            OutputDirectory = Paths.Artifacts,
            IncludeReferencedProjects = true,
            Properties = new Dictionary<string, string>
            {
                { "Configuration", configuration }
            }
        };

        NuGetPack(Paths.Project, settings);
    });