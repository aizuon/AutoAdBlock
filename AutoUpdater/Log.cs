using System;
using System.Collections.Concurrent;
using System.Text;

namespace AutoUpdater
{
    public class Log : IDisposable
    {
        private static readonly string s_Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        private static System.IO.StreamWriter s_LogStreamWriter;
        private static readonly object s_WriteLock = new object();
        private static readonly ConcurrentDictionary<string, Log> s_Logs = new ConcurrentDictionary<string, Log>();

        private readonly string p_ModuleName;
        private readonly string p_RawModuleName;

        public Log(string moduleName)
        {
            p_RawModuleName = moduleName;
            p_ModuleName = $"[{p_RawModuleName.ToUpperInvariant()}]";

            if (s_Logs.Count == 0)
                s_LogStreamWriter = new System.IO.StreamWriter(s_Path, true, Encoding.Default);

            s_Logs.TryAdd(p_RawModuleName, this);
        }

        public void Write(string text)
        {
            lock (s_WriteLock)
            {
                string timestamp = DateTime.Now.ToString();
                string log = $"[{timestamp}] {p_ModuleName} {text}";

                s_LogStreamWriter.WriteLine(log);
                s_LogStreamWriter.Flush();
            }
        }

        public void Dispose()
        {
            s_Logs.TryRemove(p_RawModuleName, out var _);

            if (s_Logs.Count == 0)
                s_LogStreamWriter.Close();
        }
    }
}
