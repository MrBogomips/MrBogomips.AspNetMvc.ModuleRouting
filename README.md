[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/MrBogomips.AspNetMvc.ModuleRouting/)

##  Motivation
This package arises from the need to develop a modular Web Api.

Asp.Net Core, by the mean of **Application Parts** allows you to modularize the code
and easily aggregate them within an hosting app.

The support for routing, in cotrast, is a bit too hostic.

**Area**s give some kind of support but are not mixable with Annotated routing
wich is the elective way to define a WebApi routing.

Thi package aims to fill the gap.

Even if it shows its usefulness at best in conjuction with modularized
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
### «Your Modular» Controller.cs
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

## Examples
Recall that you're just using the MVC routing template syntax therefore, you can use any string template that makes sense for Asp.Net MVC.
For example:
- `[RouteModule("ModuleName")]`: simply define the module-routing dependency
- `[RouteModule("ModuleName", "custom/segment")]`: the `custom/segnment` will be appended to the module routing template
- `[RouteModule("ModuleName", "[controller]")]`: the controller's name will be appended to the module routing template
- `[RouteModule("ModuleName", "[action]")]`: the action's name will be appended to the module routing template

## Suggested Practice...
... for a complex, wide Web Api organization:

- Split your api within separte modules, i.e. Assemblies
- Within each module provide a base `Controller` class decorated with one
  of the `RoutModule` attributes
- Aggregate the modules in your host app by `AddApplicationPart(…)`
- Configure your modular routing by `MapModuleRoute(…)`

### A good sample of BaseController class for modular development
```
namespace My.Module.Namespace {
    [RouteModule(ModuleBaseController.ModuleName)]
    public abstract class ModuleBaseController: BaseController
    {
        public const string ModuleName = nameof(My.Module.Namespace);
    }
}
```
Derived controllers can also tweek their routing in a more portable waqy
```
    [RouteModule(ModuleBaseController.ModuleName, "some/different/admin/path")]
    public class AdminController: ModuleBaseController
    {
        public const string ModuleName = nameof(My.Module.Namespace);
    }
```
Therefore the routing coinfig can be accomplished more strongly as
```
app.UseMvc(routes =>
{
   //...

   routes.MapModuleRoute(My.Module.Namespace.ModuleBaseController.ModuleName, "my/module/path");

   //...
}
```
For a reference app check out the Sample App of this project.
