using CommandSystem;
using PluginAPI.Commands;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utils;
using static System.Net.Mime.MediaTypeNames;

namespace PermissionNodes
{
    public class EventHandlers
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoin(PlayerJoinedEvent ev)
        {
            if (Plugin.Singleton.Config.Members.TryGetValue(ev.Player.UserId, out string roleName) && Extensions.TryGetRole(roleName != default ? roleName : "default", out Role role))
            {
                ServerRoles serverRoles = ev.Player.ReferenceHub.serverRoles;
                serverRoles.AdminChatPerms = role.HasPermission("base.admin.chat");
                serverRoles._hub.queryProcessor.GameplayData = role.HasPermission("base.moderation.gameplaydata");
                serverRoles.Permissions = role.GetNecessaryFlags();
                if (role.HasBadge)
                {
                    serverRoles.SetText(role.DisplayName);
                    serverRoles.SetColor(role.ColorString);
                }
                if (role.RaAccess)
                {
                    serverRoles.RemoteAdmin = true;
                    serverRoles.RemoteAdminMode = ServerRoles.AccessMode.LocalAccess;
                    serverRoles.GetComponent<QueryProcessor>().PasswordTries = 0;
                    serverRoles.OpenRemoteAdmin(false);
                }
            }
        }

        [PluginEvent(ServerEventType.PlayerGameConsoleCommand)]
        public bool OnPlayerGameConsoleCommand(PlayerGameConsoleCommandEvent ev)
        {
            if (QueryProcessor.DotCommandHandler.TryGetCommand(ev.Command, out CommandSystem.ICommand command))
            {
                if (command is Interfaces.ICommand customCommand)
                {
                    if (!ev.Player.HasPermission(customCommand.PermissionNode))
                    {
                        ev.Player.SendConsoleMessage($"{ev.Command.ToUpperInvariant()}#<color=red>You do not have the required permissions to run this command. Requires: {customCommand.PermissionNode}</color>", string.Empty);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    string permissionNode = command is not ParentCommand pc ? command.GetPermissionNode(false) : pc.GetPermissionNode(false, ev.Arguments);
                    if (!ev.Player.HasPermission(permissionNode))
                    {
                        ev.Player.SendConsoleMessage($"{ev.Command.ToUpperInvariant()}#<color=red>You do not have the required permissions to run this command. Requires: {permissionNode}</color>", string.Empty);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return true;
        }

        [PluginEvent(ServerEventType.RemoteAdminCommand)]
        public bool OnRemoteAdminCommand(RemoteAdminCommandEvent ev)
        {
            if (CommandProcessor.RemoteAdminCommandHandler.TryGetCommand(ev.Command, out CommandSystem.ICommand command))
            {
                Player player = Player.Get(ev.Sender);
                if (player == null)
                    return true;

                if (command is Interfaces.ICommand customCommand)
                {
                    if (!player.HasPermission(customCommand.PermissionNode))
                    {
                        ev.Sender.Respond($"You don't have permissions to execute this command.\\nRequired permission: {customCommand.PermissionNode}", false);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    string permissionNode = command is not ParentCommand pc ? command.GetPermissionNode(true) : pc.GetPermissionNode(true, ev.Arguments);
                    if (!player.HasPermission(permissionNode))
                    {
                        ev.Sender.Respond($"You don't have permissions to execute this command.\\nRequired permission: {permissionNode}", false);
                        return false;
                    }
                    else
                    {
                        if (command.Command == "ban")
                        {
                            try
                            {
                                List<ReferenceHub> list = RAUtils.ProcessPlayerIdOrNamesList(ev.Arguments.Segment(0), 0, out string[] array, false);
                                if (list == null) return true;
                                if (array == null) return true;
                                long num = Misc.RelativeTimeToSeconds(array[0], 60);
                                if (num < 0L)
                                {
                                    num = 0L;
                                    array[0] = "0";
                                }

                                if (num >= 0L && num <= 3600L && !player.HasPermission("base.admin.ban.shortterm"))
                                {
                                    ev.Sender.Respond($"You don't have permissions to execute this command.\\nRequired permission: base.moderation.kick", false);
                                    return false;
                                }
                                if (num > 3600L && num <= 86400L && !player.HasPermission("base.admin.ban.day"))
                                {
                                    ev.Sender.Respond($"You don't have permissions to execute this command.\\nRequired permission: base.admin.ban.day", false);
                                    return false;
                                }
                                if (num > 86400L && !player.HasPermission("base.admin.ban.longterm"))
                                {
                                    ev.Sender.Respond($"You don't have permissions to execute this command.\\nRequired permission: base.admin.ban.longterm", false);
                                    return false;
                                }
                            }
                            catch
                            {
                                return true;
                            }
                        }
                        return true;
                    }
                }
            }

            return true;
        }
    }
}
