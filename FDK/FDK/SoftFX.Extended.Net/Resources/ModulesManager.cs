namespace SoftFX.Extended.Resources
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    class ModulesManager
    {
        public ModulesManager(ModulesProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            this.provider = provider;

            var modules = provider.Modules.Select(Module.TryBind).Where(o => o != null);

            Modules = modules.ToDictionary(k => k.Name);
        }

        public void Extract()
        {
            var outputDirectory = Library.Path;
            if (string.IsNullOrEmpty(outputDirectory))
                return;

            var infoPath = Path.Combine(outputDirectory, Info);

            var mutexName = string.Format(@"Global\{0}-{1}", Id, Library.Id);
            var mutex = new Mutex(false, mutexName);
            mutex.WaitOne();

            try
            {
                if (!File.Exists(infoPath))
                {
                    CreateDirectory(outputDirectory);
                    foreach (var module in this.Modules.Values)
                    {
                        module.Extract();
                    }
                    var stream = File.Create(infoPath);
                    stream.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
                mutex.Dispose();
            }
        }

        public Assembly LoadAssembly(string assemblyName)
        {
            Assembly assembly = null;

            try
            {
                Module module;
                this.Modules.TryGetValue(assemblyName, out module);
                if (module != null)
                    assembly = module.LoadAssembly();
            }
            catch
            {
            }

            return assembly;
        }

        public void ExtractUnderlyingFiles(string location)
        {
            CreateDirectory(location);
            var x86Path = Path.Combine(location, "x86");
            var x64Path = Path.Combine(location, "x64");
            CreateDirectory(x86Path);
            CreateDirectory(x64Path);

            foreach (var source in this.provider.Modules)
            {
                var module = Module.TryCreate(source, matchProccess: false);
                if (module == null)
                    continue;

                var type = module.ModuleType;
                if (type == ModuleType.x86)
                    module.Extract(x86Path);
                else if (type == ModuleType.x64)
                    module.Extract(x64Path);
                else
                    module.Extract(location);
            }
        }

        static void CreateDirectory(string path)
        {
            var items = path.Split(Path.DirectorySeparatorChar);
            path = items[0] + Path.DirectorySeparatorChar;
            var count = items.Length;
            for (var index = 1; index < count; ++index)
            {
                var item = items[index];
                path = Path.Combine(path, item);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
        }

        static bool HasCorrectPlatform(string name)
        {
            if (!Environment.Is64BitProcess)
            {
                if (name.Contains("86"))
                    return true;
                if (name.Contains("32"))
                    return true;

                return false;
            }
            else
            {
                var result = name.Contains("64");
                return result;
            }
        }

        #region Members

        readonly IDictionary<string, Module> Modules;
        readonly ModulesProvider provider;

        #endregion

        #region Constants

        const string Id = "{41C5D963-25E6-4BC2-B814-B686408B5ABD}";
        const string Info = ".info";

        #endregion
    }
}
