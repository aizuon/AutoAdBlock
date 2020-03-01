using BaseLib.Registry;
using System.Diagnostics;
using System.Reflection;

namespace AutoUpdater
{
    public static class Program
    {
        public static void Main()
        {
            Registry.SetStartup("AutoAdBlock", Assembly.GetExecutingAssembly().Location);

            Updater.Start();

            Process.GetCurrentProcess().WaitForExit(); //prevent main thread from exiting
        }
    }
}
