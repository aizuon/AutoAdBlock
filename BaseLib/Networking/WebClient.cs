namespace BaseLib.Networking
{
    public static class WebClient
    {
        public static byte[] DownloadData(string url)
        {
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadData(url);
            }
        }
    }
}
