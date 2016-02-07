using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DiscordSharp;

namespace WardenEternal.Commands
{
    internal abstract class Command
    {
        public static List<Command> CommandList { get; }
        public bool IsEnabled { get; private set; }
        public string Name { get; }

        public Command()
        {
            IsEnabled = false;
            Name = GetType().Name;
        }

        static Command()
        {
            CommandList = new List<Command>();

            JObject commands = JObject.Parse(File.ReadAllText("commands.json"));

            // TODO Handle errors from invalid class names

            foreach (JToken eCommand in commands.GetValue("Enabled"))
            {
                Command newCommand = Instantiate(eCommand.ToString());
                newCommand.Enable();
                CommandList.Add(newCommand);
            }

            foreach (JToken dCommand in commands.GetValue("Disabled"))
            {
                CommandList.Add(Instantiate(dCommand.ToString()));
            }
        }

        private static Command Instantiate(string command)
        {
            dynamic type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .First(t => t.Name == command);

            return (Command) Activator.CreateInstance(type);
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public static void Enable(string command)
        {
            Lookup(command).Enable();
        }

        public static void Enable(string[] commands)
        {
            foreach (string command in commands)
            {
                Enable(command);
            }
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public static void Disable(string command)
        {
            Lookup(command).Disable();
        }

        public static void Disable(string[] commands)
        {
            foreach (string command in commands)
            {
                Disable(command);
            }
        }

        public static Command Lookup(string command)
        {
            return CommandList.Find(c => c.Name == command);
        }

        public abstract void Run(DiscordClient client, DiscordPrivateMessageEventArgs e, string[] args);
    }
}
