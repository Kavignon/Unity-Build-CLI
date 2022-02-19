using System.Diagnostics;

namespace Unity.Build.CLI.Infrastructure
{
    internal static class ProcessInfrastructure
    {
        /// <summary>
        /// Executes configured processes from the input.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="command"></param>
        /// <param name="processWaitTime"></param>
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
