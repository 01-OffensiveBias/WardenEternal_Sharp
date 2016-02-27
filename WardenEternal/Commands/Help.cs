using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp;

namespace WardenEternal.Commands
{
    class Help : Command
    {
        public override string GetUsage()
        {
            return "We need to go deeper!";
        }

        public override void Run(DiscordClient client, DiscordPrivateMessageEventArgs e, string[] args)
        {
            if (args.Length > 0)
            {
                Command command = Lookup(args[0]);
                string response;

                if (command == null)
                {
                    response = $"Command \"{args[0]}\" not recognized, make sure you spelled the command correctly.";
                }
                else
                {
                    response = command.GetUsage();
                }

                client.SendMessageToUser(response, e.author);
            }
            else
            {
                // TODO Implement standard Exception system to handle error messages in the command runner
            }
        }
    }
}
