using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp;

namespace WardenEternal.Commands
{
    class Ping : Command
    {
        public override void Run(DiscordClient client, DiscordPrivateMessageEventArgs e, string[] args)
        {
            client.SendMessageToUser("Pong", e.author);
        }
    }
}
