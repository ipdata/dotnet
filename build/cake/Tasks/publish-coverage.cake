Task("Publish-Coverage")
    .Does(() =>
    {
        var dir = MakeAbsolute(Directory("Paths.Artifacts.FullPath"));
        var path = $"{dir.FullPath}/coverage.opencover.xml";
        Information(path);
        CoverallsIo(path, new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken
        });
    });