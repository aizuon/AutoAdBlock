using System;

namespace BaseLib.Security.Hashing
{
    public static class MD5
    {
        private static string Hash(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static string HashFile(string path)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            using (var f = System.IO.File.OpenRead(path))
            {
                byte[] hash = md5.ComputeHash(f);
                return Hash(hash);
            }
        }

        public static string HashData(byte[] data)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hash = md5.ComputeHash(data);
                return Hash(hash);
            }
        }
    }
}
