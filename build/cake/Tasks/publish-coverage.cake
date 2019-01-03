Task("Publish-Coverage")
    .Does(() =>
    {
        CoverallsNet(CoverallsPath, CoverallsNetReportType.OpenCover, new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken,
            CommitBranch = EnvironmentVariable("Build.SourceBranchName") ?? "test"
        });
    });