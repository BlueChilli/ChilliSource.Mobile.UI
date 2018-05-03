using System.Threading;
using System.Text.RegularExpressions;

//////////////////////////////////////////////////////////////////////
// ADDINS
//////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.Incubator&version=2.0.0
#addin nuget:?package=Newtonsoft.Json
//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////

#tool "GitReleaseManager"
#tool "GitVersion.CommandLine"
#tool "GitLink"
#tool nuget:?package=vswhere

using Cake.Common.Build.TeamCity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

if (string.IsNullOrWhiteSpace(target))
{
    target = "Default";
}


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Should MSBuild & GitLink treat any errors as warnings?
var treatWarningsAsErrors = false;
Func<string, int> GetEnvironmentInteger = name => {
	
	var data = EnvironmentVariable(name);
	int d = 0;
	if(!String.IsNullOrEmpty(data) && int.TryParse(data, out d)) 
	{
		return d;
	} 
	
	return 0;

};

// Load json Configuartion
var configFilePath = "./config.json";
JObject config;

if(!FileExists(configFilePath)) {
	
	throw new Exception(string.Format("config.json can not be found at {0}", configFilePath));
}

var configFile = File(configFilePath);

using(var stream = new StreamReader(System.IO.File.OpenRead(configFile.Path.FullPath))) {
	var json = stream.ReadToEnd();
	config = JObject.Parse(json);
};

if(config == null) {
	throw new Exception(string.Format("config.json can not be found at {0}", configFilePath));
}



// Build configuration
var productName = config.Value<string>("productName");
var project = config.Value<string>("projectName");
var local = BuildSystem.IsLocalBuild;
var isTeamCity = BuildSystem.TeamCity.IsRunningOnTeamCity;
var isRunningOnUnix = IsRunningOnUnix();
var isRunningOnWindows = IsRunningOnWindows();
var teamCity = BuildSystem.TeamCity;
var branch = EnvironmentVariable("Git_Branch");
var isPullRequest = !String.IsNullOrEmpty(branch) && branch.ToLower().Contains("refs/pull"); //teamCity.Environment.PullRequest.IsPullRequest;
var projectName =  EnvironmentVariable("TEAMCITY_PROJECT_NAME"); //  teamCity.Environment.Project.Name;
var isRepository = StringComparer.OrdinalIgnoreCase.Equals(productName, projectName);
var isTagged = !String.IsNullOrEmpty(branch) && branch.ToLower().Contains("refs/tags");
var buildConfName = EnvironmentVariable("TEAMCITY_BUILDCONF_NAME"); //teamCity.Environment.Build.BuildConfName
var buildNumber = GetEnvironmentInteger("BUILD_NUMBER");
var isReleaseBranch = StringComparer.OrdinalIgnoreCase.Equals("master", buildConfName)|| StringComparer.OrdinalIgnoreCase.Equals("release", buildConfName);
var isCI = EnvironmentVariable("CI");

if(string.IsNullOrEmpty(isCI)) 
{
	isCI = "false";
}

var shouldAddLicenseHeader = false;
if(!string.IsNullOrEmpty(EnvironmentVariable("ShouldAddLicenseHeader"))) {
	shouldAddLicenseHeader = bool.Parse(EnvironmentVariable("ShouldAddLicenseHeader"));
}

var githubOwner = config.Value<string>("githubOwner");
var githubRepository = config.Value<string>("githubRepository");
var githubUrl = string.Format("https://github.com/{0}/{1}", githubOwner, githubRepository);
var licenceUrl = string.Format("{0}/blob/master/LICENSE", githubUrl);

// Version
string majorMinorPatch;
string semVersion;
string informationalVersion ;
string nugetVersion;
string buildVersion;

Action SetGitVersionData = () => {

	if(!isPullRequest) {
		var gitVersion = GitVersion();
		majorMinorPatch = gitVersion.MajorMinorPatch;
		semVersion = gitVersion.SemVer;
		informationalVersion = gitVersion.InformationalVersion;
		nugetVersion = gitVersion.NuGetVersion;
		buildVersion = gitVersion.FullBuildMetaData;
	}
	else {
		majorMinorPatch = "1.0.0";
		semVersion = "0";
		informationalVersion ="1.0.0";
		nugetVersion = "1.0.0";
		buildVersion = "alpha";
	}
};

