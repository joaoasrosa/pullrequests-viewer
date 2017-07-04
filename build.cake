#addin "Cake.Docker"

var target = Argument("target", "Default");
var outputDir = "./artifacts/";
var projectPath = "./src/PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.csproj";
var configuration = "Release";

Task("Clean")
    .Does(() => {
        if (DirectoryExists(outputDir))
        {
            DeleteDirectory(outputDir, recursive:true);
        }

        CreateDirectory(outputDir);
    });

Task("Restore")
    .Does(() => {
	    var settings = new DotNetCoreRestoreSettings
		{
			Sources = new[] 
			{
				"https://api.nuget.org/v3/index.json", 
				"https://dotnet.myget.org/F/aspnetcore-ci-dev/api/v3/index.json", 
				"https://www.myget.org/F/aspnet-contrib/api/v3/index.json"
			},
			DisableParallel = false
		};

        DotNetCoreRestore("./", settings);
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {
		var settings = new DotNetCoreBuildSettings
		{
			Configuration = configuration
		};
		
        DotNetCoreBuild(projectPath, settings);
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
	    var settings = new DotNetCoreTestSettings
		{
			Configuration = configuration
		};

        DotNetCoreTest("./tests/PullRequestsViewer.WebApp.Tests", settings);
        DotNetCoreTest("./tests/PullRequestsViewer.GitHub.Tests", settings);
    });

Task("Publish")
    .IsDependentOn("Test")
    .Does(() => {
	    var settings = new DotNetCorePublishSettings
		{
			Configuration = configuration,
			OutputDirectory = outputDir + "PullRequestsViewer.WebApp/"
		};

        DotNetCorePublish(projectPath, settings);
    });

Task("CreateContainer")
    .IsDependentOn("Publish")
    .Does(() => {
	    Information("TODO: create docker image.");
    });

Task("Default")
    .IsDependentOn("CreateContainer");

RunTarget(target);