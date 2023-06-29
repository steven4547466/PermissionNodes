using CommandSystem;
using HarmonyLib;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionNodes.Patches
{
    [HarmonyPatch(typeof(Misc), nameof(Misc.CheckPermission), typeof(ICommandSender), typeof(PlayerPermissions[]))]
    public class MiscCheckPermissionPatch1
    {
        public static bool Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }
    }

    [HarmonyPatch(typeof(Misc), nameof(Misc.CheckPermission), typeof(ICommandSender), typeof(PlayerPermissions))]
    public class MiscCheckPermissionPatch2
    {
        public static bool Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}
