using System;
using System.Reflection;

namespace SoftFX.Extended
{
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
                    Library.CheckRedistPackages();
                    Library.MarkAsReadOnly();
                    Library.ModulesManager.Extract();

                    if (Library.ResolveDotNetAssemblies)
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
            var result = Library.ModulesManager.LoadAssembly(name);

            if (result == null)
                return null;

            if (result.FullName != args.Name)
                return null;

            return result;
        }
    }
}
