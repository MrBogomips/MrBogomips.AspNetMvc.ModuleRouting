using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MrBogomips.AspNetMvc.ModuleRouting;

namespace Sample.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Assembly loading required in case of dynamic MVC Components injection.
            // Left for reference.
            var assemplyModuleA = typeof(Sample.MvcModuleA.ModuleController).GetTypeInfo().Assembly;
            var assemplyModuleB = typeof(Sample.MvcModuleB.ModuleController).GetTypeInfo().Assembly;
            services
                .AddMvc()
                .AddApplicationPart(assemplyModuleA)  // See above
                .AddApplicationPart(assemplyModuleB); // See above
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapModuleRoute("Sample.MvcModuleA", "moduleA");
                routes.MapModuleRoute("Sample.MvcModuleB", "moduleB");
            });
        }
    }
}
