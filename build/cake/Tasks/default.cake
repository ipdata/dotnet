Task("Default")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() => {});