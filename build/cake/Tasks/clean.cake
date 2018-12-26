Task("Clean")
    .Does(() => {
        Information("Artifacts:");

        CleanDirectories(Paths.Artifacts.FullPath);
        Information(Paths.Artifacts.FullPath);

        Information("Source:");

        CleanDirectories("../../src/**/bin");
        Information("../../src/**/bin");

        CleanDirectories("../../src/**/obj");
        Information("../../src/**/obj");

        Information("Test:");

        CleanDirectories("../../test/**/bin");
        Information("../../test/**/bin");

        CleanDirectories("../../test/**/obj");
        Information("../../test/**/obj");
    });