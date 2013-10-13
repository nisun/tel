using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telehire.Web.Framework.Infrastructure
{
    [Serializable]
    public class EmbeddedViewMetadata
    {
        public string Name { get; set; }
        public string AssemblyFullName { get; set; }
    }
}