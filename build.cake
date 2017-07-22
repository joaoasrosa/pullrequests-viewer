#addin "Cake.Docker"
#tool "nuget:?package=GitVersion.CommandLine"

var nugetApiKeyArg = Argument<string>("nugetApiKey", "");
var targetArg = Argument<string>("target", "Default");
var configurationArg = Argument<string>("configuration", "Debug");
var artifactsDir = "./artifacts/";
var publishDir = "./publish/";
var solutionPath = "./PullRequestsViewer.sln";
var projectPath = "./src/PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.csproj";
var nuspecFile = "./src/PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.nuspec";
var publishedNuspecFile = publishDir + "PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.nuspec";

//var testProjects = GetFiles("./tests/**Tests.Unit.csproj");
var testProjects = new []
{
	"./tests/PullRequestsViewer.WebApp.Tests.Unit",
	"./tests/PullRequestsViewer.GitHub.Tests.Unit",
	"./tests/PullRequestsViewer.SqlLite.Tests.Unit"
};

Task("Clean")
    .Does(() => {
        if (DirectoryExists(publishDir))
        {
            DeleteDirectory(publishDir, recursive:true);
        }

        CreateDirectory(publishDir);

		if (DirectoryExists(artifactsDir))
        {
            DeleteDirectory(artifactsDir, recursive:true);
        }

        CreateDirectory(artifactsDir);
    });

Task("Restore")
    .IsDependentOn("Clean")
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

Task("GitVersion")
    .IsDependentOn("Restore")
    .Does(() =>
	{
		var result = GitVersion(new GitVersionSettings {
			UpdateAssemblyInfo = true
		});

		Information(result.FullSemVer);
	});

Task("Build")
    .IsDependentOn("GitVersion")
    .Does(() => {
		var settings = new DotNetCoreBuildSettings
		{
			Configuration = configurationArg
		};
		
        DotNetCoreBuild(solutionPath, settings);
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
	    var settings = new DotNetCoreTestSettings
		{
			Configuration = configurationArg,
			NoBuild = true
		};

		foreach(var testProject in testProjects)
        {
			Information("Running tests in project " + testProject);
			DotNetCoreTest(testProject, settings);
		}
    });

Task("Publish")
    .IsDependentOn("Test")
    .Does(() => {
	    var settings = new DotNetCorePublishSettings
		{
			Configuration = configurationArg,
			OutputDirectory = publishDir + "PullRequestsViewer.WebApp/lib"
		};

        DotNetCorePublish(projectPath, settings);
		CopyFile(nuspecFile, publishedNuspecFile);
    });

Task("CreateNuGet")
	.IsDependentOn("Publish")
	.Does(() => {
		var settings = new NuGetPackSettings 
		{
			OutputDirectory = artifactsDir
		};

		NuGetPack(publishedNuspecFile, settings);
	});

Task("PushNuGet")
	.IsDependentOn("CreateNuGet")
	.Does(() => {
		var packages = GetFiles(artifactsDir + "*.nupkg");
		
		var settings = new NuGetPushSettings {
			Source = "https://www.nuget.org/api/v2/package",
			ApiKey = nugetApiKeyArg
		};

		NuGetPush(packages, settings);
	});

Task("CreateContainer")
    .IsDependentOn("PushNuGet")
    .Does(() => {
	    Information("TODO: create docker image.");
    });

Task("Default")
    .IsDependentOn("Test");

RunTarget(targetArg);