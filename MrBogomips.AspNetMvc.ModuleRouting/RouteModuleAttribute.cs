using System;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MrBogomips.AspNetMvc.ModuleRouting
{
    /// <summary>
    /// Specifies an attribute route on a controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RouteModuleAttribute : Attribute, IRouteTemplateProvider
    {
        private int? _order;

        /// <summary>
        /// Creates a new <see cref="RouteModuleAttribute"/> with the given route template
        /// and module reference.
        /// </summary>
        /// <param name="module">The route module</param>
        /// <param name="template">The route template. May not be null.</param>
        public RouteModuleAttribute(string module, string template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (module == null)
            {
                throw new ArgumentException(nameof(module));
            }

            Module = module;

            if (ModulesRoutes.HasMapping(Module))
            {
                
                Template = ModulesRoutes.GetMapping(Module) + "/" + template;
            }
            else
            {
                Template = template;
            }
        }
        /// <summary>
        /// Creates a new <see cref="RouteModuleAttribute"/> with the given module reference.
        /// </summary>
        /// <param name="module">The route module</param>
        public RouteModuleAttribute(string module):this(module, "") {}

        /// <inheritdoc />
        public string Template { get; }

        /// <summary>
        /// Gets the route order. The order determines the order of route execution. Routes with a lower order
        /// value are tried first. If an action defines a route by providing an <see cref="IRouteTemplateProvider"/>
        /// with a non <c>null</c> order, that order is used instead of this value. If neither the action nor the
        /// controller defines an order, a default value of 0 is used.
        /// </summary>
        public int Order
        {
            get { return _order ?? 0; }
            set { _order = value; }
        }

        /// <inheritdoc />
        int? IRouteTemplateProvider.Order => _order;

        /// <inheritdoc />
        public string Name { get; set; }
        public string Module { get; private set; }
    }
}
