Task("Publish-Coverage")
    .Does(() =>
    {
        Information(CoverallsPath);
        CoverallsIo(CoverallsPath, new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken
        });
    });