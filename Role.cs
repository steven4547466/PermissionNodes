using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet;
using YamlDotNet.Serialization;

namespace PermissionNodes
{
    public enum BadgeColor
    {
        Pink,
        Red,
        Brown,
        Silver,
        LightGreen,
        Crimson,
        Cyan,
        Aqua,
        DeepPink,
        Tomato,
        Yellow,
        Magenta,
        BlueGreen,
        Orange,
        Lime,
        Green,
        Emerald,
        Carmine,
        Nickel,
        Mint,
        ArmyGreen,
        Pumpkin
    }

    public class Role
    {
        [Description("The unique name of the role.")]
        public string Name { get; set; } = "role";

        [Description("Whether or not the role has RA panel access")]
        public bool RaAccess { get; set; } = false;

        [Description("Whether or not to display a badge on this role.")]
        public bool HasBadge { get; set; } = true;
        [Description("The display name of the role for the badge.")]
        public string DisplayName { get; set; } = "Role";
        [Description("The color of the badge.")]
        public BadgeColor Color { get; set; } = BadgeColor.Pink;

        //[Description("A list of unique role names this role can target.")]
        //public List<string> CanTarget { get; set; } = new List<string>();

        [Description("A list of unique role names to inherit the permission nodes of. This will not inherit anything else.")]
        public List<string> Inheritances { get; set; } = new List<string>();

        [Description("A list of permission nodes for this role.")]
        public List<string> PermissionNodes { get; set; } = new List<string>();

        //private List<Role> canTarget = null;
        //[YamlIgnore]
        //public List<Role> CanTargetRoles
        //{
        //    get
        //    {
        //        if (cantTarget == null)
        //        {
        //            cantTarget = this.GetCanTarget();
        //        }
        //        return canTarget;
        //    }
        //}

        private List<Role> inheritances = null;
        [YamlIgnore]
        public List<Role> RoleInheritances
        {
            get
            {
                if (inheritances == null)
                {
                    inheritances = this.GetInheritances();
                }

                return inheritances;
            }
        }

        private List<string> permissions = null;
        [YamlIgnore]
        public List<string> Permissions { 
            get
            {
                if (permissions == null)
                {
                    permissions = this.GetAllPermissions();
                }

                return permissions;
            } 
        }

        [YamlIgnore]
        public string ColorString
        {
            get
            {
                switch (Color)
                {
                    case BadgeColor.Pink:
                        return "pink";
                    case BadgeColor.Red:
                        return "red";
                    case BadgeColor.Brown:
                        return "brown";
                    case BadgeColor.Silver:
                        return "silver";
                    case BadgeColor.LightGreen:
                        return "light_green";
                    case BadgeColor.Crimson:
                        return "crimson";
                    case BadgeColor.Cyan:
                        return "cyan";
                    case BadgeColor.Aqua:
                        return "aqua";
                    case BadgeColor.DeepPink:
                        return "deep_pink";
                    case BadgeColor.Tomato:
                        return "tomato";
                    case BadgeColor.Yellow:
                        return "yellow";
                    case BadgeColor.Magenta:
                        return "magenta";
                    case BadgeColor.BlueGreen:
                        return "blue_green";
                    case BadgeColor.Orange:
                        return "orange";
                    case BadgeColor.Lime:
                        return "lime";
                    case BadgeColor.Green:
                        return "green";
                    case BadgeColor.Emerald:
                        return "emerald";
                    case BadgeColor.Carmine:
                        return "carmine";
                    case BadgeColor.Nickel:
                        return "nickel";
                    case BadgeColor.Mint:
                        return "mint";
                    case BadgeColor.ArmyGreen:
                        return "army_green";
                    case BadgeColor.Pumpkin:
                        return "pumpkin";
                    default:
                        return "pink";
                }
            }
        }
    }
}
