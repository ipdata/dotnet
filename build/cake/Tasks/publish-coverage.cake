Task("Publish-Coverage")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        CoverallsNet(CoverallsPath, CoverallsNetReportType.OpenCover, new CoverallsNetSettings
        {
            CommitId = CommitId,
            CommitBranch = CommitBranch,
            CommitMessage = CommitMessage,
            RepoToken = CoverallsToken
        });
    });