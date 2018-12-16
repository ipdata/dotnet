Task("LibTest")
	.IsDependentOn("Restore")
	.Does(() =>
	{
		var dotNetCoreTestSettings = new DotNetCoreTestSettings {
			Configuration = configuration,
			NoRestore = true,
			ArgumentCustomization = args => args.Append("--logger:trx")
		};
		var projectFiles = GetFiles(Paths.UnitTestProjects);
		foreach(var file in projectFiles)
		{
			DotNetCoreTest(file.FullPath, dotNetCoreTestSettings);
		}
	});