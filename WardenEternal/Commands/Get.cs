using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp;

namespace WardenEternal.Commands
{
    class Get : Command
    {
        public override string GetUsage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Gets the specified info from the specified user.");
            sb.AppendLine("Usage: Get {User} {Property}");
            sb.AppendLine("Where {User} In [My, {User Id}, {User Name}]");
            sb.AppendLine("And {Property} In [UserId]");
            return sb.ToString();
        }
        public override void Run(DiscordClient client, DiscordPrivateMessageEventArgs e, string[] args)
        {
            // TODO Add this argument length checking to command runner
            if (args.Length > 0)
            {
                switch (args[1])
                {
                    case "UserId":
                        if (args[0] == "My")
                        {
                            args[0] = e.author.ID;
                        }

                        // Search through all members on the server for the matching username/id

                        client.SendMessageToUser($"UserId: {e.author.ID}", e.author);
                        break;

                    default:
                        client.SendMessageToUser($"Property \"{args[1]}\" not recognized", e.author);
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
