using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Telehire.Core.Infrastructure
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeLocator typeLocator);

    }
}
