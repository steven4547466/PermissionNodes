using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace PermissionNodes
{
    public sealed class Config
    {

        [Description("Your member's ids mapped to their role.")]
        public Dictionary<string, string> Members { get; set; } = new Dictionary<string, string>()
        {
            { "yourid@steam", "owner" }
        };

        [Description("Your list of roles. See wiki for more info.")]
        public List<Role> Roles { get; set; } = new List<Role>()
        {
            new Role()
            {
                Name = "default",
                HasBadge = false,
                DisplayName = "Default",
            },
            new Role()
            {
                Name = "owner",
                HasBadge = true,
                RaAccess = true,
                DisplayName = "Owner",
                Color = BadgeColor.Red,
                //CanTarget = new List<string>() {"*"},
                PermissionNodes = new List<string>() {"*"},
            }
        };
    }
}
