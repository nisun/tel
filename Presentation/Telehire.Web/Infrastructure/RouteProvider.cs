using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telehire.Web.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            //   routes.Add
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}