using System;
using System.Collections.Specialized;

namespace MrBogomips.AspNetMvc.ModuleRouting
{
    internal static class ModulesRoutes
    {
        private static StringDictionary mappings = new StringDictionary();

        public static void AddMapping(string module, string template) {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }
            if (mappings.ContainsKey(module))
            {
                throw new InvalidOperationException(
                    string.Format("Already exists a mapping for the module. {0} -> \"{1}\".", 
                    module, 
                    mappings[module])
                );
            }
            mappings.Add(module, template.TrimEnd('/'));
        }
        public static bool HasMapping(string module) => mappings.ContainsKey(module);
        public static string GetMapping(string module) => mappings[module];
    }
}
