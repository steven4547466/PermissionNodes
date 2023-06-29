using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionNodes.Interfaces
{
    public interface ICommand : CommandSystem.ICommand
    {
        string PermissionNode { get; }
    }
}
