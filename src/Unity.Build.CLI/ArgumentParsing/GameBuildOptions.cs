using CommandLine;

namespace Unity.Build.CLI.ArgumentParsing
{
    [Verb("build-game", HelpText = "Builds the game with the selected build target.")]

    internal class GameBuildOptions
    {
        [Option('t', "build-target", Required = true, HelpText = "The build target that will be used by the Unity Editor to deploy the game.")]
        public string BuildTarget { get; set; }

        [Option('d', "unity-project-directory", Required = true, HelpText = "The path to the directory of the Unity game project.")]
        public string UnityProjectLocalPath { get; set; }

        [Option('s', "build-player-script-file", Required = true, HelpText = "The name of the script and class which holds the logic to build the player.")]
        public string ScriptFileName { get; set; }

        [Option('b', "build-player-method-name", Required = true, HelpText = "The name of the method in charge of building the player.")]
        public string BuildPlayerMethodName { get; set; }
    }
}
