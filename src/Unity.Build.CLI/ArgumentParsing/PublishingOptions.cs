using CommandLine;

namespace Unity.Build.CLI.ArgumentParsing
{
    [Verb("publish", HelpText = "Archives and copies the artifacts and logs to the location of your choice.")]
    internal class PublishingOptions
    {
        [Option('a', "assets-directory", Required = true, HelpText = "The directory path to the assets of your the game project.")]
        public string AssetsDirectoryPath { get; set; }

        [Option('l', "logs-directory", Required = true, HelpText = "The directory path to the logs of your the game project.")]
        public string LogsDirectoryPath { get; set; }

        [Option('o', "output-archive-file", Required = true, HelpText = "The file path to where the game archives will be published on the host machine.")]
        public string ArchiveFilePath { get; set; }
    }
}
