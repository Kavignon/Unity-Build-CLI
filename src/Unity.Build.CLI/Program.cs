using System.Diagnostics;

// We're taking for granted that Git is installed on the developer's machine as a SCM.
const string ScmUseByCli = "git";

static void CloneFromGit(string projectUrl, string? directoryPath, string localDirectoryName)
{
    if (string.IsNullOrEmpty(projectUrl))
    {
        // TODO: Let's add logs for when we have interesting things to tell the developer.
        
        Console.WriteLine("We cannot move forward if the project's URL is missing.");
    }

    // When the directory path isn't provided, the tool will try to clone the repository at the current local from where the EXE was invoked.
    directoryPath = CanCloneToLocal(directoryPath) ? Path.Combine(directoryPath, localDirectoryName) : string.Empty;

    ExecuteScmCommand(ScmUseByCli, $"clone {projectUrl} {directoryPath}");

    static bool CanCloneToLocal(string? directoryPath) => !string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath);
}

static void SetGitDefaultBranchName(string? branchName)
{
    string defaultName = branchName ?? "master";
    string command = $"config --global init.defaultBranch {defaultName}";

    ExecuteScmCommand(ScmUseByCli, command);
}

static void ExecuteScmCommand(string scmName, string command, int processWaitTime = 10_000)
{
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = $"{scmName}",
            Arguments = command,
            WindowStyle = ProcessWindowStyle.Hidden
        }
    };

    process.Start();
    process.WaitForExit(processWaitTime);
}

// Fixes a problem that arises when Git version 2.28+ is installed - https://stackoverflow.com/questions/64349920/git-error-fatal-invalid-branch-name-init-defaultbranch
SetGitDefaultBranchName(null);
CloneFromGit("https://github.com/wrossmck/2d-platformer/", "C:\\sources/2d-platformer", "2d-platformer");