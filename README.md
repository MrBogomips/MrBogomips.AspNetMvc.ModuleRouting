##  Motivation
This packages arises from the need to develop a modular Web Api.

Asp.Net Core, by the mean of **Application Parts** allows you to modularize the code
and easily aggregate them within an hosting app.

The support for routing, in cotrast, is a bit too hostic.

**Area**s give some kind of support but are not mixable with Annotated routing
wich is the elctive way to define a WebApi routing.

Thi package aims to fill the gap.

Even it express its usefulness at best in conjuction with modularized
app development, you can also use it as an alternative way to maintain the
routing configuration in a module-fashion style.

## Getting Start
Clone, Drop or Nuget this package within your solution.

Nuget Artifacts id: `MrBogomips.AspNetMvc.ModuleRouting`.

### Startup.cs
Here you map modules to route templates.
```
using MrBogomips.AspNetMvc.ModuleRouting;
...

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    // ...

    app.UseMvc(routes =>
    {
        routes.MapModuleRoute("Sample.MvcModuleA", "moduleA");  // Here's the fun!
        routes.MapModuleRoute("Sample.MvcModuleB", "moduleB");
    });
}

```

Within your controller you simply decorate it:
```
using MrBogomips.AspNetMvc.ModuleRouting;

namespace Sample.MvcModuleA
{
    [RouteModule("Sample.MvcModuleA")]
    public abstract class ModuleBaseController: ControllerBase {}

    // BEST Practice: use a common module controller base class
    // to achieve a better consistency and manteinability
    public class ModuleController: ModuleBaseController
    {
        [HttpGet("hello")]
        public string Hello() => "Hello from module A";
    }
}
```

et voilà…

## Suggested Practice
