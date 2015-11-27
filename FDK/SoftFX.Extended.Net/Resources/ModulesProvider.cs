namespace SoftFX.Extended.Resources
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    sealed class ModulesProvider
    {
        public static readonly ModulesProvider Instance = new ModulesProvider();

        ModulesProvider()
        {
            var properties = typeof(EmbeddedModules).GetProperties(BindingFlags.Static | BindingFlags.Public);
            this.Modules = properties.Where(o => o.PropertyType == typeof(byte[]))
                                     .Select(o => new StaticPropertyInfoModuleSource(o))
                                     .ToArray();
        }

        public IEnumerable<IModuleSource> Modules { get; private set; }
    }
}
