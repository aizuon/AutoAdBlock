using System.Runtime.InteropServices;

namespace BaseLib.Networking
{
    public static class DnsUtils
    {
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        private static extern uint DnsFlushResolverCache();

        public static void FlushCache()
        {
            DnsFlushResolverCache();
        }
    }
}
