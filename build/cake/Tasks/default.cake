Task("Default")
    .IsDependentOn("Build")
    .Does(() => {});