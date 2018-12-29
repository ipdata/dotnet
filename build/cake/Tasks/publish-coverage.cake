Task("Publish-Coverage")
    .Does(() =>
    {
        Codecov(new CodecovSettings
        {
            Files = new[] { $"{Paths.Artifacts.FullPath}/coverage.opencover.xml" },
            Token = codeCovKey,
            Verbose = false,
            Flags = "unittests"
        });
    });