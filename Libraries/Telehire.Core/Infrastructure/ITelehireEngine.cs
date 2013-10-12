using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Core.Infrastructure
{
    public interface ITelehireEngine
    {
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// Initialize components and plugins.
        /// </summary>

        void Initialize();

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>();
    }
}
