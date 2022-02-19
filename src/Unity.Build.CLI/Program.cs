using System;
using CommandLine;
using Unity.Build.CLI.ArgumentParsing;
using Unity.Build.CLI.Infrastructure;

namespace Unity.Build.CLI
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CloneOptions, GameBuildOptions, PublishingOptions>(args)
                .WithParsed<CloneOptions>(options =>
                {
                    ScmInfrastructure.SetGitDefaultBranchName();
                    ScmInfrastructure.CloneFromGit(options.ProjectUrl, options.DirectoryPath, options.SubDirectoryPath);
                })
                .WithParsed<GameBuildOptions>(options =>
                {
                    UnityEditorInfrastructure.SetActiveBuildTarget(options.BuildTarget);
                    UnityEditorInfrastructure.BuildPlayer(options.UnityProjectLocalPath, options.ScriptFileName, options.BuildPlayerMethodName);
                })
                .WithParsed<PublishingOptions>(options =>
                {
                    PublishingInfrastructure.ArchiveDirectories(options.ArchiveFilePath, new [] { options.AssetsDirectoryPath, options.LogsDirectoryPath });
                })
                .WithNotParsed(_ => Console.WriteLine("An error occurred while trying to parse the given user input."));
        }
    }
}