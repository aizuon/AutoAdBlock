using AutoUpdater.Forms;
using BaseLib.Registry;
using System.Reflection;
using System.Windows.Forms;

namespace AutoUpdater
{
    public static class Program
    {
        private static Log Log;

        public const string AssemblyName = "AutoAdBlock";

        public static void Main()
        {
            Log = new Log(nameof(Program));
            Log.Write("Starting...");

            if (Config.Instance.AutoStart && !Registry.StartupExists(AssemblyName))
            {
                Log.Write("Adding assembly to startup...");
                Registry.SetStartup(AssemblyName, Assembly.GetExecutingAssembly().Location);
            }

            Updater.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var main = new MainForm())
            {
                if (Config.Instance.StartMinimized)
                    Application.Run();
                else
                    Application.Run(main);
            }

            Log.Write("Stopping...");

            Updater.Stop();

            Log.Dispose();
        }
    }
}
