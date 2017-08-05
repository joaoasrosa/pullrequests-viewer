#addin "Cake.Docker"
#tool "nuget:?package=GitVersion.CommandLine"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var verbosity = Argument<string>("verbosity");
var nugetApiKey = Argument<string>("nugetApiKey", "");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var artifactsDir = "./artifacts/";
var publishDir = "./publish/";

var solutionPath = "./PullRequestsViewer.sln";
var projectPath = "./src/PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.csproj";
var nuspecFile = "./src/PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.nuspec";
var publishedNuspecFile = publishDir + "PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.nuspec";

var unitTestProjects = GetFiles("./tests/*.Tests.Unit/*.Tests.Unit.csproj");
var acceptanceTestProjects = GetFiles("./tests/*.Tests.Acceptance/*.Tests.Acceptance.csproj");
GitVersion gitVersion = null;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
    // Executed BEFORE the first task.
    EnsureDirectoryExists(artifactsDir);
    EnsureDirectoryExists(publishDir);
    Verbose("Running tasks...");
});

Teardown(ctx =>
{
    // Executed AFTER the last task.
    Verbose("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Description("Cleans all directories that are used during the build process.")
    .Does(() => {
		CleanDirectories("./src/**/bin/**");
		CleanDirectories("./src/**/obj/**");
		CleanDirectories("./tests/**/bin/**");
		CleanDirectories("./tests/**/obj/**");
		CleanDirectory(artifactsDir);
		CleanDirectory(publishDir);
    });

Task("Restore")
    .Description("Restores all the NuGet packages that are used by the solution.")
	.Does(() => {
	    var settings = new DotNetCoreRestoreSettings
		{
			Sources = new[] 
			{
				"https://api.nuget.org/v3/index.json"
			},
			DisableParallel = false
		};

        DotNetCoreRestore("./", settings);
    });

Task("GitVersion")
    .Description("Set the SemVer to the solution.")
    .Does(() =>
	{
		gitVersion = GitVersion(new GitVersionSettings {
			UpdateAssemblyInfo = true
		});

	    var file = File(projectPath);
	    XmlPoke(file, "/Project/PropertyGroup/AssemblyVersion", gitVersion.MajorMinorPatch);
		XmlPoke(file, "/Project/PropertyGroup/FileVersion", gitVersion.MajorMinorPatch);
		XmlPoke(file, "/Project/PropertyGroup/Version", gitVersion.FullSemVer);

		Verbose("Full SemVer: " + gitVersion.FullSemVer);
		Verbose("Major Minor Patch: " + gitVersion.MajorMinorPatch);
	});

Task("Build")
    .Description("Builds the solution.")
    .IsDependentOn("GitVersion")
    .Does(() => {
		var settings = new DotNetCoreBuildSettings
		{
			Configuration = configuration
		};
		
        DotNetCoreBuild(solutionPath, settings);
    });

Task("Unit-Test")
    .Description("Runs the Unit Tests.")
    .Does(() => {
	    var settings = new DotNetCoreTestSettings
		{
			Configuration = configuration,
			NoBuild = true
		};

		foreach(var testProject in unitTestProjects)
        {
			Information("Running tests in project " + testProject.FullPath);
			DotNetCoreTest(testProject.FullPath, settings);
		}
    });

Task("Acceptance-Test")
    .Description("Runs the Acceptance Tests.")
    .Does(() => {
	    var settings = new DotNetCoreTestSettings
		{
			Configuration = configuration,
			NoBuild = true
		};

		foreach(var testProject in acceptanceTestProjects)
        {
			Information("Running tests in project " + testProject.FullPath);
			DotNetCoreTest(testProject.FullPath, settings);
		}
    });

Task("Publish")
    .Description("Publish the Web Application.")
    .IsDependentOn("Test")
    .Does(() => {
	    var settings = new DotNetCorePublishSettings
		{
			Configuration = configuration,
			OutputDirectory = publishDir + "PullRequestsViewer.WebApp/lib"
		};

        DotNetCorePublish(projectPath, settings);
		CopyFile(nuspecFile, publishedNuspecFile);
    });

Task("Package")
    .Description("Package the Web Application.")
	.IsDependentOn("Publish")
	.Does(() => {
		var settings = new NuGetPackSettings 
		{
			OutputDirectory = artifactsDir,
			Version = gitVersion.NuGetVersionV2
		};

		NuGetPack(publishedNuspecFile, settings);
	});

Task("PushPackage")
    .Description("Pushes the Web Application package.")
	.IsDependentOn("Package")
	.Does(() => {
		var packages = GetFiles(artifactsDir + "*.nupkg");
		
		var settings = new NuGetPushSettings {
			Source = "https://www.nuget.org/api/v2/package",
			ApiKey = nugetApiKey
		};

		NuGetPush(packages, settings);
	});

Task("CreateContainer")
    .Description("Creates a container with the Web Application.")
    .IsDependentOn("Publish")
    .Does(() => {
	    Information("TODO: create docker image.");
    });

Task("Test")
    .IsDependentOn("Unit-Test")
    .IsDependentOn("Acceptance-Test");
	
Task("Build+Test")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

Task("Rebuild")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build");

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .Description("This is the default task which will be ran if no specific target is passed in.")
    .IsDependentOn("Rebuild")
    .IsDependentOn("Test")
    .IsDependentOn("Package");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);