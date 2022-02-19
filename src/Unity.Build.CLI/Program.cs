using System;
using CommandLine;
using Unity.Build.CLI.ArgumentParsing;

namespace Unity.Build.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CloneOptions>(args)
                .WithParsed<CloneOptions>(options =>
                {
                    ScmInfrastructure.SetGitDefaultBranchName();
                    ScmInfrastructure.CloneFromGit(options.ProjectUrl, options.DirectoryPath, options.SubDirectoryPath);
                })
                .WithNotParsed(_ => Console.WriteLine("An error occurred while trying to parse the given user input."));
        }
    }
}