using System.Diagnostics;

namespace Unity.Build.CLI.Infrastructure
{
    internal static class ProcessInfrastructure
    {
        /// <summary>
        /// Executes configured processes from the input.
        /// </summary>
        /// <param name="applicationName">
        ///     The application that will be executed by the process.
        /// </param>
        /// <param name="command">
        ///     The command that the application must execute.
        /// </param>
        /// <param name="processWaitTime">
        ///     The allocated amount of time given to process before we stop it.
        /// </param>
        /// <remarks>
        ///     On machines that have installed a version of Windows equal or higher than Windows Vista,
        ///     executing a process will now enter in admin mode.
        /// </remarks>
        internal static void ConfigureAndExecuteProcess(string applicationName, string command, int processWaitTime = 10_000)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $"{applicationName}",
                    Arguments = command,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = System.Environment.OSVersion.Version.Major >= 6 ? "runas" : string.Empty
                }
            };

            process.Start();
            process.WaitForExit(processWaitTime);
        }
    }
}
