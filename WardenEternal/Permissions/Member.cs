using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WardenEternal.Permissions
{
    internal class Member
    {
        public static List<Member> MemberList { get; }
        // Add username support
        public string UserId { get; }
        public List<string> Roles { get; }

        public Member(string userId)
        {
            UserId = userId;
            Roles = new List<string>();
        }

        public Member(string userId, List<string> roles)
        {
            UserId = userId;
            Roles = roles;
        }

        static Member()
        {
            MemberList = new List<Member>();

            JObject config = JObject.Parse(File.ReadAllText("permissions.json"));

            foreach (JProperty userId in ((JObject)config.SelectToken("Members")).Properties())
            {
                Member newMember = new Member(userId.Name);
                foreach (JToken role in config.SelectToken($"Members.{userId.Name}"))
                {
                    newMember.Roles.Add(role.ToString());
                }
                MemberList.Add(newMember);
            }
        }

        public static Member GetMemberById(string id)
        {
            return MemberList.First(v => v.UserId == id);
        }

        public bool IsPermitted(string command)
        {
            return IsPermitted(this, command);
        }

        public static bool IsPermitted(Member member, string command)
        {
            IEnumerable<List<string>> roleCommands =
                from memberRole in member.Roles
                join role in Role.RoleList
                    on memberRole equals role.Name
                select role.Commands;

            IEnumerable<string> commands =
                from roleCommand in roleCommands
                from sCommand in roleCommand
                where sCommand == command || sCommand == "*"
                select sCommand;

            return commands.Any();
        }
    }
}
