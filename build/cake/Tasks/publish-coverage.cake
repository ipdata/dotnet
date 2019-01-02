Task("Publish-Coverage")
    .Does(() =>
    {
        var path = new FilePath($"../{Paths.Artifacts.FullPath}/coverage.opencover.xml");
        Information(path);
        CoverallsIo(path.FullPath, new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken
        });
    });