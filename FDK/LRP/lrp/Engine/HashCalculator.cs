namespace Lrp.Engine
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    static class HashCalculator
    {
        public static string HashFromText(string text)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(text);
                    writer.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    return HashFromStream(stream);
                }
            }
        }

        static string HashFromStream(MemoryStream stream)
        {
            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(stream);
                var result = BitConverter.ToString(data);
                result = result.Replace("-", string.Empty);
                return result;
            }
        }
    }
}
