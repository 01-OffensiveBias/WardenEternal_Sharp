using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp;
using WardenEternal.Permissions;

namespace WardenEternal.Commands
{
    class Reload : Command
    {
        public override string GetUsage()
        {
            return "Reloads the specified system.\n Usage: Reload {System} where System is [Commands, Permissions]";
        }

        public override void Run(DiscordClient client, DiscordPrivateMessageEventArgs e, string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "Commands":
                        ReloadCommands();
                        client.SendMessageToUser("Reloaded all commands", e.author);
                        break;

                    case "Permissions":
                        Member.ReloadMembers();
                        Role.ReloadRoles();
                        client.SendMessageToUser("Reloaded permissions", e.author);
                        break;

                    default:
                        client.SendMessageToUser($"Could not reload \"{args[0]}.\" I do not recognize that.", e.author);
                        break;
                }
            }
            else
            {
                // TODO Implement standard Exception system to handle error messages in the command runner
            }
        }
    }
}
