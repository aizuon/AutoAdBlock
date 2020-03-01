using System;

namespace AutoUpdater
{
    public class Log : IDisposable
    {
        private readonly System.IO.StreamWriter p_LogStreamWriter;

        private readonly string p_ModuleName;

        public Log(string moduleName)
        {
            p_ModuleName = $"[{moduleName.ToUpperInvariant()}]";

            p_LogStreamWriter = new System.IO.StreamWriter("log.txt", true, System.Text.Encoding.Default);
        }

        public void Write(string text)
        {
            string timestamp = DateTime.Now.ToString();
            string log = $"[{timestamp}] {p_ModuleName} {text}";

            p_LogStreamWriter.WriteLine(log);
            p_LogStreamWriter.Flush();
        }

        public void Dispose()
        {
            p_LogStreamWriter.Close();
        }
    }
}
