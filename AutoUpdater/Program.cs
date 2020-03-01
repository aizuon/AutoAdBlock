using BaseLib.Registry;
using System.Diagnostics;
using System.Reflection;

namespace AutoUpdater
{
    public static class Program
    {
        public static void Main()
        {
            Registry.SetStartup("AutoAdBlock", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            Updater.Start();

            Process.GetCurrentProcess().WaitForExit(); //prevent main thread from exiting
        }
    }
}
