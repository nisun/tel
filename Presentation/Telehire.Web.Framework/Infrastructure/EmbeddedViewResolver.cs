using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Telehire.Core.Infrastructure;

namespace Telehire.Web.Framework.Infrastructure
{
    public class EmbeddedViewResolver : IEmbeddedViewResolver
    {
        ITypeLocator _typeLocator;
        public EmbeddedViewResolver(ITypeLocator typeLocator)
        {
            this._typeLocator = typeLocator;
        }

        public EmbeddedViewTable GetEmbeddedViews()
        {
            var assemblies = _typeLocator.GetAssemblies();
            if (assemblies == null || assemblies.Count == 0) return null;

            var table = new EmbeddedViewTable();

            foreach (var assembly in assemblies)
            {
                var names = GetNamesOfAssemblyResources(assembly);
                if (names == null || names.Length == 0) continue;

                foreach (var name in names)
                {
                    var key = name.ToLowerInvariant();
                    if (!key.Contains(".views.")) continue;

                    table.AddView(name, assembly.FullName);
                }
            }

            return table;
        }

        private static string[] GetNamesOfAssemblyResources(Assembly assembly)
        {

            try
            {
                if (!assembly.IsDynamic)
                    return assembly.GetManifestResourceNames();
            }
            catch
            {

            }

            return new string[] { };
        }
    }
}