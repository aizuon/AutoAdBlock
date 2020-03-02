using BaseLib.IO;
using BaseLib.Networking;
using BaseLib.Security.Hashing;
using BaseLib.Threading;
using System;
using System.Text;
using System.Windows.Forms;

namespace AutoUpdater
{
    public static class Updater
    {
        private const string HostsUrl = "https://raw.githubusercontent.com/StevenBlack/hosts/master/hosts";
        private const string HostsPath = @"C:\Windows\System32\drivers\etc\hosts";
        private static string s_LastUpdate_TempFile;

        private static Log Log;

        private static ThreadLoop ServiceLoop;

        public static void Start()
        {
            Log = new Log(nameof(Updater));
            Log.Write("Starting...");

            s_LastUpdate_TempFile = System.IO.Path.GetTempPath() + Program.AssemblyName + ".txt";

            ServiceLoop = new ThreadLoop(TimeSpan.FromHours(Config.Instance.UpdateInterval), Service);

            ServiceLoop.Start();
        }

        public static void WriteTempFile(UpdateResult result, DateTime time)
        {
            using (var sw = new System.IO.StreamWriter(s_LastUpdate_TempFile, false, Encoding.Default))
            {
                sw.WriteLine(result.ToString());
                sw.WriteLine(time.ToString());
            }
        }

        public static Tuple<UpdateResult, DateTime> ReadTempFile()
        {
            try
            {
                using (var sr = new System.IO.StreamReader(s_LastUpdate_TempFile, Encoding.Default))
                {
                    string _result = sr.ReadLine();
                    var result = (UpdateResult)Enum.Parse(typeof(UpdateResult), _result);
                    string _time = sr.ReadLine();
                    var time = DateTime.Parse(_time);

                    return new Tuple<UpdateResult, DateTime>(result, time);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                return null;
            }
        }

        public static void Stop()
        {
            Log.Write("Stopping...");

            ServiceLoop.Stop();

            Log.Dispose();
        }

        public static void UpdateInterval()
        {
            ServiceLoop.Stop();
            ServiceLoop = new ThreadLoop(TimeSpan.FromHours(Config.Instance.UpdateInterval), Service);
            ServiceLoop.Start();
        }

        public static UpdateResult Update()
        {
            Log.Write("Hashing local hosts...");
            string localHash = string.Empty;
            try
            {
                localHash = MD5.HashFile(HostsPath);
            }
            catch (UnauthorizedAccessException)
            {
                Log.Write("Insufficient permissions to read hosts, please run the program as administrator!");

                return UpdateResult.Failure;
            }
            Log.Write($"Local hash => {localHash}");

            Log.Write("Downloading remote hosts...");
            byte[] remoteData = WebClient.DownloadData(HostsUrl);
            Log.Write("Hashing remote hosts...");
            string remoteHash = MD5.HashData(remoteData);
            Log.Write($"Remote hash => {remoteHash}");

            if (localHash == remoteHash)
            {
                Log.Write("Hosts is up to date, returning.");

                return UpdateResult.AlreadyUpdated;
            }

            Log.Write("Remote hosts hash doesn't match local, updating...");

            try
            {
                File.Replace(HostsPath, remoteData);
            }
            catch (UnauthorizedAccessException)
            {
                Log.Write("Insufficient permissions to replace hosts, please run the program as administrator!");

                return UpdateResult.Failure;
            }

            Log.Write("Flushing dns cache...");
            DnsUtils.FlushCache();

            Log.Write("Updated hosts file.");

            return UpdateResult.Updated;
        }

        private static void Service(TimeSpan delta)
        {
            Log.Write("Start update.");

            var result = Update();

            WriteTempFile(result, DateTime.Now);

            if (result == UpdateResult.Failure)
                Application.Exit();
        }
    }
}
