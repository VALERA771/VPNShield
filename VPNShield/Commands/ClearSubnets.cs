using System;
using CommandSystem;
using Exiled.Permissions.Extensions;
using RemoteAdmin;

namespace VPNShield.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ClearSubnets : ICommand
    {
        public string Command { get; } = "vs_clearsubnets";
        public string[] Aliases { get; } = { "vs_clearsubnet" };
        public string Description { get; } = "Clear all IP subnets from VPNShield's database.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender playerSender)
            {
                if (!playerSender.CheckPermission("VPNShield.clear.subnets"))
                {
                    response = "You do not have permission to run this command.";
                    return false;
                }
            }

            if (arguments.Count == 0)
            {
                response = "Are you sure you want to delete ALL IP subnets from VPNShield's database? If so, run the command 'vs_clearsubnets confirm'.";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Confirm or not? Use 'vs_clearsubnets confirm'.";
                return false;
            }



            if (arguments.At(0).ToUpper() == "CONFIRM")
            {
                DbManager.ClearSubnets();
                response = "Deleted all IP subnets from VPNShield's database!";
                return true;
            }
            else
            {
                response = "Confirm or not? Use 'vs_clearsubnets confirm'.";
                return false;
            }
        }
    }
}
