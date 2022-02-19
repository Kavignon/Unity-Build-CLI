using CommandLine;

namespace Unity.Build.CLI.ArgumentParsing
{
    [Verb("clone", HelpText = "Clone a repository into a new directory.")]
    internal class CloneOptions
    {
        [Option('u', "url", Required = true, HelpText = "The URL towards the repository that will be clone on the host's machine.")]
        public string ProjectUrl { get; set; }

        [Option('d', "directory", Required = true, HelpText = "The directory that will contain the downloaded contents of the repository.")]
        public string DirectoryPath { get; set; }

        [Option('s', "subfolder", Required = true, HelpText = "The sub directory that will provide a reference towards the download contents of the repository.")]
        public string SubDirectoryPath { get; set; }
    }
}
