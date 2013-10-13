using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Telehire.Web.Framework.Infrastructure
{
    public class EmbeddedResourceVirtualFile : VirtualFile
    {
        private readonly EmbeddedViewMetadata _embeddedViewMetadata;

        public EmbeddedResourceVirtualFile(EmbeddedViewMetadata embeddedViewMetadata, string virtualPath)
            : base(virtualPath)
        {
            if (embeddedViewMetadata == null)
                throw new ArgumentNullException("embeddedViewMetadata");

            this._embeddedViewMetadata = embeddedViewMetadata;
        }

        public override Stream Open()
        {
            Assembly assembly = GetResourceAssembly();
            return assembly == null ? null : assembly.GetManifestResourceStream(_embeddedViewMetadata.Name);
        }

        protected virtual Assembly GetResourceAssembly()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => string.Equals(assembly.FullName, _embeddedViewMetadata.AssemblyFullName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}