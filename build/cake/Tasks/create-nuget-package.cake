Task("Create-NuGet-Package")
    .IsDependentOn("Run-Unit-Tests")
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