SetGitVersionData();
var copyright = config.Value<string>("copyright");
var authors = config.Value<JArray>("authors").Values<string>().ToList();
var iconUrl = config.Value<string>("iconUrl");
var tags = config.Value<JArray>("tags").Values<string>().ToList();

// Artifacts
var artifactDirectory = config.Value<string>("artifactDirectory");
var packageWhitelist = config.Value<JArray>("packageWhiteList").Values<string>();

var buildSolution = config.Value<string>("solutionFile");
var runUnitTests = config.Value<bool>("runUnitTests");
var configuration = "Release";
// Macros

Func<string> GetMSBuildLoggerArguments = () => {
    return BuildSystem.TeamCity.IsRunningOnTeamCity ? EnvironmentVariable("MsBuildLogger"): null;
};


Action Abort = () => { throw new Exception("A non-recoverable fatal error occurred."); };
Action<string> TestFailuresAbort = testResult => { throw new Exception(testResult); };
Action NonMacOSAbort = () => { throw new Exception("Running on platforms other macOS is not supported."); };

Action<string, string, Exception> WriteErrorLog = (message, identity, ex) => 
{
	if(isTeamCity) 
	{
		teamCity.BuildProblem(message, identity);
		teamCity.WriteStatus(String.Format("{0}", identity), "ERROR", ex.ToString());
		throw ex;
	}
	else {
		throw new Exception(String.Format("task {0} - {1}", identity, message), ex);
	}
};


Func<string, IDisposable> BuildBlock = message => {

	if(BuildSystem.TeamCity.IsRunningOnTeamCity) 
	{
		return BuildSystem.TeamCity.BuildBlock(message);
	}
	
	return null;
	
};

Func<string, IDisposable> Block = message => {

	if(BuildSystem.TeamCity.IsRunningOnTeamCity) 
	{
		BuildSystem.TeamCity.Block(message);
	}

	return null;
};

Func<FilePath> GetMsBuildPath = () => {

	FilePath msBuildPath = null;

	if(isRunningOnWindows) {
		msBuildPath =  VSWhereLatest().CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");
	}

	return msBuildPath;
};

Action<string> buildTest = (proj) =>
{
    Information("Building {0}", proj);
	using(BuildBlock("Building Test")) 
	{			
  		var msBuildPath = GetMsBuildPath();
		Information("MSBuild: {0}", msBuildPath);
	
    	MSBuild(proj, settings => {
			settings
			.SetConfiguration(configuration);

			if(isRunningOnWindows) {
				settings.ToolPath = msBuildPath;
			}

			settings
			.SetVerbosity(Verbosity.Minimal)
			.SetNodeReuse(false);
		});
    };		

};


