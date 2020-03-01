namespace BaseLib.Registry
{
    public static class Registry
    {
        private const string Startup = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public static bool StartupExists(string name)
        {
            using (var rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Startup, false))
            {
                object value = rk.GetValue(name);

                return value != null;
            }
        }

        public static void SetStartup(string name, string path)
        {
            using (var rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Startup, true))
            {
                object value = rk.GetValue(name);

                if (value == null || value.ToString() != path)
                    rk.SetValue(name, path);
            }
        }
    }
}
