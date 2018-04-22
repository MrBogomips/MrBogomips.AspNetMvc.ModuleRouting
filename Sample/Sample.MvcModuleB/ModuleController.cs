using System;
using Microsoft.AspNetCore.Mvc;
using MrBogomips.AspNetMvc.ModuleRouting;

namespace Sample.MvcModuleB
{
    [RouteModule("Sample.MvcModuleB", "fugu")]
    public abstract class ModuleBaseController : ControllerBase { }

    // BEST Practice: use a common module controller base class
    // to achieve a better consistency and manteinability
    public class ModuleController : ModuleBaseController
    {
        [HttpGet("hello")]
        public string Hello() => "Hello from module B";
    }
}
