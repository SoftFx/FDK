using System.Windows.Forms;
using Microsoft.Win32;

namespace SoftFX.Extended
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using SoftFX.Extended.Core;
    using SoftFX.Extended.Resources;
    using ImplementationResources = SoftFX.Extended.Implementation.Resources;
    using IOPath = System.IO.Path;

    /// <summary>
    /// This class provides common setting of FDK.
    /// </summary>
    public static class Library
    {
        /// <summary>
        /// Gets or sets absolute or relative path to directory, which contains native FDK libraries.
        /// For example: Libary.Path = @"C:\libs\";
        /// You can also use environment variable, for example, Library.Path = "&lt;FRE&gt;"
        /// If specified folder does not contain some dlls, then they will be extracted in runtime.
        /// </summary>
        public static string Path
        {
            get
            {
                return LibPath;
            }
            set
            {
                lock (Synchronizer)
                {
                    if (ReadOnly)
                        throw new InvalidOperationException("You can no longer change this property.");

                    if (value == null)
                        value = string.Empty;

                    if (value.Length > 1 && value[0] == '<' && value[value.Length - 1] == '>')
                    {
                        value = value.Substring(1, value.Length - 2);
                        value = Environment.GetEnvironmentVariable(value);
                        if (value == null)
                            return;
                    }

                    if (value.Length > 0)
                    {
                        if (value[value.Length - 1] != IOPath.DirectorySeparatorChar)
                        {
                            value = value + IOPath.DirectorySeparatorChar;
                        }
                    }

                    LibPath = value;
                }
            }
        }

        /// <summary>
        /// Gets FDK version.
        /// </summary>
        public static string Version { get; private set; }

        /// <summary>
        /// Gets unique identifier of the FDK.
        /// </summary>
        public static string Id { get; private set; }

        /// <summary>
        /// Gets current platroform; can be x86 or x64.
        /// </summary>
        public static string Platform { get; private set; }

        /// <summary>
        /// For internal usage: enables or disables resolving of .Net assemblies from resources.
        /// </summary>
        public static bool ResolveDotNetAssemblies { get; set; }

        static Library()
        {
            Version = string.Empty;
            Id = string.Empty;
            Platform = string.Empty;

            ResolveDotNetAssemblies = true;

            var text = ImplementationResources.Configuration;

            var separators = new[] { '\r', '\n' };
            var lines = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Version = Find("Version", lines);

            using (var stream = new FileStream(typeof(Library).Assembly.Location, FileMode.Open, FileAccess.Read))
            {
                using (var sha1 = SHA1.Create())
                {
                    var hash = sha1.ComputeHash(stream);
                    var id = BitConverter.ToString(hash);
                    Id = id.Replace("-", string.Empty);
                }
            }

            Platform = Environment.Is64BitProcess ? "x64" : "x86";

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            LibPath = IOPath.Combine(appData, FdkLibsName, Library.Id, Library.Platform);
        }

        static string Find(string key, string[] lines)
        {
            var st = string.Format(".*\\s{0}\\s*=\\s*[\"']([^\"']+)[\"'].*", key);
            var pattern = new Regex(st);

            foreach (var element in lines)
            {
                var match = pattern.Match(element);
                if (match.Success)
                {
                    var result = match.Groups[1].Value;
                    return result;
                }
            }

            var message = string.Format("Key = \"{0}\" was not found in configuration file", key);
            throw new Exception(message);
        }

        internal static void MarkAsReadOnly()
        {
            lock (Synchronizer)
            {
                ReadOnly = true;
            }
        }

        /// <summary>
        /// The method forces FDK initialization. Try to use it, if you have problems.
        /// </summary>
        public static void Initialize()
        {
            Bootstrapper.Initialize();
        }

        /// <summary>
        /// Check for VS redistributable packages
        /// </summary>
        public static void CheckRedistPackages()
        {
            try
            {
                RegistryKey runtime = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\DevDiv\vc\Servicing\14.0\RuntimeMinimum");
                if ((runtime == null) || (runtime.GetValue("Install") as int? != 1))
                    throw new Exception("Visual Studio 2015 x64 redistributable missing!");
            }
            catch (Exception)
            {
                if (MessageBox.Show(@"Please install Visual Studio 2015 " + (Environment.Is64BitProcess ? "x64" : "x86") + @" redistributable package! Do you want to install them from Microsoft web site?", @"Visual Studio 2015 x64 redistributable missing!", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    System.Diagnostics.Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=48145");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        /// <summary>
        /// Extract all underlying libraries to a specified directory.
        /// </summary>
        /// <param name="location">A relative or absolute path to directory where libraries and tools should be extracted</param>
        public static void ExtractUnderlyingFiles(string location)
        {
            ModulesManager.ExtractUnderlyingFiles(location);
        }

        /// <summary>
        /// The method delete all extracted dll/exe files from cache. The files cache location is specified by Library.Path.
        /// </summary>
        public static void DeleteFilesCache()
        {
            var path = Library.Path;
            if (!string.IsNullOrEmpty(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// The method specifies a path, which should be used for normal dump writing on exception/fatal error.
        /// </summary>
        /// <param name="path">a path to normal dump file</param>
        public static void WriteNormalDumpOnError(string path)
        {
            Native.Library.WriteNormalDumpOnError(path);
        }

        /// <summary>
        /// The method specifies a path, which should be used for full dump writing on exception/fatal error.
        /// </summary>
        /// <param name="path">a path to full dump file</param>
        public static void WriteFullDumpOnError(string path)
        {
            Native.Library.WriteFullDumpOnError(path);
        }

        /// <summary>
        /// The method write a normal dump by specified location.
        /// </summary>
        /// <param name="path">a path to normal dump file</param>
        public static void WriteNormalDump(string path)
        {
            Native.Library.WriteNormalDump(path);
        }

        /// <summary>
        /// The method write a full dump by specified location.
        /// </summary>
        /// <param name="path">a path to full dump file</param>
        public static void WriteFullDump(string path)
        {
            Native.Library.WriteFullDump(path);
        }

        #region Members

        internal static readonly ModulesManager ModulesManager = new ModulesManager(ModulesProvider.Instance);

        static readonly object Synchronizer = new object();

        static string LibPath = string.Empty;
        static bool ReadOnly = false;

        const string FdkLibsName = @"SoftFX\FDK\";

        #endregion
    }
}