Action<string> build = (solution) =>
{
    Information("Building {0}", solution);
	using(BuildBlock("Build")) 
	{			
  		var msBuildPath = GetMsBuildPath();
	
		Information("MSBuild: {0}", msBuildPath);
		
    	MSBuild(solution, settings => {
			settings
			.SetConfiguration(configuration);
			
			if(isRunningOnWindows) {
				settings.ToolPath = msBuildPath;
			}

			settings.WithTarget("restore;pack");
	
			settings
			.WithProperty("SourceLinkEnabled",  isCI)
			.WithProperty("PackageOutputPath",  MakeAbsolute(Directory(artifactDirectory)).ToString())
			.WithProperty("NoWarn", "1591") // ignore missing XML doc warnings
			.WithProperty("TreatWarningsAsErrors", treatWarningsAsErrors.ToString())
		    .WithProperty("Version", nugetVersion.ToString())
		    .WithProperty("Authors",  "\"" + string.Join(" ", authors) + "\"")
		    .WithProperty("Copyright",  "\"" + copyright + "\"")
		    .WithProperty("PackageProjectUrl",  "\"" + githubUrl + "\"")
		    .WithProperty("PackageIconUrl",  "\"" + iconUrl + "\"")
		    .WithProperty("PackageLicenseUrl",  "\"" + licenceUrl + "\"")
		    .WithProperty("PackageTags",  "\"" + string.Join(" ", tags) + "\"")
		    .WithProperty("PackageReleaseNotes",  "\"" +  string.Format("{0}/releases", githubUrl) + "\"")
			.SetVerbosity(Verbosity.Minimal)
			.SetNodeReuse(false);
		
			var msBuildLogger = GetMSBuildLoggerArguments();
		
			if(!string.IsNullOrEmpty(msBuildLogger)) 
			{
				Information("Using custom MSBuild logger: {0}", msBuildLogger);
				settings.ArgumentCustomization = arguments =>
				arguments.Append(string.Format("/logger:{0}", msBuildLogger));
			}
		});
    };		

};

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup((context) =>
{
    Information("Building version {0} of ChilliSource.Mobile. (isTagged: {1})", informationalVersion, isTagged);

		if (isTeamCity)
		{
			Information(
					@"Environment:
					 PullRequest: {0}
					 Build Configuration Name: {1}
					 TeamCity Project Name: {2}
					 Branch: {3}",
					 isPullRequest,
					 buildConfName,
					 projectName,
					 branch
					);
        }
        else
        {
             Information("Not running on TeamCity");
        }

        DeleteFiles("../src/**/*.tmp");
		DeleteFiles("../src/**/*.tmp.*");

		CleanDirectories(GetDirectories("../src/**/obj"));
		CleanDirectories(GetDirectories("../src/**/bin"));
		DeleteDirectories(GetDirectories("../src/**/obj"));
		DeleteDirectories(GetDirectories("../src/**/bin"));	
		CleanDirectory(Directory(artifactDirectory));	
});

