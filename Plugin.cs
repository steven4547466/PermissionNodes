using HarmonyLib;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionNodes
{
    public class Plugin
    {
        [PluginConfig] 
        public Config Config;

        public static Plugin Singleton { get; private set; }
        public static PluginHandler PluginHandler { get; private set; }
        public static Harmony Harmony { get; private set; }

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("PermissionNodes", "1.0.0", "A better permission system.", "Steven4547466")]
        void LoadPlugin()
        {
            Singleton = this;
            PluginHandler = PluginHandler.Get(this);

            PluginHandler.SaveConfig(this, nameof(Config));

            Harmony = new Harmony($"permissionnodes-{DateTime.Now.Ticks}");
            Harmony.PatchAll();

            EventManager.RegisterEvents<EventHandlers>(this);
        }

        [PluginUnload]
        void UnloadPlugin()
        {
            EventManager.UnregisterEvents<EventHandlers>(this);
            Harmony.UnpatchAll(Harmony.Id);
            Harmony = null;
            Singleton = null;
            PluginHandler = null;
        }
    }
}
