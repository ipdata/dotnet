Task("Publish-Coverage")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        CoverallsNet(CoverallsPath, CoverallsNetReportType.OpenCover, new CoverallsNetSettings
        {
            CommitBranch = CommitBranch,
            CommitMessage = CommitMessage,
            RepoToken = CoverallsToken
        });
    });