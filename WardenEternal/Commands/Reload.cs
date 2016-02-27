using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp;

namespace WardenEternal.Commands
{
    class Reload : Command
    {
        public override string GetUsage()
        {
            return base.GetUsage();
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
                        // TODO Implement permissions reloading
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
