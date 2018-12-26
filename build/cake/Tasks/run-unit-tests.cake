Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var dotNetCoreTestSettings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoRestore = true,
            NoBuild = true,
            ResultsDirectory = Paths.Artifacts.FullPath,
            ArgumentCustomization = args =>
            {
                args.Append("--logger trx;LogFileName=UnitTests.trx");
                args.Append("/p:CollectCoverage=true /p:CoverletOutputFormat=cobertura");
                args.Append($"/p:CoverletOutput=\"../{Paths.Artifacts.FullPath}/\"");
                args.Append("/p:Include=\"[IpData]*\"");
                return args;
            }
        };

        DotNetCoreTest(Paths.UnitTestProject.FullPath, dotNetCoreTestSettings);
    });