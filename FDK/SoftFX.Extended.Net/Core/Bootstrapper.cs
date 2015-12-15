using System;
using System.Reflection;

namespace SoftFX.Extended.Core
{
    using ManagedLibrary = Library;
    using Resources;

    /// <summary>
    /// FDK Bootstrapper
    /// </summary>
    public static class Bootstrapper
    {
        private static readonly object _locker = new object();
        private static bool _initialized;

        /// <summary>
        /// Initialize FDK Bootstrapper
        /// </summary>
        public static void Initialize()
        {
            lock (_locker)
            {
                if (!_initialized)
                {
                    ManagedLibrary.CheckRedistPackages();
                    ManagedLibrary.MarkAsReadOnly();
                    ManagedLibrary.ModulesManager.Extract();

                    if (ManagedLibrary.ResolveDotNetAssemblies)
                    {
                        AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += OnAssemblyResolve;
                        AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
                    }

                    _initialized = true;
                }
            }
        }

        static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var length = args.Name.IndexOf(',');
            if (length < 0)
                return null;

            TraceUtils.WriteLine("Resolving: '{0}'", args.Name);

            var name = args.Name.Substring(0, length);
            var result = ManagedLibrary.ModulesManager.LoadAssembly(name);

            if (result == null)
                return null;

            if (result.FullName != args.Name)
                return null;

            return result;
        }
    }
}
