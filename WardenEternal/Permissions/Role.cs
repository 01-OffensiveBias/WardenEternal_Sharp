using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WardenEternal.Permissions
{
    internal class Role
    {
        public static List<Role> RoleList { get; }
        public string Name { get; set; }
        public List<string> Commands { get; set; }

        public Role(string name)
        {
            Name = name;
            Commands = new List<string>();
        }

        public Role(string name, List<string> commands)
        {
            Name = name;
            Commands = commands;
        }

        static Role()
        {
            RoleList = new List<Role>();

            ReloadRoles();
        }

        public static void ReloadRoles()
        {
            if (RoleList.Count > 0)
            {
                RoleList.RemoveAll(v => true);
            }

            JObject config = JObject.Parse(File.ReadAllText("permissions.json"));

            foreach (JProperty role in ((JObject)config.SelectToken("Roles")).Properties())
            {
                Role newRole = new Role(role.Name);
                foreach (JToken command in config.SelectToken($"Roles.{role.Name}"))
                {
                    newRole.Commands.Add(command.ToString());
                }
                RoleList.Add(newRole);
            }
        }

        public bool IsPermitted(string command)
        {
            return IsPermitted(this, command);
        }

        public static bool IsPermitted(Role role, string command)
        {
            // TODO
            return true;
        }
    }
}
