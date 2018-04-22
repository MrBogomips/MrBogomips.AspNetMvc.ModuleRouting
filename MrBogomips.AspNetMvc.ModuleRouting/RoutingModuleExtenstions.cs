using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace MrBogomips.AspNetMvc.ModuleRouting
{
    public static class RoutingModulesExtensions
    {
        /// <summary>
        /// Maps module routes.
        /// <see cref="RouteModuleAttribute"/>.
        /// </summary>
        /// <returns>The module route.</returns>
        /// <param name="routes">Routes.</param>
        /// <param name="module">The Module Name</param>
        /// <param name="template">The route template</param>
        public static IRouteBuilder MapModuleRoute(this IRouteBuilder routes, string module, string template)
        {
            ModulesRoutes.AddMapping(module, template);
            return routes;
        }


    }
}
