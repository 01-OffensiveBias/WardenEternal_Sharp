using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DiscordSharp;
using WardenEternal.Commands;

namespace WardenEternal
{
    internal static class Program
    {
        private static DiscordClient _client;

        public static void Main(string[] args)
        {
            Init();

            _client.Connected += _client_Connected;

            _client.PrivateMessageReceived += _client_PrivateMessageReceived;

            Finish();
        }

        private static void _client_Connected(object sender, DiscordConnectEventArgs e)
        {
            Console.WriteLine($"Connected as user \"{e.user.user.username}\"");
        }

        // TODO Implement permissions system
        // Is having this event handler as async even useful?
        private static async void _client_PrivateMessageReceived(object sender, DiscordPrivateMessageEventArgs e)
        {
            Console.WriteLine($"Private Message <- [{e.author.user.username}]: \"{e.message}\"");
            string[] parts = e.message.Split(' ');
            string cmd = parts.First();
            string[] cmdArgs = new string[parts.Length - 1];

            for (int i = 1; i < parts.Length; i++)
            {
                cmdArgs[i] = parts[i];
            }

            // TODO Implement error reporting
            await Task.Run(() => Command.Lookup(cmd).Run(_client, e, cmdArgs));
        }

        private static void Init()
        {
            _client = new DiscordClient
            {
                ClientPrivateInformation =
                {
                    email = ClientConfiguration.Email,
                    password = ClientConfiguration.Password
                }
            };
        }

        private static void Finish()
        {
            _client.SendLoginRequest();
            Thread t = new Thread(_client.Connect);
            t.Start();
            Console.ReadLine();
        }
    }
}
