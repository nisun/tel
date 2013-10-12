using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;

namespace Telehire.Core.Helpers
{
    public partial class AsyncRunner : IAsyncRunner
    {
        public void Run<T>(Action<T> action)
        {
            var app = GetApplicationContainer();
            Task.Factory.StartNew(delegate
            {
                // Create a nested container which will use the same dependency
                // registrations as set for HTTP request scopes.
                using (var container = app.BeginLifetimeScope())
                {
                    var service = container.Resolve<T>();
                    action(service);
                }
            });
        }
        ILifetimeScope GetApplicationContainer()
        {

            return (ILifetimeScope)HttpContext.Current.Items[typeof(ILifetimeScope)];

        }
    }
}
