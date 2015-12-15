using System;
using System.Reflection;

namespace SoftFX.Extended.Core
{
    using ManagedLibrary = Library;
    using Resources;

    static class Bootstrapper
    {
        static Bootstrapper()
        {
            ManagedLibrary.CheckRedistPackages();
            ManagedLibrary.MarkAsReadOnly();
            ManagedLibrary.ModulesManager.Extract();

            if (ManagedLibrary.ResolveDotNetAssemblies)
            {
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += OnAssemblyResolve;
                AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
            }
        }

        public static void Initialize()
        {
            Native.Initialize();
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
