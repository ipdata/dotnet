Task("Publish-Coverage")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        Information($"Branch: {CommitBranch}");
        Information($"Message: {CommitMessage}");
        CoverallsNet(CoverallsPath, CoverallsNetReportType.OpenCover, new CoverallsNetSettings
        {
            CommitBranch = CommitBranch,
            CommitMessage = CommitMessage,
            RepoToken = CoverallsToken
        });
    });