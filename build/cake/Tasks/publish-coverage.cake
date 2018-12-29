Task("Publish-Coverage")
    .Does(() =>
    {
        var dir = MakeAbsolute(Directory("Paths.Artifacts.FullPath"));
        CoverallsIo($"{dir.FullPath}/coverage.opencover.xml", new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken
        });
    });