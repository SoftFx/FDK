namespace Mql2Fdk.Launcher.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    sealed class StrategyFinder
    {
        public StrategyFinder(string location)
        {
            var catalogs = Directory.GetFiles(location, "*.exe")
                                    .Concat(Directory.GetFiles(location, "*.dll"))
                                    .Select(o => new AssemblyCatalog(o));

            using (var container = new CompositionContainer(new AggregateCatalog(FilterCatalogs(catalogs))))
            {
                this.Strategies = container.GetExportedValues<Strategy>()
                                           .Select(o => o.GetType())
                                           .Where(o => !o.IsAbstract && !o.IsGenericTypeDefinition)
                                           .ToArray();
            }
        }

        static IEnumerable<ComposablePartCatalog> FilterCatalogs(IEnumerable<ComposablePartCatalog> catalogs)
        {
            foreach (var catalog in catalogs)
            {
                try
                {
                    if (!catalog.Parts.Any())
                        continue;
                }
                catch (ReflectionTypeLoadException)
                {
                    continue;
                }

                yield return catalog;
            }
        }

        public IEnumerable<Type> Strategies { get; private set; }
    }
}