Teardown((context) =>
{
    // Executed AFTER the last task.
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Build")
	.IsDependentOn("AddLicense")
    .Does (() =>
{
    build(buildSolution);
})
.OnError(exception => {
	WriteErrorLog("Build failed", "Build", exception);
});


Task("AddLicense")
	.WithCriteria(() => shouldAddLicenseHeader)
	.Does(() =>{
		var command = isRunningOnWindows ? "sh" : "./license-header-cmd.sh";
		var settings = isRunningOnWindows ? new ProcessSettings { Arguments = "-c \"./license-header-cmd.sh\"", RedirectStandardError = true, RedirectStandardOutput = true } : new ProcessSettings { RedirectStandardError = true, RedirectStandardOutput = true };
		var process  = StartAndReturnProcess(command, settings);		
		process.WaitForExit();

		if (process.GetExitCode() != 0){
			throw new Exception("Adding license failed.");
		}
	})
	.ReportError(exception =>{
		Information("Make sure the bash (sh) directory is set in your environment path.");
	});


var testProject = config.Value<string>("testProjectPath");
Task("RunUnitTests")
    .IsDependentOn("Build")
	.WithCriteria(() => runUnitTests)
    .Does(() =>
{
	Information("Running Unit Tests for {0}", buildSolution);
	using(BuildBlock("RunUnitTests")) 
	{
		buildTest(testProject);

		var settings = new DotNetCoreTestSettings
		{
			Configuration = configuration,
			NoBuild = true,
			NoRestore = true
		};

		DotNetCoreTest(settings, testProject,  new XUnit2Settings {
			OutputDirectory = artifactDirectory,
            XmlReportV1 = false
		});
	};
});


Task("PublishPackages")
    .IsDependentOn("RunUnitTests")
    .WithCriteria(() => !local)
    .WithCriteria(() => !isPullRequest)
    .WithCriteria(() => isRepository)
    .Does (() =>
{
	using(BuildBlock("Package"))
	{
		string apiKey;
		string source;

		if (isReleaseBranch && !isTagged)
		{
			// Resolve the API key.
			apiKey = EnvironmentVariable("MYGET_APIKEY");
			if (string.IsNullOrEmpty(apiKey))
			{
				throw new Exception("The MYGET_APIKEY environment variable is not defined.");
			}

			source = EnvironmentVariable("MYGET_SOURCE");
			if (string.IsNullOrEmpty(source))
			{
				throw new Exception("The MYGET_SOURCE environment variable is not defined.");
			}
		}
		else 
		{
			// Resolve the API key.
			apiKey = EnvironmentVariable("NUGET_APIKEY");
			if (string.IsNullOrEmpty(apiKey))
			{
				throw new Exception("The NUGET_APIKEY environment variable is not defined.");
			}

			source = EnvironmentVariable("NUGET_SOURCE");
			if (string.IsNullOrEmpty(source))
			{
				throw new Exception("The NUGET_SOURCE environment variable is not defined.");
			}
		}



		// only push whitelisted packages.
		foreach(var package in packageWhitelist)
		{
			// only push the package which was created during this build run.
			var packagePath = artifactDirectory + File(string.Concat(package, ".", nugetVersion, ".nupkg"));

			// Push the package.
			NuGetPush(packagePath, new NuGetPushSettings {
				Source = source,
				ApiKey = apiKey
			});
		}

	};

  
})
.OnError(exception => {
	WriteErrorLog("publishing packages failed", "PublishPackages", exception);
});

Task("CreateRelease")
    .IsDependentOn("RunUnitTests")
    .WithCriteria(() => !local)
    .WithCriteria(() => !isPullRequest)
    .WithCriteria(() => isRepository)
    .WithCriteria(() => isReleaseBranch)
    .WithCriteria(() => !isTagged)
    .WithCriteria(() => isRunningOnWindows)
    .Does (() =>
{
	using(BuildBlock("CreateRelease"))
	{
		var username = EnvironmentVariable("GITHUB_USERNAME");
		if (string.IsNullOrEmpty(username))
		{
			throw new Exception("The GITHUB_USERNAME environment variable is not defined.");
		}

		var token = EnvironmentVariable("GITHUB_TOKEN");
		if (string.IsNullOrEmpty(token))
		{
			throw new Exception("The GITHUB_TOKEN environment variable is not defined.");
		}

		GitReleaseManagerCreate(username, token, githubOwner, githubRepository, new GitReleaseManagerCreateSettings {
			Milestone         = majorMinorPatch,
			Name              = majorMinorPatch,
			Prerelease        = true,
			TargetCommitish   = "master"
		});
	};

})
.OnError(exception => {
	WriteErrorLog("creating release failed", "CreateRelease", exception);
});

Task("PublishRelease")
    .IsDependentOn("RunUnitTests")
    .WithCriteria(() => !local)
    .WithCriteria(() => !isPullRequest)
    .WithCriteria(() => isRepository)
    .WithCriteria(() => isReleaseBranch)
    .WithCriteria(() => isTagged)
    .WithCriteria(() => isRunningOnWindows)
    .Does (() =>
{
	using(BuildBlock("PublishRelease"))
	{
		var username = EnvironmentVariable("GITHUB_USERNAME");
		if (string.IsNullOrEmpty(username))
		{
			throw new Exception("The GITHUB_USERNAME environment variable is not defined.");
		}

		var token = EnvironmentVariable("GITHUB_TOKEN");
		if (string.IsNullOrEmpty(token))
		{
			throw new Exception("The GITHUB_TOKEN environment variable is not defined.");
		}

		// only push whitelisted packages.
		foreach(var package in packageWhitelist)
		{
			// only push the package which was created during this build run.
			var packagePath = artifactDirectory + File(string.Concat(package, ".", nugetVersion, ".nupkg"));

			GitReleaseManagerAddAssets(username, token, githubOwner, githubRepository, majorMinorPatch, packagePath);
		}

		GitReleaseManagerClose(username, token, githubOwner, githubRepository, majorMinorPatch);
	}; 
})
.OnError(exception => {
	WriteErrorLog("updating release assets failed", "PublishRelease", exception);
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("CreateRelease")
    .IsDependentOn("PublishPackages")
    .IsDependentOn("PublishRelease")
    .Does (() =>
{

});


//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
