using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.Build.CLI
{
    internal static class ScmInfrastructure
    {
        // We're taking for granted that Git is installed on the developer's machine as a SCM.
        private const string ScmUseByCli = "git";

        /// <summary>
        ///  Allows users to clone a repository from Git.    
        /// </summary>
        /// <param name="projectUrl">
        ///     The URL pointing to the project to clone.
        /// </param>
        /// <param name="directoryPath">
        ///     The local directory where to clone the contents of the repository.
        /// </param>
        /// <param name="subfolderName">
        ///     The name of the subfolder that could contain the content of the repository.
        /// </param>
        /// <remarks>
        ///      - When the directory path isn't provided, the tool will try to clone the repository at the current local from where the EXE was invoked. <br/>
        ///      - The subfolder isn't a requirement, but would make it things localized to a specific folder.
        /// </remarks>
        public static void CloneFromGit(string? projectUrl, string? directoryPath, string? subfolderName)
        {
            if (string.IsNullOrEmpty(projectUrl))
            {
                // TODO: Let's add logs for when we have interesting things to tell the developer.

                Console.WriteLine("We cannot move forward if the project's URL is missing.");
            }

            directoryPath = CanCloneToLocal(directoryPath) && !string.IsNullOrEmpty(subfolderName) ? Path.Combine(directoryPath, subfolderName) : string.Empty;

            ExecuteScmCommand(ScmUseByCli, $"clone {projectUrl} {directoryPath}");

            static bool CanCloneToLocal(string? directoryPath) => !string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath);
        }

        /// <summary>
        /// Sets the name of the default branch when interacting with Git repositories.
        /// </summary>
        /// <param name="branchName">
        ///     The name of the default branch the user would like to set of their machine.
        /// </param>
        /// <remarks>
        ///     Fixes a problem that arises when Git version 2.28+ is installed. <br/> https://stackoverflow.com/questions/64349920/git-error-fatal-invalid-branch-name-init-defaultbranch
        /// </remarks>
        public static void SetGitDefaultBranchName(string? branchName = null)
        {
            string defaultName = branchName ?? "master";
            string command = $"config --global init.defaultBranch {defaultName}";

            ExecuteScmCommand(ScmUseByCli, command);
        }

        public static void ExecuteScmCommand(string scmName, string command, int processWaitTime = 10_000)
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
    }
}
