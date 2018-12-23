Task("Default")
    .IsDependentOn("LibBuild")
    .IsDependentOn("LibTest")
    .Does(() => {});