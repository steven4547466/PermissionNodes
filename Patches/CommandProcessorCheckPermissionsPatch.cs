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
    [HarmonyPatch(typeof(CommandProcessor), nameof(CommandProcessor.CheckPermissions), typeof(CommandSender), typeof(string), typeof(PlayerPermissions), typeof(string), typeof(bool))]
    public class CommandProcessorCheckPermissionsPatch
    {
        public static bool Check(CommandSender sender, PlayerPermissions perm)
        {
            Player player = Player.Get(sender);
            if (player != null)
            {
                if (perm == PlayerPermissions.AdminChat && player.HasPermission("base.admin.chat"))
                {
                    return true;
                }
                else if (perm == PlayerPermissions.PlayerSensitiveDataAccess && player.HasPermission("base.admin.playersensitivedata"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Prefix(CommandSender sender, string queryZero, PlayerPermissions perm, string replyScreen, bool reply, ref bool __result)
        {
            if (ServerStatic.IsDedicated && sender.FullPermissions)
            {
                return true;
            }

            bool hasPerm = Check(sender, perm);
            if (hasPerm)
            {
                __result = true;
                return false;
            }

            if (reply)
            {
                sender.RaReply(queryZero + "#You don't have permissions to execute this command.\nRequired permission: " + perm.ToString(), false, true, replyScreen);
            }

            __result = false;
            return false;
        }
    }

    [HarmonyPatch(typeof(CommandProcessor), nameof(CommandProcessor.CheckPermissions), typeof(CommandSender), typeof(PlayerPermissions))]
    public class CommandProcessorCheckPermissionsPatch1
    {
        public static bool Prefix(CommandSender sender, PlayerPermissions perm, ref bool __result)
        {
            bool hasPerm = CommandProcessorCheckPermissionsPatch.Check(sender, perm);
            if (hasPerm)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}
