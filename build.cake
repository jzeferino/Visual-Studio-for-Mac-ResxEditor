#addin "Cake.Xamarin&version=2.0.1"

var target = Argument("target", "Default");
var configuration = "Release";

// Define directories.
var solutionFile = GetFiles("./*.sln").First();

// Addin project.
var binaryPath = System.IO.Path.Combine("./src/ResxEditor/bin", configuration, "ResxEditor.dll");

// Output folder.
var artifactsDir = Directory("./artifacts");

// Build configuration
var isLocalBuild = BuildSystem.IsLocalBuild;

Task("Clean-Solution")
	.IsDependentOn("Clean-Folders")
	.Does(() => 
	{
		MSBuild(solutionFile, settings => settings
			.SetConfiguration(configuration)
			.WithTarget("Clean")
			.SetVerbosity(Verbosity.Quiet));
	});

Task("Clean-Folders")
	.Does(() => 
	{
		CleanDirectory(artifactsDir);
		CleanDirectories ("./**/bin");
		CleanDirectories ("./**/obj");
	});

Task("Restore-Packages")
	.Does(() => 
	{
		NuGetRestore(solutionFile);
	});

Task("Build")
	.IsDependentOn("Clean-Solution")
    .IsDependentOn("Restore-Packages")
    .Does(() =>
	{ 		
		 MSBuild(solutionFile, settings =>
        	settings.SetConfiguration(configuration)   
			.WithTarget("Build")
            .WithProperty("TreatWarningsAsErrors", "false")
			.SetVerbosity(Verbosity.Quiet));        
    });

Task("Pack")
	.IsDependentOn("Build")
    .WithCriteria(() => isLocalBuild)
    .WithCriteria(() => HasMdTool())
	.Does(() => 
	{
		VSToolSetup.Pack(binaryPath, artifactsDir);
	});

private bool HasMdTool()
{
	// Note to self: when running locally, must add chmod to vcstool.exe
	var mdToolPath = @"/Applications/Visual Studio.app/Contents/Resources/lib/monodevelop/bin/vstool.exe";

	if (FileExists(mdToolPath)) {
		Information("mdtool exists");
		Context.Tools.RegisterFile(mdToolPath);
		return true;
	}
	Information("mdtool don't exist");
	return false;
}

Task("Default")
	.IsDependentOn("Pack");

RunTarget(target);

