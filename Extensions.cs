using CommandSystem;
using MapGeneration;
using PluginAPI.Commands;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PermissionNodes
{
    public static class Extensions
    {
        private static readonly Dictionary<string, string> BasePermissionNodesGC = new Dictionary<string, string>()
        {
            { "help", "base.commands.general.help" },
        };

        private static readonly Dictionary<string, string> BasePermissionNodesRA = new Dictionary<string, string>()
        {
            { "addcandy", "base.commands.items.give.candy" },
            { "addexperience", "base.commands.scp079.experience.add" },
            { "ban", "base.commands.admin.ban" },
            { "bring", "base.commands.moderation.player.tp.bring" },
            { "broadcast", "base.commands.message.broadcast" },
            { "buildinfo", "base.commands.developer.buildinfo" },
            { "bypass", "base.commands.cheats.bypass" },
            { "clearcassie", "base.commands.cassie.clear" },
            { "cassie", "base.commands.cassie.announce" },
            { "cassie_silent", "base.commands.cassie.announce.silent" },
            { "cassiewords", "base.commands.cassie.words" },
            { "changecolor", "base.commands.fun.rooms.changecolor" },
            { "custominfo", "base.commands.player.moderation.custominfo" },
            { "setname", "base.commands.player.moderation.setname" },
            { "cleanup", "base.commands.moderation.cleanup" },
            { "cleanup__corpses", "base.commands.moderation.cleanup.corpses" },
            { "cleanup__items", "base.commands.moderation.cleanup.items" },
            { "clearbroadcasts", "base.commands.message.broadcast.clear" },
            { "cleareffects", "base.commands.moderation.player.effects.clear" },
            { "close", "base.commands.facility.doors.close" },
            { "config", "base.commands.admin.config" },
            { "config__path", "base.commands.admin.config.path" },
            { "config__reload", "base.commands.admin.config.reload" },
            { "config__value", "base.commands.admin.config.value" },
            { "contact", "base.commands.contact" },
            { "decontamination", "base.commands.facility.decontamination" },
            { "decontamination__disable", "base.commands.facility.decontamination.disable" },
            { "decontamination__enable", "base.commands.facility.decontamination.enable" },
            { "decontamination__force", "base.commands.facility.decontamination.force" },
            { "decontamination__status", "base.commands.facility.decontamination.status" },
            { "destroy", "base.commands.facility.doors.destroy" },
            { "destroytoy", "base.commands.admin.toys.destroy" },
            { "disarm", "base.commands.moderation.player.disarm" },
            { "name", "base.commands.moderation.player.name" },
            { "doorslist", "base.commands.facility.doors.list" },
            { "doortp", "base.commands.facility.doors.teleport" },
            { "effect", "base.commands.moderation.player.effects.add" },
            { "elevator", "base.commands.facility.elevators" },
            { "elevator__list", "base.commands.facility.elevators.list" },
            { "elevator__lock", "base.commands.facility.elevators.lock" },
            { "elevator__send", "base.commands.facility.elevators.send" },
            { "elevator__unlock", "base.commands.facility.elevators.unlock" },
            { "externallookup", "base.commands.admin.externallookup" },
            { "forceattachments", "base.commands.moderation.items.forceattachments" },
            { "forcerole", "base.commands.moderation.roles.force" },
            { "forcestart", "base.commands.moderation.round.start" },
            { "friendlyfiredetector", "base.commands.moderation.logging.ff" },
            { "give", "base.commands.items.give" },
            { "giveloadout", "base.commands.items.give.loadout" },
            { "globaltag", "base.commands.tag.show.global" },
            { "god", "base.commands.cheats.god" },
            { "goto", "base.commands.moderation.player.tp.goto" },
            { "gundebug", "base.commands.developer.debug.gun" },
            { "heal", "base.commands.cheats.heal" },
            { "hello", "base.commands.hello" },
            { "help", "base.commands.general.help" },
            { "hidetag", "base.commands.tag.hide" },
            { "imute", "base.commands.moderation.player.intercom.mute" },
            { "intercom-reset", "base.commands.moderation.player.intercom.reset" },
            { "icom", "base.commands.moderation.player.intercom" },
            { "intercomtext", "base.commands.moderation.player.intercom.text" },
            { "intercom-timeout", "base.commands.moderation.player.intercom.timeout" },
            { "iunmute", "base.commands.moderation.player.intercom.unmute" },
            { "kill", "base.commands.moderation.player.kill" },
            { "lobbylock", "base.commands.moderation.lobby.lock" },
            { "lock", "base.commands.facility.doors.lock" },
            { "lockdown", "base.commands.facility.lockdown" },
            { "mute", "base.commands.moderation.player.mute" },
            { "noclip", "base.commands.moderation.noclip" },
            { "offlineban", "base.commands.admin.ban.offline" },
            { "open", "base.commands.facility.doors.open" },
            { "overcharge", "base.commands.facility.overcharge" },
            { "overwatch", "base.commands.moderation.overwatch" },
            { "perm", "base.commands.permissions.list" },
            { "permissionsmanagement", "base.commands.permissions.management" },
            { "permissionsmanagement__groups", "base.commands.permissions.management.groups.list" },
            { "permissionsmanagement__reload", "base.commands.permissions.management.reload" },
            { "permissionsmanagement__setgroup", "base.commands.permissions.management.groups.set" },
            { "permissionsmanagement__users", "base.commands.permissions.management.users.list" },
            { "permissionsmanagement__group", "base.commands.permissions.management.groups" },
            { "permissionsmanagement__group__disablecover", "base.commands.permissions.management.groups.cover.disable" },
            { "permissionsmanagement__group__enablecover", "base.commands.permissions.management.groups.cover.enable" },
            { "permissionsmanagement__group__grant", "base.commands.permissions.management.groups.permissions.grant" },
            { "permissionsmanagement__group__info", "base.commands.permissions.management.groups.info" },
            { "permissionsmanagement__group__revoke", "base.commands.permissions.management.groups.permissions.revoke" },
            { "permissionsmanagement__group__setcolor", "base.commands.permissions.management.groups.color.set" },
            { "permissionsmanagement__group__settag", "base.commands.permissions.management.groups.tag.set" },
            { "ping", "base.commands.general.ping" },
            { "playerbroadcast", "base.commands.message.broadcast.player" },
            { "playerinventory", "base.commands.moderation.player.inventory" },
            { "pocketdimension", "base.commands.fun.pocketdimension" },
            { "pddebug", "base.commands.developer.debug.pocketdimension" },
            { "setconfig", "base.commands.admin.config.set" },
            { "remoteconsole", "base.commands.developer.remoteconsole" },
            { "refreshcommands", "base.commands.developer.commands.refresh" },
            { "release", "base.commands.moderation.player.disarm.release" },
            { "reloadconfig", "base.commands.admin.config.reload.all" },
            { "removeitem", "base.commands.items.remove" },
            { "restartnextround", "base.commands.admin.restart.nextround" },
            { "ridlist", "base.commands.developer.debug.rooms.list" },
            { "roomtp", "base.commands.moderation.rooms.tp" },
            { "roundlock", "base.commands.moderation.round.lock" },
            { "roundrestart", "base.commands.moderation.round.restart" },
            { "roundtime", "base.commands.moderation.round.time" },
            { "SERVER_EVENT", "base.commands.moderation.events" },
            { "SERVER_EVENT__DETONATION_CANCEL", "base.commands.moderation.events.warhead.cancel" },
            { "SERVER_EVENT__DETONATION_INSTANT", "base.commands.moderation.events.warhead.detonate" },
            { "SERVER_EVENT__DETONATION_START", "base.commands.moderation.events.warhead.start" },
            { "SERVER_EVENT__FORCE_CI", "base.commands.moderation.events.respawn.force.chaos" },
            { "SERVER_EVENT__FORCE_MTF", "base.commands.moderation.events.respawn.force.mtf" },
            { "SERVER_EVENT__PLAY_EFFECT_CI", "base.commands.moderation.events.respawn.effects.chaos" },
            { "SERVER_EVENT__PLAY_EFFECT_MTF", "base.commands.moderation.events.respawn.effects.mtf" },
            { "SERVER_EVENT__RESPAWN_CI", "base.commands.moderation.events.respawn.spawn.chaos" },
            { "SERVER_EVENT__RESPAWN_MTF", "base.commands.moderation.events.respawn.spawn.mtf" },
            { "SERVER_EVENT__ROUND_RESTART", "base.commands.moderation.round.restart" },
            { "SERVER_EVENT__TERMINATE_UNCONN", "base.commands.moderation.events.terminate.unconnected" },
            { "SERVER_EVENT__TOKEN_RESET", "base.commands.moderation.events.respawn.tokens.reset" },
            { "setexperience", "base.commands.scp079.experience.set" },
            { "setgroup", "base.commands.permissions.management.groups.set.temporary" },
            { "hp", "base.commands.cheats.health" },
            { "setlevel", "base.commands.scp079.level.set" },
            { "showtag", "base.commands.tag.show.local" },
            { "softrestart", "base.commands.admin.restart.soft" },
            { "spawntoy", "base.commands.admin.toys.spawn" },
            { "stare", "base.commands.moderation.stare" },
            { "096state", "base.commands.scp069.state" },
            { "stopnextround", "base.commands.admin.stop.nextround" },
            { "strip", "base.commands.items.clear" },
            { "stripdown", "base.commands.developer.debug.stripdown" },
            { "stripdown__component", "base.commands.developer.debug.stripdown.component" },
            { "stripdown__object", "base.commands.developer.debug.stripdown.object" },
            { "stripdown__print", "base.commands.developer.debug.stripdown.print" },
            { "stripdown__value", "base.commands.developer.debug.stripdown.value" },
            { "tickets", "base.commands.moderation.events.respawn.tokens" },
            { "tickets__grant", "base.commands.moderation.events.respawn.tokens.grant" },
            { "tickets__info", "base.commands.moderation.events.respawn.tokens.info" },
            { "unban", "base.commands.admin.unban" },
            { "unlock", "base.commands.facility.doors.unlock" },
            { "unmute", "base.commands.moderation.player.unmute" },
            { "uptime", "base.commands.developer.uptime" },
            { "version", "base.commands.developer.version" },
            { "warhead", "base.commands.moderation.events.warhead" },
            { "warhead__cancel", "base.commands.moderation.events.warhead.cancel" },
            { "warhead__detonate", "base.commands.moderation.events.warhead.start" },
            { "warhead__disable", "base.commands.moderation.events.warhead.disable" },
            { "warhead__enable", "base.commands.moderation.events.warhead.enable" },
            { "warhead__instant", "base.commands.moderation.events.warhead.detonate" },
            { "warhead__lock", "base.commands.moderation.events.warhead.lock" },
            { "warhead__settime", "base.commands.moderation.events.warhead.settime" },
            { "warhead__status", "base.commands.moderation.events.warhead.status" },
            { "wiki", "base.commands.general.wiki" },
        };

        public static Role GetRole(string name)
        {
            return Plugin.Singleton.Config.Roles.FirstOrDefault(r => r.Name == name);
        }

        public static bool TryGetRole(string name, out Role role)
        {
            role = Plugin.Singleton.Config.Roles.FirstOrDefault(r => r.Name == name);
            return role != null;
        }

        public static Role GetRole(this Player player)
        {
            return Plugin.Singleton.Config.Members.TryGetValue(player.UserId, out string roleName) ?
                GetRole(roleName) : GetRole("default");
        }

        public static List<Role> GetInheritances(this Role role)
        {
            List<Role> roles = new List<Role>();
            foreach(string name in role.Inheritances)
            {
                if (TryGetRole(name, out Role r))
                    roles.Add(r);
            }
            return roles;
        }

        public static List<string> GetAllPermissions(this Role role)
        {
            List<string> permissions = new List<string>();

            void addPermissionsFrom(Role role1)
            {
                foreach (string permission in role1.PermissionNodes)
                {
                    permissions.Add(permission);
                }

                foreach (Role role2 in role1.RoleInheritances)
                {
                    addPermissionsFrom(role2);
                }
            }

            addPermissionsFrom(role);

            return permissions;
            
        }

        public static bool HasPermission(this Role role, string permissionNode)
        {
            if (permissionNode == "*")
                return true;

            List<string> permissions = role.Permissions;
            if (permissions.Contains("*"))
                return true;

            if (permissions.Contains(permissionNode))
                return true;

            string[] nodes = permissionNode.Split('.');

            string nodeUpToNow = string.Empty;
            for (int i = 0; i < nodes.Length; i++)
            {
                string node = nodes[i];
                nodeUpToNow += node;
                string cur = nodeUpToNow + ".*";
                if (permissions.Contains(cur))
                {
                    return true;
                }
                nodeUpToNow += ".";
            }

            return false;
        }

        public static bool HasPermission(this Player player, string permissionNode)
        {
            if (permissionNode == "*")
                return true;
            Role role = player.GetRole();
            return role?.HasPermission(permissionNode) ?? false;
        }

        private static List<Role> getRolesFromStrings(List<string> strings)
        {
            List<Role> roles = new List<Role>();

            foreach (string roleName in strings)
            {
                if (roleName == "*")
                {
                    roles.AddRange(Plugin.Singleton.Config.Roles);
                }
                else if (roleName.StartsWith("!%"))
                {
                    string name = roleName.Substring(2);
                    int index = Plugin.Singleton.Config.Roles.FindIndex(r => r.Name == name);

                    if (index == -1)
                    {
                        throw new Exception($"Role {name} does not exist");
                    }

                    for (int i = index; i >= 0; i--)
                    {
                        roles.Add(Plugin.Singleton.Config.Roles[i]);
                    }
                }
                else if (roleName.StartsWith("^%"))
                {
                    string name = roleName.Substring(2);
                    int index = Plugin.Singleton.Config.Roles.FindIndex(r => r.Name == name);

                    if (index == -1)
                    {
                        throw new Exception($"Role {name} does not exist");
                    }

                    for (int i = index; i < Plugin.Singleton.Config.Roles.Count; i++)
                    {
                        roles.Add(Plugin.Singleton.Config.Roles[i]);
                    }
                }
                else
                {
                    Role role1 = Plugin.Singleton.Config.Roles.FirstOrDefault(r => r.Name == roleName);
                    if (role1 == null)
                    {
                        throw new Exception($"Role {roleName} does not exist");
                    }
                    roles.Add(role1);
                }
            }

            return roles;
        }

        //public static List<Role> GetCantTarget(this Role role)
        //{
        //    return getRolesFromStrings(role.CantTarget);
        //}

        //public static List<Role> GetCanTarget(this Role role)
        //{
        //    return getRolesFromStrings(role.CanTarget);
        //}

        //public static bool CanTarget(this Role role, Role otherRole)
        //{
        //    if (role.CantTargetRoles.Contains(otherRole))
        //        return false;

        //    if (role.CanTargetRoles.Contains(otherRole))
        //        return true;

        //    return false;
        //}

        public static string GetPermissionNode(this ICommand command, bool remoteAdmin)
        {
            return remoteAdmin ? (BasePermissionNodesRA.TryGetValue(command.Command, out string node) ? node : $"command.{command.Command}")
                : (BasePermissionNodesGC.TryGetValue(command.Command, out string node1) ? node1 : $"command.{command.Command}");
        }

        public static string GetPermissionNode(this ParentCommand parentCommand, bool remoteAdmin, string[] arguments)
        {
            string name = parentCommand.Command;
            
            if (arguments.Length > 0)
            {
                void getName(ParentCommand pc, string[] args)
                {
                    for (int i = 0; i < arguments.Length; i++)
                    {
                        string arg = arguments[i];
                        if (parentCommand.TryGetCommand(arg, out ICommand command))
                        {
                            if (command is ParentCommand pc1)
                            {
                                name += $"__{command.Command}";
                                getName(pc1, args.Skip(i + 1).ToArray());
                                break;
                            }
                            else
                            {
                                name += $"__{command.Command}";
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                getName(parentCommand, arguments);
            }

            return remoteAdmin ? (BasePermissionNodesRA.TryGetValue(name, out string node) ? node : $"command.{parentCommand.Command}")
                : (BasePermissionNodesGC.TryGetValue(name, out string node1) ? node1 : $"command.{parentCommand.Command}");
        }

        public static ulong GetNecessaryFlags(this Role role)
        {
            ulong flags = 0;
            if (role.HasPermission("base.moderation.gameplaydata"))
            {
                flags |= (ulong)PlayerPermissions.GameplayData;
            }

            if (role.HasPermission("base.admin.chat"))
            {
                flags |= (ulong)PlayerPermissions.AdminChat;
            }

            if (role.HasPermission("base.admin.playersensitivedata"))
            {
                flags |= (ulong)PlayerPermissions.PlayerSensitiveDataAccess;
            }

            if (role.HasPermission("base.admin.ban.longterm"))
            {
                flags |= (ulong)PlayerPermissions.LongTermBanning;
            }

            if (role.HasPermission("base.admin.ban.day"))
            {
                flags |= (ulong)PlayerPermissions.BanningUpToDay;
            }

            if (role.HasPermission("base.admin.ban.shortterm"))
            {
                flags |= (ulong)PlayerPermissions.KickingAndShortTermBanning;
            }

            return flags;
        }
    }
}
