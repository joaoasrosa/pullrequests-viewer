#addin "Cake.Docker"
#tool "nuget:?package=GitVersion.CommandLine"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var verbosity = Argument<string>("verbosity");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var artifactsDir = "./artifacts/";
var publishDir = "./publish/";

var solutionPath = "./PullRequestsViewer.sln";
var projectPath = "./src/PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.csproj";
var nuspecFile = "./src/PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.nuspec";
var publishedNuspecFile = publishDir + "PullRequestsViewer.WebApp/PullRequestsViewer.WebApp.nuspec";
var dockerFile = "./build/dockerfile";
var publishedDockerFile = publishDir + "PullRequestsViewer.WebApp/lib/dockerfile";

var unitTestProjects = GetFiles("./tests/*.Tests.Unit/*.Tests.Unit.csproj");
var acceptanceTestProjects = GetFiles("./tests/*.Tests.Acceptance/*.Tests.Acceptance.csproj");
GitVersion gitVersion = null;
string dockerImageName = null;
string dockerImageFullName = null;

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
// HELPERS
///////////////////////////////////////////////////////////////////////////////

string GetNuGetApiKey()
{
  return GetSetting("nugetapikey");
}

string GetNuGetApiUrl()
{
  return GetSetting("nugetapiurl", "https://www.nuget.org/api/v2/package");
}

string GetDockerHubRepo()
{
  return GetSetting("dockerhubrepo", "joaoasrosa/pullrequestsviewer");
}

string GetSetting(string key, string defaultValue = "")
{
  return EnvironmentVariable("cake-" + key) ?? Argument<string>(key, defaultValue);
}

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

Task("Unit-Tests")
    .Description("Runs the Unit Tests.")
    .Does(() => {
	    var settings = new DotNetCoreTestSettings
		{
			Configuration = configuration,
			NoBuild = true
		};

		foreach(var testProject in unitTestProjects)
        {
			Verbose("Running tests in project " + testProject.FullPath);
			DotNetCoreTest(testProject.FullPath, settings);
		}
    });

Task("Acceptance-Tests")
    .Description("Runs the Acceptance Tests.")
    .Does(() => {
	    var settings = new DotNetCoreTestSettings
		{
			Configuration = configuration,
			NoBuild = true
		};

		foreach(var testProject in acceptanceTestProjects)
        {
			Verbose("Running tests in project " + testProject.FullPath);
			DotNetCoreTest(testProject.FullPath, settings);
		}
    });

Task("Publish")
    .Description("Publish the Web Application.")
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
    .IsDependentOn("Rebuild")
    .IsDependentOn("Test")
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

		var settings = new NuGetPushSettings
    {
			Source = GetNuGetApiUrl(),
			ApiKey = GetNuGetApiKey()
		};

		NuGetPush(packages, settings);
	});

Task("CreateContainer")
    .Description("Creates a container with the Web Application.")
    .IsDependentOn("Rebuild")
    .IsDependentOn("Test")
    .IsDependentOn("Publish")
    .Does(() => {
      CopyFile(dockerFile, publishedDockerFile);

      dockerImageName = GetDockerHubRepo();
      dockerImageFullName = dockerImageName + ":" + gitVersion.MajorMinorPatch;

      var settings = new DockerBuildSettings
      {
        Tag = new[] { dockerImageName, dockerImageFullName },
        ForceRm = true,
        Pull = true
      };

      DockerBuild(settings, "./publish/PullRequestsViewer.WebApp/lib");
    });

Task("PushContainer")
  .Description("Pushes the container with the Web Application")
  .IsDependentOn("CreateContainer")
  .Does(() => {
    Verbose("Pushing '" + dockerImageName + "'");

    var pushSettings = new DockerPushSettings
    {
      DisableContentTrust = false
    };

    DockerPush(pushSettings, dockerImageName);

    Verbose("Pushing '" + dockerImageFullName + "'");

    DockerPush(pushSettings, dockerImageFullName);
  });

Task("Test")
    .IsDependentOn("Unit-Tests")
    .IsDependentOn("Acceptance-Tests");

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
    .IsDependentOn("Test");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
