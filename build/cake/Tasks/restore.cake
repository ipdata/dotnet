Task("Restore")
    .Does(() => {
        DotNetCoreRestore(Paths.Solution.FullPath);
    });