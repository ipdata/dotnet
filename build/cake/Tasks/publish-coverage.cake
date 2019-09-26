Task("Publish-Coverage")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        CoverallsIo(coverallsPath, new CoverallsIoSettings
        {
            RepoToken = coverallsToken
        });
    });