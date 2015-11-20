namespace SoftFX.Lrp.Resources
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    static class FilesResolver
    {
        public static string LrpX86HostPath
        {
            get
            {
                return X86path;
            }
        }

        public static string LrpX64HostPath
        {
            get
            {
                return X64path;
            }
        }

        static FilesResolver()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var lrpId = GenerateLrpId();
            var path = Path.Combine(appData, LrpName, lrpId);
            Create(path);

            X86path = Path.Combine(path, "LrpHost.x86.exe");
            X64path = Path.Combine(path, "LrpHost.x64.exe");

            Extract(X86path, Files.LrpHost_x86);
            Extract(X64path, Files.LrpHost_x64);

        }

        static void Extract(string path, byte[] data)
        {
            if (!Directory.Exists(path))
            {
                if (!CheckExisting(path, data))
                {
                    using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
        }

        static bool CheckExisting(string path, byte[] data)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var data2 = new byte[stream.Length];
                var count = stream.Read(data2, 0, (int)stream.Length);
                if (stream.Length != count)
                {
                    return false;
                }
                var result = Enumerable.SequenceEqual(data, data2);
                return result;
            }
        }

        static void Create(String path)
        {
            var items = path.Split('\\');
            path = items[0] + "\\";
            var count = items.Length;
            for (var index = 1; index < count; ++index)
            {
                var item = items[index];
                path = Path.Combine(path, item);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        static string GenerateLrpId()
        {
            using (var stream = new FileStream(typeof(FilesResolver).Assembly.Location, FileMode.Open, FileAccess.Read))
            {
                using (var sha1 = SHA1CryptoServiceProvider.Create())
                {
                    var hash = sha1.ComputeHash(stream);
                    var id = BitConverter.ToString(hash);
                    var result = id.Replace("-", string.Empty);
                    return result;
                }
            }
        }


        #region Constants

        const string LrpName = @"SoftFX\LRP\";
        static string X86path;
        static string X64path;

        #endregion
    }
}
