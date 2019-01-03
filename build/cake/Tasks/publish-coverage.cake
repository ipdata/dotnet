Task("Publish-Coverage")
    .Does(() =>
    {
        CoverallsIo(CoverallsPath, new CoverallsIoSettings()
        {
            RepoToken = CoverallsToken
        });
    });