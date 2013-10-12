using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Core.Infrastructure
{
    public class ContainerConfigurer
    {
        public virtual void Configure(ITelehireEngine engine, ContainerManager containerManager)
        {
            containerManager.AddComponentInstance<ITelehireEngine>(engine, "Appform.engine");
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "Appform.containerConfigurer");

            //type finder
            containerManager.AddComponent<ITypeLocator, WebAppTypeLocator>("Forms.typeFinder");

            //register dependencies provided by other assemblies
            var typeFinder = containerManager.Resolve<ITypeLocator>();
            containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));


                foreach (var dependencyRegistrar in drInstances)
                    dependencyRegistrar.Register(x, typeFinder);
            });

        }
    }
}
