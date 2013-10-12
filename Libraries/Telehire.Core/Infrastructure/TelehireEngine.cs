using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Telehire.Core.Infrastructure
{
    public class TelehireEngine: ITelehireEngine
    
    {
        private ContainerManager _containerManager;

        public TelehireEngine() 
            : this(new ContainerConfigurer())
		{
		}

        public TelehireEngine(ContainerConfigurer configurer)
		{
          
            InitializeContainer(configurer);
		}
        

        public T Resolve<T>() where T : class
		{
            return ContainerManager.Resolve<T>();
		}

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }
        
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

		

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        private void InitializeContainer(ContainerConfigurer configurer)
        {
            var builder = new ContainerBuilder();

            _containerManager = new ContainerManager(builder.Build());
            configurer.Configure(this, _containerManager);

        }

        public void Initialize()
        {
            RunStartupTasks();
        }

        private void RegisterPlugins()
        {

        }
        private void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeLocator>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
          

            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

    }
}
