Task("Prepare-Unit-Tests")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
    });