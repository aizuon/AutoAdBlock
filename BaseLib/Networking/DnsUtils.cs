using System.Runtime.InteropServices;

namespace BaseLib.Networking
{
    public static class DnsUtils
    {
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        private static extern uint DnsFlushResolverCache();

        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCacheEntry_A")]
        private static extern int DnsFlushResolverCacheEntry(string hostName);

        public static void FlushCache()
        {
            DnsFlushResolverCache();
        }

        public static void FlushCache(string hostName)
        {
            DnsFlushResolverCacheEntry(hostName);
        }
    }
}
