using BaseLib.Registry;
using System.Diagnostics;
using System.Reflection;

namespace AutoUpdater
{
    public static class Program
    {
        private static Log Log;

        private const string AssemblyName = "AutoAdBlock";

        public static void Main()
        {
            Log = new Log(nameof(Program));
            Log.Write("Starting...");

            if (!Registry.StartupExists(AssemblyName))
            {
                Log.Write("Adding assembly to startup...");
                Registry.SetStartup(AssemblyName, Assembly.GetExecutingAssembly().Location);
            }

            Updater.Start();

            Process.GetCurrentProcess().WaitForExit(); //prevent main thread from exiting

            Log.Write("Stopping...");

            Log.Dispose();
        }
    }
}
