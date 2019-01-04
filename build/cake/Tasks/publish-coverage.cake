Task("Publish-Coverage")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        CoverallsIo(CoverallsPath, new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken
        });
    });