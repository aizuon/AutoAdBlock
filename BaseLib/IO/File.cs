namespace BaseLib.IO
{
    public static class File
    {
        public static void Replace(string path, byte[] data)
        {
            using (var fs = System.IO.File.Open(path, System.IO.FileMode.Open))
            {
                fs.SetLength(0);
                fs.Flush();

                fs.Write(data, 0, data.Length);
                //fs is flushed when this scope ends
            }
        }
    }
}
