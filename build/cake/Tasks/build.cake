Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() => {
        var dotNetCoreBuildSettings = new DotNetCoreBuildSettings {
            Configuration = configuration,
            NoRestore = true
        };

        DotNetCoreBuild(Paths.Solution.FullPath, dotNetCoreBuildSettings);
    });