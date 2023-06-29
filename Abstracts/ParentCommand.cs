using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionNodes.Abstracts
{
    public abstract class ParentCommand : global::ParentCommand
    {
        public abstract string PermissionNode { get; }
    }
}
