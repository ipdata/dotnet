Task("Publish-Coverage")
    .Does(() =>
    {
        CoverallsIo($"{Paths.Artifacts.FullPath}/coverage.opencover.xml", new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken
        });
    });