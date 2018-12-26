Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() => {
        DotNetCoreRestore(Paths.Solution.FullPath);
    });