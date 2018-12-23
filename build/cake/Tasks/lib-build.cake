Task("LibBuild")
    .IsDependentOn("Restore")
    .Does(() => {
        var dotNetCoreBuildSettings = new DotNetCoreBuildSettings {
            Configuration = configuration,
            NoRestore = true
        };

        DotNetCoreBuild(Paths.Solution.FullPath, dotNetCoreBuildSettings);
    });