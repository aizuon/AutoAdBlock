using BaseLib.IO;
using BaseLib.Networking;
using BaseLib.Security.Hashing;
using BaseLib.Threading.Tasks;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdater
{
    public static class Updater
    {
        private const string HostsUrl = "https://raw.githubusercontent.com/StevenBlack/hosts/master/hosts";
        private const string HostsPath = @"C:\Windows\System32\drivers\etc\hosts";
        private static string s_LastUpdate_TempFile;

        private static Log Log;

        private static TaskLoop ServiceLoop;

        public static void Start()
        {
            Log = new Log(nameof(Updater));
            Log.Write("Starting...");

            s_LastUpdate_TempFile = System.IO.Path.GetTempPath() + Program.AssemblyName + ".txt";

            ServiceLoop = new TaskLoop(TimeSpan.FromHours(Config.Instance.UpdateInterval), Service);

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
            ServiceLoop = new TaskLoop(TimeSpan.FromHours(Config.Instance.UpdateInterval), Service);
            ServiceLoop.Start();
        }

        public static async Task<UpdateResult> Update()
        {
            Log.Write("Hashing local hosts...");
            string localHash = string.Empty;
            try
            {
                localHash = MD5.HashFile(HostsPath);
            }
            catch (UnauthorizedAccessException)
            {
                Log.Write("Insufficient permission to read hosts, please run the program as administrator!");

                return UpdateResult.InsufficientPermission;
            }
            Log.Write($"Local hash => {localHash}");

            Log.Write("Downloading remote hosts...");
            byte[] remoteData = Array.Empty<byte>();
            try
            {
                remoteData = await WebClient.DownloadDataTaskAsync(HostsUrl);
            }
            catch (System.Net.WebException)
            {
                Log.Write("WebException! Remote host is down or no internet connection?");

                return UpdateResult.NoInternet;
            }
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
                Log.Write("Insufficient permission to replace hosts, please run the program as administrator!");

                return UpdateResult.InsufficientPermission;
            }

            Log.Write("Flushing dns cache...");
            DnsUtils.FlushCache();

            Log.Write("Updated hosts file.");

            return UpdateResult.Updated;
        }

        private static async Task Service(TimeSpan delta)
        {
            Log.Write("Start update.");

            var result = await Update();

            WriteTempFile(result, DateTime.Now);

            if (result == UpdateResult.InsufficientPermission)
                Application.Exit();
        }
    }
}
