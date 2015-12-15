namespace SoftFX.Extended.Resources
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    [DebuggerDisplay("{Name}")]
    sealed class Module
    {
        #region Construction

        Module(ModuleType type, IModuleSource source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.ModuleType = type;
            this.source = source;
            var name = source.Name;

            name = name.Replace("__", "{**}");
            name = name.Replace('_', '.');
            name = name.Replace("{**}", "_");
            switch (type)
            {
                case ModuleType.x86:
                    name = name.Replace(".86", string.Empty);
                    break;
                case ModuleType.x64:
                    name = name.Replace(".64", string.Empty);
                    break;
                case ModuleType.Exe:
                    name = name.Replace(".exe", string.Empty);
                    break;
            }

            this.Name = name;

            this.assemblyReference = new WeakReference(null);
        }

        public static Module TryBind(IModuleSource source)
        {
            return TryCreate(source, matchProccess: true);
        }

        public static Module TryCreate(IModuleSource source, bool matchProccess)
        {
            var type = ModuleTypeFromName(source.Name);

            if (matchProccess)
            {
                if (!Environment.Is64BitProcess)
                {
                    if (type == ModuleType.x64)
                        return null;
                }
                else
                {
                    if (type == ModuleType.x86)
                        return null;
                }
            }

            return new Module(type, source);
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public string FileName
        {
            get
            {
                if (this.ModuleType == ModuleType.Exe)
                    return this.Name + ".exe";
                else
                    return this.Name + ".dll";
            }
        }

        public string FullPath
        {
            get
            {
                var result = Path.Combine(Library.Path, this.FileName);
                return result;
            }
        }

        public ModuleType ModuleType { get; private set; }

        public bool ShouldBeExtracted
        {
            get
            {
                return this.ModuleType != ModuleType.AnyCpu;
            }
        }

        #endregion

        #region Methods

        public Assembly LoadAssembly()
        {
            Assembly assembly;

            assembly = (Assembly)this.assemblyReference.Target;
            if (assembly != null)
                return assembly;

            if (this.ShouldBeExtracted)
                assembly = Assembly.LoadFile(this.FullPath);
            else
                assembly = Assembly.Load(this.source.Data);

            if (assembly != null)
                this.assemblyReference.Target = assembly;

            return assembly;
        }

        public void Extract()
        {
            if (!this.ShouldBeExtracted)
                return;

            try
            {
                this.ExtractCore(this.FullPath);
            }
            catch
            {
                SafeDeleteFile(this.FullPath);
                throw;
            }
        }

        public void Extract(string location)
        {
            var path = Path.Combine(location, this.FileName);

            this.ExtractCore(path);
        }

        void ExtractCore(string fullPath)
        {
            var data = this.source.Data;
            using (var stream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Write(data, 0, data.Length);
            }

        }

        static void SafeDeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
            }
        }

        static ModuleType ModuleTypeFromName(string name)
        {
            if (name.Contains("86") || name.Contains("32"))
                return ModuleType.x86;
            else if (name.Contains("64"))
                return ModuleType.x64;
            else if (name.Contains("exe"))
                return ModuleType.Exe;
            else
                return ModuleType.AnyCpu;
        }

        #endregion

        #region Members

        readonly WeakReference assemblyReference;
        readonly IModuleSource source;

        #endregion
    }
}
