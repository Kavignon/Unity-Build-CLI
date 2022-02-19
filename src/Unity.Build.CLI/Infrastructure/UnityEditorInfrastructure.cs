using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Unity.Build.CLI.Infrastructure
{
    internal static class UnityEditorInfrastructure
    {
        private const string UnityEditorExecutableHostPath = @"C:\Program Files\Unity\Hub\Editor\2020.3.29f1\Editor\Unity.exe";

        // Instead of a hashset, there would be an API that can be queried to get the results instead of coupling the CLI tool to this information.
        private static readonly HashSet<string> AvailableBuildTargetSet = new HashSet<string>()
        {
            "Standalone",
            "Win",
            "Win64",
            "OSXUniversal",
            "Linux64",
            "iOS",
            "Android",
            "WebGL",
            "XboxOne",
            "PS4",
            "WindowsStoreApps",
            "Switch",
            "tvOS"
        };

        internal static void SetActiveBuildTarget(string buildTargetName)
        {
            if (string.IsNullOrEmpty(buildTargetName))
            {
                Console.Error.WriteLine("The build target couldn't be selected because the input was missing from the user.");
            }

            if (!AvailableBuildTargetSet.Contains(buildTargetName))
            {
                Console.Error.WriteLine($"The provided build target {buildTargetName} isn't part of the supported list of targets by the Unity Editor.");
            }

            ProcessInfrastructure.ConfigureAndExecuteProcess(UnityEditorExecutableHostPath, $"-buildTarget {buildTargetName}");
        }

        /// <summary>
        /// The function will go in the game's directory to try to build the player with the enabled build target in Unity Editor.
        /// </summary>
        /// <param name="unityGameProjectPath">
        ///     The project's path for the game.
        /// </param>
        /// <param name="classNameForBuild">
        ///     The name of the class which holds the function in charge of creating the player.
        /// </param>
        /// <param name="methodNameForBuild">
        ///     The name of the method in charge of building the player.
        /// </param>
        internal static void BuildPlayer(string unityGameProjectPath, string classNameForBuild, string methodNameForBuild)
        {
            if (string.IsNullOrEmpty(unityGameProjectPath))
            {
                Console.Error.WriteLine("The project path wasn't given and therefore, we can't build the player for the game.");
            }

            if (!Directory.Exists(unityGameProjectPath))
            {
                Console.Error.WriteLine("The project's path doesn't exist on the host. We can't move forward with building the player.");
            }

            if (string.IsNullOrEmpty(classNameForBuild))
            {
                Console.Error.WriteLine("The class name for building the player wasn't provided.");
            }

            if (!Directory.EnumerateFiles(unityGameProjectPath, $"{classNameForBuild}.cs", SearchOption.AllDirectories).Any())
            {
                Console.Error.WriteLine("The script file responsible for building the player wasn't found in the project.");
            }

            if (string.IsNullOrEmpty(methodNameForBuild))
            {
                Console.Error.WriteLine("The method in charge of building the player wasn't provided to the tool and therefore, we cannot move forward.");
            }

            ProcessInfrastructure.ConfigureAndExecuteProcess(UnityEditorExecutableHostPath, $"-quit -batchmode -projectPath {unityGameProjectPath} -executeMethod {classNameForBuild}.{methodNameForBuild}");
        }
    }
}
