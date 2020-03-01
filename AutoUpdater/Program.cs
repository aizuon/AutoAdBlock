using System.Diagnostics;

namespace AutoUpdater
{
    public static class Program
    {
        public static void Main()
        {
            Updater.Start();

            Process.GetCurrentProcess().WaitForExit(); //prevent main thread from exiting
        }
    }
}
