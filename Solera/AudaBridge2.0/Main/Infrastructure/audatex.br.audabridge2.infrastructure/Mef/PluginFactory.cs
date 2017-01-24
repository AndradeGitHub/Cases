using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using audatex.br.audabridge2.infrastructure.mef.interfaces;
using audatex.br.audabridge2.infrastructure.cache;

namespace audatex.br.audabridge2.infrastructure.mef
{
    public static class PluginFactory
    {
        const string cacheKey = "Plugins";

        public static void CreatePlugin(string path, string pluginCreate, object i360Obj)
        {
            try
            {
                var plugins = RecuperaPlugins(path);
                foreach (var plugin in plugins)
                {
                    if (plugin.ToString().ToLower().Contains(pluginCreate.ToLower()))
                    {
                        plugin.Execute(i360Obj);                        
                    }
                }
            }
            catch (CompositionException compositionException)
            {                
                throw compositionException;                
            }
            catch (Exception ex)
            {                
                throw ex;                
            }
        }

        private static IList<IPlugin> RecuperaPlugins(string path)
        {
            var plugins = Cache.Get<List<IPlugin>>(cacheKey);
            if (plugins == null)
            {
                var pluginInvoke = new PluginInvoke(path);

                plugins = pluginInvoke.Plugins;

                Cache.AddItem(cacheKey, plugins, 1);
            }

            return plugins;
        }
    }
}