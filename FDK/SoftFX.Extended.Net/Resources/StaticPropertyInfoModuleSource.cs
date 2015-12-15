using SoftFX.Extended.Zip;

namespace SoftFX.Extended.Resources
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;

    sealed class StaticPropertyInfoModuleSource : IModuleSource
    {
        readonly PropertyInfo property;

        public StaticPropertyInfoModuleSource(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            if (property.PropertyType != typeof(byte[]))
                throw new ArgumentException("Property type should be byte[].");

            if (!property.GetGetMethod(true).IsStatic)
                throw new ArgumentException("Property should be static.");

            this.property = property;
        }

        public string Name
        {
            get { return this.property.Name; }
        }

        public byte[] Data
        {
            get { return (byte[])this.property.GetValue(null, null); }
        }


        public void Extract(string location)
        {
            TraceUtils.WriteLine("Writing to {0}", location);
            ExtractCore(location);
        }

        void ExtractCore(string fullPath)
        {
            var data = this.Data;
            using (var compressedFileStream = new MemoryStream(data))
            {
                ZipArchive.Extract(compressedFileStream, fullPath);
            }
        }

        public ModuleType ModuleType { get; private set; }
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
        public bool ShouldBeExtracted
        {
            get
            {
                return this.ModuleType != ModuleType.AnyCpu;
            }
        }
        internal Assembly LoadAssembly()
        {
    
            Assembly assembly;
            
            if (this.ShouldBeExtracted)
                assembly = Assembly.LoadFile(this.FullPath);
            else
                assembly = Assembly.Load(this.Data);

            return assembly;
        }

    }
}
