using System;

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
            this.Modules = properties
                .Where(p => p.Name.EndsWith("_zip", StringComparison.InvariantCulture))
                .Where(o => o.PropertyType == typeof(byte[]))
                .Select(o => new StaticPropertyInfoModuleSource(o))
                .ToArray();
        }

        public IEnumerable<StaticPropertyInfoModuleSource> Modules { get; private set; }
    }
}
