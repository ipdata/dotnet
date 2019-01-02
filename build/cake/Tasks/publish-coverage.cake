Task("Publish-Coverage")
    .Does(() =>
    {
        var dir = MakeAbsolute(Directory(Paths.Artifacts.FullPath));
        var path = $"{dir}/coverage.opencover.xml";
        CoverallsIo(path, new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken
        });
    });